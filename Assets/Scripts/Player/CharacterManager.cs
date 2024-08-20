using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CopyMomentum;

public class CharacterManager : MonoBehaviour
{
    public CameraFollow cameraFollow;
    
    [Space]
    public List<CharacterWithPos> charactersBySize;
    public int initialCharacterIndex;
    
    public int currentCharacter;
    private int lastCharacter = -1;
    private GameObject currentCharacterGameobject;

    void Start()
    {
        currentCharacter = initialCharacterIndex;

        ChangeCharacter();
    }

    public void IncreaseSize()
    {
        currentCharacter++;
        currentCharacter = Mathf.Min(currentCharacter, charactersBySize.Count - 1);

        ChangeCharacter();
    }

    public void DecreaseSize()
    {
        currentCharacter--;
        currentCharacter = Mathf.Max(currentCharacter, 0);

        ChangeCharacter();
    }

    private void ChangeCharacter()
    {
        if (currentCharacter == lastCharacter)
        {
            return;
        }

        var characterWorldPos = currentCharacterGameobject?.transform.GetChild(0).position ?? transform.position;
        // characterWorldPos.y = transform.position.y;
        if (lastCharacter > 0)
        {
            characterWorldPos.y -= charactersBySize[lastCharacter].PositionOffset.y;
        }
        
        var prevController = currentCharacterGameobject?.transform.GetChild(0).GetComponent<UnicycleController>() ?? null;
        var prevCharacterGameobject = currentCharacterGameobject;

        // Destroy(prevCharacterGameobject);

        currentCharacterGameobject = GameObject.Instantiate(charactersBySize[currentCharacter].Prefab, transform);
        
        currentCharacterGameobject.transform.GetChild(0).position = characterWorldPos + charactersBySize[currentCharacter].PositionOffset;
        var controller = currentCharacterGameobject.transform.GetChild(0).GetComponent<UnicycleController>();
        InitNewCharacter(controller, prevController);

        Destroy(prevCharacterGameobject);

        cameraFollow.player = currentCharacterGameobject.transform.GetChild(0).transform;

        lastCharacter = currentCharacter;
    }

    private void InitNewCharacter(UnicycleController controller, UnicycleController prevController)
    {
        controller.characterManager = this;

        if (prevController != null)
        {
            CopyMomentum.Copy(prevController, controller);
        }
    }
}

[Serializable]
public struct CharacterWithPos
{
    public GameObject Prefab;
    public Vector3 PositionOffset;
}

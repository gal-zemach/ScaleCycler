using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        SetActiveStates();
    }

    public void IncreaseSize()
    {
        currentCharacter++;
        currentCharacter = Mathf.Min(currentCharacter, charactersBySize.Count - 1);

        SetActiveStates();
    }

    public void DecreaseSize()
    {
        currentCharacter--;
        currentCharacter = Mathf.Max(currentCharacter, 0);

        SetActiveStates();
    }

    private void SetActiveStates()
    {
        if (currentCharacter == lastCharacter)
        {
            return;
        }

        var characterPos = currentCharacterGameobject?.transform.GetChild(0).position ?? Vector3.zero;
        Destroy(currentCharacterGameobject);

        currentCharacterGameobject = GameObject.Instantiate(charactersBySize[currentCharacter].Prefab, transform);
        
        currentCharacterGameobject.transform.GetChild(0).position = characterPos + charactersBySize[currentCharacter].PositionOffset;
        var controller = currentCharacterGameobject.transform.GetChild(0).GetComponent<UnicycleController>();
        InitNewCharacter(controller);

        cameraFollow.player = currentCharacterGameobject.transform.GetChild(0).transform;

        lastCharacter = currentCharacter;
    }

    private void InitNewCharacter(UnicycleController controller)
    {
        controller.characterManager = this;
    }
}

[Serializable]
public struct CharacterWithPos
{
    public GameObject Prefab;
    public Vector3 PositionOffset;
}

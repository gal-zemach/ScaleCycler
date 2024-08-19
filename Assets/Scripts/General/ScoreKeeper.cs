using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    public int score;

    public Text ScoreCounter;
    public Transform scoreLayoutGroupTransform;
    public GameObject scoreImagePrefab;
    
    void Start()
    {
        score = 0;
        UpdateUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            DecrementScore();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            IncrementScore();
        }
    }

    public void IncrementScore()
    {
        score++;
        UpdateUI();
    }

    public void DecrementScore()
    {
        score = Mathf.Max(0, score - 1);
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (ScoreCounter != null)
        {
            ScoreCounter.text = $"{score}";
        }

        if (scoreLayoutGroupTransform != null && scoreImagePrefab != null)
        {
            if (scoreLayoutGroupTransform.childCount == score)
            {
                return;
            }
            

            int childCount = scoreLayoutGroupTransform.childCount;
            for (int i = 0; i < score - childCount; i++)
            {
                GameObject.Instantiate(scoreImagePrefab, scoreLayoutGroupTransform);
                Debug.Log("adding child");
            }

            // while (scoreLayoutGroup.transform.childCount < score)
            // {
            //     GameObject.Instantiate(scoreImagePrefab, scoreLayoutGroup);
            //     Debug.Log("adding child");
            // }
            
            for (int i = 0; i < childCount - score; i++)
            {
                Destroy(scoreLayoutGroupTransform.GetChild(0).gameObject);
                Debug.Log("removing child");
            }

            // while (scoreLayoutGroup.transform.childCount > score)
            // {
            //     Destroy(scoreLayoutGroup.GetChild(0).gameObject);
            //     Debug.Log("removing child");
            // }
        }
    }
}

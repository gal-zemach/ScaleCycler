using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    public GameObject TextScore;
    public Text ScoreCounter;

    [Space]
    public GameObject GridScore;
    public Transform scoreLayoutGroupTransform;
    
    [Space]
    public GameObject scoreImagePrefab;
    public int scoreToDisplayAsText = 10;

    [Space]
    public int score;
    
    void Start()
    {
        score = 0;
        UpdateUI();
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
        if (score >= scoreToDisplayAsText)
        {
            UpdateTextScore();
        }
        else
        {
            UpdateGridScore();
        }
    }

    private void UpdateTextScore()
    {
        TextScore.SetActive(true);
        GridScore.SetActive(false);
        
        if (ScoreCounter != null)
        {
            ScoreCounter.text = $"{score}";
        }
    }

    private void UpdateGridScore()
    {
        TextScore.SetActive(false);
        GridScore.SetActive(true);
        
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
            }
            
            for (int i = 0; i < childCount - score; i++)
            {
                Destroy(scoreLayoutGroupTransform.GetChild(0).gameObject);
            }
        }
    }
}

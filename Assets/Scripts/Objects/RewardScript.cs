using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RewardScript : MonoBehaviour
{
    private ScoreKeeper scoreKeeper;
    
    void Start()
    {
        var go = GameObject.Find("ScoreKeeper");
        scoreKeeper = go.GetComponent<ScoreKeeper>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            scoreKeeper.IncrementScore();
            Destroy(this.gameObject);
        }
    }
}

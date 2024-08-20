using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Animator animator;
    
    void Start()
    {
        
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Weapon"))
        {
            animator.Play("GoldyDeath");
            // Destroy(this.gameObject);
        }
    }
}

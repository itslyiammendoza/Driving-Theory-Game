using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100.0f;
    public float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        GameOver gameOver = gameObject.GetComponent<GameOver>();

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Debug.Log("dead");
            gameOver.End();
        }
    }

}

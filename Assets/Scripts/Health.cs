using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Health : MonoBehaviour
{
    public int health = 3;
    public int maxHealth = 5;

    public int healthUpModifier;
    public int healthDnModifier;
    public Sprite fullBar;
    public Sprite emptyBar;
    public Image[] healths;


    private void Start()
    {
        Debug.Log("Health stats at start: " + health + "/" + maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
 
        if(health > maxHealth)
        {
            health = maxHealth;
        }

        for (int i = 0;i < healths.Length; i++)
        {
            if(i < maxHealth)
            {
                healths[i].enabled = true;
            }
            else
            {
                healths[i].enabled = false;
            }

            if (i < health)
            {
                healths[i].sprite = fullBar;
            }
            else
            {
                healths[i].sprite = emptyBar;
            }
        }
    }

    public void gainHealth()
    {
        healthUpModifier = 1;
        //void ChangeHealth(modifier)
        //pub int healthModifier() get/set healthModifier()
        //refactor notes above

        if (health < maxHealth)
        {
            Debug.Log("Health stats at method to add: " + health + "/" + maxHealth);
            health += healthUpModifier;
            Debug.Log("Health stats after adding: " + health + "/" + maxHealth);
        }

    }

    public void loseHealth()
    {
        healthDnModifier = -1;
        health += healthDnModifier;
        Debug.Log("Health stats after hit: " + health + "/" + maxHealth);
        //if enemy touches, lose modifier amt health
    }


}

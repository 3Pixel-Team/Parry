using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterStats : MonoBehaviour, IDamageable
{
    public int health = 100;
    public int maxHealth = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // TakedDamage removes the given amount of health to a character
    // after checking if the new value isn't negative
    public void TakeDamage(int amount) 
    {
        int newValue = health - amount;
        if (newValue < 0 ) 
        {
            health = 0;
        }
        else 
        {
            health = newValue;
        }
    }
}

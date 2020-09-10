using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    // CharacterStats is the script where the player hp variable is
    // health -> player health rn;
    // maxHealth -> player maximum health;

    [SerializeField] int heal = 20;
    [SerializeField] string pot_name;
    [SerializeField] string description;

    private void Start() 
    {
        description = "Its a potion, heals you for " + heal + "hp.";
    }

    private void OnTriggerEnter(Collider other)
    {
        CharacterStats stats = other.GetComponent<CharacterStats>();
        stats.health += heal;
        if (stats.health > stats.maxHealth)
            stats.health = stats.maxHealth;
    }
}

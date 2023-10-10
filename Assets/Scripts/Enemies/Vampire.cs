using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampire : Enemy
{
    public Vampire() { }
    public void Start()
    {
        currentHealth = 20;
        maxHealth = 20;
        currentSpeed = 0;
        attack = 3;
        physicalDefense = 3;
        magicDefense = 3;
        baseSpeed = 3;
    }
}

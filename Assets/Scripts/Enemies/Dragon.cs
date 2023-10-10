using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Enemy
{
    public Dragon() { }
    public void Start()
    {
        currentHealth = 30;
        maxHealth = 30;
        currentSpeed = 0;
        attack = 4;
        physicalDefense = 3;
        magicDefense = 3;
        baseSpeed = 2;
    }
}

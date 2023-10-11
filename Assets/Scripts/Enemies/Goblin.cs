using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{
    public void Start()
    {
        currentHealth = 10;
        maxHealth = 10;
        currentSpeed = 0;
        attack = 1;
        physicalDefense = 1;
        magicDefense = 1;
        baseSpeed = 3;
    }
    protected override void Update()
    {
        base.Update();
    }
}
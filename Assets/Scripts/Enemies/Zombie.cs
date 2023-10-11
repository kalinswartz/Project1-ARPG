using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    public void Start()
    {
        currentHealth = 10;
        maxHealth = 10;
        currentSpeed = 0;
        attack = 2;
        physicalDefense = 2;
        magicDefense = 2;
        baseSpeed = 1;
    }
    protected override void Update()
    {
        base.Update();
    }
}

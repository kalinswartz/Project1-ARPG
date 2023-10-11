using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirit : Enemy
{
    public void Start()
    {
        currentHealth = 15;
        maxHealth = 15;
        currentSpeed = 0;
        attack = 2;
        physicalDefense = 2;
        magicDefense = 3;
        baseSpeed = 2;
    }
    protected override void Update()
    {
        base.Update();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Golem : Enemy
{
    public void Start()
    {
        currentHealth = 25;
        maxHealth = 25;
        currentSpeed = 0;
        attack = 3;
        physicalDefense = 4;
        magicDefense = 1;
        baseSpeed = 1;
    }
    protected override void Update()
    {
        base.Update();
    }
}

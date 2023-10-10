using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Golem : Enemy
{
    public Golem() { }
    public void Start()
    {
        currentHealth = 25;
        currentSpeed = 0;
        attack = 3;
        physicalDefense = 4;
        magicDefense = 1;
        baseSpeed = 1;
    }
}

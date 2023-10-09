using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Barbarian : Player
{
    void Start()
    {
        maxHealth = 8;
        currentHealth = maxHealth;
        attack = 3;
        defense = 1;
        baseSpeed = 2;
    }
    public override void DoDamage(Enemy enemy)
    {//physical 3
        enemy.currentHealth -= Mathf.Max(0, enemy.physicalDefense - attack);
    }

    public override void Support(GameManager.Buff self, GameManager.Buff other)
    {//double attack -2 def
        self.Def = -2;
        self.Attack = attack;
    }
}

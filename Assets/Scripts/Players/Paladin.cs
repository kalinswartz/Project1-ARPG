using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paladin : Player
{
    void Start()
    {
        maxHealth = 10;
        currentHealth = maxHealth;
        attack = 2;
        defense = 2;
        baseSpeed = 1;
    }
    public override void DoDamage(Enemy enemy)
    {//physical 2
        enemy.currentHealth -= Mathf.Max(0, enemy.physicalDefense - attack);
    }

    public override void Support(GameManager.Buff self, GameManager.Buff other)
    {//1 def  all party
        self.Def = 1;
        other.Def = 1;
    }
}

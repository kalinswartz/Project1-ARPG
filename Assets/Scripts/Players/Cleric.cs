using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleric : Player
{
    void Start()
    {
        maxHealth = 9;
        currentHealth = maxHealth;
        attack = 1;
        defense = 2;
        baseSpeed = 1;
    }
    public override void DoDamage(Enemy enemy)
    {//magic 1
        enemy.currentHealth -= Mathf.Max(0, enemy.magicDefense - attack);
    }

    public override void Support(GameManager.Buff self, GameManager.Buff other)
    {//1 all stats all party
        self.Health = 2;
        other.Health = 2;
    }
}

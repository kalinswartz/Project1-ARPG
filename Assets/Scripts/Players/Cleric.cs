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
        enemy.currentHealth -= Mathf.Max(0, attack - enemy.magicDefense);
    }

    public override void Support(ref GameManager.Buff self,ref GameManager.Buff other)
    {//1 all stats all party
        self.Health = 2;
        other.Health = 2;
    }
}

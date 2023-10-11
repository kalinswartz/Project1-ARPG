using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bard : Player
{
    void Start()
    {
        maxHealth = 8;
        currentHealth = maxHealth;
        attack = 2;
        defense = 2;
        baseSpeed = 2;
    }
    public override void DoDamage(Enemy enemy)
    {//magic 2
        enemy.currentHealth -= Mathf.Max(0, attack - enemy.magicDefense);
    }

    public override void Support(ref GameManager.Buff self, ref GameManager.Buff other)
    {//1 all stats all party
        self.Def = 1;
        self.Attack = 1;
        self.Speed = 1;

        other.Def = 1;
        other.Attack = 1;
        other.Speed = 1;
    }
}
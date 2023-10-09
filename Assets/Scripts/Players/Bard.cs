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
        enemy.currentHealth -= Mathf.Max(0, enemy.magicDefense - attack);
    }

    public override void Support(GameManager.Buff self, GameManager.Buff other)
    {//1 all stats all party
        self.Def = 1;
        self.Attack = 1;
        self.Speed = 1;

        other.Def = 1;
        other.Attack = 1;
        other.Speed = 1;
    }
}
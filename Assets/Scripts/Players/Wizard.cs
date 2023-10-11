using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Player
{
    void Start()
    {
        maxHealth = 7;
        currentHealth = maxHealth;
        attack = 3;
        defense = 2;
        baseSpeed = 1;
    }
    public override void DoDamage(Enemy enemy)
    {//magic 3
        enemy.currentHealth -= Mathf.Max(0,attack - enemy.magicDefense);
    }

    public override void Support(ref GameManager.Buff self, ref GameManager.Buff other)
    {//double self attack -2hp all
        self.Attack = attack;
        self.Health = -2;
        other.Health = -2;
    }
}

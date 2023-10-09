using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rogue : Player
{
    void Start()
    {
        maxHealth = 7;
        currentHealth = maxHealth;
        attack = 2;
        defense = 1;
        baseSpeed = 3;
    }
    public override void DoDamage(Enemy enemy)
    {//phys 2
        enemy.currentHealth -= Mathf.Max(0, enemy.physicalDefense - attack);
    }

    public override void Support(GameManager.Buff self, GameManager.Buff other)
    {//2 speed all party
        self.Speed = 2;
        other.Speed = 2;
    }
}

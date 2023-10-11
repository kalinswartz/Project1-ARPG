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
        enemy.currentHealth -= Mathf.Max(0, attack - enemy.physicalDefense);
    }

    public override void Support(ref GameManager.Buff self, ref GameManager.Buff other)
    {//2 speed all party
        self.Speed = 2;
        other.Speed = 2;
    }
}

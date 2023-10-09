using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


//!!abstract class that all enemies will inherit e.g. public class Goblin : Enemy {}
public abstract class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public int currentHealth;
    public int maxHealth;
    public int currentSpeed;
    public int baseSpeed;

    public int attack;
    public int physicalDefense;
    public int magicDefense;
    public void Attack(Player p1, Player p2)
    {
        if (Random.value > 0.5f)
        {
            int damage = Mathf.Max(attack - p1.defense, 0);
            p1.currentHealth -= damage;
        }
        else
        {
            int damage = Mathf.Max(attack - p2.defense, 0);
            p2.currentHealth -= damage;
        }
    }
    public void StartTurn()
    {
        currentSpeed += baseSpeed;
    }
}
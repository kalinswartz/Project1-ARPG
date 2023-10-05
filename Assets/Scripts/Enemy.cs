using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//!!abstract class that all enemies will inherit e.g. public class Goblin : Enemy {}
public abstract class Enemy : MonoBehaviour 
{
    // Start is called before the first frame update
    public int currentHealth;
    public int currentSpeed;
    public int baseSpeed;

    protected int attack;
    protected int physicalDefense;
    protected int magicDefense;
    public abstract void Attack();
    public void StartTurn()
    {
        currentSpeed += baseSpeed;
    }
}

public class Goblin : Enemy
{
    public void Start()
    {
        currentHealth = 10;
        currentSpeed = 0;
        attack = 1;
        physicalDefense = 1;
        magicDefense = 1;
        baseSpeed = 3;
    }

    public override void Attack()
    {

    }
}

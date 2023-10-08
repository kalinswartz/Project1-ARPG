using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//!!abstract class that all player characters will inherit e.g. public class Paladin : Player {}
public abstract class Player : MonoBehaviour
{

    public int currentHealth;
    public int currentSpeed; //where base speed gets added to each action
    public int defense;
    public int baseSpeed;
    public int maxHealth;
    public int attack;
    // Start is called before the first frame update
    public void StartTurn()
    {
        currentSpeed += baseSpeed;
    }

    public abstract void Support(GameManager.Buff self, GameManager.Buff other);
    public abstract void Attack(Enemy enemy);
}
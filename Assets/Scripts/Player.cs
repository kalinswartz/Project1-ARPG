using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


//!!abstract class that all player characters will inherit e.g. public class Paladin : Player {}
public abstract class Player : MonoBehaviour
{

    public int currentHealth;
    public int currentSpeed; //where base speed gets added to each action
    public int defense;
    public int baseSpeed;
    public int maxHealth;
    public int attack;
    bool attackingForward;
    bool attackingBackward;
    Vector3 startingPosition;
    Enemy enemyTarget;
    // Start is called before the first frame update


    private void Start()
    {
        attackingForward = false;
        attackingBackward = false;
    }

    private void Update()
    {
        if (attackingForward)
        {
            transform.position = Vector3.MoveTowards(transform.position, enemyTarget.transform.position, 0.2f);
            if (transform.position == enemyTarget.transform.position)
            {
                attackingForward = false;
                attackingBackward = true;
            }
        }
        else if (attackingBackward)
        {
            transform.position = Vector3.MoveTowards(transform.position, startingPosition, 0.2f);
            if (transform.position == startingPosition)
            {
                attackingBackward = false;
            }
        }
    }
    public void StartTurn()
    {
        currentSpeed += baseSpeed;
    }
    public void Attack(Enemy enemy)
    {
        DoDamage(enemy);
        MoveToAndBack(enemy);
    }

    public void MoveToAndBack(Enemy enemy)
    {
        enemyTarget = enemy;
        startingPosition = this.transform.position;
        attackingForward = true;
    }

    public abstract void DoDamage(Enemy enemy);
    public abstract void Support(ref GameManager.Buff self, ref GameManager.Buff other);
}
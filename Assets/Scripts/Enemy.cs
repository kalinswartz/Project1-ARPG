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

    public bool attackingForward;
    public bool attackingBackward;
    public Vector3 targetPos;
    public Vector3 startingPosition;
    private void Start()
    {
        attackingForward = false;
        attackingBackward = false;
    }

    protected virtual void Update()
    {
        Debug.Log(attackingForward);
        Debug.Log(attackingBackward);
        if (attackingForward)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 0.2f);
            if (transform.position == targetPos)
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
    public void Attack(Player p1, Player p2)
    {
        if (Random.value > 0.5f)
        {
            int damage = Mathf.Max(attack - p1.defense, 0);
            p1.currentHealth -= damage;
            targetPos = p1.transform.position;
        }
        else
        {
            int damage = Mathf.Max(attack - p2.defense, 0);
            p2.currentHealth -= damage;
            targetPos = p2.transform.position;
        }
        MoveToAndBack();
    }

    public void MoveToAndBack()
    { 
        startingPosition = this.transform.position;
        attackingForward = true;
    }
    public void StartTurn()
    {
        currentSpeed += baseSpeed;
    }
}
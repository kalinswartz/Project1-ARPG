using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vuforia;

public class GameManager : MonoBehaviour
{
    public enum State
    {
        SelectingPlayers,
        SelectingEnemy,
        Player1Turn,
        Player2Turn,
        SupportActions,
        EnemyTurn,
        GameOver
    }
    public struct Buff
    {
        public int Def;
        public int Speed;
        public int Attack;
    }

    [SerializeField] private List<GameObject> characterList;
    [SerializeField] private List<GameObject> enemyList;
    public Player player1 = null;
    public Player player2 = null;
    public Enemy enemy = null;
    [SerializeField] private MultiTargetBehaviour EnemyCube;
    [SerializeField] private MultiTargetBehaviour PlayerCube;

    private Rigidbody playerCubeRb;
    private Rigidbody enemyCubeRb;

    private bool playerCubeVisible;
    private bool enemyCubeVisible;
    private bool attackImageVisible;
    private bool supportImageVisible;
    Buff p1ActiveBuf;
    Buff p1NextBuf;
    Buff p2ActiveBuf;
    Buff p2NextBuf;
    public State currentGameState;
    
    // Start is called before the first frame update
    void Start()

    {
        p1ActiveBuf = new Buff { Def = 0, Speed = 0, Attack = 0};
        p2ActiveBuf = new Buff { Def = 0, Speed = 0, Attack = 0};
        p1NextBuf = new Buff { Def = 0, Speed = 0, Attack = 0 };
        p2NextBuf = new Buff { Def = 0, Speed = 0, Attack = 0 };
        playerCubeRb = PlayerCube.GetComponent<Rigidbody>();   
        enemyCubeRb = EnemyCube.GetComponent<Rigidbody>();
        playerCubeVisible = false;
        enemyCubeVisible = false;
        attackImageVisible = false;
        supportImageVisible = false;
        currentGameState = State.SelectingPlayers;
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentGameState)
        {
            case State.SelectingPlayers:
                if (playerCubeVisible && playerCubeRb.velocity.magnitude <= 0.01f)
                {
                    //  player1 = which face of playerCube is up
                    player1 = SelectPlayerFromCube();
                }
                if(player1 != null && playerCubeVisible && playerCubeRb.velocity.magnitude <= 0.01f)//if player1 != null && detect
                {
                    Player temp = SelectPlayerFromCube();
                    if (temp.GetType() != player1.GetType())
                    {
                        //  player2 = up face of playerCube + not player1
                        player2 = temp;
                        currentGameState = State.SelectingEnemy;
                    }
                }
                break;

            case State.SelectingEnemy:
                if(enemyCubeVisible && enemyCubeRb.velocity.magnitude <= 0.01f)
                {
                    enemy = SelectEnemyFromCube();
                    currentGameState = State.Player1Turn;
                }
                break;
                
            case State.Player1Turn:
                player1.StartTurn(); //Add baseSpeed to current speed;
                while(player1.currentSpeed >= 2) //Check if player wants to attack or support
                {
                    if (attackImageVisible)
                    {
                        player1.Attack(enemy);
                        player1.currentSpeed -= 2; //only decrement speed if an action occurs
                    }
                    else if (supportImageVisible)
                    {
                        player1.Support(p1NextBuf, p2NextBuf); //Self as first argument, in case support only applys to self
                        player1.currentSpeed -= 2;
                    }
                }

                //Check if enemy killed
                if (enemy.currentHealth <= 0)
                {
                    currentGameState = State.GameOver;
                }

                if (player1.currentSpeed < 2)
                {
                    currentGameState = State.Player2Turn;
                }
                break;

            case State.Player2Turn:
                //Check whether p2 wants to attack or support
                player1.StartTurn();
                while (player2.currentSpeed >= 2)
                {
                    if (attackImageVisible)
                    {
                        player2.Attack(enemy);
                        player2.currentSpeed -= 2; 
                    }
                    else if (supportImageVisible)
                    {
                        player2.Support(p2NextBuf, p1NextBuf); 
                        player2.currentSpeed -= 2;
                    }
                }

                if(enemy.currentHealth <= 0)
                {
                    currentGameState = State.GameOver;
                }

                if (player2.currentSpeed < 2)
                {
                    currentGameState = State.SupportActions;
                }
                break;

            case State.SupportActions:
                //Remove currently active buffs
                player1.defense -= p1ActiveBuf.Def;
                player1.baseSpeed -= p1ActiveBuf.Speed;
                player1.attack -= p1ActiveBuf.Attack;

                player2.defense -= p2ActiveBuf.Def;
                player2.baseSpeed -= p2ActiveBuf.Speed;
                player2.attack -= p2ActiveBuf.Attack;

                //Make support actions from this turn active buffs
                p1ActiveBuf = p1NextBuf;
                p2ActiveBuf = p2NextBuf;
                //Clear next buffs for next turn
                p1NextBuf = new Buff { Def = 0, Speed = 0, Attack = 0 };
                p2NextBuf = new Buff { Def = 0, Speed = 0, Attack = 0 };

                //Apply buffs from this turn
                player1.defense += p1ActiveBuf.Def;
                player1.baseSpeed += p1ActiveBuf.Speed;
                player1.attack += p1ActiveBuf.Attack;

                player2.defense += p2ActiveBuf.Def;
                player2.baseSpeed += p2ActiveBuf.Speed;
                player2.attack += p2ActiveBuf.Attack;

                //Change state
                currentGameState = State.EnemyTurn;
                break;

            case State.EnemyTurn:
                enemy.StartTurn(); //add base speed to currentSpeed
                while(enemy.currentSpeed >= 2) {
                    enemy.Attack(player1, player2); //randomly choose which player to damage
                    enemy.currentSpeed -= 2;
                }
                currentGameState = State.Player1Turn;
                break;
        }
    }

    //Helper functions
    public Player SelectPlayerFromCube()
    {
        Transform cubeTransform = PlayerCube.transform;
        Player currentPlayer = null;
        float minimumAngle = float.MaxValue;
        if(Vector3.Angle(cubeTransform.up, Vector3.up) < minimumAngle)  //up
        {
            //currentPlayer = barbarian
            minimumAngle = Vector3.Angle(cubeTransform.up, Vector3.up);
        }
        if(Vector3.Angle(-cubeTransform.up, Vector3.up) < minimumAngle) //down
        {
            //currentPlayer = wizard
            minimumAngle = Vector3.Angle(-cubeTransform.up, Vector3.up);
        }
        
        if (Vector3.Angle(cubeTransform.right, Vector3.up) < minimumAngle) //right
        {
            //currentPlayer = cleric
            minimumAngle = Vector3.Angle(cubeTransform.right, Vector3.up);
        }
        if (Vector3.Angle(-cubeTransform.right, Vector3.up) < minimumAngle) //left
        {
            //currentPlayer = bard
            minimumAngle = Vector3.Angle(-cubeTransform.right, Vector3.up);
        }

        if (Vector3.Angle(cubeTransform.forward, Vector3.up) < minimumAngle) //right
        {
            //currentPlayer = paladin
            minimumAngle = Vector3.Angle(cubeTransform.forward, Vector3.up);
        }
        if (Vector3.Angle(-cubeTransform.forward, Vector3.up) < minimumAngle) //left
        {
            //currentPlayer = rogue
        }
        return currentPlayer;
    }

    public Enemy SelectEnemyFromCube()
    {
        Transform cubeTransform = EnemyCube.transform;
        Enemy currentEnemy = null;
        float minimumAngle = float.MaxValue;
        if (Vector3.Angle(cubeTransform.up, Vector3.up) < minimumAngle)  //up
        {
            //currentEnemy = golem
            minimumAngle = Vector3.Angle(cubeTransform.up, Vector3.up);
        }
        if (Vector3.Angle(-cubeTransform.up, Vector3.up) < minimumAngle) //down
        {
            //currentEnemy = dragon
            minimumAngle = Vector3.Angle(-cubeTransform.up, Vector3.up);
        }

        if (Vector3.Angle(cubeTransform.right, Vector3.up) < minimumAngle) //right
        {
            //currentEnemy = zombie
            minimumAngle = Vector3.Angle(cubeTransform.right, Vector3.up);
        }
        if (Vector3.Angle(-cubeTransform.right, Vector3.up) < minimumAngle) //left
        {
            //currentEnemy = vampire
            minimumAngle = Vector3.Angle(-cubeTransform.right, Vector3.up);
        }

        if (Vector3.Angle(cubeTransform.forward, Vector3.up) < minimumAngle) //forward
        {
            //currentEnemy = goblin
            minimumAngle = Vector3.Angle(cubeTransform.forward, Vector3.up);
        }
        if (Vector3.Angle(-cubeTransform.forward, Vector3.up) < minimumAngle) //back
        {
            //currentEnemy = spirit
        }
        return currentEnemy;
    }

    public void AttackImageDetecte()
    {
        attackImageVisible = true;
    }
    public void AttackImageLost()
    {
        attackImageVisible = false;
    }
    public void SupportImageDetecte()
    {
        supportImageVisible = true;
    }
    public void SupportImageLost()
    {
        supportImageVisible = false;
    }
    public void PlayerCubeDetected()
    {
        playerCubeVisible = true;
    }
    public void PlayerCubeLost()
    {
        playerCubeVisible = false;
    }
    public void EnemyCubeDetected()
    {
        enemyCubeVisible = true;
    }
    public void EnemyCubeLost()
    {
        enemyCubeVisible = false;
    }
}

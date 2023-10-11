using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using Vuforia;

public class GameManager : MonoBehaviour
{
    public enum State
    {
        WaitForNextState,
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
        public int Health;
    }

    public List<GameObject> characterList;
    public List<GameObject> characterList2;
    public List<GameObject> enemyList;
    public Player player1 = null;
    public Player player2 = null;
    public Enemy enemy = null;
    [SerializeField] private MultiTargetBehaviour EnemyCube;
    [SerializeField] private MultiTargetBehaviour PlayerCube;
    [SerializeField] private MultiTargetBehaviour Player2Cube;

    private Rigidbody playerCubeRb;
    private Rigidbody enemyCubeRb;
    private Rigidbody player2CubeRb;

    private bool playerCubeVisible;
    private bool player2CubeVisible;
    private bool enemyCubeVisible;
    public bool attackImageVisible;
    public bool supportImageVisible;
    public bool p1StartTurn;
    public bool p2StartTurn;
    public Buff p1ActiveBuf;
    public Buff p1NextBuf;
    public Buff p2ActiveBuf;
    public Buff p2NextBuf;
    public State currentGameState;
    public State nextGameState;
    float waitTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        waitTimer = 5.0f;
        p1ActiveBuf = new Buff { Def = 0, Speed = 0, Attack = 0, Health = 0 };
        p2ActiveBuf = new Buff { Def = 0, Speed = 0, Attack = 0, Health = 0 };
        p1NextBuf = new Buff { Def = 0, Speed = 0, Attack = 0, Health = 0 };
        p2NextBuf = new Buff { Def = 0, Speed = 0, Attack = 0, Health = 0 };
        playerCubeRb = PlayerCube.GetComponent<Rigidbody>();
        player2CubeRb = Player2Cube.GetComponent<Rigidbody>();
        enemyCubeRb = EnemyCube.GetComponent<Rigidbody>();
        playerCubeVisible = false;
        player2CubeVisible = false;
        enemyCubeVisible = false;
        attackImageVisible = false;
        supportImageVisible = false;
        p1StartTurn = true;
        p2StartTurn = true;
        currentGameState = State.SelectingPlayers;
        nextGameState = State.SelectingEnemy;
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentGameState)
        {
            case State.WaitForNextState:
                waitTimer -= Time.deltaTime;
                if (waitTimer < 0)
                {
                    currentGameState = nextGameState;
                    waitTimer = 4.0f;
                }
                break;

            case State.SelectingPlayers:
                if (playerCubeVisible && playerCubeRb.velocity.magnitude <= 0.5f)
                {
                    //  player1 = which face of playerCube is up
                    player1 = SelectPlayerFromCube();
                }
                if(player1 != null && player2CubeVisible && player2CubeRb.velocity.magnitude <= 0.5f)//if player1 != null && detect
                {
                    Player temp = SelectPlayer2FromCube();
                    if (temp.GetType() != player1.GetType())
                    {
                        //  player2 = up face of playerCube + not player1
                        player2 = temp;
                        nextGameState = State.SelectingEnemy;
                        currentGameState = State.SelectingEnemy;
                    }
                }
                break;

            case State.SelectingEnemy:
                if(enemyCubeVisible && enemyCubeRb.velocity.magnitude <= 0.01f)
                {
                    enemy = SelectEnemyFromCube();
                    nextGameState = State.Player1Turn;
                    currentGameState = State.WaitForNextState;
                }
                break;
                
            case State.Player1Turn:
                if (player1.gameObject.activeSelf) { 
                if (p1StartTurn)
                {
                    player1.StartTurn();
                    p1StartTurn = false;
                }
                 //Add baseSpeed to current speed;
                if (attackImageVisible)
                {
                    player1.Attack(enemy);
                    player1.currentSpeed -= 2; //only decrement speed if an action occurs
                    currentGameState = State.WaitForNextState;
                }
                else if (supportImageVisible)
                {
                    player1.Support(ref p1NextBuf, ref p2NextBuf); //Self as first argument, in case support only applys to self
                    player1.currentSpeed -= 2;
                    currentGameState = State.WaitForNextState;
                }

                if (player1.currentSpeed < 2)
                {
                    currentGameState = State.WaitForNextState;
                    nextGameState = State.Player2Turn;
                }
                if (enemy.currentHealth <= 0)
                {
                    nextGameState = State.GameOver;
                }
                }
                else
                {
                    nextGameState = State.Player2Turn;
                    currentGameState = State.Player2Turn;
                }
                break;

            case State.Player2Turn:
                if (player2.gameObject.activeSelf) { 
                if (p2StartTurn)
                {
                    player2.StartTurn();
                    p2StartTurn = false;
                }
                //Check whether p2 wants to attack or support
                if (attackImageVisible)
                {
                    player2.Attack(enemy);
                    player2.currentSpeed -= 2;
                    currentGameState = State.WaitForNextState;
                }
                else if (supportImageVisible)
                {
                    player2.Support(ref p2NextBuf, ref p1NextBuf); 
                    player2.currentSpeed -= 2;
                    currentGameState = State.WaitForNextState;
                }

                if (player2.currentSpeed < 2)
                {
                    currentGameState = State.WaitForNextState;
                    nextGameState = State.SupportActions;
                }
                if (enemy.currentHealth <= 0)
                {
                    nextGameState = State.GameOver;
                }
                }
                else
                {
                    nextGameState = State.EnemyTurn;
                    currentGameState = State.EnemyTurn;
                }
                break;

            case State.SupportActions:
                p1StartTurn = true;
                p2StartTurn = true;
                //Remove currently active buffs
                player1.defense -= p1ActiveBuf.Def;
                player1.baseSpeed -= p1ActiveBuf.Speed;
                player1.attack -= p1ActiveBuf.Attack;

                player2.defense -= p2ActiveBuf.Def;
                player2.baseSpeed -= p2ActiveBuf.Speed;
                player2.attack -= p2ActiveBuf.Attack;

                //Make support actions from this turn active buffs
                p1ActiveBuf.Def = p1NextBuf.Def;
                p1ActiveBuf.Speed = p1NextBuf.Speed;
                p1ActiveBuf.Attack = p1NextBuf.Attack;
                p1ActiveBuf.Health = p1NextBuf.Health;

                p2ActiveBuf.Def = p2NextBuf.Def;
                p2ActiveBuf.Speed = p2NextBuf.Speed;
                p2ActiveBuf.Attack = p2NextBuf.Attack;
                p2ActiveBuf.Health = p2NextBuf.Health;
                
                //Clear next buffs for next turn
                p1NextBuf = new Buff { Def = 0, Speed = 0, Attack = 0, Health = 0 };
                p2NextBuf = new Buff { Def = 0, Speed = 0, Attack = 0, Health = 0 };

                //Apply buffs from this turn
                player1.defense += p1ActiveBuf.Def;
                player1.baseSpeed += p1ActiveBuf.Speed;
                player1.attack += p1ActiveBuf.Attack;
                player1.currentHealth += p1ActiveBuf.Health;

                player2.defense += p2ActiveBuf.Def;
                player2.baseSpeed += p2ActiveBuf.Speed;
                player2.attack += p2ActiveBuf.Attack;
                player2.currentHealth += p2ActiveBuf.Health;

                if(player1.currentHealth > player1.maxHealth)
                {
                    player1.currentHealth = player1.maxHealth;
                }
                if(player2.currentHealth > player2.maxHealth)
                {
                    player2.currentHealth = player2.maxHealth;
                }

                //Change state
                nextGameState = State.EnemyTurn;
                currentGameState = State.EnemyTurn;
                break;

            case State.EnemyTurn:
                enemy.StartTurn(); //add base speed to currentSpeed
                while(enemy.currentSpeed >= 2) {
                    enemy.Attack(player1, player2); //randomly choose which player to damage
                    enemy.currentSpeed -= 2;
                }

                if(player1.currentHealth <= 0)
                {
                    player1.gameObject.SetActive(false);
                }
                if(player2.currentHealth <= 0)
                {
                    player2.gameObject.SetActive(false);
                }
                if(!player1.gameObject.activeSelf && !player2.gameObject.activeSelf) {
                    currentGameState = State.GameOver;
                    break;
                }

                nextGameState = State.Player1Turn;
                currentGameState = State.WaitForNextState;
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
            currentPlayer = characterList[5].GetComponent<Barbarian>();//barbarian
            minimumAngle = Vector3.Angle(cubeTransform.up, Vector3.up);
        }
        if(Vector3.Angle(-cubeTransform.up, Vector3.up) < minimumAngle) //down
        {
            currentPlayer = characterList[1].GetComponent<Wizard>();//wizard
            minimumAngle = Vector3.Angle(-cubeTransform.up, Vector3.up);
        }
        
        if (Vector3.Angle(cubeTransform.right, Vector3.up) < minimumAngle) //right
        {
            currentPlayer = characterList[2].GetComponent<Cleric>();//cleric
            minimumAngle = Vector3.Angle(cubeTransform.right, Vector3.up);
        }
        if (Vector3.Angle(-cubeTransform.right, Vector3.up) < minimumAngle) //left
        {
            currentPlayer = characterList[4].GetComponent<Bard>();//bard
            minimumAngle = Vector3.Angle(-cubeTransform.right, Vector3.up);
        }

        if (Vector3.Angle(cubeTransform.forward, Vector3.up) < minimumAngle) //right
        {
            currentPlayer = characterList[0].GetComponent<Paladin>();//paladin
            minimumAngle = Vector3.Angle(cubeTransform.forward, Vector3.up);
        }
        if (Vector3.Angle(-cubeTransform.forward, Vector3.up) < minimumAngle) //left
        {
            currentPlayer = characterList[3].GetComponent<Rogue>();//rogue
        }
        foreach (GameObject obj in characterList)
        {
            obj.SetActive(false);
            if(obj == currentPlayer.gameObject)
            {
                obj.SetActive(true);
            }
        }
        return currentPlayer;

    }
    public Player SelectPlayer2FromCube()
    {
        Transform cubeTransform = Player2Cube.transform;
        Player currentPlayer = null;
        float minimumAngle = float.MaxValue;
        if (Vector3.Angle(cubeTransform.up, Vector3.up) < minimumAngle)  //up
        {
            currentPlayer = characterList2[5].GetComponent<Barbarian>();//barbarian
            minimumAngle = Vector3.Angle(cubeTransform.up, Vector3.up);
        }
        if (Vector3.Angle(-cubeTransform.up, Vector3.up) < minimumAngle) //down
        {
            currentPlayer = characterList2[1].GetComponent<Wizard>();//wizard
            minimumAngle = Vector3.Angle(-cubeTransform.up, Vector3.up);
        }

        if (Vector3.Angle(cubeTransform.right, Vector3.up) < minimumAngle) //right
        {
            currentPlayer = characterList2[2].GetComponent<Cleric>();//cleric
            minimumAngle = Vector3.Angle(cubeTransform.right, Vector3.up);
        }
        if (Vector3.Angle(-cubeTransform.right, Vector3.up) < minimumAngle) //left
        {
            currentPlayer = characterList2[4].GetComponent<Bard>();//bard
            minimumAngle = Vector3.Angle(-cubeTransform.right, Vector3.up);
        }

        if (Vector3.Angle(cubeTransform.forward, Vector3.up) < minimumAngle) //right
        {
            currentPlayer = characterList2[0].GetComponent<Paladin>();//paladin
            minimumAngle = Vector3.Angle(cubeTransform.forward, Vector3.up);
        }
        if (Vector3.Angle(-cubeTransform.forward, Vector3.up) < minimumAngle) //left
        {
            currentPlayer = characterList2[3].GetComponent<Rogue>();//rogue
        }
        foreach (GameObject obj in characterList2)
        {
            obj.SetActive(false);
            if (obj == currentPlayer.gameObject)
            {
                obj.SetActive(true);
            }
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
            currentEnemy = enemyList[1].GetComponent<Golem>();//golem
            minimumAngle = Vector3.Angle(cubeTransform.up, Vector3.up);
        }
        if (Vector3.Angle(-cubeTransform.up, Vector3.up) < minimumAngle) //down
        {
            currentEnemy = enemyList[5].GetComponent<Dragon>();//dragon
            minimumAngle = Vector3.Angle(-cubeTransform.up, Vector3.up);
        }

        if (Vector3.Angle(cubeTransform.right, Vector3.up) < minimumAngle) //right
        {

            currentEnemy = enemyList[2].GetComponent<Zombie>(); //zombie
            minimumAngle = Vector3.Angle(cubeTransform.right, Vector3.up);
        }
        if (Vector3.Angle(-cubeTransform.right, Vector3.up) < minimumAngle) //left
        {
            currentEnemy = enemyList[4].GetComponent<Vampire>();//vamp
            minimumAngle = Vector3.Angle(-cubeTransform.right, Vector3.up);
        }

        if (Vector3.Angle(cubeTransform.forward, Vector3.up) < minimumAngle) //forward
        {
            currentEnemy = enemyList[0].GetComponent<Goblin>(); //goblin
            minimumAngle = Vector3.Angle(cubeTransform.forward, Vector3.up);
        }
        if (Vector3.Angle(-cubeTransform.forward, Vector3.up) < minimumAngle) //back
        {
            currentEnemy = enemyList[3].GetComponent<Spirit>();//spirit
        }
        foreach (GameObject obj in enemyList)
        {
            obj.SetActive(false);
            if (obj == currentEnemy.gameObject)
            {
                obj.SetActive(true);
            }
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
    public void Player2CubeDetected()
    {
        player2CubeVisible = true;
    }
    public void Player2CubeLost()
    {
        player2CubeVisible = false;
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

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vuforia;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> characterList;
    [SerializeField] private List<GameObject> enemyList;
    private Player player1 = null;
    private Player player2 = null;
    private Enemy enemy = null;
    [SerializeField] private MultiTargetBehaviour EnemyCube;
    [SerializeField] private MultiTargetBehaviour PlayerCube;

    private Rigidbody playerCubeRb;
    private Rigidbody enemyCubeRb;

    private bool playerCubeVisible;
    private bool enemyCubeVisible;
    private bool attackImageVisible;
    private bool supportImageVisible;
    public enum State
    {
        SelectingPlayers,
        SelectingEnemy,
        Player1Turn,
        Player2Turn,
        RemoveBuffs,
        ApplyBuffs,
        EnemyTurn,
        GameOver
    }
    public State currentGameState;
    
    // Start is called before the first frame update
    void Start()

    {
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
                //if (detect EnemyCube)
                //  enemy = whichever face of enemyCube is up
                //  state = State.Player1Turn
                break;                
            case State.Player1Turn:
                player1.StartTurn();
                while(player1.currentSpeed >= 2)
                {
                    if (attackImageVisible)
                    {
                        player1.Attack(enemy);
                        player1.currentSpeed -= 2;
                    }
                    else if (supportImageVisible)
                    {
                        //player1.Support(player1, player2); read over support actions to see if this will work in all cases
                        player1.currentSpeed -= 2;
                    }
                }
                if(player1.currentSpeed < 2)
                {
                    currentGameState = State.Player2Turn;
                }
                break;
            case State.Player2Turn:
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
                        //player2.Support(player1, player2); read over support actions to see if this will work in all cases
                        player2.currentSpeed -= 2;
                    }
                }
                if (player2.currentSpeed < 2)
                {
                    currentGameState = State.Player2Turn;
                }
                break;
            case State.RemoveBuffs:
                //read support actions to see if this works, maybe constant buff struct
                //hopefully will remove buffs that were active from previous turn 
                break;
            case State.ApplyBuffs: 
                //applys buffs from any support actions this turn
                break;
            case State.EnemyTurn:
                enemy.StartTurn();
                while(enemy.currentSpeed >= 2) {
                    enemy.Attack();
                    enemy.currentSpeed -= 2;
                }
                break;
        }
    }

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

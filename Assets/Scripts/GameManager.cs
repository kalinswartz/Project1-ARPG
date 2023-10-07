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
    [SerializeField] private MultiTargetBehaviour EnemyCube = null;
    [SerializeField] private MultiTargetBehaviour PlayerCube = null;

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
        enemy = enemyList[0].GetComponentInChildren<Goblin>();
        playerCubeVisible = false;
        enemyCubeVisible = false;
        attackImageVisible = false;
        supportImageVisible = false;
        currentGameState = State.SelectingPlayers;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log(enemy.baseSpeed);
        }
        switch(currentGameState)
        {
            case State.SelectingPlayers:
                if (playerCubeVisible)
                {
                    //  player1 = which face of playerCube is up
                }
                //if (detect playerCube)
                //
                //
                //if player1 != null && detech playerCube
                //  player2 = playerCube up face
                //  state = State.SelectingEnemy

                break;
            case State.SelectingEnemy:
                //if (detect EnemyCube)
                //  enemy = whichever face of enemyCube is up
                //  state = State.Player1Turn
                break;                
            case State.Player1Turn:
                //player1.StartTurn();
                //while(player1.speed >= 2){
                //  if(detect AttackTracker);
                //     //player1.attack(enemy)
                //      player1.speed -=2;
                //  else if(detect SupportTracker)
                //      player1.Support(player2);
                //
                //}
                break;
            case State.Player2Turn: 
                //same as bove
                break;
            case State.RemoveBuffs:
                //removes buffs that were active from previous turn
                break;
            case State.ApplyBuffs: 
                //applys buffs from any support actions this turn
                break;
            case State.EnemyTurn:
                //enemy.StartTurn()
                //while(enemy.speed >= 2){
                //  enemy.attack();
                break;
        }
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

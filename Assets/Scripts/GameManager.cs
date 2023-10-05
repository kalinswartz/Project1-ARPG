using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player[] characterList;
    [SerializeField] private Enemy[] enemyList;
    private Player player1 = null;
    private Player player2 = null;
    private Enemy enemy = null;  
    public enum State
    {
        SelectingPlayers,
        SelectingEnemy,
        Player1Turn,
        Player2Turn,
        EnemyTurn,
        GameOver
    }
    public State currentGameState;
    
    // Start is called before the first frame update
    void Start()
    {
        currentGameState = State.SelectingPlayers;
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentGameState)
        {
            case State.SelectingPlayers:
                //if (detect playerCube)
                //  player1 = which face of playerCube is up
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
                //      player1.Support(player2);       (might need modified for support actions) e.g. addbuf After player2turn, removeBuf (inverse) before player1turn
                //
                //}
                break;
            case State.Player2Turn: 
                //same as bove
                break;
            case State.EnemyTurn:
                //enemy.StartTurn()
                //while(enemy.speed >= 2){
                //  enemy.attack();
                break;
        }
    }

    public void PlayerCubeDetected()
    {

    }
}

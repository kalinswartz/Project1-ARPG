using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public Image p1Health;
    public Image p2Health;
    public Image enemyHealth;
    public GameManager gameManager;
    public Text p1Name;
    public Text p2Name;
    public Text enemyName;
    public Text p1DefText;
    public Text p2DefText;
    public Text p1SpdText;
    public Text p2SpdText;
    public Text p1AtkText;
    public Text p2AtkText;
    public Text enemyAtkText;
    public Text enemyMDefText;
    public Text enemyPDefText;
    public Text enemySpdText;
    public Text gameState;
    public GameObject p1Info;
    public GameObject p2Info;
    public GameObject enemyInfo;
    public Text gameOver;


    // Start is called before the first frame update
    void Start()
    {
        p1Info.SetActive(false);
        p2Info.SetActive(false);
        enemyInfo.SetActive(false);
        p1Health.fillAmount = 1;
        p2Health.fillAmount = 1;
        enemyHealth.fillAmount = 1;
        gameOver.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.currentGameState == GameManager.State.SelectingPlayers)
        {
            gameState.text = "Selecting Players";
        }
        if (gameManager.currentGameState == GameManager.State.GameOver)
        {
            gameOver.enabled = true;
            gameState.enabled = false;
        }
        if (gameManager.currentGameState == GameManager.State.SelectingEnemy)
        {
            gameState.text = "Selecting Enemy";
        }
        if (gameManager.currentGameState == GameManager.State.Player1Turn)
        {
            gameState.text = "Player 1's Turn";
        }
        if (gameManager.currentGameState == GameManager.State.Player2Turn)
        {
            gameState.text = "Player 2's Turn";
        }
        if (gameManager.currentGameState == GameManager.State.EnemyTurn)
        {
            gameState.text = "Enemy's Turn";
        }
        if (gameManager.currentGameState == GameManager.State.SupportActions)
        {
            gameState.text = "Support Actions";
        }

        if (gameManager.player1 != null)
        {
            p1Info.SetActive(true);
            p1Name.text = "Player 1 : " + gameManager.player1.GetType().Name;
            p1Health.fillAmount = gameManager.player1.currentHealth / gameManager.player1.maxHealth;
            p1DefText.text = "" + gameManager.player1.defense;
            p1AtkText.text = "" + gameManager.player1.attack;
            p1SpdText.text = "" + gameManager.player1.currentSpeed;
        }
        if (gameManager.player2 != null)
        {
            p2Info.SetActive(true);
            p2Name.text = "Player 2 : " + gameManager.player2.GetType().Name;
            p2Health.fillAmount = gameManager.player2.currentHealth / gameManager.player2.maxHealth;
            p2AtkText.text = "" + gameManager.player2.attack;
            p2DefText.text = "" + gameManager.player2.defense;
            p2SpdText.text = "" + gameManager.player2.currentSpeed;
        }
        if (gameManager.enemy != null)
        {
            enemyInfo.SetActive(true);
            enemyName.text = "Enemy : " + gameManager.enemy.GetType().Name;
            enemyHealth.fillAmount = gameManager.enemy.currentHealth / gameManager.enemy.maxHealth;
            enemyAtkText.text = "" + gameManager.enemy.attack;
            enemyMDefText.text = "" + gameManager.enemy.magicDefense;
            enemyPDefText.text = "" + gameManager.enemy.physicalDefense;
            enemySpdText.text = "" + gameManager.enemy.currentSpeed;
        }
    }
}

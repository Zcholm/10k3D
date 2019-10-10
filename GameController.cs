using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
   
    public DiceController diceController;
    public UI userInterface;
    public AI aI;
    private int currentPlayer;
    private int[] playerScore = new int[2];
    private bool waitingForAI;
    public float aIWaitTime = 0.5f;
    public int goalScore = 10000;

    // Start is called before the first frame update
    void Start() {
        currentPlayer = 0;
        userInterface.SetPlayer(currentPlayer);
        playerScore[0] = 0;
        playerScore[1] = 0;
        waitingForAI = false;
        Parameters.AIPlaying = false;
        Parameters.winner = 0;
    }

    private void Update() {
        ButtonControl();
        if (!Dice.rolling && !waitingForAI)
        {
            StartCoroutine(PlayerControl());
        }
        
    }

    private IEnumerator PlayerControl() {
        bool aITurn = false;
        int aIMove = 0;
        if (Parameters.AIPlaying)
        {
            aITurn = true;
            waitingForAI = true;
            yield return new WaitForSeconds(aIWaitTime);
            aIMove = aI.GetMove(playerScore[currentPlayer]);
            // Sometimes the diceController says there are no scoring dice,
            // so have it wait some time before deciding to hand over the turn.
            if (aIMove == 3)
            {
                yield return new WaitForSeconds(aIWaitTime * 3);
                aIMove = aI.GetMove(playerScore[currentPlayer]);
            }
        }

        if (userInterface.GetRollPressed() || (aITurn && aIMove == 1) ) {
            diceController.Roll();
            userInterface.ResetRollButton();
        }
        else if (userInterface.GetBankPressed() || (aITurn && aIMove == 2)) {
            playerScore[currentPlayer] += diceController.GetHandScore();
            userInterface.scoreBoard.AddPlayerScore(currentPlayer,playerScore[currentPlayer]);

            // Winning can only be achieved when banking, so here is a good place to check if we won:
            if (playerScore[currentPlayer] >= goalScore)
            {
                PlayerWon(currentPlayer);
            }

            diceController.TurnReset();
            ShiftPlayer();
            userInterface.ResetBankButton();
        }
        else if (diceController.FailedThrow() && (Input.anyKeyDown ||aIMove == 3 ))
        {
            ShiftPlayer();
        }
        waitingForAI = false;
    }

    private void ButtonControl() {
        // Exit to menu has highest priority:
        if (userInterface.GetMenuPressed())
        {
            userInterface.ResetMenuButton();
            ExitGame();
        }
        // If it is the AI´s turn, disable the playbuttons,
        // otherwise enable the appropriate buttons for the player:
        if (Parameters.AIPlaying)
        {
            userInterface.buttons.DisableRoll();
            userInterface.buttons.DisableBank();
        }
        else
        {
            if (diceController.CanRoll())
            {
                userInterface.buttons.EnableRoll();
            }
            else
            {
                userInterface.buttons.DisableRoll();
            }

            if (diceController.CanBank(playerScore[currentPlayer]))
            {
                userInterface.buttons.EnableBank();
            }
            else
            {
                userInterface.buttons.DisableBank();
            }
        }
        
    }

    private void ShiftPlayer() {
        diceController.TurnReset();
        if (currentPlayer == 0) {
            currentPlayer = 1;
            if (Parameters.AI)
            {
                Parameters.AIPlaying = true;
            }
        }
        else {
            currentPlayer = 0;
            Parameters.AIPlaying = false;
        }
        userInterface.SetPlayer(currentPlayer);
    }

    private void ExitGame()
    {
        diceController.TurnReset();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    private void PlayerWon(int player)
    {
        // player is 0 or 1, player should be written out as player 1 or 2:
        Parameters.winner = player + 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

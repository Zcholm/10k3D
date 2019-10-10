using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    public ScoreBoard scoreBoard;
    public Text selectedScoreText;
    public Text handScoreText;
    public DiceController diceController;
    public Buttons buttons;

    private void Update() {
        if (!diceController.FailedThrow())
        {
            selectedScoreText.text = "Score selected dice: " + diceController.GetSelectedScore();
            handScoreText.text = "Score in hand: " + (diceController.GetHandScore());
        }
        else
        {
            if (Parameters.AIPlaying)
            {
                selectedScoreText.text = "";
                handScoreText.text = "";
            }
            else
            {
                selectedScoreText.text = "No points thrown, press anywhere to switch player";
                handScoreText.text = "";
            }
        }
     }

    public void SetPlayer(int player) {
        scoreBoard.HighlightPlayer(player);
    }

    public bool GetRollPressed() {
        return buttons.rollPressed;
    }

    public void ResetRollButton() {
        buttons.rollPressed = false;
    }


    public bool GetBankPressed() {
        return buttons.bankPressed;
    }

    public void ResetBankButton() {
        buttons.bankPressed = false;
    }

    public bool GetMenuPressed() {
        return buttons.menuPressed;
    }

    public void ResetMenuButton() {
        buttons.menuPressed = false;
    }

}

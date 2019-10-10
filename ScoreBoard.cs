using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {
    public Text playerOneText;
    public Text playerTwoText;
    public Text playerOneScoreText;
    public Text playerTwoScoreText;
    public Text playerOneStrikethrough;
    public Text playerTwoStrikethrough;

    private Color textColor = new Color32(50, 50, 50, 255);
    private Color highlightColor = new Color32(0, 91, 226, 255);

    private static string strikethrough = "____" + System.Environment.NewLine;

    private void Start() {
        if (Parameters.AI)
        {
            playerOneText.text = "Player";
            playerTwoText.text = "AI";
        }
        ClearScores();
    }

    public void AddPlayerScore(int player, int score) {
        if (player == 0) {
            // Only add a striketrhough if it is not the first score that is being added:
            if (playerOneScoreText.text != "") {
                playerOneStrikethrough.text = playerOneStrikethrough.text + strikethrough;
            }
            playerOneScoreText.text = playerOneScoreText.text + score + System.Environment.NewLine;
        }
        else {
            // Only add a striketrhough if it is not the first score that is being added:
            if (playerTwoScoreText.text != "") {
                playerTwoStrikethrough.text = playerTwoStrikethrough.text + strikethrough;
            }
            playerTwoScoreText.text = playerTwoScoreText.text + score + System.Environment.NewLine;
        }
    }

    public void HighlightPlayer(int player) {
        if (player == 0) {
            playerOneText.color = highlightColor;
            playerTwoText.color = textColor;
        }
        else {
            playerOneText.color = textColor;
            playerTwoText.color = highlightColor;
        }
    }

    void ClearScores() {
        playerOneScoreText.text = "";
        playerTwoScoreText.text = "";
        playerOneStrikethrough.text = "";
        playerTwoStrikethrough.text = "";
    }

}
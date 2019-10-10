using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoreController : MonoBehaviour
{


    public int selectedScore;
    public int handScore;

    private void Update() {

    }

    public void UpdateSelectedScore(ArrayList thrownDice) {
        int score = 0;

        // Loop through values 1-6 and see how many points for each:
        for (int value = 1; value<= 6 ; value++) {
            // Get how many of the value we are dealing with:
            int duplicates = NumberOfDuplicatesInSelection(value, thrownDice);

            // deal points for single ones and fives:
            if (duplicates < 3) {
                if (value == 1) {
                    score += 100 * duplicates;
                }
                else if (value == 5) {
                    score += 50 * duplicates;
                }
            }
            // deal points for triples and more:
            else {
                if (value == 1) {
                    score += 1000 * (int)Mathf.Pow(2, duplicates - 3);
                }
                else {
                    score += value * 100 * (int)Mathf.Pow(2, duplicates - 3);
                }
            }

        }
        // If there is a straight selected none of the above matters,
        // and we will set the score accordingly:
        foreach(RollingDie die in thrownDice)
        {
            // If we find a single die that is unselected or not a part of a
            // straight, we do not have a straight selected:
            if(!die.selected || !die.straight)
            {
                break;
            }
            score = 500;
        }

        selectedScore = score;
    }

    public void UpdateSelectable(ArrayList thrownDice) {
        foreach (RollingDie die in thrownDice) {
            if (die.value == 1 || die.value == 5 || NumberOfDuplicates(die.value, thrownDice) >= 3) {
                die.selectable = true;
            }
            else {
                die.selectable = false;
            }
        }
        SetStraight(thrownDice);
    }

    public int NumberOfDuplicates(int value, ArrayList thrownDice) {
        int duplicates = 0;
        foreach (RollingDie die in thrownDice) {
            if (die.value == value){
                duplicates++;
            }
        }
        return duplicates;
    }

    public void AddToHandScore() {
        handScore += selectedScore;
    }

    private int NumberOfDuplicatesInSelection(int value, ArrayList thrownDice) {
        int duplicates = 0;
        foreach (RollingDie die in thrownDice) {
            if (die.value == value && die.selected) {
                duplicates++;
            }
        }
        return duplicates;
    }

    private void SetStraight(ArrayList thrownDice)
    {
        // A straight has exactly 6 dice, and 
        if (thrownDice.Count != 6)
        {
            return;
        }
        // A straight has exactly one instance of each value:
        for (int val = 1; val <= 6; val++)
        {
            if (NumberOfDuplicates(val, thrownDice) != 1)
            {
                return;
            }
        }
        foreach(RollingDie die in thrownDice)
        {
            die.straight = true;
            die.selectable = true;
        }
    }

    public bool CanBank(int playerScore) {
        if (handScore+selectedScore < 300) {
            return false;
        }
        if (handScore+selectedScore < 1000 && playerScore == 0) {
            return false;
        }
        return true;
    }
}

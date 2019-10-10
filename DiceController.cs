using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceController : MonoBehaviour
{
    public ScoreController scoreController;
    public DieSelector dieSelector;
    public SavedDice savedDice;

    public Vector3 rollPosition;
    public Vector3 rollForce;
    private GameObject spawnPoint = null;

    private ArrayList thrownDice;
    public int noOfThrown;
    public int noOfSelected;
    public int noOfSaved;

    public string diceColor;
    public bool dots;

    private bool newThrow;
    private bool firstThrow;
    private bool failedThrow;

    // Start is called before the first frame update
    void Start()
    {
        newThrow = false;
        firstThrow = true;
        thrownDice = null;
        failedThrow = false;
    }

    // Update is called once per frame
    void Update()
    {
        // we want to update thrownDice once per throw, otherwise we will get new references to the dice each frame:
        if (Dice.rolling && !newThrow) {
            newThrow = true;
        }
        if (!Dice.rolling && newThrow) {
            thrownDice = Dice.GetAllDice();
            newThrow = false;
        }

        scoreController.UpdateSelectedScore(thrownDice);
        scoreController.UpdateSelectable(thrownDice);

        // Handle input from dice being clicked:
        if (Input.GetMouseButtonDown(0) && !Dice.rolling && !Parameters.AIPlaying) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000.0f)) {
                if (hit.transform && hit.transform.gameObject.tag == "Dice") {
                    RollingDie die = GetRollingDie(hit.transform.gameObject);
                    dieSelector.OnClick(die, thrownDice, scoreController.NumberOfDuplicates(die.value, thrownDice));
                }
            }
        }

        // update number of selected dice:
        noOfSelected = 0;
        if (!firstThrow) {
            foreach (RollingDie die in thrownDice) {
                if (die.selected) {
                    noOfSelected++;
                }
            }
        }

        if (!CanPlay() && !Dice.rolling) {
            SetFail();
        }
        // If we have marked a fail wrongly, redeem it:
        else if (failedThrow){
            ResetFail();
        }
        // Sometimes there is a bug that markes a single die throw as fail before it has time to start rolling, this should stop that:
        if (Dice.rolling && failedThrow) {
            ResetFail();
        }
    }

    public void SaveSelected() {
        foreach(RollingDie die in thrownDice) {
            if (die.selected) {
                die.selected = false;
                die.saved = true;
                savedDice.SaveDie(die.value);

            }
        }
    }

    public void Roll() {

        if (firstThrow) {
            Dice.Clear();
            RollDice(6);
            firstThrow = false;
        }
        else if (noOfSelected > 0) {
            noOfSaved += noOfSelected;
            scoreController.handScore += scoreController.selectedScore;
            SaveSelected();
            Dice.SaveSelected();
            Dice.Clear();
            newThrow = true;
            //if all are saved, throw all dice again:
            if (noOfSaved == 6) {
                noOfSaved = 0;
                savedDice.ClearSaved();
            }
            RollDice(6 - noOfSaved);
        }
    }

    private void RollDice(int noDice) {
        Dice.Roll(noDice + "d6", "d6-" + diceColor + (dots ? "-dots" : ""), rollPosition, rollForce);
    }

    public void TurnReset() {
        scoreController.handScore = 0;
        scoreController.selectedScore = 0;
        noOfSaved = 0;
        Dice.Clear();
        firstThrow = true;
        failedThrow = false;
        savedDice.ClearSaved();
    }

    public int GetHandScore() {
        return scoreController.handScore + scoreController.selectedScore;
    }

    public int GetSelectedScore() {
        return scoreController.selectedScore;
    }

    public void SelectSelectable() {
        foreach(RollingDie die in thrownDice) {
            if (die.selectable) {
                dieSelector.SelectDie(die, scoreController.NumberOfDuplicates(die.value, thrownDice), thrownDice);
            }
        }
    }

    private RollingDie GetRollingDie(GameObject gameObject) {
        foreach (RollingDie die in thrownDice) {
            if (die.gameObject == gameObject) {
                return die;
            }
        }
        return null;
    }

    public bool CanBank(int playerScore) {
        if (firstThrow || dieSelector.NumberOfSelected(thrownDice) == thrownDice.Count || failedThrow) {
            return false;
        }
        if (dieSelector.AllSelected(thrownDice)) {
            return scoreController.CanBank(playerScore);
        }
        return false;
    }

    private bool CanPlay() {
        if (firstThrow) {
            return true;
        }
        foreach (RollingDie die in thrownDice) {
            // When we encounter a single die that is selectable we can return true:
            if (die.selectable) {
                return true;
            }
        }
        
        return false;
    }

    private void SetFail() {
        dieSelector.MarkFail(thrownDice);
        failedThrow = true;
    }

    private void ResetFail() {
        dieSelector.UnmarkFail(thrownDice);
        failedThrow = false;
    }

    public bool FailedThrow() {
        return failedThrow;
    }

    public bool CanRoll() {
        if (Dice.rolling) {
            return false;
        }
        // If we have not rolled any dice yet, we can roll
        if (firstThrow)
        {
            return true;
        }
        // We have to save at least one dice each roll:
        if (dieSelector.NumberOfSelected(thrownDice) == 0) {
            return false;
        }
        if (failedThrow)
        {
            return false;
        }
        return true;
    }
}
using UnityEngine;

public class AI : MonoBehaviour
{
    public DiceController diceController;
    public int bankThreshold;

    public int GetMove(int playerScore) {

        diceController.SelectSelectable();

        // Returns 1 for roll, 2 for bank, 3 for player handover.
        // The AI rolls the dice, selects all score giving dice, and banks
        // whenever it reaches a score above a set threshold (1000 for the first throw in
        // accordance with the rules). 

        if (diceController.CanBank(playerScore) && diceController.GetHandScore() >= bankThreshold)
        {
            return 2;
        }
        if (diceController.FailedThrow())
        {
            return 3;
        }
        return 1;
        
    }
}

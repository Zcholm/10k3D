using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieSelector: MonoBehaviour
{
    // We dont want to have to select the color of the dice in several places in unity, therefore the logic to retrieve this info is implemented:
    public Material ogMaterial;
    // This class takes care of the highlighting, therefore its okay to hard code the highlight material here:
    public Material highlightedMaterial;

    public Material failMaterial;

    private void PrintName(RollingDie die) {
        print(die.gameObject.name);
    }

    public void OnClick(RollingDie die, ArrayList thrownDice, int duplicates) {
        if (die.selectable) {
            if (die.selected) {
                DeselectDie(die, thrownDice);
            }
            else {
                SelectDie(die, duplicates, thrownDice);
            }
        }
    }

    public void Highlight(RollingDie die) {
        die.gameObject.GetComponent<Renderer>().material = highlightedMaterial;
    }

    private void DeHighlight(RollingDie die) {
        die.gameObject.GetComponent<Renderer>().material = ogMaterial;
    }

    public void SelectDie(RollingDie die, int duplicates, ArrayList thrownDice) {
        // Select the clicked die:
        Highlight(die);
        die.selected = true;
   
        // See if it is a triple and not a 1 or 5:
        if(die.value != 1 && die.value != 5 && duplicates >= 3) {
            SelectTriple(die, thrownDice);
        }

        // See if it is a straight and not a 1 or 5:
        if(die.value != 1 && die.value != 5 && die.straight)
        {
            SelectStraight(thrownDice);
        }
    }

    /*public void SelectDie(RollingDie die) {
        // Select the clicked die:
        if (die.selectable)
        {
            Highlight(die);
            die.selected = true;
        }
    }//*/

    private void DeselectDie(RollingDie die, ArrayList thrownDice) {
        DeHighlight(die);
        die.selected = false;

        // See if it is a triple and not a one or a five:
        if(die.value != 1 && die.value!= 5 && die.triple) {
            DeselectTriple(die.value, thrownDice);
        }
        // See if it is a straight: (Even if it is a one or a five we must
        //deselect the entire straight, since the other values cannot be
        //selected without all being selected)
         if (die.straight)
        {
            DeselectStraight(thrownDice);
        }
    }

    private void SelectTriple(RollingDie clickedDie, ArrayList thrownDice) {
        // mark the clicked die as a part if the triple:
        clickedDie.triple = true;
        // There is one die selected already:
        int selected = 1;

        foreach(RollingDie die in thrownDice) {
            // Select the die if we have less than 3 selected, it is not the already selected one, and the die is of the same value:
            if(selected < 3 && die != clickedDie && die.value == clickedDie.value) {
                // This is not done using the SelectDie(RollingDie die) function since that would cause an inf loop.
                Highlight(die);
                die.selected = true;
                die.triple = true;
                selected++;
            }
        }
    }

    private void DeselectTriple(int value, ArrayList thrownDice) {
        foreach(RollingDie die in thrownDice) {
            if (die.triple && die.value == value) {
                DeHighlight(die);
                die.selected = false;
                die.triple = false;
            }
        }
    }

    private void SelectStraight(ArrayList thrownDice)
    {
        foreach(RollingDie die in thrownDice)
        {
            die.selected = true;
            Highlight(die);
        }
    }

    private void DeselectStraight(ArrayList thrownDice)
    {
        foreach (RollingDie die in thrownDice)
        {
            if(die.value != 1 && die.value != 5)
            {
                DeHighlight(die);
                die.selected = false;
            }
        }
    }

    public void MarkFail(ArrayList thrownDice) {
        foreach(RollingDie die in thrownDice) {
            die.gameObject.GetComponent<Renderer>().material = failMaterial;
        }
    }

    public void UnmarkFail(ArrayList thrownDice) {
        foreach (RollingDie die in thrownDice) {
            die.gameObject.GetComponent<Renderer>().material = ogMaterial;
        }
    }

    public bool AllSelected(ArrayList thrownDice) {
        foreach (RollingDie die in thrownDice) {
            // we only need to find one selectable and see if it is not selected to determine false:
            if (die.selectable && !die.selected) {
                return false;
            }
        }
        return true;
    }

    public int NumberOfSelected(ArrayList thrownDice) {
        int selected = 0;
        foreach (RollingDie die in thrownDice) {
            if (die.selected) {
                selected++;
            }
        }
        return selected;
    }

}
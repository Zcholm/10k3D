using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{

    public bool rollPressed = false;
    public bool bankPressed = false;
    public bool menuPressed = false;

    public Text rollButtonText;
    public Text bankButtonText;

    public Transform rollButton;
    public Transform bankButton;

    private Color textColorDisabled = new Color32(50, 50, 50, 255);
    private Color textColorEnabled = new Color32(180, 145, 91, 255);

    private 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RollPressed() {
        rollPressed = true;
    }

    public void BankPressed() {
        bankPressed = true;
    }

    public void MenuPressed() {
        menuPressed = true;
    }

    public void DisableRoll() {
        rollButtonText.color = textColorDisabled;
        rollButton.GetComponent<Button>().interactable = false;
    }

    public void EnableRoll() {
        rollButtonText.color = textColorEnabled;
        rollButton.GetComponent<Button>().interactable = true;
    }

    public void DisableBank() {
        bankButtonText.color = textColorDisabled;
        bankButton.GetComponent<Button>().interactable = false;
    }

    public void EnableBank() {
        bankButtonText.color = textColorEnabled;
        bankButton.GetComponent<Button>().interactable = true;
    }


    
}

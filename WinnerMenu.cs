using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinnerMenu : MonoBehaviour
{
    public Text header;
    public Text body;

    // Start is called before the first frame update
    void Start()
    {
        if (Parameters.AI) {
            if (Parameters.winner == 1)
            {
                header.text = "Congratulations!";
                body.text = "You won!";
            }
            else {
                header.text = "You lost";
                body.text = "Better luck next time";
            }
        }
        else
        {
            header.text = "Congratulations!";
            body.text = "The winner is Player " + Parameters.winner + "!";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    public void ExitButton()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropdown : MonoBehaviour
{

    public void ToggleAI()
    {
        Parameters.AI = !Parameters.AI;
    }

}

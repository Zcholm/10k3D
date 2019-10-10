using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedDice : MonoBehaviour
{

    private Quaternion rotation1 = Quaternion.Euler(270, 0, 0);
    private Quaternion rotation2 = Quaternion.Euler(180, 0, 0);
    private Quaternion rotation3 = Quaternion.Euler(0, 0, 270);
    private Quaternion rotation4 = Quaternion.Euler(0, 0, 90);
    private Quaternion rotation5 = Quaternion.Euler(0, 0, 0);
    private Quaternion rotation6 = Quaternion.Euler(90, 0, 0);

    private Quaternion[] rotations = new Quaternion[6];

    public GameObject saved1;
    public GameObject saved2;
    public GameObject saved3;
    public GameObject saved4;
    public GameObject saved5;
    public GameObject saved6;

    private GameObject[] dice = new GameObject[6];
    private ArrayList savedDice;
    private int noOfSaved;
    

    // Start is called before the first frame update
    void Start()
    {
        noOfSaved = 0;

        rotations[0] = Quaternion.Euler(270, 0, 0);
        rotations[1] = Quaternion.Euler(180, 0, 0);
        rotations[2] = Quaternion.Euler(0, 0, 270);
        rotations[3] = Quaternion.Euler(0, 0, 90);
        rotations[4] = Quaternion.Euler(0, 0, 0);
        rotations[5] = Quaternion.Euler(90, 0, 0);

        dice[0] = saved1;
        dice[1] = saved2;
        dice[2] = saved3;
        dice[3] = saved4;
        dice[4] = saved5;
        dice[5] = saved6;

        foreach (GameObject die in dice) {
            die.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SaveDie(int val) 
    {
        dice[noOfSaved].transform.rotation = rotations[val - 1];
        dice[noOfSaved].SetActive(true);
        noOfSaved++;
        print("SaveDie is called with value " + val);
    }

    public void ClearSaved() {
        noOfSaved = 0;

        for(int i = 0; i<6; i++) {
            dice[i].SetActive(false);
        }

    }
}

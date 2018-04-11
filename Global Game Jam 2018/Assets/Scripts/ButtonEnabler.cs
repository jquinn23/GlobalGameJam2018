/**
 * Author: Jonathan R. Quinn
 * Date of Last Modification: 4/10/18
 * Person to last modify: Jonathan R. Quinn
 * Changes Made: Created the basic file
 * 
 * This script will be used to turn on and off buttons on the main menu so as to create a system of player progression
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEnabler : MonoBehaviour {
    public GameObject gm;
    public List<GameObject> Buttons = new List<GameObject>();
    void Awake() { 
        //Finds the GameManager object for access to completed levels
        gm = GameObject.Find("GameManager");
        print("hit");

        //Integer stops one short of the end to avoid a bounds error
        for (int j = 0; j < gm.GetComponent<GameControl>().LevelComplete.Count-1; j++)
        {
            //Disables buttons when 
            if(!gm.GetComponent<GameControl>().LevelComplete[j])
            {
                Buttons[j+1].SetActive(false);
            }

        }
    }

}

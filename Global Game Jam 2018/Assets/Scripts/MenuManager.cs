using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
    public GameObject gm;
    public List<GameObject> Buttons = new List<GameObject>();
    public GameObject MainMenu;
    public GameObject LevelSelect;

    // Use this for initialization
    void Awake () {
        //if(gm == null)
        //{
            gm = GameObject.Find("GameManager");
        //}

        //Integer stops one short of the end to avoid a bounds error
        List<bool> levelCompleted = gm.GetComponent<GameControl>().LevelComplete;
        for(int j = 0; j < levelCompleted.Count - 1; j++)
        {
                Buttons[j+1].SetActive(levelCompleted[j]);
        }

        if (gm.GetComponent<GameControl>().loadOnLevelSelectMenu)
        {
            MainMenu.SetActive(false);
            LevelSelect.SetActive(true);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void LoadLevel(int levelIndex)
    {
        gm.GetComponent<GameControl>().LoadLevel(levelIndex);
    }
}

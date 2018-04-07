using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
    public GameObject gm;

	// Use this for initialization
	void Awake () {
        //if(gm == null)
        //{
            gm = GameObject.Find("GameManager");
        //}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void LoadLevel(int levelIndex)
    {
        gm.GetComponent<GameControl>().LoadLevel(levelIndex);
    }
}

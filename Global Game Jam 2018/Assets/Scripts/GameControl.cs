using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {

    public static GameControl control;
    public string currentLevel = "MenuScene";
    public List<string> Scenes = new List<string>();
    public List<bool> LevelComplete = new List<bool>();
	// Use this for initialization
	void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if(control != this)
        {
            Destroy(gameObject);
        }
	}

    public void LoadLevel(int levelIndex)
    {
        currentLevel = Scenes[levelIndex];
        SceneManager.LoadScene(currentLevel);
    }

    public void EndLevel()
    {
        for(int i = 0; i < Scenes.Count; i++)
        {
            if (currentLevel.Equals(Scenes[i]))
            {
                LevelComplete[i] = true;
            }
        }
        currentLevel = "MenuScene";
        SceneManager.LoadScene("MenuScene");
    }
}

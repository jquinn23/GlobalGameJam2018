using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour {
    public GameObject gameManager;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            GetComponent<AudioSource>().Play();
            gameManager.GetComponent<GameControl>().EndLevel();
        }
    }

}

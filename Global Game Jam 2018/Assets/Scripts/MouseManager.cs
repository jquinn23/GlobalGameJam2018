/**
 *Author: Jonathan R. Quinn 
 * <summary>
 * This script allows animated sprites to be put in place of the regular mouse
 * In order to use this, attatch the script to a mouse object that has an animator 
 * containing the desired animation
 * </summary>
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour {
    private Vector3 mousePosition;
    public float moveSpeed = 0.1f;

    // Use this for initialization
    void Start () {
		Cursor.visible = false;
	}

    // Update is called once per frame
    void Update()
    {
        
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
    }
}

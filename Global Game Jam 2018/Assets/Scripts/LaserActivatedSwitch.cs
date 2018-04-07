using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserActivatedSwitch : MonoBehaviour {
    public bool viewToggle;
    private bool Toggle;
    public bool Inverted;

    private int laserTimeOutCounter;
    private int MAX_LASER_TIMEOUT = 2;
    public List<GameObject> childLasers = new List<GameObject>();

    private void Start()
    {
        for (int j = 0; j < childLasers.Count; j++)
        {
            if (Inverted)
            {
                childLasers[j].GetComponent<LaserCaster>().laserEnabled = true;
            }
            else
            {
                childLasers[j].GetComponent<LaserCaster>().laserEnabled = false;
            }
        }
    }

    void Update()
    {
        viewToggle = Toggle;
    }
    public void FixedUpdate()
    {
        if (laserTimeOutCounter <= 0)
        {
            Toggle = false;
            for (int j = 0; j < childLasers.Count; j++)
            {
                if (Inverted)
                {
                    childLasers[j].GetComponent<LaserCaster>().laserEnabled = true;
                }
                else
                {
                    childLasers[j].GetComponent<LaserCaster>().laserEnabled = false;
                }
            }
        }
        laserTimeOutCounter--;
    }

    public void ActivateSwitch()
    {
        Toggle = true;
        laserTimeOutCounter = MAX_LASER_TIMEOUT;
        for (int j = 0; j < childLasers.Count; j++)
        {
            if (Inverted)
            {
                childLasers[j].GetComponent<LaserCaster>().laserEnabled = false;
            }
            else
            {
                childLasers[j].GetComponent<LaserCaster>().laserEnabled = true;
            }
        }
    }

   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorHandler : MonoBehaviour {

    private GameObject parentEmitter;
    private LaserCaster plc;
    private LaserCaster reflectionLaser;
    private int laserTimeOutCounter;
    private int MAX_LASER_TIMEOUT = 2;
    /* Indicates what position the mirror is in relative to its open sides:
    * 0 = Up and Right, 1 = Right and Down, 2 = Left and Down, 3 = Up and left
    */
    public int mirrorOrientation;
    /* Matrix mapping laser and mirror orientations to reflection direction [LaserOrientation, MirrorOrientation]
     *                       
     *                        Mirror
     *                     0   1   2    3
     *                    ___ ___ ___ ___
     *          Right   |[-1] [-1] [1] [3]
     * Laser    Down    |[0] [-1] [-1] [2]
     *          Left    |[3] [1] [-1] [-1]
     *          Up      |[-1] [0] [2] [-1]
     */
    int[,] laserReflections = new int[4, 4] { { -1, -1, 1, 3 }, { 0, -1, -1, 2 }, { 3, 1, -1, -1 }, { -1, 0, 2, -1 }};
	
    
    // Use this for initialization
	void Start () {
        reflectionLaser = GetComponentInChildren<LaserCaster>();
        reflectionLaser.laserEnabled = false;
        Debug.Log(laserReflections[2, 0]);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(laserTimeOutCounter <= 0)
        {
            reflectionLaser.laserEnabled = false;
        }

        Quaternion tempRotation = Quaternion.identity;
        switch (mirrorOrientation)
        {
            case 0:
                tempRotation.eulerAngles = new Vector3(0, 0, 0);
                break;
            case 1:
                tempRotation.eulerAngles = new Vector3(0, 0, -90);
                break;
            case 2:
                tempRotation.eulerAngles = new Vector3(0, 0, -180);
                break;
            case 3:
                tempRotation.eulerAngles = new Vector3(0, 0, -270);
                break;
            default:
                mirrorOrientation = 0;
                break;
        }
        transform.rotation = tempRotation;

        laserTimeOutCounter--;
	}

    public void ReflectLaser(Vector3 hitPos, GameObject pe)
    {
        parentEmitter = pe;
        plc = parentEmitter.GetComponent<LaserCaster>();
        laserTimeOutCounter = MAX_LASER_TIMEOUT;

        reflectionLaser.laserOrientation = laserReflections[plc.laserOrientation, mirrorOrientation];
        reflectionLaser.laserEnabled = true;
        reflectionLaser.emitterOffset = transform.InverseTransformPoint(hitPos) + reflectionLaser.baseEmitterOffset;
        //reflectionLaser.emitterOffset = transform.TransformPoint(reflectionLaser.baseEmitterOffset) - hitPos;
        //Debug.Log("eo " + reflectionLaser.emitterOffset);
        //Debug.Log("beot " + transform.TransformPoint(reflectionLaser.baseEmitterOffset));
        //Debug.Log("hp " + hitPos);
    }
}

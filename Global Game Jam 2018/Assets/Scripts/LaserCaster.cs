using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCaster : MonoBehaviour {
    public GameObject laserBeam;
    public float laserLength = 10;
    public Vector3 hitPoint;
    public Vector3 emitterPos;
    public Vector3 emitterOffset;
    public Vector3 eo2;
    public Vector3 emitterDir;
    public Vector3 baseEmitterOffset =  new Vector3(0.5f, 0);
    public bool laserEnabled = true;
    /* Indicates what direction the laser is being emitted:
    * 0 = Right, 1 =Down, 2 = Left, 3 = Up
    */
    public int laserOrientation;
	// Use this for initialization
	void Start () {
        laserBeam = Instantiate(laserBeam, transform.position, Quaternion.identity);
        laserBeam.transform.SetParent(transform);
        emitterOffset = baseEmitterOffset;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;
        layerMask |= 1 << 2;
        layerMask |= 1 << 9;
        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;


        //Rotation sync
        Quaternion tempRotation = Quaternion.identity;
        switch (laserOrientation)
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
            case -1:
                laserEnabled = false;
                break;
            default:
                laserOrientation = -1;
                break;
        }
        transform.rotation = tempRotation;

        //laser emission code
        emitterPos = transform.TransformPoint(emitterOffset+ eo2);
        emitterDir = transform.TransformDirection(Vector3.right);
        if (laserEnabled)
        {
            RaycastHit2D hit = Physics2D.Raycast(emitterPos, emitterDir, 100, layerMask);
            if (hit.collider != null)
            {
                hitPoint = new Vector3(hit.point.x, hit.point.y);
                laserLength = Vector2.Distance(transform.position, hit.point);
                laserBeam.transform.position = Vector3.Lerp(emitterPos, new Vector3(hit.point.x, hit.point.y), 0.5f);
                Vector3 tempScale = laserBeam.transform.localScale;
                tempScale.x = laserLength;
                laserBeam.transform.localScale = tempScale;
                Debug.DrawRay(emitterPos, emitterDir, Color.green, laserLength);
                if (hit.collider.gameObject.GetComponent<MirrorHandler>())
                {
                    hit.collider.gameObject.GetComponent<MirrorHandler>().ReflectLaser(hitPoint, gameObject);
                }
                if (hit.collider.gameObject.GetComponent<LaserActivatedSwitch>())
                {
                    hit.collider.gameObject.GetComponent<LaserActivatedSwitch>().ActivateSwitch();
                }
            }
            else
            {
                laserLength = 100;
                laserBeam.transform.position = Vector3.Lerp(emitterPos, transform.TransformPoint(new Vector3(100, 0)), 0.5f);
                Vector3 tempScale = laserBeam.transform.localScale;
                tempScale.x = laserLength;
                laserBeam.transform.localScale = tempScale;
            }
        }
        else
        {
            laserLength = 0;
            laserBeam.transform.position = emitterPos;
            Vector3 tempScale = laserBeam.transform.localScale;
            tempScale.x = laserLength;
            laserBeam.transform.localScale = tempScale;
        }
        
    }
}

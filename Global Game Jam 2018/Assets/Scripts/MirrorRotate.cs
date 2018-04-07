using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorRotate : MonoBehaviour {

	void OnMouseDown()
	{
		gameObject.GetComponent<MirrorHandler> ().RotateClick ();
	}

}

using UnityEngine;
using System.Collections;

public class SHAGI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.S)) {
			GetComponent<AudioSource > ().UnPause ();
		} else { 
			GetComponent<AudioSource> ().Pause ();
		}

	}
}

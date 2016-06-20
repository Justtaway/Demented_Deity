using UnityEngine;
using System.Collections;

public class SHAGI2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.DownArrow)) {
			GetComponent<AudioSource > ().UnPause ();
		} else { 
			GetComponent<AudioSource> ().Pause ();
		}

	}
}

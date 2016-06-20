using UnityEngine;
using System.Collections;

public class eq : MonoBehaviour {

	public GameObject invert;
	public GameObject pl;
	private GameObject equpment;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Player2 equpment = pl.GetComponent<Player2> ();

		if (invert.activeSelf) 
		{
			equpment.enabled = false;

		} else 

		{
			equpment.enabled = true;
		}

	}
}

using UnityEngine;
using System.Collections;

public class eq2 : MonoBehaviour {

	public GameObject invert;
	public GameObject pl;
	private GameObject equpment;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Player equpment = pl.GetComponent<Player> ();

		if (invert.activeSelf) 
		{
			equpment.enabled = false;

		} else 

		{
			equpment.enabled = true;
		}

	}
}

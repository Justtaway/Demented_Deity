using UnityEngine;
using System.Collections;

public class ConsItem2 : MonoBehaviour {
	private GameObject pla2;
	// Use this for initialization
	void Start () {
		
		ConsumeItem2 pla2 = gameObject.GetComponentInChildren<ConsumeItem2>();
		if (pla2 != null) 
		{
			pla2.enabled = false;
		}
	
	}
	
	// Update is called once per frame
	void Update () {

		ConsumeItem2 pla2 = gameObject.GetComponentInChildren<ConsumeItem2>();
		if (pla2 != null) 
		{
			pla2.enabled = false;
		}
	
	}
}

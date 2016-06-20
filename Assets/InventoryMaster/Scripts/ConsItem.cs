using UnityEngine;
using System.Collections;

public class ConsItem : MonoBehaviour {
	private GameObject pla2;
	// Use this for initialization
	void Start () {
		
		ConsumeItem pla2 = gameObject.GetComponentInChildren<ConsumeItem>();
		if (pla2 != null) 
		{
			pla2.enabled = false;
		}
	
	}
	
	// Update is called once per frame
	void Update () {

		ConsumeItem pla2 = gameObject.GetComponentInChildren<ConsumeItem>();
		if (pla2 != null) 
		{

			pla2.enabled = false;
		}
	
	}
}

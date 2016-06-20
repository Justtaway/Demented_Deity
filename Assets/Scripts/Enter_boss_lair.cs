using UnityEngine;
using System.Collections;

public class Enter_boss_lair : MonoBehaviour {

	public GameObject Cube;

	void OnTriggerExit2D  (Collider2D col){
		if (col.gameObject.name == "Player") {
			
			Cube.GetComponent<Collider2D> ().isTrigger = false;

		}
	}
}

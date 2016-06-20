using UnityEngine;
using System.Collections;

public class Enter : MonoBehaviour {
	public GameObject tutrDia;
	public GameObject SkillBar;
	public GameObject Inver;
	public GameObject Cube;

	void OnTriggerEnter2D  (Collider2D col){
		if (col.gameObject.name == "Player") {
			tutrDia.SetActive (true);
			SkillBar.SetActive (false);
			Inver.SetActive (false);
			Destroy (Cube);
            col.gameObject.GetComponent<PlayerMovement>().moveSpeed = 1f;
		}


	}
	/*void Update(){
		if (tutrDia == enabled) {
			Time.timeScale = 0;
		} 
		else {
			Time.timeScale = 1;
		}
	
	}*/
}

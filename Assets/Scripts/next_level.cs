using UnityEngine;
using System.Collections;

public class next_level : MonoBehaviour {

	public GameObject Player_box;
	public GameObject level_box;

	private AudioSource[] aSources;
	public AudioSource mainSource;
	public AudioSource nextSource;

	void start()
	{
		aSources = GetComponents<AudioSource>();
		mainSource = aSources[0] as AudioSource;
		nextSource = aSources[1] as AudioSource;
		mainSource.dopplerLevel = 0f;
		nextSource.dopplerLevel = 0f;
		mainSource.loop = true;
		nextSource.loop = true;
	}


	void OnTriggerEnter2D  (Collider2D col){
		if (col.gameObject.name == "Player") {

			Player_box.transform.position = level_box.transform.position;

			if (mainSource.isPlaying) 
			{
				mainSource.Stop ();
			}

			if (!nextSource.isPlaying) 
			{
				nextSource.Play ();
			}


		
		}
}
}

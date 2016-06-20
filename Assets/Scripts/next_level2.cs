using UnityEngine;
using System.Collections;

public class next_level2 : MonoBehaviour {

	public GameObject Player_box;
	public GameObject level_box;

	private AudioSource[] aSources;
	public AudioSource mainSource;
	public AudioSource bossSource;

	void start()
	{
		aSources = GetComponents<AudioSource>();
		mainSource = aSources[0] as AudioSource;
		bossSource = aSources[1] as AudioSource;
		mainSource.dopplerLevel = 0f;
		bossSource.dopplerLevel = 0f;
		mainSource.loop = true;
		bossSource.loop = true;
	}


	void OnTriggerEnter2D  (Collider2D col){
		if (col.gameObject.name == "Player2") {

			Player_box.transform.position = level_box.transform.position;

			if (mainSource.isPlaying) 
			{
				mainSource.Stop ();
			}

			if (!bossSource.isPlaying) 
			{
				bossSource.Play ();
			}


		
		}
}
}

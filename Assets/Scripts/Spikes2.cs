using UnityEngine;
using System.Collections;
using System;

public class Spikes2 : MonoBehaviour {

    public Animator anim;
    private HP2 player;

	private AudioSource[] aSources;
	public AudioSource spikeSource;

    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player2").GetComponent<HP2>();
		aSources = GetComponents<AudioSource>();
		spikeSource = aSources [0] as AudioSource;
		spikeSource.dopplerLevel = 0f;
		spikeSource.loop = true;
		spikeSource.pitch = 2.80f;
    }

    void OnTriggerEnter2D(Collider2D other)

    {

        if (other.gameObject.tag == "Player2") {

            anim.SetBool("Attack", true);
            player.Damage(10f);


            if (!spikeSource.isPlaying) 
			{
				spikeSource.Play ();
			}
            
        }

    }

	void OnTriggerExit2D(Collider2D other)
	{
		anim.SetBool ("Attack", false);
		if (spikeSource.isPlaying)
		{
			spikeSource.Stop ();
		}
	}


}

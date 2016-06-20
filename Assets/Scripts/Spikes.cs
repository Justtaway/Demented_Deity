using UnityEngine;
using System.Collections;
using System;

public class Spikes : MonoBehaviour {

    public Animator anim;
    private HP player;

	private AudioSource[] aSources;
	public AudioSource spikeSource;

    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<HP>();
		aSources = GetComponents<AudioSource>();
		spikeSource = aSources [0] as AudioSource;
		spikeSource.dopplerLevel = 0f;
		spikeSource.loop = true;
		spikeSource.pitch = 2.80f;
    }

    void OnTriggerEnter2D(Collider2D other)

    {

        if (other.gameObject.tag == "Player") {

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

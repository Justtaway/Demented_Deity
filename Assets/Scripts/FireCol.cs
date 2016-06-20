using UnityEngine;
using System.Collections;

public class FireCol : MonoBehaviour
{

    private GameObject Badoom; // переменна для взрыва
    private GameObject Skill;  // переменная для скила

	private AudioSource[] aSources;
	public AudioSource flySource; //звук

    // Use this for initialization
    void Start()
    {
		aSources = GetComponents<AudioSource>();
		flySource = aSources[1] as AudioSource;
		flySource.dopplerLevel = 0f;
		flySource.loop = true; //луп
    }

    // Update is called once per frame
    void Update()
    {
        Skill = this.gameObject;

		if (!flySource.isPlaying) //звук звук не воспроизводится 
		{

			flySource.Play(); //воспроизвести звук 

		}


    }
    void OnTriggerEnter2D(Collider2D collison) // при столкновении колайдеров 
    {
        if (collison.gameObject.tag == "Mob" || collison.gameObject.tag == "EdgeCollider")
        {
           Destroy(Skill); // уничтожение фаирбола
           Badoom = Instantiate(Resources.Load("exploit")) as GameObject; // появление взрыва
           Badoom.transform.position = Skill.transform.position;        // позиция фаирбала
           Destroy(Badoom, 0.7f);
        }
    }
}

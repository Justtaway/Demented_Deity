using UnityEngine;
using System.Collections;

public class AngelMov : MonoBehaviour {

    Rigidbody2D rbody;
    Animator anima;
    private GameObject player;
    private GameObject mob;
    public float moveSpeed;
    public float playerDistance;

    private AudioSource[] aSources;
    public AudioSource stepSource;
    public AudioSource fightSource;
    float x, y;

    // Use this for initialization
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();
        aSources = GetComponents<AudioSource>();
        stepSource = aSources[0] as AudioSource;
        fightSource = aSources[1] as AudioSource;
        fightSource.dopplerLevel = 0f;
        stepSource.dopplerLevel = 0f;
        fightSource.loop = true;
        stepSource.loop = true;
        player = GameObject.Find("Player");
        mob = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<AudioSource>().Pause ();

        playerDistance = Vector2.Distance(mob.transform.position, player.transform.position); //расстояние до игрока
        var movVect = player.transform.position - mob.transform.position; //направление на игрока

        Vector2 movement_vector = new Vector2(movVect.x, movVect.y);

        x = movVect.x;
        y = movVect.y;

        if ((playerDistance < 90f) & (playerDistance > 15f))
        {
            //орк начинает идти на игрока
            if (movement_vector != Vector2.zero)
            {

                anima.SetBool("iswalking", true);       //анимация ходьбы 
                anima.SetFloat("input_x", movement_vector.x);
                anima.SetFloat("input_y", movement_vector.y);
                walk();         //само движение

            }
            else
            {
                anima.SetBool("iswalking", false); //остановка
                anima.SetFloat("input_x", movement_vector.x);
                anima.SetFloat("input_y", movement_vector.y);
                stopwlk();

            }

        }
        var moVect = player.transform.position - mob.transform.position;
        if (playerDistance < 15f)

        {
            stopwlk();           //остановка



            anima.SetFloat("input_x", x);
            anima.SetFloat("input_y", y);
            anima.SetBool("defence", true); // шита атаки

            //звуки удара
            if (!fightSource.isPlaying)
            {

                fightSource.Play();

            }



        }
        else
        {
            //остановка шита
            anima.SetBool("defence", false);
            anima.SetFloat("input_x", movement_vector.x);
            anima.SetFloat("input_y", movement_vector.y);
        }

        if (playerDistance > 90)
        { //останавливается если отойти далеко
            anima.SetFloat("input_x", x);
            anima.SetFloat("input_y", y);
            anima.SetBool("iswalking", false);
        }

    }
    void walk()
    {

        var movVect = player.transform.position - mob.transform.position;
        transform.Translate(Vector3.MoveTowards(movVect, movVect, 0f) * moveSpeed * Time.deltaTime);
        if (!stepSource.isPlaying)
        {

            stepSource.Play();

        }

        if (fightSource.isPlaying)
        {

            fightSource.Pause();

        }


    }
    void stopwlk()
    {
        var movVect = player.transform.position - mob.transform.position;
        transform.Translate(Vector3.MoveTowards(movVect, movVect, 0f) * 0f * Time.deltaTime);
        if (stepSource.isPlaying)
        {

            stepSource.Pause();

        }

    }

}

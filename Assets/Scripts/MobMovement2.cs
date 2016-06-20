using UnityEngine;
using System.Collections;


public class MobMovement2 : MonoBehaviour
{
    
     Rigidbody2D rbody;
    Animator anima;
    BoxCollider2D Coll;
    public GameObject healthBar;
    private GameObject player;
    private GameObject mob;
    public float moveSpeed;
    public float playerDistance;

    private AudioSource[] aSources;
    public AudioSource stepSource;
    public AudioSource fightSource;
    float x, y;

    private HP2 player_HP2; // переменная для определения игрока
    public float dmg;

    // Use this for initialization
    void Start()
    {
        player_HP2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<HP2>(); // оперделение игрока
        Coll = GetComponent<BoxCollider2D>(); 
        rbody = GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();
        aSources = GetComponents<AudioSource>();
        stepSource = aSources[0] as AudioSource;
        fightSource = aSources[1] as AudioSource;
        fightSource.dopplerLevel = 0f;
        stepSource.dopplerLevel = 0f;
        fightSource.loop = true;
        stepSource.loop = true;
        player = GameObject.Find("Player2");
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
        if ((playerDistance < 60f) & (playerDistance > 55f))
        {

            //Скелет встает если подойти близко

            anima.SetBool("wake", true);        //анимация восстания

            // скелет просто стоит
            anima.SetBool("idle", true);        //анимация стояния
            anima.SetFloat("input_x", movement_vector.x);
            anima.SetFloat("input_y", movement_vector.y);
            healthBar.SetActive(true);
			Coll.enabled = true;
            

        }
        else
        {
            if ((playerDistance < 55f) & (playerDistance > 10f))
            {
                //скелет начинает идти на игрока
                if (movement_vector != Vector2.zero)
                {

                    anima.SetBool("iswalking", true);       //анимация ходьбы 
                    anima.SetFloat("input_x", movement_vector.x);
                    anima.SetFloat("input_y", movement_vector.y);
                    walk();         //само движение

                }
                else
                {
                    anima.SetBool("iswalking", false);
                    anima.SetFloat("input_x", movement_vector.x);
                    anima.SetFloat("input_y", movement_vector.y);

                }

            }
        }
        if (playerDistance > 60)
        {
            stopwlk();  //остановка
            anima.SetBool("idle", false);

        }
        var moVect = player.transform.position - mob.transform.position;
      
		if (playerDistance< 10f)

        {
            stopwlk();           //остановка



            anima.SetFloat("input_x", x);
            anima.SetFloat("input_y", y);
            anima.SetBool("attacking", true); // анимация атаки

            //звуки удара
            if (!fightSource.isPlaying)
            {

                fightSource.Play();

            }



        }
        else
        {
            //остановка атаки
            anima.SetBool("attacking", false);
            anima.SetFloat("input_x", movement_vector.x);
            anima.SetFloat("input_y", movement_vector.y);
        }

        if (playerDistance > 60)
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

        if (anima.GetBool("attacking"))
        {
            player_HP2.Damage(dmg);
        }

    }

}

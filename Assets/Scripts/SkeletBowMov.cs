using UnityEngine;
using System.Collections;

public class SkeletBowMov : MonoBehaviour {

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
        fightSource.loop = false;
        stepSource.loop = true;
        player = GameObject.Find("Player");
        mob = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<AudioSource>().Pause ();

        playerDistance = Vector2.Distance(mob.transform.position, player.transform.position); //расстояние до игрока
        var movVect = player.transform.position - mob.transform.position; //вектор от моба на игрока

        Vector2 movement_vector = new Vector2(movVect.x, movVect.y); // позиции вектора

        x = movVect.x; // позиция Х
        y = movVect.y; // позиция У


        
        if ((playerDistance > 100) & (playerDistance < 120)) //скелет подходит к игроку
        {
            anima.SetBool("iswalking", true); //анимация ходьбы
            anima.SetFloat("input_x", movement_vector.x); // вводим значения Х
            anima.SetFloat("input_y", movement_vector.y); // вводим значения У
            walk(); // анимация ходьбы 
        }
        if (playerDistance < 100f)      //скелет стреляет в игрока
        {
			stopwlk ();

            anima.SetBool("attacking", true);       //анимация стрельбы
            anima.SetFloat("input_x", movement_vector.x);
            anima.SetFloat("input_y", movement_vector.y);

			if (!fightSource.isPlaying)
			{

				fightSource.Play();

			}
        }
        else
        {
            if ((playerDistance > 100) & (playerDistance < 120)) //скелет подходит к игроку
            {
                anima.SetBool("attacking", false);       // анимация ходьбы
                anima.SetFloat("input_x", movement_vector.x); // вводим значения Х
                anima.SetFloat("input_y", movement_vector.y); // вводим значения У
                walk();// анимация ходьбы 

            }
        }
        if (playerDistance > 120)
        {
            anima.SetBool("iswalking", false); //остановка
            anima.SetFloat("input_x", movement_vector.x); // вводим значения Х
            anima.SetFloat("input_y", movement_vector.y); // вводим значения У
            stopwlk();// остановка
        }
    
        
         
    }
    void walk() // движение
    {

        var movVect = player.transform.position - mob.transform.position; //вектор от моба на игрока
        transform.Translate(Vector3.MoveTowards(movVect, movVect, 0f) * moveSpeed * Time.deltaTime);
       
		if (!stepSource.isPlaying)
        {

            stepSource.Play();

        }




    }
    void stopwlk() // остановка
    {
        var movVect = player.transform.position - mob.transform.position; //вектор от моба на игрока
        transform.Translate(Vector3.MoveTowards(movVect, movVect, 0f) * 0f * Time.deltaTime);
        
		if (stepSource.isPlaying)
        {

            stepSource.Pause();

        }

    }

}


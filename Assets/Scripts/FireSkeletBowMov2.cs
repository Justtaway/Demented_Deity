using UnityEngine;
using System.Collections;

public class FireSkeletBowMov2 : MonoBehaviour {

    Rigidbody2D rbody;
    static Animator anima, anima1;
    GameObject hit;
    private GameObject player;
    private GameObject mob;
    private GameObject Arrow;   // переменная для создания скила
    public float moveSpeed;
    public float playerDistance;
    float x, y;                 //переменные позиции Х, У.
    public float currentCoolDown;
    public float cooldown;
    private AudioSource[] aSources;
    public AudioSource stepSource;
    public AudioSource fightSource;


    public void ArrowLaunch()
    {
        Arrow = Instantiate(Resources.Load("arrow")) as GameObject; // появление стрелы
        anima1 = Arrow.GetComponent<Animator>(); // присвоения компонента АНИМАТОР в переменную
        var movVect = mob.GetComponent<Animator>(); // направление скелета
        x = movVect.GetFloat("input_x"); // поззиция Х
        y = movVect.GetFloat("input_y"); // позиция У
        anima1.SetFloat("input_x", x); // присвоение аниматору стрелы позиции Х
        anima1.SetFloat("input_y", y); // присвоение аниматору стрелы позиции У
        Arrow.transform.position = mob.transform.position; // позиция игрока
    }

    // Use this for initialization
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();

        player = GameObject.Find("Player2");
        mob = this.gameObject;
    }


    // Update is called once per frame
    void Update()
    {

        playerDistance = Vector2.Distance(mob.transform.position, player.transform.position); //расстояние до игрока
        var movVect = player.transform.position - mob.transform.position; //вектор от моба на игрока

        Vector2 movement_vector = new Vector2(movVect.x, movVect.y); // позиции вектора

        x = movVect.x; // позиция Х
        y = movVect.y; // позиция У
                
        if ((playerDistance > 100) & (playerDistance < 120)) //скелет подходит к игроку
        {
            anima.SetBool("iswalking", true); //анимация ходьбы
            anima.SetBool("attacking", false);
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
    

			if (currentCoolDown >= cooldown)
                {
                    currentCoolDown = 0;
                    ArrowLaunch();
                }
                if (currentCoolDown < cooldown)
                {
                    currentCoolDown += Time.deltaTime;
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

        var movVect1 = player.transform.position - mob.transform.position; //вектор от моба на игрока
        transform.Translate(Vector3.MoveTowards(movVect1, movVect1, 0f) * moveSpeed * Time.deltaTime);

		if (!stepSource.isPlaying)
		{

			stepSource.Play();

		}

		if (fightSource.isPlaying)
		{

			fightSource.Pause();

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


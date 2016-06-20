using UnityEngine;
using System.Collections;

public class arrowLaunch : MonoBehaviour
{
    public float ArSpeed; // скорость стрелы
    private GameObject arrow;   // переменная для определения стрелы
    private HP player_HP; // переменная для определения игрока
    private Transform Player;
    public float dmg;
    private Transform mobTrans;
    private float x;
    private float y;
    private Animator MobAnimator;
    private Animator ArrowAnim;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        player_HP = GameObject.FindGameObjectWithTag("Player").GetComponent<HP>(); // оперделение игрока
        arrow = gameObject; // определение стрелы
        mobTrans = GameObject.FindGameObjectWithTag("Mob").transform;
        MobAnimator = GameObject.FindGameObjectWithTag("Mob").GetComponent<Animator>();
        ArrowAnim = arrow.GetComponent<Animator>();
        x = ArrowAnim.GetFloat("input_x"); // позиция Х
        y = ArrowAnim.GetFloat("input_y"); // позиция У

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var movVect = Player.position - arrow.transform.position; //вектор от моба на игрока

        if ((x >= 0) && (arrow != null)) //&& (movVect.magnitude >= 15f)) // при таких значениях стрела летит вправо
        {
            Debug.Log("right: " + x);
            arrow.GetComponent<Rigidbody2D>().AddForce(mobTrans.right * ArSpeed, ForceMode2D.Impulse);
        }
        if ((x <= 0) && (arrow != null)) //&& (movVect.magnitude >= 15f)) // при таких значениях стрела летит влево
        {
            Debug.Log("left: " + x);
            arrow.GetComponent<Rigidbody2D>().AddForce(-mobTrans.right * ArSpeed, ForceMode2D.Impulse);
        }

    }

    void OnTriggerEnter2D(Collider2D collison) // при столкновении колайдеров 
    {
        if (collison.gameObject.tag == "Player")
        {
            player_HP.Damage(dmg);
            Destroy(arrow);
        } else
        {
            Destroy(arrow, 3f);
        }
    }
}

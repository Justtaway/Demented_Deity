using UnityEngine;
using System.Collections;

public class arrowLaunch2 : MonoBehaviour
{
    public float ArSpeed; // скорость стрелы
    private GameObject arrow2;   // переменная для определения стрелы
    private HP2 player2; // переменная для определения игрока
    public float dmg;
    private Transform mobTrans;
    private float x;
    private float y;
    private Animator MobAnimator;
    private Animator ArrowAnim;

    // Update is called once per frame
    void FixedUpdate()
    {
        player2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<HP2>(); // оперделение игрока
        arrow2 = gameObject; // определение стрелы
        mobTrans = GameObject.FindGameObjectWithTag("Mob").transform;
        MobAnimator = GameObject.FindGameObjectWithTag("Mob").GetComponent<Animator>();
        ArrowAnim = arrow2.GetComponent<Animator>();
        x = MobAnimator.GetFloat("input_x"); // позиция Х
        y = MobAnimator.GetFloat("input_y"); // позиция У

        var movVect = player2.transform.position - arrow2.transform.position; //вектор от моба на игрока

        if ((x >= 0) && (arrow2 != null) && (movVect.magnitude >= 15f)) // при таких значениях стрела летит вправо
        {
            arrow2.GetComponent<Rigidbody2D>().AddForce(mobTrans.right * ArSpeed, ForceMode2D.Impulse);
        }
        if ((x <= 0) && (arrow2 != null) && (movVect.magnitude >= 15f)) // при таких значениях стрела летит влево
        {
            arrow2.GetComponent<Rigidbody2D>().AddForce(-mobTrans.right * ArSpeed, ForceMode2D.Impulse);
        }

    }

    void OnTriggerEnter2D(Collider2D collison) // при столкновении колайдеров 
    {
        if (collison.gameObject.tag == "Player2")
        {
            player2.Damage(dmg);
            Destroy(arrow2);
        } else
        {
            Destroy(arrow2, 3f);
        }
    }
}

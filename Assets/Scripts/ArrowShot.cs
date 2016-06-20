using UnityEngine;
using System.Collections;

public class ArrowShot : MonoBehaviour
{
    Animator anima;
    private GameObject Arrow;   // переменная для создания скила
    private GameObject Player;  // переменная для определения позиции игрока
    private GameObject mob;
    public float Speed;         // публичная переменная СКОРОСТИ
    float x, y;                 //переменные позиции Х, У.
    private Vector3 offset;
        
    public void ArrowLaunch()
    {
        offset = new Vector3(70, 30, 0);
        Arrow = Instantiate(Resources.Load("arrow")) as GameObject; // появление стрелы
        mob = GameObject.Find("skelet_bow"); // определение моба
        Player = GameObject.Find("Player"); // отпределение игрока
        anima = Arrow.GetComponent<Animator>(); // присвоения компонента АНИМАТОР в переменную

        var movVect = mob.GetComponent<Animator>(); // направление скелета
        x = movVect.GetFloat("input_x"); // поззиция Х
        y = movVect.GetFloat("input_y"); // позиция У

        anima.SetFloat("input_x", x); // присвоение аниматору стрелы позиции Х
        anima.SetFloat("input_y", y); // присвоение аниматору стрелы позиции У

        Arrow.transform.position = mob.transform.position; // позиция игрока
        Arrow.GetComponent<AudioSource>().Play(); // воспроизведение звука 
        //anima.SetBool("flying", true); // переход на анимацию полета
    }

    void Update()
    {
        //запускает скилл при атаке
        if (anima.GetBool("attacking"))
        {
            if (Arrow)
           {
              /*  //если скилл есть, уничтожает предыдущий и запускает новый
               Destroy(Arrow);
                
            }
            else
            {*/
                ArrowLaunch();
            }
        }

        if ((x == 0f) & (y == 1f) && (Arrow != null)) // при таких значениях СТРЕЛА летит вверх
        {
            Arrow.transform.position = Arrow.transform.TransformPoint(Vector2.up * Speed);
        }
        if ((x == 0f) & (y == -1f) && (Arrow != null)) // при таких значениях СТРЕЛА летит вниз
        {
            Arrow.transform.position = Arrow.transform.TransformPoint(Vector2.down * Speed);
        }
        if ((x == 1f) & (y == 0f) && (Arrow != null)) // при таких значениях СТРЕЛА летит вправо
        {
            Arrow.transform.position = Arrow.transform.TransformPoint(Vector2.right * Speed);
        }
        if ((x == -1f) & (y == 0f) && (Arrow != null)) // при таких значениях СТРЕЛА летит влево
        {
            Arrow.transform.position = Arrow.transform.TransformPoint(Vector2.left * Speed);
        }
    }
}

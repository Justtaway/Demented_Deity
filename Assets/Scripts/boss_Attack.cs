using UnityEngine;
using System.Collections;

public class boss_Attack : MonoBehaviour {
    private HP player; // переменная для определения игрока
    public float dmg;

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<HP>(); // оперделение игрока
    }

    void OnTriggerEnter2D(Collider2D collison) // при столкновении колайдеров 
    {
        if (collison.gameObject.tag == "Player")
        {
            player.Damage(dmg);
        }
    }
}

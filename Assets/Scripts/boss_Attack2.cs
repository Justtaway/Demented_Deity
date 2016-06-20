using UnityEngine;
using System.Collections;

public class boss_Attack2 : MonoBehaviour {
    private HP2 player; // переменная для определения игрока
    public float dmg;

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player2").GetComponent<HP2>(); // оперделение игрока
    }

    void OnTriggerEnter2D(Collider2D collison) // при столкновении колайдеров 
    {
        if (collison.gameObject.tag == "Player2")
        {
            player.Damage(dmg);
        }
    }
}

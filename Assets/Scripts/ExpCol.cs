using UnityEngine;
using System.Collections;

public class ExpCol : MonoBehaviour
{
    private GameObject mob; //переменная для определения моба
    private SkeletonHealth health; // скипт здоровья
    public float health_mob; // значение здоровья
    public float result = 0f; // итоговое значение

    // Use this for initialization
    void Start()
    {
        mob = this.gameObject;
    }

    void OnTriggerEnter2D(Collider2D collison) // при столкновении колайдеров 
    {
        if (collison.gameObject.tag == "Sword")
        {
            result = health_mob - collison.gameObject.GetComponent<Damage>().damage; // вычисление результата
            gameObject.GetComponent<SkeletonHealth>().cur_Health = result; // присваиваем значение результата ЗДОРОВЬЮ
            Mobs();
        }
        if (collison.gameObject.tag == "Explo")
        {
            result = health_mob - collison.gameObject.GetComponent<Damage>().damage; // вычисление результата
            gameObject.GetComponent<SkeletonHealth>().cur_Health = result; // присваиваем значение результата ЗДОРОВЬЮ 
            Mobs();
        }
        if (collison.gameObject.tag == "Lightning")
        {
            result = health_mob - collison.gameObject.GetComponent<Damage>().damage; // вычисление результата
            gameObject.GetComponent<SkeletonHealth>().cur_Health = result; // присваиваем значение результата ЗДОРОВЬЮ
            Mobs();
        }
    }

    void Mobs()
    {
        if (mob.gameObject.name == "Skeleton")
        {
            if (result <= 0) // проверка если здоровье <= 0
            {
                Destroy(mob); // уничтожение моба
                GameObject death = Instantiate(Resources.Load("Skelet_death")) as GameObject; // вызов префаба смерти
                death.transform.position = mob.transform.position; // позиция моба
                death.GetComponent<Animator>().SetBool("isdead", true); // переход на анимацию смерти 
            }
        }
        else
        if (mob.gameObject.name =="orc_blade")
        {
            if (result <= 0) // проверка если здоровье <= 0
            {
                Destroy(mob); // уничтожение моба
                GameObject death = Instantiate(Resources.Load("orc_blade_dead")) as GameObject; // вызов префаба смерти
                death.transform.position = mob.transform.position; // позиция моба
                death.GetComponent<Animator>().SetBool("isdead", true); // переход на анимацию смерти 

            }
        }
        else
        if (mob.gameObject.name == "orc_mace")
        {
            if (result <= 0) // проверка если здоровье <= 0
            {
                Destroy(mob); // уничтожение моба
                GameObject death = Instantiate(Resources.Load("orc_mace_dead")) as GameObject; // вызов префаба смерти
                death.transform.position = mob.transform.position; // позиция моба
                death.GetComponent<Animator>().SetBool("isdead", true); // переход на анимацию смерти 

            }
        }
        else
        if (mob.gameObject.name == "orc_bow")
        {
            if (result <= 0) // проверка если здоровье <= 0
            {
                Destroy(mob); // уничтожение моба
                GameObject death = Instantiate(Resources.Load("orc_bow_dead")) as GameObject; // вызов префаба смерти
                death.transform.position = mob.transform.position; // позиция моба
                death.GetComponent<Animator>().SetBool("isdead", true); // переход на анимацию смерти 

            }
        }
        else
        if (mob.gameObject.name == "skelet_bow")
        {
            if (result <= 0) // проверка если здоровье <= 0
            {
                Destroy(mob); // уничтожение моба
                GameObject death = Instantiate(Resources.Load("Skelet_death")) as GameObject; // вызов префаба смерти
                death.transform.position = mob.transform.position; // позиция моба
                death.GetComponent<Animator>().SetBool("isdead", true); // переход на анимацию смерти 
            }
        }
        else
        if (mob == GameObject.Find("BossSkelet"))
        {
            if (result <= 0) // проверка если здоровье <= 0
            {
                Destroy(mob); // уничтожение моба
                GameObject death = Instantiate(Resources.Load("BossSkeletDead")) as GameObject; // вызов префаба смерти
                death.transform.position = mob.transform.position; // позиция моба
                death.GetComponent<Animator>().SetBool("isdead", true); // переход на анимацию смерти 

            }
        }
        if (mob.gameObject.name == "Angel")
        {
            if (result <= 0) // проверка если здоровье <= 0
            {
                Destroy(mob); // уничтожение моба
                GameObject death = Instantiate(Resources.Load("Death_Angel")) as GameObject; // вызов префаба смерти
                death.transform.position = mob.transform.position; // позиция моба
                death.GetComponent<Animator>().SetBool("isdead", true); // переход на анимацию смерти 

            }
        }
        if (mob.gameObject.name == "peetooh")
        {
            if (result <= 0) // проверка если здоровье <= 0
            {
                Destroy(mob); // уничтожение моба
                GameObject death = Instantiate(Resources.Load("PetoohDead")) as GameObject; // вызов префаба смерти
                death.transform.position = mob.transform.position; // позиция моба
                death.GetComponent<Animator>().SetBool("isdead", true); // переход на анимацию смерти 

            }
        }
		if (mob.gameObject.name == "angel_blue")
		{
			if (result <= 0) // проверка если здоровье <= 0
			{
				Destroy(mob); // уничтожение моба
				GameObject death = Instantiate(Resources.Load("angel_mob_dead")) as GameObject; // вызов префаба смерти
				death.transform.position = mob.transform.position; // позиция моба
				death.GetComponent<Animator>().SetBool("isdead", true); // переход на анимацию смерти 

			}
		}
        if (mob.gameObject.name == "angel_black")
        {
            if (result <= 0) // проверка если здоровье <= 0
            {
                Destroy(mob); // уничтожение моба
                GameObject death = Instantiate(Resources.Load("angel_mob_dead")) as GameObject; // вызов префаба смерти
                death.transform.position = mob.transform.position; // позиция моба
                death.GetComponent<Animator>().SetBool("isdead", true); // переход на анимацию смерти 

            }
        }
        if (mob.gameObject.name == "angel_girl")
        {
            if (result <= 0) // проверка если здоровье <= 0
            {
                Destroy(mob); // уничтожение моба
                GameObject death = Instantiate(Resources.Load("angel_mob_dead")) as GameObject; // вызов префаба смерти
                death.transform.position = mob.transform.position; // позиция моба
                death.GetComponent<Animator>().SetBool("isdead", true); // переход на анимацию смерти 

            }
        }
    }
}

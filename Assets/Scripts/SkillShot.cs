using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SkillShot : MonoBehaviour
{
    Animator anima;
    private GameObject Skill;   // переменная для создания скила
    private GameObject Player;  // переменная для определения позиции игрока
    private Animator movVect;
    public float Speed;         // публичная переменная СКОРОСТИ
    private float x, y;                 //переменные позиции Х, У.

    private bool isPressed = true; //переменная для проверки нажата ли кнопка

    public List<Skills> skills;
    private GameObject mobs;
    private Vector3 direction;

    public float Distance;

    public float heal_amount;
    public float LightningDistance;

    void Start()
    {
        Player = GameObject.Find("Player"); // отпределение игрока
    }

    public void SkillLaunch(string skill_name)
    {
        Skill = Instantiate(Resources.Load(skill_name)) as GameObject; // появление фаирбола

        anima = Skill.GetComponent<Animator>(); // присвоения компонента АНИМАТОР в переменную
        movVect = Player.GetComponent<Animator>(); // направление игрока

        x = movVect.GetFloat("input_x"); // поззиция Х
        y = movVect.GetFloat("input_y"); // позиция У

        Skill.transform.position = Player.transform.position; // позиция игрока
        if (skill_name == "fireball")
        {
            anima.SetFloat("input_x", x); // присвоение аниматору позиции Х
            anima.SetFloat("input_y", y); // присвоение аниматору позиции У
            Skill.GetComponent<AudioSource>().Play(); // воспроизведение звука 
            anima.SetBool("flying", true); // переход на анимацию полета
        }

        if (skill_name == "lightning")
        {
            if ((x == 1f) && (y == 0f) && (Skill != null)) // при таких значениях молния летит вправо
            {
                Skill.transform.position = Player.transform.position + new Vector3(LightningDistance, 0f, 0f);

            }
            if ((x == -1f) && (y == 0f) && (Skill != null)) // при таких значениях молния летит влево
            {
                Skill.transform.position = Player.transform.position + new Vector3(-LightningDistance, 0f, 0f);
            }
        }

        if (skill_name == "Heal")
        {
            Player.GetComponent<HP>().Heal(heal_amount);
            Destroy(Skill, 2f);
        }

    }

    void Update()
    {
        mobs = GameObject.FindGameObjectWithTag("Mob"); //каждый фрейм ищем объект с тегом Mob
        direction = mobs.transform.position - Player.transform.position; //вычисляем расстояние между ним и нашим игроком
        if (Input.GetKeyDown(KeyCode.Alpha1)) //&& direction.x >= 40f)
        { //если нажата кнопка и расстояние больше 40, запускает скилл
            if (skills[0].currentCoolDown >= skills[0].cooldown)
            {
                skills[0].currentCoolDown = 0;
                SkillLaunch("fireball");

                if ((x == 0f) && (y == 1f) && (Skill != null)) // при таких значениях ФАИРБАЛ летит вверх
                {
                    Skill.GetComponent<Rigidbody2D>().AddForce(Player.transform.up * Speed, ForceMode2D.Impulse);
                }
                if ((x == 0f) && (y == -1f) && (Skill != null)) // при таких значениях ФАИРБАЛ летит вниз
                {
                    Skill.GetComponent<Rigidbody2D>().AddForce(-Player.transform.up * Speed, ForceMode2D.Impulse);
                }
                if ((x == 1f) && (y == 0f) && (Skill != null)) // при таких значениях ФАИРБАЛ летит вправо
                {
                    Skill.GetComponent<Rigidbody2D>().AddForce(Player.transform.right * Speed, ForceMode2D.Impulse);
                }
                if ((x == -1f) && (y == 0f) && (Skill != null)) // при таких значениях ФАИРБАЛ летит влево
                {
                    Skill.GetComponent<Rigidbody2D>().AddForce(-Player.transform.right * Speed, ForceMode2D.Impulse);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (skills[1].currentCoolDown >= skills[1].cooldown)
            {
                skills[1].currentCoolDown = 0;
                SkillLaunch("lightning");
                Destroy(Skill, 1f);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (skills[2].currentCoolDown >= skills[2].cooldown)
            {
                skills[2].currentCoolDown = 0;
                SkillLaunch("Heal");

            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (skills[3].currentCoolDown >= skills[3].cooldown)
            {
                skills[3].currentCoolDown = 0;
                StartCoroutine(Haste());
            }
        }

        //если скилл есть и он ХИЛ, то он следует за нами ВСЕГДА
        if (Skill && Skill.gameObject.tag == "Heal")
        {
            Skill.transform.position = Player.transform.position;
        }
        

        foreach (Skills s in skills)
            if (s.currentCoolDown < s.cooldown)
            {
                s.currentCoolDown += Time.deltaTime;
                s.skillIcon.fillAmount = s.currentCoolDown / s.cooldown;
            }
    }

    IEnumerator Haste()
    {
        Player.GetComponent<PlayerMovement>().moveSpeed += 0.5f;
        yield return new WaitForSeconds(2.5f);
        Player.GetComponent<PlayerMovement>().moveSpeed -= 0.5f;
    }
}
[System.Serializable]
public class Skills
{
    public float cooldown;
    public Image skillIcon;
    [HideInInspector]
    public float currentCoolDown;
}

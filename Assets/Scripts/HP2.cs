using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HP2 : MonoBehaviour {

    public float max_Health = 100f;
    public float cur_Health = 0;
    public GameObject HealthBar;

    // Use this for initialization
    void Start ()
    {
        cur_Health = max_Health;
	}

    public void SetHealthBar(float myHealth)
    {
        //задаёт масштаб хелсбару, от 0 до 1
        HealthBar.transform.localScale = new Vector3(myHealth, HealthBar.transform.localScale.y, HealthBar.transform.localScale.z);
    }

    public void Damage(float dmg)
    {

        if (cur_Health > max_Health)
        {
            cur_Health = max_Health;
        }

        cur_Health -= dmg;

        float calc_Health = cur_Health / max_Health;
        SetHealthBar(calc_Health);

        if (cur_Health <= 0)
        {
            Die();
        }

    }

    public void Heal(float heal_amount)
    {

        if (cur_Health + heal_amount > max_Health)
        {
            cur_Health = max_Health;
        } else
        {
            cur_Health += heal_amount;
        }
        float calc_Health = cur_Health / max_Health;
        SetHealthBar(calc_Health);

    }

    void Die()
    {
        //Restart
		SceneManager.LoadScene("UMenuGallery");
    }
}

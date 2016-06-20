using UnityEngine;
using System.Collections;

public class SkeletonHealth : MonoBehaviour
{

    public float max_Health; //  здоровье
    public float cur_Health = 0f;
    public GameObject HealthBar;

    void Start()
    {
        cur_Health = max_Health; //текущее здоровье
    }

    void Update()
    {
        gameObject.GetComponent<ExpCol>().health_mob = cur_Health;  // запись в переменную значения из скрипта СКЕЛЕТОНХЕАЛТХ
        float calc_Health = cur_Health / max_Health; //if cur 80 / 100 - 0.8f
        SetHealthBar(calc_Health); //функция с текущим значение здоровья
    }

    public void SetHealthBar(float myHealth) // функция передачи здоровья хилбару
    {
        HealthBar.transform.localScale = new Vector3(Mathf.Clamp(myHealth, 0f, 1f), HealthBar.transform.localScale.y, HealthBar.transform.localScale.z);
    }
}
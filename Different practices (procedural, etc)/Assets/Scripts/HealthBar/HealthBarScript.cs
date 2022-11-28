using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    [SerializeField]
    Image healthBar;
    [SerializeField]
    float speed;

    float targetHealthBarAmount = 1;

    public void UpdateHealthBar(float maxHealth, float currHealth)
    {
        targetHealthBarAmount = (currHealth / maxHealth);
       
    }

    // Update is called once per frame
    void Update()
    {
        //   transform.rotation = Quaternion.LookRotation(transform.position - Helpers.Camera.transform.position);
        transform.LookAt(Helpers.Camera.transform);
        healthBar.fillAmount = Mathf.MoveTowards(healthBar.fillAmount, targetHealthBarAmount, Time.deltaTime * speed);
    }
}

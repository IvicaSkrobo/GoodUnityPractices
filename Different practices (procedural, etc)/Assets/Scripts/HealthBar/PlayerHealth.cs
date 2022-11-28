using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    HealthBarScript healthBar;

    [SerializeField]
    float maxHealth = 3;
    float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        currentHealth--;
        if(currentHealth<=0)
        {
            Debug.Log("Dead");
            //death effect
        }
        else
        {
            //hit effect
        }
        healthBar.UpdateHealthBar(maxHealth, currentHealth);
    }

    

}

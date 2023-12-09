using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController Instance;
    public int maxHealth, currentHealth;
    private float invincCounter;
    public float invincibleLength;

    void Awake()
    {
        Instance = this;

    }

    private void Start()
    {
        currentHealth = maxHealth;
        UIController.Instance.healthSlider.maxValue = maxHealth;
        UIController.Instance.healthSlider.value = currentHealth;
        UIController.Instance.healthText.text = "HP: " + currentHealth + "/" + maxHealth;
    }

    private void Update()
    {
        if(invincCounter > 0)
        {
            invincCounter -= Time.deltaTime;
        }
    }

    public void DamagePlayer(int damageAmount)
    {
        if (invincCounter <= 0) 
        {
            AudioManager.Instance.PlaySFX(0);
            currentHealth -= damageAmount;
            UIController.Instance.Showdamage();

           if (currentHealth <= 0) 
           {
               gameObject.SetActive(false);

                currentHealth = 0;

                GameManager.Instance.PlayerDied();
                AudioManager.Instance.StopBGM();
                AudioManager.Instance.PlaySFX(1);
                AudioManager.Instance.StopSFX(0);

            }
            invincCounter = invincibleLength;

            UIController.Instance.healthSlider.value = currentHealth;
            UIController.Instance.healthText.text = "HP: " + currentHealth + "/" + maxHealth;
        }
    }

    public void healPlayer(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth) 
        {
            currentHealth = maxHealth;
        }

        UIController.Instance.healthSlider.value = currentHealth;
        UIController.Instance.healthText.text = "HP: " + currentHealth + "/" + maxHealth;

    }    
}

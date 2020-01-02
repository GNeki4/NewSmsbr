using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    bool isDead;

    public Image healthImage;

    void Start()
    {
        currentHealth = maxHealth;
        isDead = false;
    }

    void Update()
    {
        healthImage.fillAmount = currentHealth / maxHealth;

        if (isDead)
        {
            //TODO: Заставка на конец игры
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        if (currentHealth <= 0f)
        {
            isDead = true;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] float initialHealth;
    [SerializeField] float maxHealth;
    [SerializeField] float initialArmor;
    [SerializeField] float maxArmor;
    [SerializeField] UnityEvent<GameObject, string> objectIsDead;
    [SerializeField] UnityEvent<float, float> updateHealth;
    [SerializeField] UnityEvent<float, float> updateArmor;
    float currentHealth;
    float currentArmor;
    [SerializeField] UnityEvent dmgFeedback;

    private void Awake()
    {
        currentHealth = initialHealth;
        currentArmor = initialArmor;
        updateHealth.Invoke(currentHealth, maxHealth);
        updateArmor.Invoke(currentArmor, maxArmor);
    }
    public void TakeDamage(float damage)
    {
        

        if (currentArmor > 0)
        {
            currentArmor -= 0.75f * damage;
            currentHealth -= 0.25f * damage;
        }

        else
        {
            currentHealth -= damage;
        }
        if (gameObject.tag == "Player")
        {
            dmgFeedback.Invoke();
        }
        if(currentArmor <0.0f)
        {
            currentArmor = 0.0f;
        }
        if (currentHealth <= 0.0f)
        {
            die();
        }
        updateHealth.Invoke(currentHealth, initialHealth);
        updateArmor.Invoke(currentArmor, maxArmor);
    }

    public void die()
    {
        objectIsDead.Invoke(gameObject, gameObject.name);
        if (gameObject.CompareTag("Target")) {
            foreach (Transform child in transform) {
                if (child.CompareTag("Decal"))
                {
                    child.transform.parent = null;
                    child.transform.gameObject.SetActive(false);
                }
            }
        }
        if (gameObject.TryGetComponent(out DropItem item))
            item.dropItem();
        Destroy(gameObject);
    }

    public void addHealth(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        updateHealth.Invoke(currentHealth, maxHealth);
    }

    public void addArmor(float amount)
    {
        currentArmor += amount;
        if (currentArmor > maxArmor)
        {
            currentArmor = maxArmor;
        }

        updateArmor.Invoke(currentArmor, maxArmor);
    }

    public bool checkCurrentHealth()
    {
        return currentHealth < maxHealth;
    }

    public bool checkCurrentArmor()
    {
        return currentArmor < maxArmor;
    }

    public float getCurrentHealth()
    {
        return currentHealth;
    }

    public void getDestroyed()
    {
        Destroy(gameObject);
    }
}

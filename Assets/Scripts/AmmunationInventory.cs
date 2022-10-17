using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AmmunationInventory : MonoBehaviour
{
    [SerializeField] float maxBulletCharger;
    [SerializeField] float maxTotalBullet;
    [SerializeField] float reserveBullets;
    [SerializeField] UnityEvent<float, float> updateAmmo;
    float currentAmmo;

    private void Awake()
    {
        currentAmmo = maxBulletCharger;
        updateAmmo.Invoke(currentAmmo, reserveBullets);
    }

    public void reload()
    {
        float howMany = maxBulletCharger - currentAmmo;
        if (reserveBullets <= howMany)
        {
            currentAmmo += reserveBullets;
            reserveBullets = 0;
        }
        else
        {
            reserveBullets -= howMany;
            currentAmmo += howMany;
        }
        updateAmmo.Invoke(currentAmmo, reserveBullets);
    }

    public float getCurrentAmo()
    {
        return currentAmmo;
    }

    public float getmaxBulletCharger()
    {
        return maxBulletCharger;
    }

    public float getReserveBullets()
    {
        return reserveBullets;
    }

    public void shoot()
    {
        if (currentAmmo > 0)
        {
            currentAmmo -= 1;
        }

        updateAmmo.Invoke(currentAmmo, reserveBullets);
    }

    public void addAmmo(float amount)
    {
        reserveBullets += amount;
        if (reserveBullets > maxTotalBullet)
        {
            reserveBullets = maxTotalBullet;
        }
        updateAmmo.Invoke(currentAmmo, reserveBullets);
    }

    public bool checkCurrentAmmo()
    {
        return reserveBullets < maxTotalBullet;
    }

}

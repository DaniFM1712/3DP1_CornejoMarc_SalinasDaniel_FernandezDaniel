using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalShooting : MonoBehaviour
{
    // Este script no se usará en la práctica
    //Es solo para saber como se usa físicas en bala. Pero esto consume mucho

    [SerializeField] GameObject bullet;
    [SerializeField] Transform weaponDummy;
    [SerializeField] float bulletSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bulletInstance = Instantiate(bullet, weaponDummy.position, weaponDummy.rotation);
        bulletInstance.GetComponent<Rigidbody>().velocity = weaponDummy.forward * bulletSpeed;
    }
}

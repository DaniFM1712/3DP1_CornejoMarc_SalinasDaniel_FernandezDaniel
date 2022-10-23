using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastShooting : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float maxShootDist;
    [SerializeField] LayerMask shootingMask;
    [SerializeField] Transform weaponDummy;
    [SerializeField] GameObject decalParticles;
    [SerializeField] float zOffset;
    [SerializeField] GameObject decalImage;
    [SerializeField] float damage;
    [SerializeField] Animation weaponAnimation;
    [SerializeField] ObjectPool decalPool;
    [SerializeField] AmmunationInventory amoInventory;
    [SerializeField] KeyCode reloadKey = KeyCode.R;
    bool aiming = false;
    [SerializeField] float timeToShoot;
    float lastTimeShooted = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            shoot();
        }
        if (Input.GetKeyDown(reloadKey))
        {
            reload();
        }
        if (Input.GetMouseButtonUp(1))
        {
            if (!aiming)
            {
                animateAim();
            }
            else
            {
                animateIdle();
            }
            aiming = !aiming;
        }
    }

    private void shoot()
    {
        if (canShoot())
        {
            lastTimeShooted = Time.time;
            amoInventory.shoot();
            animateShoot();
            Ray r = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
            RaycastHit hitInfo;
            if (Physics.Raycast(r, out hitInfo, maxShootDist, shootingMask))
            {
                Debug.Log("Shoot to: " + hitInfo.collider.gameObject.name);
                //Debug.DrawRay(r.origin, r.direction);

                GameObject decal = decalPool.enableObject(hitInfo.point + hitInfo.normal * zOffset, Quaternion.LookRotation(hitInfo.normal));

                decal.transform.parent = hitInfo.collider.gameObject.transform;
                //Instantiate(decalImage, hitInfo.point +hitInfo.normal* zOffset, Quaternion.LookRotation(hitInfo.normal));
                //ya no instanciamos porque lo hace la clase ObjectPool

                if (hitInfo.collider.gameObject.TryGetComponent<HitColider>(out HitColider health))
                {
                    health.TakeDamage(damage);
                }
                if (hitInfo.collider.gameObject.TryGetComponent<TargetController>(out TargetController target))
                {
                    target.addPoints();
                }
            }
            //Instantiate(decalParticles, weaponDummy.position, weaponDummy.rotation);
            //ESTO ES PARA PONER PARTICULAS EN EL CAÑÓN DEL ARMA
        }
        else if (amoInventory.getCurrentAmo() == 0)
        {
            animateCantShoot();
        }

    }

    void animateShoot()
    {
        
        weaponAnimation.CrossFade("Shoot",0.1f); //hace una interpolación entre la animación que esté haciendo en ese momento y la que pongamos entre las comillas
                                            //es para hacer una transición más suave entre las animaciones
        weaponAnimation.CrossFadeQueued("Idle"); //este método es para que haga esta animación cuando haya acabado la animación que esté haciendo en ese momento
        if (aiming)
        {
            cam.fieldOfView = 60;
            aiming = !aiming;
        }
    }

    void animateCantShoot()
    {
        weaponAnimation.CrossFade("CantShoot", 0.1f); 
        weaponAnimation.CrossFadeQueued("Idle");
    }

    bool canShoot()
    {
        return amoInventory.getCurrentAmo() > 0 && lastTimeShooted+timeToShoot < Time.time;
    }

    void reload()
    {
        if (amoInventory.getCurrentAmo() != amoInventory.getmaxBulletCharger() && amoInventory.getReserveBullets() > 0)
        {
            amoInventory.reload();
            animateReload();
        }
    }

    void animateReload()
    {
        weaponAnimation.CrossFade("Reload");
        weaponAnimation.CrossFadeQueued("Idle");
    }

    void animateAim()
    {
        weaponAnimation.CrossFade("Aim",0.1f);
        cam.fieldOfView = 30;
    }

    void animateIdle()
    {
        weaponAnimation.CrossFade("NotAim");
        weaponAnimation.CrossFadeQueued("Idle");
        cam.fieldOfView = 60;
    }
}

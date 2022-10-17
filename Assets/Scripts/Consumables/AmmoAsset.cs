using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "items/ammo")]

public class AmmoAsset : ConsumableAsset
{
    [SerializeField] float ammoToAdd;
    override public bool consume(GameObject consumer)
    {
        AmmunationInventory consumerChild = consumer.GetComponentInChildren<AmmunationInventory>();
        if (consumerChild != null)
        {
            if (consumerChild.checkCurrentAmmo())
            {
                Debug.Log("Recharged " + ammoToAdd);
                consumerChild.addAmmo(ammoToAdd);
                return true;
            }
        }
        return false;
    }

}

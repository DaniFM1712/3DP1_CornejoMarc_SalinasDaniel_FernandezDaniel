using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "items/armor")]

public class ArmorAsset : ConsumableAsset
{
    [SerializeField] float armorToAdd;
    override public bool consume(GameObject consumer)
    {
        if (consumer.TryGetComponent(out HealthSystem armor))
        {
            if (armor.checkCurrentArmor())
            {
                Debug.Log("Armored " + armorToAdd);
                armor.addArmor(armorToAdd);
                return true;
            }
        }
        return false;
    }




}

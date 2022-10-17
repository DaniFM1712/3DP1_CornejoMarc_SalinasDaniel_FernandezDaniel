using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="items/health")]
public class HealthAsset : ConsumableAsset
{
    [SerializeField] float healthToAdd;
    override public bool consume(GameObject consumer)
    {
        if (consumer.TryGetComponent(out HealthSystem health))
        {
            if (health.checkCurrentHealth())
            {
                Debug.Log("Healed " + healthToAdd);
                health.addHealth(healthToAdd);
                return true;
            }
        }
        return false;
    }

}

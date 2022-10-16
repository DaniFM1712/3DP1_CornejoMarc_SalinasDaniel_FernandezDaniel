using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="items/health")]
public class HealthAsset : ConsumableAsset
{
    [SerializeField] float healthToAdd;
    override public void consume(GameObject consumer)
    {
        if (consumer.TryGetComponent(out HealthSystem health))
        {
            Debug.Log("Healed " + healthToAdd);
            health.addHealth(healthToAdd);
        }
    }

}

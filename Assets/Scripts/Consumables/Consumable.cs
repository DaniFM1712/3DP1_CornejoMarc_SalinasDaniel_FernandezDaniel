using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    [SerializeField] ConsumableAsset consumableAsset;
    public void consume(GameObject consumer)
    {

        if (consumableAsset.consume(consumer))
        {
            Debug.Log("El consumer " + consumer.name + "m'ha consumit!");
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    [SerializeField] float heal;
    [SerializeField] float ammo;
    [SerializeField] ConsumableAsset consumableAsset;
    public void consume(GameObject consumer)
    {
        Debug.Log("El consumer " + consumer.name + "m'ha consumit!");
        consumableAsset.consume(consumer);
        Destroy(gameObject);
    }
}

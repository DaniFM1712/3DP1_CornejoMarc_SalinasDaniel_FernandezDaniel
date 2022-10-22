using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] GameObject drop;
    public void dropItem()
    {
        Instantiate(drop, transform.position, gameObject.transform.rotation);
    }
}

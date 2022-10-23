using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyController : MonoBehaviour
{
    [SerializeField] private GameObject[] doors;

    private void Start()
    {
        doors = GameObject.FindGameObjectsWithTag("door");
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        foreach (GameObject door in doors)
        {
            door.GetComponent<CheckDistanceToPlayer>().enabled = true;
        }
    }
}

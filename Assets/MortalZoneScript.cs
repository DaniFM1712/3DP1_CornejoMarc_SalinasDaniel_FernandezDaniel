using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortalZoneScript : MonoBehaviour
{
    [SerializeField] Transform spawnPosition;
    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("ENTRA");
            other.gameObject.transform.position = new Vector3(spawnPosition.position.x, spawnPosition.position.y, spawnPosition.position.z);
        }
    }
}

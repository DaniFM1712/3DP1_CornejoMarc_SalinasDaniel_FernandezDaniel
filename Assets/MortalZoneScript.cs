using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MortalZoneScript : MonoBehaviour
{
    [SerializeField] Transform spawnPosition;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("ENTRA");
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(1);
        }
    }
}

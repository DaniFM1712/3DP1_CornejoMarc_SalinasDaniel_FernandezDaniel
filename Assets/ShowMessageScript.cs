using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMessageScript : MonoBehaviour
{

    [SerializeField] Text text;
    [SerializeField] string message;
    // Start is called before the first frame update
    void Awake()
    {
        text.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        text.enabled = true;
        text.text = message;
    }


    private void OnTriggerExit(Collider other)
    {
        text.enabled = false;
    }
}

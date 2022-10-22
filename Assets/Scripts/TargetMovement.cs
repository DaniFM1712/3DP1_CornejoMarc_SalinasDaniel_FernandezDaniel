using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{

    [SerializeField] private List<GameObject> destinations;
    private bool firstMove;
    [SerializeField] private float speed; 
    private void Update()
    {
        if (transform.position == destinations[0].transform.position)
        {
            firstMove = false;
        }
        if (transform.position == destinations[1].transform.position)
        {
            firstMove = true;
        }
        
        if (firstMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, destinations[0].transform.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, destinations[1].transform.position, speed * Time.deltaTime);
        }
        
    }
}

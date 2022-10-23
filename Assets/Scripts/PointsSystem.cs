using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PointsSystem : MonoBehaviour
{
    [SerializeField] UnityEvent<float, float> updatePoints;
    float currentPoints;
    [SerializeField] float pointsToPass;
    [SerializeField] GameObject door;
    [SerializeField] float minDistanceToPlayer;

    private void Awake()
    {
        updatePoints.Invoke(currentPoints, 0f);
    }

    public void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.E))
        {
            //addPoints(10f);
        }*/
        if (PointsNeeded())
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        if(door.TryGetComponent<Animator>(out Animator doorAnimator))
        {
            if ((door.transform.position - transform.position).magnitude < minDistanceToPlayer)
            {
                doorAnimator.SetBool("character_nearby", true);
            }
            else doorAnimator.SetBool("character_nearby", false);
            
        }
    }

    private bool PointsNeeded()
    {
        return currentPoints >= pointsToPass;
    }

    public void addPoints(float amount)
    {
        currentPoints += amount;
        updatePoints.Invoke(currentPoints, 0f);
    }
}

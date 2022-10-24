using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PointsSystem : MonoBehaviour
{
    [SerializeField] UnityEvent<float, float> updatePoints;
    [SerializeField] UnityEvent<float, float> resetGallery;
    float currentPoints;
    [SerializeField] float pointsToPass;
    [SerializeField] GameObject door;
    [SerializeField] float minDistanceToPlayer;
    [SerializeField] KeyCode galleryReset = KeyCode.RightControl;

    private void Awake()
    {
        updatePoints.Invoke(currentPoints, 0f);
    }

    public void Update()
    {
        if (Input.GetKeyDown(galleryReset))
        {
            resetGallery.Invoke(-currentPoints,0f);
            currentPoints = 0;
            
        }
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

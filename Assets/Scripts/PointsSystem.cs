using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PointsSystem : MonoBehaviour
{
    [SerializeField] UnityEvent<float, float> updatePoints;
    float currentPoints;

    private void Awake()
    {
        updatePoints.Invoke(currentPoints, 0f);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            addPoints(10f);
        }
    }

    public void addPoints(float amount)
    {
        currentPoints += amount;
        updatePoints.Invoke(currentPoints, 0f);
    }
}

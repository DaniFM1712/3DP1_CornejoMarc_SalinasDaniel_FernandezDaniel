using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargetController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float pointsToAdd;
    [SerializeField] UnityEvent<float> updatePoints;

    public void addPoints()
    {
        updatePoints.Invoke(pointsToAdd);
    }
}

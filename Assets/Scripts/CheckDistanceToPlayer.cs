using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDistanceToPlayer : MonoBehaviour
{
    [SerializeField] private float minDistance;
    [SerializeField] private GameObject player;
    [SerializeField] private Animator animator;

    // Update is called once per frame
    void Update()
    {
        if (inDistance())
        {
            Debug.Log("ESTOY MÁS CERCA");
            animator.SetBool("character_nearby",true);
        }
        else animator.SetBool("character_nearby", false);

    }

    private bool inDistance()
    {
        return Vector3.Distance(transform.position, player.transform.position) < minDistance; 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HitColider : MonoBehaviour
{
    [SerializeField] HealthSystem hs;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] float damageMultiplier;
    [SerializeField] float speedMultiplier;

    public void TakeDamage(float damage)
    {
        hs.TakeDamage(damage * damageMultiplier);
        agent.speed *= speedMultiplier;
    }
}

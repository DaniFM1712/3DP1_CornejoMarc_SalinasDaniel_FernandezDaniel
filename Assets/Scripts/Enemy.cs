using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    
    NavMeshAgent agent;
    [SerializeField] LayerMask obstacleMask;
    Vector3 distanceToPlayer;
    [SerializeField] UnityEvent<GameObject> objectIsDead;



    enum State { IDLE, PATROL, ALERT, CHASE, ATTACK, HIT, DIE}
    [SerializeField] State currentState;

    [SerializeField] GameObject player;
    [SerializeField] float hearDistance;

    [Header("IDLE")]
    [SerializeField] float idleTime;
    float idleStarted = 0.0f;
    State lastState;
    
    [Header("PATROL")]
    [SerializeField] List<Transform> patrolTargets;
    [SerializeField] float patrolMinDistance;
    [SerializeField] int patrolRoundsToIdle;
    [SerializeField] float patrolSpeed;
    [SerializeField] float patrolAcceleration;
    int currentPatrolTarget = 0;

    [Header("ALERT")]
    [SerializeField] float alertSpeedRotation;
    float totalRotated = 0.0f;

    [Header("CHASE")]
    [SerializeField] float CHASE_MAX;



    [Header("ATTACK")]
    [SerializeField] float damage;
    [SerializeField] LayerMask shootingMask;
    [SerializeField] float timeToShoot;
    [SerializeField] float SHOOT_MAX;
    float lastTimeShooted;


    [Header("HIT")]
    float lastCheckedHealth;

    [Header("DIE")]
    [SerializeField] float fadeSpeed;
    [SerializeField] MeshRenderer bodyRenderer;
    [SerializeField] MeshRenderer backRenderer;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        lastCheckedHealth = GetComponent<HealthSystem>().getCurrentHealth();
        currentState = State.IDLE;
    }

    void Update()
    {
        distanceToPlayer = player.transform.position - transform.position;
        switch (currentState)
        {
            case State.ATTACK:
                lastState = State.ATTACK;
                updateAttack();
                ChangeFromAttack();
                break;
            case State.IDLE:
                lastState = State.IDLE;
                updateIdle();
                ChangeFromIdle();
                break;
            case State.PATROL:
                lastState = State.PATROL;
                updatePatrol();
                ChangeFromPatrol();
                break;
            case State.ALERT:
                lastState = State.ALERT;
                updateAlert();
                ChangeFromAlert();
                break;
            case State.CHASE:
                lastState = State.CHASE;
                updateChase();
                ChangeFromChase();
                break;
            case State.HIT:
                updateHit();
                ChangeFromHit();
                break;
            case State.DIE:
                updateDie();
                break;
        }
    }


    void updateIdle()
    {
        //Do nothing
    }

    void ChangeFromIdle()
    {

        if (hearsPlayer())
        {
            currentState = State.ALERT;
        }
        if (seesPlayer() && !PlayerInRange())
        {
            currentState = State.CHASE;
        }
        isHit();
        if (Time.time >= idleStarted + idleTime)
        {
            currentState = State.PATROL;
            currentPatrolTarget = 0;
        }
    }

    private void updatePatrol()
    {
        if(agent.isStopped) agent.isStopped = false;
        agent.speed = patrolSpeed;
        agent.acceleration = patrolAcceleration;
        if (agent.hasPath && agent.remainingDistance <= patrolMinDistance)
            currentPatrolTarget++;

        agent.SetDestination(patrolTargets[currentPatrolTarget % patrolTargets.Count].position);

    }

    private void ChangeFromPatrol()
    {
        if (currentPatrolTarget > patrolRoundsToIdle * patrolTargets.Count)
        {
            currentState = State.IDLE;
            idleStarted = Time.time;
        }

        if (hearsPlayer())
        {
            currentState = State.ALERT;
            totalRotated = 0.0f;
        }
        isHit();
    }

    void updateAlert()
    {
        agent.isStopped = true;
        float frameRotation = alertSpeedRotation * Time.deltaTime;
        transform.Rotate(new Vector3(0.0f, frameRotation, 0.0f));
        totalRotated += frameRotation;
    }

    void ChangeFromAlert()
    {
        
        if (seesPlayer() && PlayerInRange())
        {
            currentState = State.ATTACK;
        }
        if (seesPlayer() && !PlayerInRange())
        {
            currentState = State.CHASE;
        }
        if (!hearsPlayer() || totalRotated >= 360.0f)
        {
            currentState = State.IDLE;
            idleStarted = Time.time;
        }
        isHit();
    }

    bool PlayerInRange()
    {
        return (distanceToPlayer).magnitude < SHOOT_MAX;
    }

    bool seesPlayer()
    {
        float playerDistance = (player.transform.position - transform.position).magnitude;

        if (Vector3.Angle(transform.forward, distanceToPlayer.normalized) <= 15){
            Ray r = new Ray(transform.position, distanceToPlayer.normalized);
            if (Physics.Raycast(r, out RaycastHit hitInfo, playerDistance, obstacleMask))
            {
                return false;
            }
            return true;
        }

        return false;

    }
    bool hearsPlayer()
    {
        return (transform.position - player.transform.position).magnitude < hearDistance;
    }



    void updateChase()
    {
        if (agent.isStopped) agent.isStopped = !agent.isStopped;
        agent.SetDestination(player.transform.position);
    }

    void ChangeFromChase()
    {
        if (seesPlayer() && PlayerInRange())
        {
            currentState = State.ATTACK;
        }
        if(distanceToPlayer.magnitude > CHASE_MAX)
        {
            currentState = State.PATROL;
        }
        isHit();
    }


    void updateAttack()
    {
  
        if (!agent.isStopped) agent.isStopped = !agent.isStopped;
        Debug.Log(seesPlayer());
        if ((lastTimeShooted + timeToShoot < Time.time) && seesPlayer() && Physics.Raycast(transform.position, distanceToPlayer.normalized, out RaycastHit hitInfo, SHOOT_MAX, shootingMask))
        {
            if (hitInfo.collider.gameObject.TryGetComponent<HealthSystem>(out HealthSystem health) && lastTimeShooted + timeToShoot < Time.time)
            {
                lastTimeShooted = Time.time;
                health.TakeDamage(damage);
            }
        }
    }

    void ChangeFromAttack()
    {
        if (seesPlayer() && !PlayerInRange())
        {
            currentState = State.CHASE;
        }
        if (!seesPlayer())
        {
            currentState = State.ALERT;
        }
        isHit();
    }

    void updateHit()
    {
        lastCheckedHealth = GetComponent<HealthSystem>().getCurrentHealth();
        agent.isStopped = true;
    }

    void ChangeFromHit()
    {
        if(lastCheckedHealth <= 0)
        {
            currentState = State.DIE;
        }
        else if(lastState == State.IDLE || lastState == State.PATROL || lastState == State.ALERT)
        {
            currentState = State.ALERT;
        }
        else 
        {
            currentState = lastState;
        }
    }

    void updateDie()
    {
        objectIsDead.Invoke(gameObject);
        //StartCoroutine(FadeOutObject());
    }

    /* NO FUNCIONA PER ALGUN MOTIU
    public IEnumerator FadeOutObject()
    {
        Debug.Log("FADEOUT");
        float alpha = 1f;
        while (alpha > 0.0)
        {
            alpha -= Time.deltaTime / fadeSpeed;
            bodyRenderer.material.color = new Color(bodyRenderer.material.color.r, bodyRenderer.material.color.g, bodyRenderer.material.color.b, alpha);
            backRenderer.material.color = new Color(backRenderer.material.color.r, backRenderer.material.color.g, backRenderer.material.color.b, alpha);

            yield return new WaitForEndOfFrame();
        }
    }
    */

    private void isHit()
    {
        if (lastCheckedHealth > GetComponent<HealthSystem>().getCurrentHealth())
        {
            if (GetComponent<HealthSystem>().getCurrentHealth() <= 0)
            {
                currentState = State.DIE;
            }
            else currentState = State.HIT;
        }
    }
}

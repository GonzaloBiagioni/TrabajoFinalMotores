using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;
    private bool chasing;
    public float distanceToChase = 10f; 
    public float distanceToLose = 15f;
    public float distanceToStop;

    private Vector3 targetPoint, startPoint;

    public NavMeshAgent agent;

    public float keepChasingTime = 5f;
    private float chaseCounter;

    public GameObject bullet;
    public Transform firePoint;

    public float fireRate, waitBetweenShots = 2f, timeToShoot = 1f;
    private float fireCount, shotWaitCounter, shootTimeCounter;

    public Animator anim;
    private bool wasShot;

    void Start()
    {
        startPoint = transform.position;

        shootTimeCounter = timeToShoot;
        shotWaitCounter = waitBetweenShots;
    }

    
    void Update()
    {
        targetPoint = PlayerController.Instance.transform.position;
        targetPoint.y = transform.position.y;

        if (!chasing)
        {
            if(Vector3.Distance(transform.position,targetPoint) < distanceToChase) 
            {
                chasing = true;

                shootTimeCounter = timeToShoot;
                shotWaitCounter = waitBetweenShots;
                AudioManager.Instance.PlaySFX(9);
            }


            if (chaseCounter > 0)
            {
                chaseCounter -= Time.deltaTime;

                if (chaseCounter <= 0)
                {
                    agent.destination = startPoint;
                    AudioManager.Instance.PlaySFX(10);
                }

            }

            if(agent.remainingDistance < .25f)
            {
                anim.SetBool("isMoving", false);
            }
            else
            {
                anim.SetBool("isMoving", true);
                
            }
        }
        else
        {
            agent.destination = targetPoint;

            if (Vector3.Distance(transform.position,targetPoint) > distanceToLose)
            {
                if(!wasShot)
                {
                    chasing = false; 
                }
                chaseCounter = keepChasingTime;                
            }
            else
            {
                wasShot = false;
            }

            if(shotWaitCounter > 0)
            {
                shotWaitCounter -= Time.deltaTime;

                if (shotWaitCounter <= 0)
                {
                    shootTimeCounter = timeToShoot;
                }

                anim.SetBool("isMoving", true);

            }
            else
            {
                if (PlayerController.Instance.gameObject.activeInHierarchy)
                {
                    shootTimeCounter -= Time.deltaTime;

                    if (shootTimeCounter > 0)
                    {
                        fireCount -= Time.deltaTime;

                        if (fireCount <= 0)
                        {
                            fireCount = fireRate;

                            firePoint.LookAt(PlayerController.Instance.transform.position + new Vector3(0f, 0.5f, 0f));

                            // chequeo del angulo del jugador
                            Vector3 targetDir = PlayerController.Instance.transform.position - transform.position;
                            float angle = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up);

                            if (Mathf.Abs(angle) < 30f)
                            {
                                Instantiate(bullet, firePoint.position, firePoint.rotation);
                                anim.SetTrigger("fireShot");
                            }
                            else
                            {
                                shotWaitCounter = waitBetweenShots;
                            }
                        }
                        agent.destination = transform.position;
                    }
                    else
                    {
                        shotWaitCounter = waitBetweenShots;
                    }
                }

                anim.SetBool("isMoving", false);
                
            }

        }

    }
    public void GetShot()
    {
        wasShot = true;
        chasing = true;
        
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Animator animator;
    GameObject player;
    PlayerController playerController;
    UnityEngine.AI.NavMeshAgent agent;
    public Rigidbody bullet;
    public Transform shootingPoint;
    bool shootOn = true;
    int enemyLive = 3;
    //float prevSpeed;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //prevSpeed = animator.speed;

        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 5) {
            agent.SetDestination(transform.position);

            // Rotate the enemy towards the player
            transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position, transform.up);

            // Shoot bullet
            if (shootOn) {
                shootOn = false;
                Rigidbody p = Instantiate(bullet, shootingPoint.position, shootingPoint.rotation);
                p.velocity = transform.forward * 20;

                Invoke("shootAgain", 3.0f);
            }

            //animator.speed = 0; // pause animation
        } else {
            agent.SetDestination(player.transform.position);

            //animator.speed = prevSpeed; // continue animation
        }

        if (enemyLive == 0) {
            playerController.killCount++;
            Destroy(gameObject);
        }
    }

    void shootAgain() {
        shootOn = true;
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "PlayerBullet") {
            enemyLive--;
        }
    }
}

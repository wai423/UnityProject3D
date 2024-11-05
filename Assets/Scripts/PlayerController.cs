using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    public Slider healthBar;
    int speed = 4, bulletSpeed = 30;
    public int killCount = 0, powerUp = 0;
    public Rigidbody bullet;
    public Transform shootingPoint;
    public GameObject enemy;
    public TextMeshProUGUI killCountText, objective;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        healthBar.maxValue = 100;
        healthBar.value = 100;

        spawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {   
        // Player rotate
        transform.Rotate(0, Input.GetAxis("Horizontal"), 0);

        // Play move forward/backward
        if (Input.GetAxis("Vertical")!=0) {
            animator.SetBool("runAim", true);

            // if (Input.GetAxis("Vertical")>0) { 
            //     healthBar.value-=0.1f;
            // } else if (Input.GetAxis("Vertical")<0) {
            //     healthBar.value+=0.1f;
            // }
            transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime);

            // Player shoot
            if (Input.GetKeyDown("space")) {
                Rigidbody p = Instantiate(bullet, shootingPoint.position, shootingPoint.rotation);
                p.velocity = transform.forward * bulletSpeed;
            }

        } else {
            animator.SetBool("runAim", false);
        }

        // Kill count
        killCountText.text = "Kill: " + killCount;
        if(killCount == 10){
            SceneManager.LoadScene("FinishScene");
        }

        // PowerUp update
        if (powerUp <= 5 && powerUp != 0) { // Health
            healthBar.value+=3;
            powerUp = 0;
        } else if (powerUp > 5) { // Speed
            speed = 10;
            animator.speed = 10;
            powerUp = 0;
            Invoke("speedToNormal", 5.0f);
        }

         // Check if health is zero
        if (healthBar.value <= 0) {
            SceneManager.LoadScene("Easy"); // Call method to restart the game
        }
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "EnemyBullet") {
            healthBar.value-=3;
        }
    }

    void spawnEnemy() {
        Vector3 enemyLoc = new Vector3(Random.Range(-15.0f, 15.0f),transform.position.y,Random.Range(-15.0f, 15.0f));
        Instantiate(enemy,enemyLoc,transform.rotation);

        Invoke("spawnEnemy", 5.0f);
    }

    void speedToNormal() {
        speed = 4;
        animator.speed = 1;
    }
}

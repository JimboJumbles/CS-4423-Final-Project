using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    [SerializeField] ParticleSystem deathParticleSystem;
    [SerializeField] int maxHealth = 3;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] BoxCollider2D enemyFloorCollider;
    [SerializeField] GameObject edgeDetector;
    [SerializeField] Movement movement;
    [SerializeField] GameObject enemyDeathSound;
    [SerializeField] Animator animator;
    [SerializeField] string animation;
    EdgeDetection edgeDetection;
    GameObject deathPlane;
    BoxCollider2D deathPlaneCollider;
    string direction = "left";
    public int health;

    void Start(){
        health = maxHealth;
        deathPlane = GameObject.FindWithTag("DeathPlane");
        deathPlaneCollider = deathPlane.GetComponent<BoxCollider2D>();
        edgeDetection = edgeDetector.GetComponent<EdgeDetection>();
        animator.Play(animation);
    }

    void FixedUpdate(){
        Vector3 vel = Vector3.zero;

        if (direction == "left") vel.x = -3;
        else vel.x = 3;

        movement.EnemyMoveRB(vel);
    }

    void Update(){
        if (enemyFloorCollider.IsTouching(deathPlaneCollider)) die();

        if (edgeDetection.isAtEdge()) flip();

    }


    void OnTriggerEnter2D(Collider2D collider){
        GameObject otherObject;
        otherObject = collider.gameObject;
        Debug.Log(otherObject.tag);
        if (otherObject.tag == "Ground") flip();
        else if (otherObject.tag == "Player"){
            otherObject.GetComponent<PlayerHealth>().damagePlayer(1);
        }
        else if (otherObject.tag == "Laser"){
            damageEnemy(1, otherObject);
        }
        else if (otherObject.tag == "Explosion"){
            damageEnemy(3, otherObject);
        }
    }

    void damageEnemy(int damageInflicted, GameObject projectileObject){
        health -= damageInflicted;
        Destroy(projectileObject);
        if (health <= 0) die(true);
        GetComponent<AudioSource>().Play();
    }

    void die(bool killedByWeapon = false){
        Instantiate(deathParticleSystem, transform.position, deathParticleSystem.transform.rotation).Play();
        Instantiate(enemyDeathSound);
        if (killedByWeapon) Instantiate(coinPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    void flip(){
        if (direction == "left"){
            transform.eulerAngles = new Vector3(0, 180, 0);
            direction = "right";
        }
        
        else {
            transform.eulerAngles = new Vector3(0, 0, 0);
            direction = "left";
        }
    }
}

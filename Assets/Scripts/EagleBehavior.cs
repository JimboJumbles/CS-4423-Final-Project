using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleBehavior : MonoBehaviour
{

    [SerializeField] ParticleSystem deathParticleSystem;
    [SerializeField] int maxHealth = 3;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] Movement movement;
    [SerializeField] GameObject enemyDeathSound;
    string direction = "left";
    public int health;

    void Start(){
        health = maxHealth;
    }

    void FixedUpdate(){
        Vector3 vel = Vector3.zero;

        if (transform.position.x < -15 || transform.position.x > 72) flip();
        if (direction == "left") vel.x = -8;
        else vel.x = 8;

        movement.EnemyMoveRB(vel);
    }

    void OnTriggerEnter2D(Collider2D collider){
        GameObject otherObject;
        otherObject = collider.gameObject;
        if (otherObject.tag == "Player"){
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
        Instantiate(coinPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
    //

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

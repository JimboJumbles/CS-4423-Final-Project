using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    [SerializeField] ParticleSystem deathParticleSystem;
    [SerializeField] int maxHealth = 3;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] BoxCollider2D enemyFloorCollider;
    GameObject deathPlane;
    BoxCollider2D deathPlaneCollider;
    public int health;

    void Start(){
        health = maxHealth;
        GameObject deathPlane = GameObject.FindWithTag("DeathPlane");
        deathPlaneCollider = deathPlane.GetComponent<BoxCollider2D>();
    }

    void Update(){
        if (enemyFloorCollider.IsTouching(deathPlaneCollider)) die();
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
    }

    void die(bool killedByWeapon = false){
        Instantiate(deathParticleSystem, transform.position, deathParticleSystem.transform.rotation).Play();
        if (killedByWeapon) Instantiate(coinPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}

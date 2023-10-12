using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    [SerializeField] ParticleSystem deathParticleSystem;
    [SerializeField] int maxHealth = 3;
    public int health;

    void Start(){
        health = maxHealth;
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
        if (health <= 0) die();
    }

    void die(){
        Instantiate(deathParticleSystem, transform.position, deathParticleSystem.transform.rotation).Play();
        Destroy(this.gameObject);
    }
}

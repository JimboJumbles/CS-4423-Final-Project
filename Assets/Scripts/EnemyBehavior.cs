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
        GameObject projectileObject;
        projectileObject = collider.gameObject;
        if (collider.tag == "Collider"){
            projectileObject = collider.transform.parent.gameObject;
        }
        if (projectileObject.tag == "Damaging"){
            damageEnemy(1, projectileObject);
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

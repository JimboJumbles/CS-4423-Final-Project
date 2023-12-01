using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonBehavior : MonoBehaviour
{

    [SerializeField] ParticleSystem deathParticleSystem;
    [SerializeField] int maxHealth = 3;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject enemyDeathSound;
    float centerPosition;
    string direction = "up";
    public int health;

    void Start(){
        health = maxHealth;
        centerPosition = transform.position.y;
    }

    void Update(){
        if (direction == "up"){
                    if (transform.position.y > (centerPosition + 0.3f)) direction = "down";
                    else transform.position += new Vector3(0, 0.005f, 0);
        }
        else if (direction == "down"){
                    if (transform.position.y < (centerPosition - 0.3f)) direction = "up";
                    else transform.position += new Vector3(0, -0.005f, 0);
        }
    }

    void FixedUpdate(){
        Vector3 vel = Vector3.zero;

        if (transform.position.x < -15 || transform.position.x > 72) flip();
        if (direction == "left") vel.x = -8;
        else vel.x = 8;

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

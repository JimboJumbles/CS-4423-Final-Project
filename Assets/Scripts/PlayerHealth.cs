using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    int maxHealth = 5;
    int currentHealth;
    [SerializeField] CompositeCollider2D playerCollider;
    [SerializeField] BoxCollider2D playerFloorCollider;
    [SerializeField] Text healthText;
    BoxCollider2D deathPlaneCollider;
    bool invincible = false;
    bool transparent = false;

    void Awake(){
        currentHealth = maxHealth;
        GameObject deathPlane = GameObject.FindWithTag("DeathPlane");
        deathPlaneCollider = deathPlane.GetComponent<BoxCollider2D>();
        updateHealth();
    }

    void Update(){
        //Fall In Pit
        if (playerFloorCollider.IsTouching(deathPlaneCollider)) die();

    }

    void FixedUpdate(){
        //Blink while invincible
        if (invincible){
            if (transparent){
                gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                transparent = false;
            }
            else{
                gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
                transparent = true;
            }
        }

        if (!invincible && transparent){
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        }
    }

    public void damagePlayer(int damage){
        if (!invincible){
            currentHealth -= damage;
            updateHealth();
            if (currentHealth <= 0) die();
            else turnInvincible();
        }
    }

    void healPlayer(){
        currentHealth++;
        updateHealth();
    }

    void updateHealth(){
        healthText.text = "Health: " + currentHealth;
    }

    void turnInvincible(){

        StartCoroutine(InvincibilityRoutine());

        IEnumerator InvincibilityRoutine(){
            invincible = true;
            Debug.Log("Invincibility Activated");
            yield return new WaitForSeconds(2);
            invincible = false;
            Debug.Log("Invincibility Deactivated");
            yield return null;
        }
    }

    void die(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

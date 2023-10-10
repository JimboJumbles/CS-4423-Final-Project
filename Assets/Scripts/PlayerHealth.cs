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

    void Awake(){
        currentHealth = maxHealth;
        GameObject deathPlane = GameObject.FindWithTag("DeathPlane");
        deathPlaneCollider = deathPlane.GetComponent<BoxCollider2D>();
        updateHealth();
    }

    void Update(){
        //Fall In Pit
        if (playerFloorCollider.IsTouching(deathPlaneCollider)) die();
        
        //Touch Enemy
        if (playerCollider.IsTouchingLayers(7) && !invincible) damagePlayer();

    }

    public void damagePlayer(){
        currentHealth--;
        updateHealth();
        if (currentHealth <= 0) die();
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

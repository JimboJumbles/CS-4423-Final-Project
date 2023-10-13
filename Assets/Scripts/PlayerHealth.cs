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
    [SerializeField] GameObject UIHandler;
    BoxCollider2D deathPlaneCollider;
    PlayerInputHandler inputHandler;
    bool invincible = false;
    bool transparent = false;

    void Start(){
        currentHealth = maxHealth;
        GameObject deathPlane = GameObject.FindWithTag("DeathPlane");
        deathPlaneCollider = deathPlane.GetComponent<BoxCollider2D>();
        UIHandler.GetComponent<UIHandler>().updateHealth(maxHealth);
        inputHandler = gameObject.GetComponent<PlayerInputHandler>();
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
                if (inputHandler.currentWeapon.name == "Laser Gun" || inputHandler.chargingGrenade){
                    inputHandler.currentWeapon.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                }
                transparent = false;
            }
            else{
                gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
                if (inputHandler.currentWeapon.name == "Laser Gun" || inputHandler.chargingGrenade){
                    inputHandler.currentWeapon.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
                }
                
                transparent = true;
            }
        }

        if (!invincible && transparent){
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            if (inputHandler.currentWeapon.name == "Grenade") inputHandler.currentWeapon.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        }
    }

    public void damagePlayer(int damage){
        if (!invincible){
            currentHealth -= damage;
            if (currentHealth <= 0){
                UIHandler.GetComponent<UIHandler>().updateHealth(0);
                die();
            }
            else{
                UIHandler.GetComponent<UIHandler>().updateHealth(currentHealth);
                turnInvincible();
                GetComponent<Movement>().knockback();
            }
        }
    }

    void healPlayer(){
        currentHealth++;
        UIHandler.GetComponent<UIHandler>().updateHealth(currentHealth);
    }

    void turnInvincible(){

        StartCoroutine(InvincibilityRoutine());

        IEnumerator InvincibilityRoutine(){
            invincible = true;
            yield return new WaitForSeconds(2);
            invincible = false;
            yield return null;
        }
    }

    void die(){
        SceneManager.LoadScene("DeathScene");
    }
}

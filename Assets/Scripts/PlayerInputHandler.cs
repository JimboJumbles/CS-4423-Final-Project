using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class PlayerInputHandler : MonoBehaviour
{

    [SerializeField] Movement movement;
    [SerializeField] BoxCollider2D floorCollider;
    [SerializeField] GameObject terrainParent;
    CompositeCollider2D [] terrainColliders;
    [SerializeField] JetpackHandler jetpackHandler;
    [SerializeField] GameObject UIHandler;
    [SerializeField] GameObject laserGunPrefab;
    [SerializeField] GameObject grenadePrefab;
    [SerializeField] float maxGrenadePower = 2f;
    [SerializeField] ParticleSystem jetpackParticleSystem;
    GameObject currentWeapon = null;
    bool canJump = false;
    float power;
    string direction = "right";


    void Awake(){
        Cursor.visible = false;
        terrainColliders = terrainParent.GetComponentsInChildren<CompositeCollider2D>();
        
    }

    void Start(){
        currentWeapon = laserGunPrefab;
    }

    void FixedUpdate(){
        Vector3 vel = Vector3.zero;
        
        //Move Left
        if (Input.GetKey(KeyCode.A)){
            vel.x = -3;
        }

        //Move Right
        if (Input.GetKey(KeyCode.D)){
            vel.x = 3;
            
        }

        //Use Jetpack
        if (Input.GetKey(KeyCode.LeftShift) && !isTouchingFloor()){
            if (jetpackHandler.fuelRemaining()){
                movement.useJetpack(vel.x);
                jetpackHandler.drainFuel();
                UIHandler.GetComponent<UIHandler>().changeFuelGauge(jetpackHandler.getCurrentFuel(), jetpackHandler.getMaxFuel());
            }
        }

        movement.MoveRB(vel);
    }

    void Update()
    {
        if (isTouchingFloor() && jetpackHandler.getCurrentFuel() != jetpackHandler.getMaxFuel()) jetpackHandler.refilling = true;


        //Jump
        if (Input.GetKeyDown(KeyCode.Space)){
            canJump = isTouchingFloor();
            if(canJump){
                movement.Jump();
                jetpackHandler.refilling = false;
                canJump = false;
            }
        }

        //Select Laser Gun
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            currentWeapon = laserGunPrefab;
            laserGunPrefab.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        }

        //Select Grenade
        if (Input.GetKeyDown(KeyCode.Alpha2)){
            currentWeapon = grenadePrefab;
            laserGunPrefab.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        }

        //Use Laser Gun
        if (currentWeapon.name == "Laser Gun" && Input.GetKeyDown(KeyCode.Mouse0)){
            currentWeapon.GetComponent<WeaponHandler>().useWeapon(Camera.main.ScreenToWorldPoint(Input.mousePosition), "Laser Gun");
        }

        //Start charging Grenade
        if (currentWeapon.name == "Grenade" && Input.GetMouseButtonDown(0)){
            power = 0f;
        }

        //Charge Grenade
        if (currentWeapon.name == "Grenade" && Input.GetKey(KeyCode.Mouse0)){
            if (power < maxGrenadePower) power += 0.1f;
            else if (power > maxGrenadePower) power = maxGrenadePower;
        }

        //Release Grenade
        if (currentWeapon.name == "Grenade" && Input.GetMouseButtonUp(0)){
            currentWeapon.GetComponent<WeaponHandler>().useWeapon(Camera.main.ScreenToWorldPoint(Input.mousePosition), "Grenade", power);
            Debug.Log("Released with power " + power);

        }

        //Start Jetpack particle system
        if (Input.GetKeyDown("left shift") && !isTouchingFloor() && jetpackHandler.fuelRemaining()){
            jetpackParticleSystem.Play();
        }

        //Stop Jetpack Particle System
        if (Input.GetKeyUp("left shift") || !jetpackHandler.fuelRemaining()){
            jetpackParticleSystem.Stop();
        }

        //Face Left
        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x && direction == "right"){
            transform.eulerAngles = new Vector3(0, 180, 0);
            direction = "left";
        }

        //Face Right
        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x && direction == "left"){
            transform.eulerAngles = new Vector3(0, 0, 0);
            direction = "right";
        }
    }

    bool isTouchingFloor(){
        foreach (CompositeCollider2D collider in terrainColliders){
            if (floorCollider.IsTouching(collider)){
                return true;
            }
        }
        return false;
    }
}

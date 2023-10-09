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
    public GameObject currentWeapon = null;
    bool canJump = false;


    void Awake(){
        Cursor.visible = false;
        terrainColliders = terrainParent.GetComponentsInChildren<CompositeCollider2D>();
        
    }

    void Start(){
        currentWeapon = GameObject.FindGameObjectsWithTag("Weapon")[0];
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

        //Use Weapon
        if (Input.GetKeyDown(KeyCode.Mouse0)){
            currentWeapon.GetComponent<WeaponHandler>().useWeapon(Camera.main.ScreenToWorldPoint(Input.mousePosition), currentWeapon.name);
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

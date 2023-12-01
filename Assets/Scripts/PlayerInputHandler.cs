using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class PlayerInputHandler : MonoBehaviour
{

    [SerializeField] Movement movement;
    [SerializeField] BoxCollider2D floorCollider;
    [SerializeField] JetpackHandler jetpackHandler;
    [SerializeField] GameObject UIHandler;
    [SerializeField] GameObject laserGunPrefab;
    [SerializeField] GameObject grenadePrefab;
    [SerializeField] float maxGrenadePower = 20f;
    [SerializeField] float grenadeChargeSpeed = 1f;
    [SerializeField] ParticleSystem jetpackParticleSystem;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject pauseHandlerObject;
    [SerializeField] GameObject playerSFXObject;
    [SerializeField] AnimationStateChanger animationStateChanger;
    CompositeCollider2D groundCollider;
    PlayerHealth playerHealth;
    PauseControl pauseHandler;
    PlayerSFXHandler playerSFXHandler;
    public GameObject currentWeapon = null;
    public bool chargingGrenade = false;
    bool canJump = false;
    float power;
    public string direction = "right";


    void Awake(){
        Cursor.visible = false;
        groundCollider = GameObject.FindWithTag("Ground").GetComponent<CompositeCollider2D>();
        playerHealth = gameObject.GetComponent<PlayerHealth>();
        pauseHandler = pauseHandlerObject.GetComponent<PauseControl>();
        playerSFXHandler = playerSFXObject.GetComponent<PlayerSFXHandler>();
    }

    void Start(){
        currentWeapon = laserGunPrefab;
    }

    void FixedUpdate(){
        Vector3 vel = Vector3.zero;
        
        //Move Left
        if (Input.GetKey(KeyCode.A)){
            vel.x = -3;
            if (isTouchingFloor()) animationStateChanger.changeAnimationState("Player_Walk");
        }

        //Move Right
        if (Input.GetKey(KeyCode.D)){
            vel.x = 3;
            if (isTouchingFloor()) animationStateChanger.changeAnimationState("Player_Walk");
        }

        //Use Jetpack
        if (Input.GetKey(KeyCode.LeftShift) && !isTouchingFloor()){
            if (jetpackHandler.fuelRemaining()){
                movement.useJetpack(vel.x);
                jetpackHandler.drainFuel();
                UIHandler.GetComponent<UIHandler>().changeFuelGauge(jetpackHandler.getCurrentFuel(), jetpackHandler.getMaxFuel());
            }
        }

        //Charge Grenade
            if (currentWeapon.name == "Grenade" && Input.GetKey(KeyCode.Mouse0)){
                if (power < maxGrenadePower){
                    power += 0.1f * grenadeChargeSpeed;
                    changeGrenadeChargeAngle(power);
                }
                else if (power > maxGrenadePower){
                    power = maxGrenadePower;
                }
            }

        movement.MoveRB(vel);
    }

    void Update()
    {
       if (!pauseHandler.isPaused){
            if (isTouchingFloor() && jetpackHandler.getCurrentFuel() != jetpackHandler.getMaxFuel()) jetpackHandler.refilling = true;

            if (!isTouchingFloor()) animationStateChanger.changeAnimationState("Player_Jump");

            //Jump
            if (Input.GetKeyDown(KeyCode.Space)){
                canJump = isTouchingFloor();
                if(canJump){
                    playerSFXHandler.playJumpSFX();
                    movement.Jump();
                    jetpackHandler.refilling = false;
                    canJump = false;
                }
            }

            //Select Laser Gun
            if (Input.GetKeyDown(KeyCode.Alpha1) && currentWeapon == grenadePrefab){
                currentWeapon = laserGunPrefab;
                laserGunPrefab.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                UIHandler.GetComponent<UIHandler>().changeWeaponUI("Laser Gun");
            }

            //Select Grenade
            if (Input.GetKeyDown(KeyCode.Alpha2) && currentWeapon == laserGunPrefab){
                currentWeapon = grenadePrefab;
                laserGunPrefab.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
                UIHandler.GetComponent<UIHandler>().changeWeaponUI("Grenade");
            }

            //Use Laser Gun
            if (currentWeapon.name == "Laser Gun" && Input.GetKeyDown(KeyCode.Mouse0)){
                currentWeapon.GetComponent<WeaponHandler>().useWeapon(Camera.main.ScreenToWorldPoint(Input.mousePosition), "Laser Gun");
                playerSFXHandler.playLaserSFX();
            }

            //Start charging Grenade
            if (currentWeapon.name == "Grenade" && Input.GetMouseButtonDown(0)){
                chargingGrenade = true;
                power = 0f;
                grenadePrefab.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            }

            // //Charge Grenade
            // if (currentWeapon.name == "Grenade" && Input.GetKey(KeyCode.Mouse0)){
            //     if (power < maxGrenadePower){
            //         power += 0.1f * grenadeChargeSpeed;
            //         changeGrenadeChargeAngle(power);
            //     }
            //     else if (power > maxGrenadePower){
            //         power = maxGrenadePower;
            //     }
            // }

            //Release Grenade
            if (currentWeapon.name == "Grenade" && Input.GetMouseButtonUp(0)){
                currentWeapon.GetComponent<WeaponHandler>().useWeapon(Camera.main.ScreenToWorldPoint(Input.mousePosition), "Grenade", power, direction);
                grenadePrefab.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
                chargingGrenade = false;
            }

            //Start Jetpack particle system
            if (Input.GetKeyDown("left shift") && !isTouchingFloor() && jetpackHandler.fuelRemaining()){
                jetpackParticleSystem.Play();
                playerSFXHandler.playJetpackSFX();
            }

            //Stop Jetpack Particle System
            if (Input.GetKeyUp("left shift") || !jetpackHandler.fuelRemaining()){
                jetpackParticleSystem.Stop();
                playerSFXHandler.stopJetpackSFX();
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

            //Pause
            if (Input.GetKeyDown("escape")){
                PauseMenu.GetComponent<PauseMenu>().pressPauseButton();
            }
        }
        else{
            //Unpause
            if (Input.GetKeyDown("escape")){
                PauseMenu.GetComponent<PauseMenu>().pressPauseButton();
            }
        }
        
    }

    bool isTouchingFloor(){
        if (floorCollider.IsTouching(groundCollider)){
            return true;
        }
        return false;
    }

    void changeGrenadeChargeAngle(float currentPower){
        if (direction == "left"){
            grenadePrefab.transform.eulerAngles = new Vector3(0, 180, 45 * currentPower/maxGrenadePower);
        }
        else{
            grenadePrefab.transform.eulerAngles = new Vector3(0, 0, 45 * currentPower/maxGrenadePower);
        }
        
    }
}

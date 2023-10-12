using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 1f;


    public void useWeapon(Vector3 targetPosition, string weaponName, float power = 0f){
        Rigidbody2D newProjectileRB;
        switch (weaponName){
            case ("Laser Gun"):
                targetPosition.z = 0;
                newProjectileRB = Instantiate(projectilePrefab, transform.position, 
                    Quaternion.LookRotation(transform.forward, (targetPosition - transform.position))).GetComponent<Rigidbody2D>();
                newProjectileRB.velocity = (targetPosition - transform.position).normalized * projectileSpeed;
                break;

            case ("Grenade"):
                targetPosition.z = 0;
                newProjectileRB = Instantiate(projectilePrefab, transform.position, 
                    Quaternion.LookRotation(transform.forward, (targetPosition - transform.position))).GetComponent<Rigidbody2D>();
                newProjectileRB.velocity = (targetPosition - transform.position).normalized * projectileSpeed * power;
                break;

            default:
                Debug.Log("No weapon equipped");
                break;
        }
    }
}

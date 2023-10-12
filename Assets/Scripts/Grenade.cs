using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] ParticleSystem grenadeParticleSystem;
    [SerializeField] GameObject explosionObject;
    [SerializeField] float explosionRadius = 1f;
    [SerializeField] int fuseTime = 2;

    void Start()
    {
        StartCoroutine(ActivateGrenadeRoutine());
    }

    IEnumerator ActivateGrenadeRoutine(){
        yield return new WaitForSeconds(fuseTime);
        Instantiate(grenadeParticleSystem, transform.position, grenadeParticleSystem.transform.rotation).Play();
        GameObject explosionInstance = Instantiate(explosionObject, transform.position, Quaternion.identity);
        explosionInstance.transform.localScale = new Vector3(explosionRadius, explosionRadius, explosionRadius);
        Destroy(this.gameObject);
        yield return null;
    }
}

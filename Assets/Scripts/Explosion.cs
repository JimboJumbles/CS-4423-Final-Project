using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    bool damaging = true;
    
    void Start(){
        StartCoroutine(explodeRoutine());
    }

    void OnTriggerEnter2D(Collider2D collider){
        GameObject otherObject;
        otherObject = collider.gameObject;
        if (damaging && otherObject.tag == "Player"){
            otherObject.GetComponent<PlayerHealth>().damagePlayer(3);
        }
    }

    IEnumerator explodeRoutine(){
        yield return new WaitForSeconds(0.1f);
        damaging = false;
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1f);
        Destroy (this.gameObject);
        yield return null;
    }
}

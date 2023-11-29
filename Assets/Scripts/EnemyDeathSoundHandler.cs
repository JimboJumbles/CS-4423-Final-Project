using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathSoundHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(deathSoundRoutine());
    }

    IEnumerator deathSoundRoutine(){
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
        yield return null;
    }

}

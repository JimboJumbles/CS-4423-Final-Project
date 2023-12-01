using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpawner : MonoBehaviour
{

    [SerializeField] GameObject fireballPrefab;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnFireballsRoutine());
    }

    

    IEnumerator SpawnFireballsRoutine(){
        while(true){
            GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(3);
            Destroy(fireball);
        }
        

        yield return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] GameObject enemyPrefab;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {   
        if (Vector3.Distance(transform.position, player.transform.position) <= 15){
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}

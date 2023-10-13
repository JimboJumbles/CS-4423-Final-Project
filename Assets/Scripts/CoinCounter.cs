using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] GameObject UIHandler;
    int numCoins;

    // Start is called before the first frame update
    void Start()
    {
        numCoins = 0;
    }

    public void getCoin(){
        numCoins++;
        UIHandler.GetComponent<UIHandler>().updateCoins(numCoins);
    }
}

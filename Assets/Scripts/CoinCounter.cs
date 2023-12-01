using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] GameObject UIHandler;
    public static CoinCounter singleton;
    public int numCoins;

    // Start is called before the first frame update
    void Start()
    {
        if (singleton == null) singleton = this;
        else Destroy(this.gameObject);
        numCoins = 0;
    }
    
    public void getCoin(){
        numCoins++;
        UIHandler.GetComponent<UIHandler>().updateCoins(numCoins);
    }
}

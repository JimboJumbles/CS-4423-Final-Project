using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackHandler : MonoBehaviour
{
    [SerializeField] float maxFuel = 12f;
    [SerializeField] float currentFuel;
    [SerializeField] GameObject UIHandler;
    public bool refilling = false;

    void Start(){
        currentFuel = maxFuel;
    }

    public float getMaxFuel(){
        return maxFuel;
    }

    public float getCurrentFuel(){
        return currentFuel;
    }

    public bool fuelRemaining(){
        return currentFuel > 0;
    }

    public void drainFuel(){
        currentFuel -= 0.1f;
        if (currentFuel < 0) currentFuel = 0;
    }

    void Update(){
        if (refilling){
            currentFuel += 0.1f;
            UIHandler.GetComponent<UIHandler>().changeFuelGauge(currentFuel, maxFuel);
            if (currentFuel >= maxFuel){
                currentFuel = maxFuel;
                refilling = false;
            } 
        }
    }
    
}

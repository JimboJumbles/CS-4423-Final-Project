using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] Image fuelGauge;
    
    public void changeFuelGauge(float currentFuel, float maxFuel){
        if (currentFuel > maxFuel) currentFuel = maxFuel;
        fuelGauge.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, ((currentFuel / maxFuel) * 0.9f) + 0.1f);
        fuelGauge.GetComponent<Image>().color = new Color32(255, (byte)(255 * currentFuel/maxFuel), 0, 255);
    }
}

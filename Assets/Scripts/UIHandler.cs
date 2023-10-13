using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] Image fuelGauge;
    [SerializeField] Image weaponFrame;
    [SerializeField] Sprite laserGunSprite;
    [SerializeField] Sprite grenadeSprite;
    [SerializeField] GameObject healthBar;
    [SerializeField] Text coinCounterUI;
    Image[] hearts;
    
    void Awake(){
        weaponFrame.sprite = laserGunSprite;
        hearts = healthBar.GetComponentsInChildren<Image>();
    }

    public void changeFuelGauge(float currentFuel, float maxFuel){
        if (currentFuel > maxFuel) currentFuel = maxFuel;
        fuelGauge.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, ((currentFuel / maxFuel) * 0.9f) + 0.1f);
        fuelGauge.GetComponent<Image>().color = new Color32(255, (byte)(255 * currentFuel/maxFuel), 0, 255);
    }

    public void changeWeaponUI(string weaponName){
        switch (weaponName){
            case ("Laser Gun"):
                weaponFrame.sprite = laserGunSprite;
                weaponFrame.transform.position = weaponFrame.transform.position + new Vector3(-.025f, 0, 0);
                break;
            case ("Grenade"):
                weaponFrame.sprite = grenadeSprite;
                weaponFrame.transform.position = weaponFrame.transform.position + new Vector3(.025f, 0, 0);
                break;
            default:
                break;
        }
    }

    public void updateHealth(int health){
        int i;
        for(i = 0; i < health; i++){
            hearts[i].color = new Color(255, 0, 0, 255);
        }
        for (i = health; i < hearts.Length; i++){
            hearts[i].color = new Color(255, 255, 255, 255);
        }
    }

    public void updateCoins(int numCoins){
        coinCounterUI.text = "COINS: " + numCoins;
    }
}

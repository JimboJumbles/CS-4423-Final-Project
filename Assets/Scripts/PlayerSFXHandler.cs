using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFXHandler : MonoBehaviour
{
    [SerializeField] AudioSource damageSFX;
    [SerializeField] AudioSource jetpackSFX;
    [SerializeField] AudioSource jumpSFX;
    [SerializeField] AudioSource laserSFX;

    public void playDamageSFX(){
        damageSFX.Play();
    }

    public void playJetpackSFX(){
        jetpackSFX.Play();
    }

    public void stopJetpackSFX(){
        jetpackSFX.Stop();
    }

    public void playJumpSFX(){
        jumpSFX.Play();
    }

    public void playLaserSFX(){
        laserSFX.Play();
    }
}

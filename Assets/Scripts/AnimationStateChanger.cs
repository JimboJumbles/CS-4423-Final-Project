using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateChanger : MonoBehaviour
{
    [SerializeField] Animator animator;
    public string currentstate = "Player_Idle";

    void Start(){
        changeAnimationState(currentstate);
    }

    public void changeAnimationState(string newState){

        if (newState == currentstate) return;

        currentstate = newState;
        animator.Play(newState);
    }
}

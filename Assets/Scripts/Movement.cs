using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed = 1f;
    [SerializeField] float jumpForce = 1f;
    [SerializeField] float jetpackForce = 0.1f;

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }


    public void MoveRB(Vector3 vel){
        vel.y = rb.velocity.y;
        rb.velocity = vel * speed;
    }

    public void Jump(){
        rb.AddForce(new Vector3(0,jumpForce,0), ForceMode2D.Impulse);
    }

    public void useJetpack(float vel){
        rb.velocity = (new Vector3(vel,jetpackForce,0));
    }
}

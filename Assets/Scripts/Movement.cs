using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed = 1f;
    [SerializeField] float jumpForce = 1f;
    [SerializeField] float jetpackForce = 0.1f;
    public bool knockbackActive = false;

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }


    public void MoveRB(Vector3 vel){
        if (!knockbackActive){
            vel.y = rb.velocity.y;
            rb.velocity = vel * speed;
        }
    }

    public void MoveTransform(Vector3 vel){
        transform.position += vel * speed * Time.deltaTime;
    }

    public void Jump(){
        rb.AddForce(new Vector3(0, jumpForce,0), ForceMode2D.Impulse);
    }

    public void useJetpack(float vel){
        rb.velocity = (new Vector3(vel,jetpackForce,0));
    }

    public void knockback(){
        float vel = GetComponent<Rigidbody2D>().velocity.x;
        StartCoroutine(KnockbackRoutine());

        IEnumerator KnockbackRoutine(){
            knockbackActive = true;
            if (vel > 0) rb.velocity = new Vector2(-5, 8);
            else if (vel < 0) rb.velocity = new Vector2(5, 8);
            else{
                switch (GetComponent<PlayerInputHandler>().direction){
                    case ("right"):
                        rb.velocity = new Vector2(-4, 9);
                        break;
                    case ("left"):
                        rb.velocity = new Vector2(4, 9);
                        break;
                    default:
                        break;
                }
            }
            yield return new WaitForSeconds(0.8f);
            knockbackActive = false;
            yield return null;
        }
    }
}

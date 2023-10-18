using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeDetection : MonoBehaviour
{
    BoxCollider2D edgeCollider;
    [SerializeField] BoxCollider2D floorCollider;
    CompositeCollider2D groundCollider;

    // Start is called before the first frame update
    void Start()
    {
        edgeCollider = GetComponent<BoxCollider2D>();
        groundCollider = GameObject.FindWithTag("Ground").GetComponent<CompositeCollider2D>();
    }

    public bool isAtEdge(){
        if (edgeCollider.IsTouching(groundCollider) == false && floorCollider.IsTouching(groundCollider)){
            return true;
        }
        return false;
    }
}

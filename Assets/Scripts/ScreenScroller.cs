using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenScroller : MonoBehaviour
{
    [SerializeField] GameObject player;

    void LateUpdate()
    {
        Vector3 oldPosition = transform.position;
        Vector3 newPosition = player.GetComponent<Transform>().position;
        transform.position = new Vector3(newPosition.x, 0, -10);
        player.GetComponent<CrosshairHandler>().moveCrosshairWithScreen(new Vector3(newPosition.x-oldPosition.x, 0, 0));
    }
}

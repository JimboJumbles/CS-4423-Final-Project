using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairHandler : MonoBehaviour
{
    
    [SerializeField] Canvas parentCanvas;
    [SerializeField] Image crosshair;

    void Start()
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle
            (parentCanvas.transform as RectTransform, Input.mousePosition, parentCanvas.worldCamera, out pos);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movePos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle
            (parentCanvas.transform as RectTransform, Input.mousePosition, parentCanvas.worldCamera, out movePos);

        crosshair.GetComponent<RectTransform>().position = parentCanvas.transform.TransformPoint(movePos);
    }

    public void moveCrosshairWithScreen(Vector3 direction){
        crosshair.GetComponent<RectTransform>().Translate(direction * Time.deltaTime);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArrowHeadMover : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Toggle GridSnapToggle;
    private bool held;
    private Vector2 mousePos;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        held = true;
        Debug.Log($"Click");
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        held = false;
        Debug.Log($"UnClick");
    }

    private void Update()
    {
        print(held);
        if(held)
        {
            mousePos = Input.mousePosition;
            Vector2 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            if (GridSnapToggle.isOn)
            {
                newPos = new Vector2(Mathf.RoundToInt(newPos.x), Mathf.RoundToInt(newPos.y));
            }
            
            gameObject.transform.position = newPos;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ArrowHeadMover : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
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
            gameObject.transform.position = newPos;
            
        }
    }
}

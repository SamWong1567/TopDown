﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {

    private Image bgImg;
    private Image joystickImg;
    private Image joystickKnob;
    private Vector2 move;   
    
    private Vector2 joystickOrigin;

    // Use this for initialization
    void Start () {

        //Get components
        bgImg = GetComponent<Image>();
        joystickImg = transform.GetChild(0).GetComponent<Image>();        
        joystickKnob = joystickImg.transform.GetChild(0).GetComponent<Image>();

        //joystick disabled on start
        joystickImg.enabled = false;
        joystickKnob.enabled = false;

        //change color alpha of joystick boundaries to transparent
        Color color = bgImg.color;
        color.a = 0;
        bgImg.color = color;                  
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    //Event: when cursor is dragged after clicking
    public virtual void OnDrag(PointerEventData ped) {

        Vector2 pos;
        //Get coordinates in joystick background image to get movement
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickImg.rectTransform, ped.position, ped.pressEventCamera, out pos)) {

            pos.x = (pos.x * 2 / joystickImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y * 2 / joystickImg.rectTransform.sizeDelta.y);

            move = new Vector2(pos.x, pos.y);
            move = (move.magnitude > 1.0f) ? move.normalized : move;
            
            Vector2 joystickMove = move * (joystickImg.rectTransform.sizeDelta.x / 2);
            joystickKnob.rectTransform.anchoredPosition = joystickMove;
        }                                
    }

    //Event: when screen is clicked down
    public virtual void OnPointerDown(PointerEventData ped) {
        Vector2 pos;
        //Get coordinates in joystick boundary to determine on-click joystick anchor point
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform, ped.position, ped.pressEventCamera, out pos)) {

            //enable joystick on click
            //change position of joystick to click location
            joystickKnob.enabled = true;
            joystickImg.enabled = true;
            joystickOrigin = new Vector2(pos.x, pos.y);
            joystickImg.rectTransform.anchoredPosition = joystickOrigin;       

        }
    }

    //Event: when click is released
    public virtual void OnPointerUp(PointerEventData ped) {
        //Reset position and disable joystick image
        move = Vector3.zero;
        joystickImg.rectTransform.anchoredPosition = Vector3.zero;
        joystickKnob.rectTransform.anchoredPosition = Vector3.zero;
        joystickKnob.enabled = false;
        joystickImg.enabled = false;
    }
    
    public float Horizontal() {         
            return move.x;                
    }

    public float Vertical() {        
            return move.y;       
    }
    
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {

    private Image bgImg;
    private Image joystickImg;
    private Vector3 originPoint;
    private Vector3 dragPoint;
    private Vector3 move;
    private RectTransform canvas;
    private RectTransform joystickRect;

    private Vector2 canvasSize;
    private Vector2 joystickOrigin;

    // Use this for initialization
    void Start () {

        bgImg = GetComponent<Image>();
        joystickImg = transform.GetChild(0).GetComponent<Image>();
        canvas = transform.parent.gameObject.GetComponent<RectTransform>();
        joystickRect = GetComponent<RectTransform>();


        joystickImg.enabled = false;

        Color color = bgImg.color;
        color.a = 0;
        bgImg.color = color;

        canvasSize = new Vector2(canvas.rect.width, canvas.rect.height);

        Debug.Log(canvasSize);

        transform.position = new Vector2(canvasSize.x/4,canvasSize.y/2);
        joystickRect.sizeDelta = new Vector2(canvasSize.x/2,canvasSize.y);


	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public virtual void OnDrag(PointerEventData ped) {
        Vector2 pos;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform, ped.position, ped.pressEventCamera, out pos)) {




            pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);




            dragPoint = new Vector3(pos.x * 2, 0, pos.y * 2);
            move = dragPoint - originPoint;
           
            move = (move.magnitude > 1.0f) ? move.normalized : move;
            Debug.Log(move);



            Vector2 joystickMove = new Vector2(move.x, move.z) * 100;
            joystickImg.rectTransform.anchoredPosition = joystickOrigin + joystickMove;
                ;
        }
        
    }


    public virtual void OnPointerDown(PointerEventData ped) {

        Vector2 pos;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform, ped.position, ped.pressEventCamera, out pos)) {
            joystickImg.enabled = true;
            joystickOrigin = new Vector2(pos.x, pos.y);
            joystickImg.rectTransform.anchoredPosition = joystickOrigin;


            pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);




            originPoint = new Vector3(pos.x * 2, 0, pos.y * 2);
            originPoint = (originPoint.magnitude > 1.0f) ? originPoint.normalized : originPoint;

            Debug.Log(originPoint);

            

        }






    }

    public virtual void OnPointerUp(PointerEventData ped) {

        move = Vector3.zero;
        joystickImg.rectTransform.anchoredPosition = Vector3.zero;
        joystickImg.enabled = false;
    }
    
    public float Horizontal() {

        if (move.x != 0) {

            return move.x;
        }
        else {

            return Input.GetAxisRaw("Horizontal");
        }
    }

        public float Vertical() {

        if (move.z != 0) {

            return move.z;
        }
        else {

            return Input.GetAxisRaw("Vertical");
        }
    }
    
}

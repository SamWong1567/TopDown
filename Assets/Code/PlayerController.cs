using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private float rotationSpeed = 1000;
    private float walkSpeed = 10;
    private float runSpeed = 5;

    public VirtualJoystick joystick;
    public VirtualJoystick1 joystickLook;
    

    private Quaternion targetRotation;


    private  CharacterController controller;


	// Use this for initialization
	void Start () {

        controller = GetComponent<CharacterController>();
        
	}
	
	// Update is called once per frame
	void Update () {
	
        Vector3 input = new Vector3 (joystick.Horizontal(), 0, joystick.Vertical());
        Vector3 lookInput = new Vector3(joystickLook.Horizontal(), 0, joystickLook.Vertical());


        if (lookInput != Vector3.zero) {
            targetRotation = Quaternion.LookRotation(lookInput);
            transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);
        }

        Vector3 motion = input.normalized;
        motion += Vector3.up * -8;


        controller.Move(motion*walkSpeed * Time.deltaTime);

        
	}
}

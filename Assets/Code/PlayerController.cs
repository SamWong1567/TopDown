using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private float rotationSpeed = 1000;
    private float walkSpeed = 10;
    private float runSpeed = 20;

    public ProjectileLauncher[] projs;
    public ProjectileLauncher proj;
     
    public VirtualJoystick joystick;
    public VirtualJoystick joystickLook;   

    private Quaternion targetRotation;

    private CharacterController controller;
    public ProjectileLauncher projectileLauncher;

    public GameObject canvas;

	// Use this for initialization
	void Start () {

        controller = GetComponent<CharacterController>();
        canvas.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        
	    //Get input from the joysticks
        Vector3 input = new Vector3 (joystick.Horizontal(), 0, joystick.Vertical());
        Vector3 lookInput = new Vector3(joystickLook.Horizontal(), 0, joystickLook.Vertical());

        //Rotate player
        if (lookInput != Vector3.zero) {
            targetRotation = Quaternion.LookRotation(lookInput);            
            transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);
            //and shoot
            projectileLauncher.Shoot();
        } 

        //Move player
        Vector3 motion;
        if (input.magnitude!=0) {
             motion = input;
        }
        else {

            motion = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        }

        motion = (motion.magnitude > 1) ? motion.normalized : motion;
        motion += Vector3.up * -8;
        controller.Move(motion*walkSpeed * Time.deltaTime);        
	}
}

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

    //private CharacterController controller;
    private Controller2D controller;
    public ProjectileLauncher projectileLauncher;

    public GameObject canvas;    

	void Start () {

        
        controller = GetComponent<Controller2D>();
        canvas.SetActive(true);
    }
	
	void Update () {
        
	    //Get input from the joysticks
        Vector2 input = new Vector2 (joystick.Horizontal(),joystick.Vertical());
        Vector3 lookInput = new Vector3(joystickLook.Horizontal(),0,joystickLook.Vertical());

        //Rotate player
        if (lookInput != Vector3.zero) {
            targetRotation = Quaternion.LookRotation(lookInput);
            targetRotation = Quaternion.Inverse(targetRotation);
            //Debug.Log(targetRotation);
            //Debug.Log(targetRotation.eulerAngles);      
            transform.eulerAngles = Vector3.forward * Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);
           
            //and shoot
            projectileLauncher.Shoot();
        } 

        //Move player
        Vector2 motion;
        if (input.magnitude!=0) {
             motion = input;
        }
        else {

            motion = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        }

        motion = (motion.magnitude > 1) ? motion.normalized : motion;

        controller.Move(motion * walkSpeed * Time.deltaTime);

        //Below NEEDS to be changed as this does not take into account the game physics and causes the player to pass through objects
        //transform.Translate(motion * walkSpeed * Time.deltaTime,Space.World);

        //motion += Vector3.up * -8;
        //controller.Move(motion*walkSpeed * Time.deltaTime);        
	}
}

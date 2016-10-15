using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private float rotationSpeed = 1000;
    private float walkSpeed = 10;
    private Vector2 velocity;

    public ProjectileLauncher[] projs;
    public ProjectileLauncher proj;
     
    public VirtualJoystick joystick;
    public VirtualJoystick joystickLook;   

    private Quaternion targetRotation;

    //private CharacterController controller;
    //private Controller2D controller;
    public ProjectileLauncher projectileLauncher;
    private Rigidbody2D rigidbody2D;


    public GameObject canvas;    

	void Start () {


        // = GetComponent<Controller2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
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
            //transform.eulerAngles = Vector3.forward * Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);
            transform.eulerAngles = Vector3.forward * targetRotation.eulerAngles.y;
           
            //and shoot
            projectileLauncher.Shoot();
        } 

        //Move player
        
        if (input.magnitude!=0) {
             velocity = input;
        }
        else {

            velocity = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        }

        //Direction of Movement
        velocity = ((velocity.magnitude > 1) ? velocity.normalized : velocity)*walkSpeed;

        //controller.Move(motion * walkSpeed * Time.deltaTime);

        //Below NEEDS to be changed as this does not take into account the game physics and causes the player to pass through objects
        //transform.Translate(motion * walkSpeed * Time.deltaTime,Space.World);

        //motion += Vector3.up * -8;
        //controller.Move(motion*walkSpeed * Time.deltaTime);        
	}

    void FixedUpdate() {

        rigidbody2D.MovePosition(rigidbody2D.position + velocity * Time.fixedDeltaTime);
    }
}

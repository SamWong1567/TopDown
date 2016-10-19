using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    //Variables
    //private float rotationSpeed = 1000;
    public float walkSpeed = 15;
    private Vector2 velocity;
    private Vector3 lookDirection;
    Vector2 input;
    Vector3 lookInput;

    public ProjectileLauncher[] projs;
    public ProjectileLauncher proj;

    //Components
    public VirtualJoystick joystick;
    public VirtualJoystick joystickLook;
    public ProjectileLauncher projectileLauncher;
    private Rigidbody2D rigidbodyPlayer;
    public GameObject canvas;

    //System
    private Quaternion targetRotation;

    

	void Start () {
   
        rigidbodyPlayer = GetComponent<Rigidbody2D>();
        canvas.SetActive(true);
    }

    void FixedUpdate() {

        //Get input from the joysticks
        input = new Vector2(joystick.Horizontal(), joystick.Vertical());
        lookInput = new Vector3(joystickLook.Horizontal(), 0, joystickLook.Vertical());

        RotatePlayer();
        MovePlayer();
        //Move Player
        rigidbodyPlayer.MovePosition(rigidbodyPlayer.position + velocity * Time.fixedDeltaTime);
    }

    void RotatePlayer() {

        //Check whether input are coming from keyboard or touch
        if (lookInput.magnitude != 0) {
            lookDirection = lookInput;
        }
        else {
            lookDirection = new Vector3(Input.GetAxisRaw("HorizontalLook"), 0, Input.GetAxisRaw("VerticalLook"));
        }

        //Rotate player
        if (lookDirection.magnitude != 0) {
            targetRotation = Quaternion.LookRotation(lookDirection);
            targetRotation = Quaternion.Inverse(targetRotation);
            //Debug.Log(targetRotation);
            //Debug.Log(targetRotation.eulerAngles); 

            //Below used for smoother rotation     
            //transform.eulerAngles = Vector3.forward * Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);

            transform.eulerAngles = Vector3.forward * targetRotation.eulerAngles.y;

            //and shoot if using joystick
            if (lookInput.magnitude != 0) {
                projectileLauncher.Shoot();
            }
        }

        //Shoot when spacebar is pressed, Used only when testing on PC
        if (Input.GetKey("space")) {
            projectileLauncher.Shoot();
        }

    }

    void MovePlayer() {

        //Get move direction and velocity
        if (input.magnitude != 0) {
            velocity = input;
        }
        else {

            velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }

        //Direction of Movement
        velocity = ((velocity.magnitude > 1) ? velocity.normalized : velocity) * walkSpeed;

        //Below NEEDS to be changed as this does not take into account the game physics and causes the player to pass through objects
        //transform.Translate(motion * walkSpeed * Time.deltaTime,Space.World);

        //motion += Vector3.up * -8;
        //controller.Move(motion*walkSpeed * Time.deltaTime);        
    }

}

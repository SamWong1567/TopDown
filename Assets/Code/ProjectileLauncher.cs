using UnityEngine;
using System.Collections;


public class ProjectileLauncher : MonoBehaviour {   

    public float rpm;
    
    
    //Components
    public Transform origin;
    public GameObject proj;
   
    //System
    private float secondsBetweenShots;
    private float nextPossibleShootTime;

    RaycastHit hit;
    // Use this for initialization
    void Start () {
        secondsBetweenShots = 60/rpm;
       
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Shoot() {

        if (CanShoot()) {

            ////ignore ray for now
            //Ray ray = new Ray(origin.position, origin.up);
            float shotDistance = 100;
            //if (Physics.Raycast(ray, out hit, shotDistance)) {

            //    // shotDistance = hit.distance;

            //}

            //Debug.DrawRay(ray.origin, ray.direction * shotDistance, Color.red, 2);

            Ray2D ray = new Ray2D(origin.position, origin.up);
            RaycastHit2D hit = Physics2D.Raycast(origin.position, origin.up, shotDistance);
            shotDistance = hit.distance;
            Debug.Log(hit.distance);
            Debug.DrawRay(ray.origin, ray.direction * shotDistance, Color.red, 2);

            nextPossibleShootTime = Time.time + secondsBetweenShots;

            //Instantiate the current projectile           
            //GameObject newProj = Instantiate(proj, transform.position, transform.rotation) as GameObject;                      
        }
    }

    private bool CanShoot() {
 
        bool canShoot = true;

        if (Time.time < nextPossibleShootTime) {
            canShoot = false;
        }

        return canShoot;

    }
}

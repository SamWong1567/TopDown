using UnityEngine;
using System.Collections;


public class ProjectileLauncher : MonoBehaviour {   

    //Public variables
    public float rpm;
    public int numberOfProj;
    public LayerMask mask;

    //Components
    public Transform origin;
    public GameObject proj;
    

    //System
    private float secondsBetweenShots;
    private float nextPossibleShootTime;
    private float angleBetweenProj = 10;
    private Quaternion[] projectileAngle;

    RaycastHit hit;
    // Use this for initialization
    void Start () {
        secondsBetweenShots = 60/rpm;
    }

    public void Shoot() {

        if (CanShoot()) {

            ////RAYCAST2D not used for now
            //float shotDistance = 100;
            //Ray2D ray = new Ray2D(origin.position, origin.up);
            //RaycastHit2D hit = Physics2D.Raycast(origin.position, origin.up, shotDistance,mask.value);            
            //shotDistance = hit.distance;
            ////Debug.Log(hit.distance);
            ////Debug.DrawRay(ray.origin, ray.direction * shotDistance, Color.red, 1);


            //Determine when in game time can the projectile next fire
            nextPossibleShootTime = Time.time + secondsBetweenShots;

            //MULTIPLE PROJECTILES
            projectileAngle = new Quaternion[numberOfProj];

            Quaternion initialAngle = transform.rotation;
            initialAngle = Quaternion.Euler(0, 0, initialAngle.eulerAngles.z - (numberOfProj - 1) * angleBetweenProj / 2);
            Debug.Log(initialAngle.eulerAngles);
            for (int i = 0; i < numberOfProj; i++) {
                projectileAngle[i] = transform.rotation;
                projectileAngle[i] = Quaternion.Euler(0, 0, initialAngle.eulerAngles.z+i*10);

                //Instantiate the current projectile/s           
                GameObject newProj = Instantiate(proj, transform.position, projectileAngle[i]) as GameObject;
            }          
        }
    }

    private bool CanShoot() {
 
        bool canShoot = true;

        if (Time.time < nextPossibleShootTime) {
            canShoot = false;
        }

        return canShoot;

    }

    public void UpdateRPM(float changeInRPM) {

        rpm += changeInRPM;
        secondsBetweenShots = 60 / rpm;
    }


    
}

using UnityEngine;
using System.Collections;


public class ProjectileLauncher : MonoBehaviour {   

    //Public variables
    public float rpm;
    public int chanceToPierce;
    public int numberOfProj;
    public LayerMask mask;

    //Components
    public Transform origin;
    public GameObject proj;
    GameObject newProj;

    //System
    private float secondsBetweenShots;
    private float nextPossibleShootTime;
    private float angleBetweenProj = 10;
    private Quaternion[] projectileAngle;
    Quaternion initialAngle;

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

            CalculateAngleBetweenProjectiles();

            //Instantiate the current projectile/s  
            for (int i = 0; i < numberOfProj; i++) {
                newProj = Instantiate(proj, transform.position, projectileAngle[i]) as GameObject;
                newProj.gameObject.GetComponent<Projectile>().percentChanceToPierce = chanceToPierce;
            }
        }
    }
    public void CalculateAngleBetweenProjectiles() {

        initialAngle = transform.rotation;
        projectileAngle = new Quaternion[numberOfProj];

        initialAngle = Quaternion.Euler(0, 0, initialAngle.eulerAngles.z - (numberOfProj - 1) * angleBetweenProj / 2);
        for (int i = 0; i < numberOfProj; i++) {
            projectileAngle[i] = transform.rotation;
            projectileAngle[i] = Quaternion.Euler(0, 0, initialAngle.eulerAngles.z + i * 10);           
        }
    }
    public void UpdatePierce(int changeInPierce) {
        chanceToPierce += changeInPierce;
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

using UnityEngine;
using System.Collections;


public class ProjectileLauncher : MonoBehaviour {

    public float projectileSpeed;
    public float rpm;
    
    //Components
    public Transform origin;
    public Rigidbody proj;
   
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

            Ray ray = new Ray(origin.position, origin.forward);
                     
            float shotDistance = 20;

            if (Physics.Raycast(ray, out hit, shotDistance)) {

                shotDistance = hit.distance;

            }

            nextPossibleShootTime = Time.time + secondsBetweenShots;
            Debug.DrawRay(ray.origin, ray.direction * shotDistance, Color.red);

            StartCoroutine("FireProjectile");
            
        }

    }

    private bool CanShoot() {
 
        bool canShoot = true;

        if (Time.time < nextPossibleShootTime) {
            canShoot = false;
        }

        return canShoot;

    }

    IEnumerator FireProjectile() {

        Rigidbody newProj = Instantiate(proj, transform.position, transform.rotation) as Rigidbody;
        //newProj.velocity = origin.forward * projectileSpeed;
        while (newProj) {
            newProj.transform.Translate(Vector3.forward * projectileSpeed*Time.deltaTime);
            yield return null;
        }

               

    }
}

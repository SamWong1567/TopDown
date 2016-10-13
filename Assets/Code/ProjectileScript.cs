using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {

    
    public float rpm;

    //Components
    public Transform origin;

    //System
    private float secondsBetweenShots;
    private float nextPossibleShootTime;

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
            RaycastHit hit;

            float shotDistance = 20;

            if (Physics.Raycast(ray, out hit, shotDistance)) {

                shotDistance = hit.distance;

            }

            nextPossibleShootTime = Time.time + secondsBetweenShots;
            Debug.DrawRay(ray.origin, ray.direction * shotDistance, Color.red);
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

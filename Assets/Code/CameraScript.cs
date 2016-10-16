using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    private Vector3 cameraTarget;
    private Transform target;

	// Use this for initialization
	void Start () {
        target = GameObject.FindGameObjectWithTag("Player").transform;

	}

    // Update is called once per frame
    void Update() {
        if (target) {
            cameraTarget = new Vector3(target.position.x, target.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, cameraTarget, Time.deltaTime * 15);
        }

    }
}

using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject target;
    public float damping = 2;
    public Vector3 offset;

	void Start ()
    {
        transform.position = target.transform.position + offset;
        transform.LookAt(target.transform);
	}
	
	void Update ()
    {
        Vector3 targetPosition = new Vector3(target.transform.position.x + offset.x, offset.y, target.transform.position.z + offset.z);
        Vector3 lerpPosition = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * damping);
        transform.position = lerpPosition;
	}
}

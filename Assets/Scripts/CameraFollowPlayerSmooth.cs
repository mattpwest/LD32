using UnityEngine;
using System.Collections;

public class CameraFollowPlayerSmooth : MonoBehaviour {
	public float dampTime = 0.15f;
	public Transform target;
	private Camera camera;
	private Vector3 velocity = Vector3.zero;

	void Start () {
		camera = GameObject.FindObjectOfType<Camera>();
	}
	
	void Update () {
		if (target)	{
			Vector3 point = camera.WorldToViewportPoint(target.position);
			Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.3f, 0.3f, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}
	}
}

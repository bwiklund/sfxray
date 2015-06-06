using UnityEngine;
using System.Collections;

public class OnRailsCamera : MonoBehaviour {
	Vector3 initialPosition;

	float speed = 0.1f;

	void Start () {
		initialPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		var angle = Time.time * speed;
		transform.position = initialPosition + new Vector3(Mathf.Sin (angle), 0, Mathf.Cos (angle));
	}
}

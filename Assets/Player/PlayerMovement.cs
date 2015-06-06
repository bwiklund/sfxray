using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float speed = 1.0f;
	public GameObject trackedObject;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float h_mov = Input.GetAxis ("Horizontal");
		float v_mov = Input.GetAxis ("Vertical");

		this.transform.position += this.speed *
			Time.deltaTime * 
			h_mov * 
			this.trackedObject.transform.right;

		this.transform.position += this.speed *
			Time.deltaTime *
			v_mov *
			this.trackedObject.transform.forward;

		Debug.Log (this.transform.position);
	}
}

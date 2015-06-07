using UnityEngine;
using System.Collections;

public class BucketCtrl : MonoBehaviour {
	float value = 0f;
	float dampedValue = 0f;
	float speed = 1f;
	
	void Start () {
		var color = GetComponentInChildren<Renderer>().material.color;
		color *= Random.Range(0.5f,0.6f);
		GetComponentInChildren<Renderer>().material.color = color;
	}
	
	public void SetValue(float f) {
	}
	
	void Update () {
	}
}

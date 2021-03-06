﻿using UnityEngine;
using System.Collections;

public class VoxelCtrl : MonoBehaviour {
	float value = 0f;
	float dampedValue = 0f;
	float speed = 1f;

	void Start () {
		var color = GetComponentInChildren<Renderer>().material.color;
		color *= Random.Range(0.3f,0.9f);
		GetComponentInChildren<Renderer>().material.color = color;
	}

	public void SetValue(float f) {
		value = f;
	}

	void Update () {
		dampedValue = Mathf.Lerp(dampedValue, value, Time.deltaTime * speed);
		var scale = new Vector3(1f, dampedValue, 1f);
		transform.localScale = scale;
	}
}

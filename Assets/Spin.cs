using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour {
  public float speed = 1f;

	void Update () {
    transform.localRotation = Quaternion.AngleAxis(Time.time * speed, Vector3.up);
	}
}

using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour {
	void Update () {
    transform.rotation = Quaternion.AngleAxis(Time.time, Vector3.up);
	}
}

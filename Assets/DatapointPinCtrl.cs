using UnityEngine;
using System.Collections;

public class DatapointPinCtrl : MonoBehaviour {

	void Start () {
		Randomize();
	}

	void Randomize(){
		transform.position += new Vector3(0f, Random.Range (0f, -1f), 0f);
	}
}

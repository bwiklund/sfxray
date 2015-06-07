using UnityEngine;
using System.Collections;

public class ZipcodeMapCtrl : MonoBehaviour {
	public Transform mapTransform;

	void Start () {
		Randomize();
	}

	void Randomize () {
		foreach (Transform child in mapTransform) {
			var scale = child.localScale;
			scale.Scale(new Vector3(1f, 1f, Random.Range(1f, 2f)));
			child.localScale = scale;
		}
	}
}

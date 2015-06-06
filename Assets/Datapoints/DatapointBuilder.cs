using UnityEngine;
using System.Collections;

public class DatapointBuilder : MonoBehaviour {
	public Transform datapointPrefab;

	int num = 80;

	void Start () {
		Build();
	}

	void Build () {
		for (int i = 0; i < num; i++) {
			// foo
			var pos = (Random.insideUnitSphere + new Vector3(0.5f, 0.5f, 0.5f)) * 16f;
			var instance = (Transform) Instantiate(datapointPrefab, pos, Quaternion.identity);
			instance.SetParent(transform);
		}
	}
}

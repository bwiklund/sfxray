using UnityEngine;
using System.Collections;

public class DatapointBuilder : MonoBehaviour {
	public Transform datapointPrefab;

	void Start () {
		Build();
	}

	void Build () {
		var datapoints = GetComponent<DatapointLoader>().Load ();
		for (int i = 0; i < datapoints.Count; i++) {
			var datapoint = datapoints[i];
//			var pos = (Random.insideUnitSphere + new Vector3(0.5f, 0.5f, 0.5f)) * 16f;
			var instance = (Transform) Instantiate(datapointPrefab, datapoint.position, Quaternion.identity);
			instance.SetParent(transform);
		}
	}
}

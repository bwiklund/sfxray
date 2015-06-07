using UnityEngine;
using System.Collections;

public class DatapointBuilder : BaseDatasetPresenter {
	public Transform datapointPrefab;

	public void EnterScene () {
		var datapoints = GetComponent<DatapointLoader>().Load ();
		for (int i = 0; i < datapoints.Count; i++) {
			var datapoint = datapoints[i];
			var instance = (Transform) Instantiate(datapointPrefab, datapoint.position, Quaternion.identity);
			instance.SetParent(transform);
		}
	}

	public void ExitScene () {
		foreach (Transform child in transform) {
			Destroy(child.gameObject);
		}	
	}
}

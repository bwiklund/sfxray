using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BucketPointBuilder : BaseDatasetPresenter {
	public Transform datapointPrefab;

	void Start() {
		EnterScene ();
	}
	
	public void EnterScene () {
		var datapoints = GetComponent<DatapointLoader>().Load ();

		Dictionary<string, Datapoint> datapoint_map = new Dictionary<string, Datapoint> ();
		var max_size = 0f;

		for (int i = 0; i < datapoints.Count; i++) {
			var datapoint = datapoints[i];
			var dpstring = datapoint.position.x.ToString() + ":" + datapoint.position.y.ToString () + ":" + datapoint.position.z.ToString();
			if (datapoint_map.ContainsKey(dpstring)) {
				datapoint_map[dpstring].size += 1f;
				if (datapoint_map[dpstring].size > max_size) {
					max_size = datapoint_map[dpstring].size;
				}
			} else {
				datapoint_map.Add (dpstring, datapoint);
			}
		}

		// max_size * x = 3
		float multiplier = 5/max_size;

		foreach (KeyValuePair<string, Datapoint> pair in datapoint_map) {
			var datapoint = pair.Value;
			var instance = (Transform) Instantiate(datapointPrefab, datapoint.position, Quaternion.identity);
			instance.transform.localScale = new Vector3(0.1f, -1 *datapoint.size * multiplier, 0.1f);
			instance.transform.localPosition = new Vector3(instance.transform.localPosition.x, 0f + 0.1f, instance.transform.localPosition.z);
			instance.SetParent(transform);
		}
	}
}
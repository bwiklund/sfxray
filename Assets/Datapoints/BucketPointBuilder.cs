using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BucketPointBuilder : MonoBehaviour {
	public Transform datapointPrefab;
	
	void Start () {
		Build();
	}
	
	void Build () {
		var datapoints = GetComponent<DatapointLoader>().Load ();

		Dictionary<string, Datapoint> datapoint_map = new Dictionary<string, Datapoint> ();
		for (int i = 0; i < datapoints.Count; i++) {

			var datapoint = datapoints[i];
			//			var pos = (Random.insideUnitSphere + new Vector3(0.5f, 0.5f, 0.5f)) * 16f;
			var dpstring = datapoint.position.x.ToString() + ":" + datapoint.position.y.ToString () + ":" + datapoint.position.z.ToString();
			if (datapoint_map.ContainsKey(dpstring)) {
				datapoint.size += 1f;
			} else {
				datapoint_map.Add (dpstring, datapoint);
			}
		}
		Debug.Log (datapoint_map.Count);

		foreach (KeyValuePair<string, Datapoint> pair in datapoint_map) {
			var datapoint = pair.Value;
			var instance = (Transform) Instantiate(datapointPrefab, datapoint.position, Quaternion.identity);
			instance.transform.localScale = new Vector3(0.1f, datapoint.size * 2f, 0.1f);
			instance.transform.localPosition += new Vector3(0f, datapoint.size, 0f);
			instance.SetParent(transform);
		}
	}
}
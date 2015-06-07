using UnityEngine;
using System.Collections;

public class CalibrationService : MonoBehaviour {
	public static CalibrationService instance;
	
	public Transform leftCalibrationPoint, rightCalibrationPoint;

	Vector3 transformOffset;
	float transformScale;

	void Awake () {
		instance = this;
		UpdateCalibration();
	}

	void UpdateCalibration () {
		// we need to figure out a translation and scaling that will put these in the right space:
		// our calibration points for convenience
		// LEFT: -122.44642, 37.79275
		// RIGHT: -122.42347, 37.79572
		var leftCalibrationPointLatLon = new Vector3(-122.44642f, 0f, 37.79275f);
		var rightCalibrationPointLatLon = new Vector3(-122.42347f, 0f, 37.79572f);
		
		var leftCalibrationPointGameSpace = leftCalibrationPoint.transform.position;
		var rightCalibrationPointGameSpace = rightCalibrationPoint.transform.position;
		
		leftCalibrationPointGameSpace.Scale(new Vector3(1f, 0f, 1f));
		rightCalibrationPointGameSpace.Scale(new Vector3(1f, 0f, 1f));
		
		var worldDistance = (leftCalibrationPointLatLon - rightCalibrationPointLatLon).magnitude;
		var gameDistance = (leftCalibrationPointGameSpace - rightCalibrationPointGameSpace).magnitude;
		
		transformScale = gameDistance / worldDistance;
		transformOffset = leftCalibrationPointGameSpace - (leftCalibrationPointLatLon * transformScale);
		
		Debug.Log(transformOffset);
		Debug.Log(transformScale);

		var flip = new Vector3(1f, 1f, -1f);
	}

	public Vector3 LatLonToGameSpace (Vector3 position) {
		return (position * transformScale) + transformOffset;
	}
}

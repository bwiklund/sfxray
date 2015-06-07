using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text.RegularExpressions;

public class KMLBucketParser : DatapointLoader {
	public Transform leftCalibrationPoint, rightCalibrationPoint;
	
	public override List<Datapoint> Load () {
		return Parse ("/Users/ben/Downloads/POPOS/POPOS.kml");
	}
	
	List<Datapoint> Parse(string filename) {
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
		
		var transformScale = gameDistance / worldDistance;
		var transformOffset = leftCalibrationPointGameSpace - (leftCalibrationPointLatLon * transformScale);
		
		Debug.Log(transformOffset);
		Debug.Log(transformScale);
		
		// TODO: make a static util for converting latlon to game space for other uses
		//		var topLeft = new Vector3(-122.554379f, 0f, 37.814353f);
		//		var latLonScale = 0.25f;
		//		var gameScale = 32f;
		var flip = new Vector3(1f, 1f, -1f);
		
		XNamespace ns = "http://earth.google.com/kml/2.2";
		var xdoc = XDocument.Load("/Users/darksunrose/Hackathons/sfxray/Assets/DataSets/rentburden.kml");
		var placemarkQuery = xdoc.Root.Element (ns + "Document").
			Elements (ns + "Placemark");

		var placemarks = placemarkQuery.Select( x => {
			var coordinatesStr = x.Element(ns + "Multigeometry").Element (ns + "Polygon").Element(ns + "coordinates").Value;
			var coordinatesArr = Regex.Split(coordinatesStr, " ");

			var sumvector = new Vector3();
			foreach (string coordinates in coordinatesArr) {
				var coordArr = Regex.Split (coordinates, ",");
				sumvector.x = sumvector.x + float.Parse (coordArr[0]);
				sumvector.y = sumvector.y + float.Parse (coordArr[2]);
				sumvector.z = 0f;

			}
			var position = new Vector3(
				(float)(sumvector.x / coordinatesArr.Length),
				(float)(sumvector.y / coordinatesArr.Length),
			    (float)(sumvector.z / coordinatesArr.Length)
			);

			Debug.Log(position);

			/*
			var position = new Vector3(
				float.Parse(coordinatesArr[0]),
				float.Parse(coordinatesArr[2]),
				float.Parse(coordinatesArr[1])
			);
			*/

			var transformedPosition = (position * transformScale) + transformOffset;
			//			transformedPosition.Scale(flip);
			return new Datapoint {
				position = transformedPosition
			};
		}).ToList();
		Debug.Log ("Loading " + placemarks.Count + " kml placemarks");
		return placemarks;
	}
}
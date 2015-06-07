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

	private Vector3 voxelize(Vector3 vect) {
		Vector3 topleft = new Vector3(-122.5220f, 0f, 37.8094f);
		float bin_size = .01f;
		float xdiff = -122.5220f - vect.x;
		float zdiff = 37.8094f - vect.z;
		int xint = (int)(xdiff / bin_size);
		int zint = (int)(zdiff / bin_size);
		vect = new Vector3 (-122.5220f - bin_size * xint, 0, 37.8094f - bin_size*zint);
		Debug.Log (vect);
		return vect;
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
		
		//Debug.Log(transformOffset);
		//Debug.Log(transformScale);
		
		// TODO: make a static util for converting latlon to game space for other uses
		//		var topLeft = new Vector3(-122.554379f, 0f, 37.814353f);
		//		var latLonScale = 0.25f;
		//		var gameScale = 32f;
		var flip = new Vector3(1f, 1f, -1f);
		
		XNamespace ns = "http://www.opengis.net/kml/2.2";
		var xdoc = XDocument.Load("/Users/darksunrose/Hackathons/sfxray/Assets/DataSets/rentburden.kml");
		var placemarkQuery = xdoc.Root.Element (ns + "Document").Element (ns + "Folder").Elements (ns + "Placemark");

		var placemarks = placemarkQuery.Select( x => {
			var coordinatesStr = x.Element(ns + "MultiGeometry").Element (ns + "Polygon").Element (ns + "outerBoundaryIs").Element (ns + "LinearRing").Element(ns + "coordinates").Value;
			var coordinatesArr = Regex.Split(coordinatesStr, " ");

			var sumvector = new Vector3();
			foreach (string coordinates in coordinatesArr) {
				var coordArr = Regex.Split (coordinates, ",");
				sumvector.x = sumvector.x + float.Parse (coordArr[0]);
				sumvector.y = 0f;
				sumvector.z = sumvector.z + float.Parse (coordArr[1]);

			}

			var position = new Vector3(
				(float)(sumvector.x / coordinatesArr.Length),
				(float)(sumvector.y / coordinatesArr.Length),
			    (float)(sumvector.z / coordinatesArr.Length)
			);
			position = this.voxelize(position);

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
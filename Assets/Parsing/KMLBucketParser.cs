using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text.RegularExpressions;

public class KMLBucketParser : DatapointLoader {
	
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

			return new Datapoint {
				position = CalibrationService.instance.LatLonToGameSpace(position)
			};
		}).ToList();

		Debug.Log ("Loading " + placemarks.Count + " kml placemarks");
		return placemarks;
	}
}
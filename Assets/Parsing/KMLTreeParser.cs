using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text.RegularExpressions;

public class KMLTreeParser : DatapointLoader {

	public override List<Datapoint> Load () {
		return Parse ("/SFXRAY/trees.kml");
	}

	private Vector3 voxelize(Vector3 vect) {
		Vector3 topleft = new Vector3(-122.5220f, 0f, 37.8094f);
		float bin_size = .01f;
		float xdiff = -122.5220f - vect.x;
		float zdiff = 37.8094f - vect.z;
		int xint = (int)(xdiff / bin_size);
		int zint = (int)(zdiff / bin_size);
		vect = new Vector3 (-122.5220f - bin_size * xint, 0, 37.8094f - bin_size*zint);
		return vect;
	}

	List<Datapoint> Parse(string filename) {
		
		XNamespace ns = "http://www.opengis.net/kml/2.2";
		var xdoc = XDocument.Load(filename);
		var placemarkQuery = xdoc.Root.Element (ns + "Document").Element (ns + "Folder").Elements (ns + "Placemark");

		var placemarks = placemarkQuery.Select( x => {
			var coordinatesStr = x.Element(ns + "Point").Element (ns + "coordinates").Value;
			var coordinatesArr = Regex.Split(coordinatesStr, ",");

			var position = new Vector3(
				float.Parse(coordinatesArr[0]),
				0f,
			    float.Parse(coordinatesArr[1])
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
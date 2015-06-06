using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text.RegularExpressions;

public class KMLParser : DatapointLoader {
	public override List<Datapoint> Load () {
		return Parse ("/Users/ben/Downloads/POPOS/POPOS.kml");
	}

	List<Datapoint> Parse(string filename) {
		// TODO: make a static util for converting latlon to game space for other uses
		var topLeft = new Vector3(-122.5f, 0f, 37.8f);
		var latLonScale = 0.25f;
		var gameScale = 32f;
		var flip = new Vector3(1f, 1f, -1f);

		XNamespace ns = "http://earth.google.com/kml/2.2";
		var xdoc = XDocument.Load("/Users/ben/Downloads/POPOS/POPOS.kml");
		var placemarkQuery = xdoc.Root.Element(ns + "Document").Elements(ns + "Placemark");
		var placemarks = placemarkQuery.Select( x => {
			var coordinatesStr = x.Element(ns + "Point").Element (ns + "coordinates").Value;
			var coordinatesArr = Regex.Split(coordinatesStr, ",");
			var position = new Vector3(
				float.Parse(coordinatesArr[0]),
				float.Parse(coordinatesArr[2]),
				float.Parse(coordinatesArr[1])
			);
			var transformedPosition = (position - topLeft) / latLonScale * gameScale;
			transformedPosition.Scale(flip);
			return new Datapoint {
				position = transformedPosition
			};
		}).ToList();
		Debug.Log ("Loading " + placemarks.Count + " kml placemarks");
		return placemarks;
	}
}
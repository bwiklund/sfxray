using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text.RegularExpressions;

public class KMLParser : DatapointLoader {

	public override List<Datapoint> Load () {
		return Parse ("/SFRAY/POPOS.kml");
	}

	List<Datapoint> Parse(string filename) {
		XNamespace ns = "http://earth.google.com/kml/2.2";
		var xdoc = XDocument.Load(filename);
		var placemarkQuery = xdoc.Root.Element(ns + "Document").Elements(ns + "Placemark");
		var placemarks = placemarkQuery.Select( x => {
			var coordinatesStr = x.Element(ns + "Point").Element (ns + "coordinates").Value;
			var coordinatesArr = Regex.Split(coordinatesStr, ",");
			var position = new Vector3(
				float.Parse(coordinatesArr[0]),
				float.Parse(coordinatesArr[2]),
				float.Parse(coordinatesArr[1])
			);
//			transformedPosition.Scale(flip);
			return new Datapoint {
				position = CalibrationService.instance.LatLonToGameSpace(position)
			};
		}).ToList();
		Debug.Log ("Loading " + placemarks.Count + " kml placemarks");
		return placemarks;
	}
}
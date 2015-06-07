using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text.RegularExpressions;

public class KMLAccidentParser : DatapointLoader {
	
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
		var xdoc = XDocument.Load("/Users/darksunrose/Desktop/accidents.xml");
		Debug.Log ("XML IS: ");
		Debug.Log (xdoc.Root.Element ("row"));
		var placemarkQuery = xdoc.Root.Element ("row").Elements ("row");

		var placemarks = placemarkQuery.Select( x => {
			var coordinatex = x.Element("x").Value;
			var coordinatez = x.Element("y").Value;
			
			var position = new Vector3(
				float.Parse (coordinatex),
				0f,
				float.Parse (coordinatez)
			);
			
			return new Datapoint {
				position = CalibrationService.instance.LatLonToGameSpace(position)
			};
		}).ToList();
		
		Debug.Log ("Loading " + placemarks.Count + " kml placemarks");
		return placemarks;
	}
}
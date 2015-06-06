using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class KMLParser : DatapointLoader {
	public override List<Datapoint> Load () {
		return Parse ("/Users/ben/Downloads/POPOS/POPOS.kml");
	}
	
	List<Datapoint> Parse(string filename) {
		XmlDocument xdoc = new XmlDocument ();
		try { xdoc.Load(filename); }
		catch (System.IO.FileNotFoundException) {
			Debug.Log("File not found: " + filename);
		}

		var placemarks = xdoc.FirstChild.NextSibling.FirstChild.ChildNodes;
		return new List<Datapoint>();
	}
}

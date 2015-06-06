using UnityEngine;
using System.Collections.Generic;

public class DatapointLoader : MonoBehaviour {
	public virtual List<Datapoint> Load(){
		return new List<Datapoint>();
	}
}

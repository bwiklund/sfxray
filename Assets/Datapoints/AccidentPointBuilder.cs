using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AccidentPointBuilder : MonoBehaviour {
	public Transform datapointPrefab;
	
	void Start () {
		Build();
	}

	void MakeParticles (Vector3 pos, List<ParticleSystem.Particle> particles) {

		var particle = new ParticleSystem.Particle () {
			position = pos,
			size = 0.05f,
			color = new Color(190f,90f,140f),
			startLifetime = 1000f,
			lifetime = 1000f
		};
		particles.Add (particle);
	}
	

	
	void Build () {
		var datapoints = GetComponent<DatapointLoader>().Load ();

		List<ParticleSystem.Particle> particles = new List<ParticleSystem.Particle>();
		foreach (Datapoint datapoint in datapoints) {
			this.MakeParticles(new Vector3(datapoint.position.x, 1.5f + Random.insideUnitSphere.y, datapoint.position.z), particles);
		}

		Debug.Log ("this many parts");
		Debug.Log (particles.Count);
		var pSys = GetComponent<ParticleSystem>();
		pSys.SetParticles(particles.ToArray(), particles.Count);

	}
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AccidentPointBuilder : BaseDatasetPresenter {
	public Transform datapointPrefab;
	
	public void EnterScene () {
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
	
	void MakeParticles (Vector3 pos, List<ParticleSystem.Particle> particles) {
		var particle = new ParticleSystem.Particle () {
			position = pos,
			size = 0.02f,
			color = new Color(0.8f,0.1f,0.1f),
			startLifetime = 1000f,
			lifetime = 1000f
		};
		particles.Add (particle);
	}
	
	public void ExitScene () {
		GetComponent<ParticleSystem>().Clear ();
	}
}
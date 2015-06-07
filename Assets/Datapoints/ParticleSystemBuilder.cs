using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleSystemBuilder : MonoBehaviour {
	void Start () {
		var particles = new List<ParticleSystem.Particle>();
		for (int i = 0; i < 1000; i++) {
			var pos = Random.insideUnitSphere;
			
			var particle = new ParticleSystem.Particle() {
				position = pos,
				color = Color.black,
				size = 1f,
				startLifetime = 1000f,
				lifetime = 1000f
			};
			particles.Add (particle);
		}

		var pSys = GetComponent<ParticleSystem>();
		pSys.SetParticles(particles.ToArray(), particles.Count);
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ObjParticleBehaviour : MonoBehaviour {
  void Start() {
    var loader = new ObjToParticles() {
      filepath = "/particles/Scaled/Scaled.obj"
    };
    var points = loader.Load();

    var pSystem = GetComponent<ParticleSystem>();
    var particles = new List<ParticleSystem.Particle>();
    foreach (var point in points) {
      var particle = new ParticleSystem.Particle() {
        color = Color.white,
        size = 0.01f,
        startLifetime = 5000f,
        position = point * 0.01f
      };
      particles.Add(particle);
    }
    pSystem.SetParticles(particles.ToArray(), particles.Count);
 
  }
}

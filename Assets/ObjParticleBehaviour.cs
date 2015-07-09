using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ObjParticleBehaviour : MonoBehaviour {
  List<Vector3> points;

  void Start() {
    var loader = new ObjToParticles() {
      filepath = "/particles/Scaled/Scaled.obj"
    };
    points = loader.Load();

    var pSystem = GetComponent<ParticleSystem>();
    var particles = new List<ParticleSystem.Particle>();
    foreach (var point in points) {
      var particle = new ParticleSystem.Particle() {
        color = Color.white,
        size = 0.01f,
        startLifetime = 5000f,
        position = point,
        velocity = Vector2.zero
      };
      particles.Add(particle);
    }
    pSystem.SetParticles(particles.ToArray(), particles.Count);
  }

  void Update() {
    var pSystem = GetComponent<ParticleSystem>();

    var sfKey = Input.GetKey(KeyCode.Q);
    var sphereKey = Input.GetKey(KeyCode.W);

    ParticleSystem.Particle[] particles = new ParticleSystem.Particle[pSystem.particleCount];
    pSystem.GetParticles(particles);
    for (int i = 0; i < particles.Length; i++) {
      var particle = particles[i];
      particle.velocity += 0.02f * Time.deltaTime * Random.insideUnitSphere;
      particle.position += particle.velocity;
      particle.velocity *= (1f - Time.deltaTime);

      if (sfKey) {
        particle.position = Vector3.Lerp(particle.position, points[i], 0.5f);
      }

      if (sphereKey) {
        var radius = 0.3f;
        var angle = (i * 0.007f);
        var v = Mathf.PI * (i / (float)points.Count - 0.5f);
        var spherePos = new Vector3(
          Mathf.Cos(angle) * Mathf.Cos(v),
          Mathf.Sin(v),
          Mathf.Sin(angle) * Mathf.Cos(v)
        ) * radius;
        particle.position = Vector3.Lerp(particle.position, spherePos, 0.5f);
      }

      particles[i] = particle;
    }
    pSystem.SetParticles(particles, particles.Length);
  }
}

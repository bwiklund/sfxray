using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ObjParticleBehaviour : MonoBehaviour {
  List<Vector3> points, textPoints, year1, year2;

  void Start() {
    points = new ObjToParticles { filepath = "/particles/Scaled/Scaled.obj" }.Load(0.1f);
    textPoints = new ObjToParticles { filepath = "/particles/SanFrancisco/SanFrancisco.obj" }.Load(1f);
    year1 = new ObjToParticles { filepath = "/particles/0000/0000.obj" }.Load(1f);
    year2 = new ObjToParticles { filepath = "/particles/2015/2015.obj" }.Load(1f);

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
    var textKey = Input.GetKey(KeyCode.E);
    var year1key = Input.GetKey(KeyCode.R);
    var year2key = Input.GetKey(KeyCode.T);

    ParticleSystem.Particle[] particles = new ParticleSystem.Particle[pSystem.particleCount];
    pSystem.GetParticles(particles);
    for (int i = 0; i < particles.Length; i++) {
      var particle = particles[i];
      particle.velocity += 0.02f * Time.deltaTime * Random.insideUnitSphere;
      particle.position += particle.velocity;
      particle.velocity *= (1f - Time.deltaTime);

      if (sfKey) particle.position = Vector3.Lerp(particle.position, points[i], 0.5f);
      if (textKey) particle.position = Vector3.Lerp(particle.position, textPoints[i % textPoints.Count] * 100f, 0.5f);
      if (year1key) particle.position = Vector3.Lerp(particle.position, year1[i % year1.Count] * 100f, 0.5f);
      if (year2key) particle.position = Vector3.Lerp(particle.position, year2[i % year2.Count] * 100f, 0.5f);

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

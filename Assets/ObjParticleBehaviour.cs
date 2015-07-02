using UnityEngine;
using System.Collections;

public class ObjParticleBehaviour : MonoBehaviour {
  void Start() {
    var loader = new ObjToParticles() {
      filepath = "/particles/Scaled/Scaled.obj"
    };
    var points = loader.Load();

    foreach (var point in points) {
      
    }
  }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class ObjToParticles {
  public string filepath;

  public List<Vector3> Load() {
    StreamReader reader = new StreamReader(filepath);
    var points = new List<Vector3>();

    while (reader.Peek() != -1) {
      var line = reader.ReadLine();
      if (Random.Range(0, 10) < 9) continue;

      var parts = line.Split(' ');
      if (parts[0] == "v") {
        var point = new Vector3(
          float.Parse(parts[1]),
          float.Parse(parts[2]),
          float.Parse(parts[3])
        ) * 0.001f;
        points.Add(point);
      }
    }

    Debug.Log("Loaded points: " + points.Count);

    return points;
  }
}

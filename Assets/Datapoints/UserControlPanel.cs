using UnityEngine;
using System.Collections;

public class UserControlPanel : MonoBehaviour {
	public Transform[] datasets;

	void Start () {
//		datasets[0].SendMessage ("EnterScene");
//		foreach (var dataset in datasets) {
//			dataset.SendMessage ("EnterScene");
//		}
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.Q)) datasets[0].SendMessage ("Toggle");
		if(Input.GetKeyDown(KeyCode.W)) datasets[1].SendMessage ("Toggle");
		if(Input.GetKeyDown(KeyCode.E)) datasets[2].SendMessage ("Toggle");
		if(Input.GetKeyDown(KeyCode.R)) datasets[3].SendMessage ("Toggle");

		if(Input.GetKeyDown(KeyCode.JoystickButton0)) datasets[0].SendMessage ("Toggle");
		if(Input.GetKeyDown(KeyCode.JoystickButton1)) datasets[1].SendMessage ("Toggle");
		if(Input.GetKeyDown(KeyCode.JoystickButton2)) datasets[2].SendMessage ("Toggle");
		if(Input.GetKeyDown(KeyCode.JoystickButton3)) datasets[3].SendMessage ("Toggle");
	}
}

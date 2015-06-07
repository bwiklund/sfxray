using UnityEngine;
using System.Collections;

public class UserControlPanel : MonoBehaviour {
	public Transform[] datasets;

	void Start () {
		foreach (var dataset in datasets) {
			dataset.GetComponent<BaseDatasetPresenter>().EnterScene();
		}
	}
}

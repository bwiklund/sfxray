using UnityEngine;

// a small base class for consitent control of different datasets
public class BaseDatasetPresenter : MonoBehaviour {
	bool isEnabled = false;

	public void Toggle () {
		isEnabled = !isEnabled;
		if (isEnabled) {
			SendMessage("EnterScene");
		} else {
			SendMessage("ExitScene");
		}
	}
}

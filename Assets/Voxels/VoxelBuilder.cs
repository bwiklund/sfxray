﻿using UnityEngine;
using System.Collections;

public class VoxelBuilder : BaseDatasetPresenter {
	public Transform voxelPrefab;

	int xSize = 16, zSize = 16;

	public void EnterScene () {
		for(int x = 0; x < xSize; x++){
			for(int z = 0; z < zSize; z++){
				var pos = new Vector3(x, 0, z);
				var obj = (Transform) Instantiate(voxelPrefab, pos, Quaternion.identity);
				obj.SetParent(transform);

				// foo data
				var f = Random.Range(0.1f, 6f) * Random.Range(0f, 1f);
				obj.GetComponent<VoxelCtrl>().SetValue(f);
			}
		}
	}
}

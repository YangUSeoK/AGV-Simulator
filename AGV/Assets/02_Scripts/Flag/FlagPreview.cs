using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagPreview : Preview
{
	public Vector3 Pos => this.transform.position;

	private void Update()
	{
		if(GameManager.GameMode != EGameMode.CreateFlag)
		{
			this.gameObject.SetActive(false);
		}

		Vector3 createVec = Input.mousePosition;
		createVec.z = Camera.main.transform.position.y;
		this.transform.position = Camera.main.ScreenToWorldPoint(createVec);
	}
}

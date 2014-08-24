/*
* |===============================|
* |								  |
* |		COPYRIGHT				  |
* |		TECHBLOGOGY	2014		  |
* | 							  |
* |								  |
* |===============================|
* 
* 	WERY BAD CODE. USE ONLY AT YOUR OWN RISK. ALTHOUGHT I'D RECOMEND NOT USING IT
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkyboxAnim:MonoBehaviour
{
	public Material skyBoxMat;

	void Start()
	{

	}

	private int ind = 1;
	private float time = 0.0f;
	private Color curCol;
	void Update()
	{
		time += Time.deltaTime * 0.1f * ind;

		if (time >= 1.0f) ind = -1;
		else if (time <= 0.0f) ind = 1;

		curCol = new Color(time,0.25f,0.25f,1.0f);
		skyBoxMat.SetColor("_Tint",curCol);
	}
}
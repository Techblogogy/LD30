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

public class BadEnding:GameState
{
	public BadEnding() : base(null,null,null,0,0,0,0,null)
	{

	}

	public override void StageStart ()
	{
		//base.StageStart ();

		CameraGUI.setBadEnd = true;
		Camera.main.GetComponents<AudioSource>()[2].Play();
	}

	public override void StageUpdate ()
	{
		//base.StageUpdate ();
	}

	public override void StageEnd ()
	{
		//base.StageEnd ();
	}
}
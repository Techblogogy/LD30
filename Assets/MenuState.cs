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

public class MenuState:GameState
{
	public static bool startGame = false;
	private bool audioPlayed = false;
	private AudioClip vs;
	private string vsPath;

	public MenuState(string vsPath) : base(null,null,null,0,0,0,0,null)
	{
		this.vsPath = vsPath;
	}

	public override void StageStart ()
	{
		//base.StageStart ();

		CameraGUI.setMenu = true;

		vs = Resources.Load<AudioClip>(vsPath);
		Camera.main.GetComponents<AudioSource>()[3].volume = 1;
		Camera.main.GetComponents<AudioSource>()[3].clip = vs;
	}

	public override void StageUpdate ()
	{
		//base.StageUpdate (); 

		if (startGame)
		{
			CameraGUI.setMenu = false;

			if (!Camera.main.GetComponents<AudioSource>()[3].isPlaying && !audioPlayed)
			{
				Camera.main.GetComponents<AudioSource>()[3].Play();
				audioPlayed = true;
			}
			else if (!Camera.main.GetComponents<AudioSource>()[3].isPlaying && audioPlayed)
			{
				gameStagesObj.NextState();
			}

			Debug.Log(Camera.main.GetComponents<AudioSource>()[3].isPlaying);
		}

	}

	public override void StageEnd ()
	{
		//base.StageEnd ();

		CameraGUI.setMenu = false;
	}
}
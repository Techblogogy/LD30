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

public class GoodEnd:GameState
{
	private AudioClip musicP;
	private AudioClip vs;

	private string musP;
	private string vsPath;

	private bool created = false;

	private GameObject objectPrefab;

	public GoodEnd(string musP, string vs) : base(null,null,null,0,0,0,0,null)
	{
		this.musP = musP;
		this.vsPath = vs;
	}
	
	public override void StageStart ()
	{
		//base.StageStart ();

		//CameraGUI.setGoodEnd = true;

		musicP = Resources.Load<AudioClip>(musP);
		Camera.main.GetComponents<AudioSource>()[0].volume = 1;
		Camera.main.GetComponents<AudioSource>()[0].clip = musicP;
		Camera.main.GetComponents<AudioSource>()[0].Play();

		vs = Resources.Load<AudioClip>(vsPath);
		Camera.main.GetComponents<AudioSource>()[3].volume = 1;
		Camera.main.GetComponents<AudioSource>()[3].clip = vs;
		Camera.main.GetComponents<AudioSource>()[3].Play();

		objectPrefab = Resources.Load<GameObject>("Mesh/Human");
	}
	
	public override void StageUpdate ()
	{
		//base.StageUpdate ();

		if (!Camera.main.GetComponents<AudioSource>()[3].isPlaying && !created)
		{
			oHolder = new GameObject();
			oHolder.name = "Human";

			GameObject hm = (GameObject)MonoBehaviour.Instantiate(objectPrefab, Vector3.zero,Quaternion.Euler(new Vector3(-475.0f,180.0f,1.5f)));
			hm.transform.localScale = new Vector3(2f,2f,2f);
			hm.transform.parent = oHolder.transform;

			Camera.main.GetComponents<AudioSource>()[1].Play();

			created = true;

			vs = Resources.Load<AudioClip>("SoundFX/Voice/Sound7");
			Camera.main.GetComponents<AudioSource>()[3].volume = 1;
			Camera.main.GetComponents<AudioSource>()[3].clip = vs;
			Camera.main.GetComponents<AudioSource>()[3].Play();
		}
	}
	
	public override void StageEnd ()
	{
		//base.StageEnd ();
	}
}
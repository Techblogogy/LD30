using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState
{
	protected GameStages gameStagesObj;

	protected ConObj oController;
	protected GameObject oHolder;

	protected string objName;

	private string meshPath;

	private AudioClip ac;
	private string clipPath;

	private float minDist;
	private float maxDist;
	private float spawnTime;

	public GameState(string objName, string meshPath, string clipPath, float minDist, float maxDist, float spawnTime)
	{
		gameStagesObj = GameObject.Find("StageMachine").GetComponent<GameStages>();

		this.objName = objName;
		this.meshPath = meshPath;
		this.clipPath = clipPath;

		this.minDist = minDist;
		this.maxDist = maxDist;
		this.spawnTime = spawnTime;
	}

	public virtual void StageStart()
	{
		oHolder = new GameObject();
		oHolder.name = objName;
		oController = new ConObj(oHolder, meshPath, spawnTime, minDist, maxDist);

		//allowFadeIn = true;

		ac = Resources.Load<AudioClip>(clipPath);
		Camera.main.GetComponents<AudioSource>()[0].volume = 0;
		Camera.main.GetComponents<AudioSource>()[0].clip = ac;
		Camera.main.GetComponents<AudioSource>()[0].Play();

	}

	public virtual void StageUpdate()
	{
		FadeInSound();
		FadeOutAndSwitch();
	}

	public virtual void StageEnd()
	{
		MonoBehaviour.Destroy(oHolder);
	}

	/* Object Animations */
	private float alpha = 0.0f;
	private bool faidingIn = true;
	protected void FadeInSound()
	{
		if (faidingIn)
		{
			alpha += Time.deltaTime * 0.5f;
			Camera.main.GetComponents<AudioSource>()[0].volume = alpha;

			if (alpha >= 1.0f)
			{
				alpha = 1.0f;
				faidingIn = false;
			}
		}
	}

	private Vector3 transformDirection = Vector3.zero;
	protected void FadeOutAndSwitch()
	{
		if (oController.objList.Count >= 5)
		{
			alpha -= Time.deltaTime * 0.5f;
			
			foreach (Transform go in oHolder.transform)
			{
				go.gameObject.renderer.material.shader = Shader.Find("Transparent/Diffuse");
				go.gameObject.renderer.material.color = new Color(go.gameObject.renderer.material.color.r,
				                                                  go.gameObject.renderer.material.color.g,
				                                                  go.gameObject.renderer.material.color.b,
				                                                  alpha);
			}

			CameraClick.moveTo = Vector3.zero;
			
			/*transformDirection = new Vector3(0,0,-1);
			transformDirection = Camera.main.transform.TransformDirection(transformDirection);
			transformDirection *= 10.0f; 
			
			Camera.main.GetComponent<CharacterController>().Move(transformDirection * Time.deltaTime);*/
			Camera.main.GetComponents<AudioSource>()[0].volume = alpha;
			
			if (alpha <= 0)
			{
				MonoBehaviour.Destroy(oHolder);
				gameStagesObj.NextState();
			}
		}
	}
}
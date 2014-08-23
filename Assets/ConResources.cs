using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConResources:MonoBehaviour
{
	public int res = 0;
	private int linkHealt = 100;
	public float linkAlpha = 1.0f;

	public float genTime = 5.0f;
	public int healtDamage = 5;

	public float spawnTime = 0.0f;

	public List<GameObject> conList = new List<GameObject>();

	public ConObj co;

	void Start()
	{

	}

	void Update()
	{
		GenerateResources();
	}

	/* Mange Resources */
	
	private float resourceTime = 0.0f;
	private void GenerateResources()
	{
		resourceTime += Time.deltaTime;
		if (resourceTime >= spawnTime)
		{
			res++;
			SetLink(-healtDamage);

			if (linkHealt <= 0)
				GameObject.Find("StageMachine").GetComponent<GameStages>().SetBadEnd();
			
			resourceTime = 0;
		}
	}
	
	private void SetLink(int h)
	{
		linkHealt += h;
		
		if (h>0)
			res--;
		
		linkAlpha = (float)linkHealt/100.0f;
		
		foreach (GameObject gm in conList)
		{
			gm.renderer.material.color = new Color(gm.renderer.material.color.r,
			                                       gm.renderer.material.color.g,
			                                       gm.renderer.material.color.b,
			                                       linkAlpha);
		}
	}

	void OnGUI()
	{
		GUI.TextArea(new Rect(0,0,100,20), "Resources: "+res);
		GUI.TextArea(new Rect(100,0,100,20), "Healt: "+linkHealt);

		if (res > 0)
		{
			if ( GUI.Button(new Rect(20,20,200,20), "Add Object(-1 Resource)") )
			{
				res--;
				Vector3 randomVec = new Vector3(Random.Range(transform.position.x + 5.0f, transform.position.x - 5.0f),
				                                0,
				                                Random.Range(transform.position.z + 5.0f, transform.position.z - 5.0f) );


				co.CreateConnectableObject(randomVec);
			}
		}

		if (res > 0 && linkHealt < 100 && conList.Count > 0)
		{
			if (GUI.Button(new Rect(20,40,200,20), "Heal Link (-1 Resource)"))
			{
				SetLink(healtDamage);
				Camera.main.GetComponents<AudioSource>()[2].Play();
			}
		}
	}
}
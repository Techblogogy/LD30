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

	private GUISkin gs;
	public string objName;

	public int ammount;

	void Start()
	{
		gs = Resources.Load<GUISkin>("GameGUI");
	}

	void Update()
	{
		GenerateResources();
	}

	/* Mange Resources */
	
	public float resourceTime;
	private void GenerateResources()
	{
		resourceTime -= Time.deltaTime;
		if (resourceTime <= 0)
		{
			res++;

			if (conList.Count > 0)
				SetLink(-healtDamage);

			if (linkHealt <= 0)
				GameObject.Find("StageMachine").GetComponent<GameStages>().SetBadEnd();
			
			resourceTime = spawnTime;
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

	private Vector3 cPos = Vector3.zero;
	private Vector3 lastPos = Vector3.zero;
	private int lastAmp = 5;
	void OnGUI()
	{
		GUI.skin = gs;

		GUI.Label(new Rect(10,0,150,50), "Energy: "+res);
		GUI.Label(new Rect(160,0,200,50), "Link Health: "+linkHealt);
		GUI.Label(new Rect(Screen.width-150,0,150,50), "Time: "+((int)resourceTime+1));

		GUI.Label(new Rect(10,Screen.height-50,400,50), objName + "s Until Upgrade: " + (ammount - co.objList.Count));

		if (res > 0)
		{
			if ( GUI.Button(new Rect(Screen.width-250,Screen.height-50,250,50), "Add "+objName+" (-1 Energy)") )
			{
				res--;
				/*cPos = co.objList[co.objList.Count-1].transform.position;
				Vector3 randomVec = new Vector3(Random.Range(cPos.x + co.minDist, cPos.x + co.maxDist),
				                                0,
				                                Random.Range(cPos.z + co.minDist, cPos.z + co.maxDist) );*/

				float radomR = Random.Range(-0.2f, 0.2f);
				Vector3 randomVec = new Vector3(5 * Mathf.Sin(radomR * (2*Mathf.PI)) + lastPos.x,
				                                0,
				                                5 * Mathf.Cos(radomR * (2*Mathf.PI)) + lastPos.z );


				co.CreateConnectableObject(randomVec);

				Debug.Log(randomVec +" "+lastPos);

				lastAmp+=5;
				lastPos = randomVec;
			}
		}

		if (res > 0 && linkHealt < 100 && conList.Count > 0)
		{
			if (GUI.Button(new Rect(Screen.width-250,Screen.height-100,250,50), "Heal Link (-1 Energy)"))
			{
				SetLink(healtDamage);
				Camera.main.GetComponents<AudioSource>()[1].Play();
			}
		}
	}
}
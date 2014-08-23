using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConnectableObject:MonoBehaviour
{
	public ConObj controller;
	private ConResources cr;

	void OnCollisionEnter(Collision col)
	{
		Destroy(GetComponent<Rigidbody>());
	}

	void Start()
	{
		LoadPrefab();

		cr = controller.objectParent.GetComponent<ConResources>();
	}
	
	void Update()
	{
		FadeIn();

		ConnectableObjectHop();
		//SpawnConnectableObject();
	}
	
	/* Connectable Obj Connectivity */

	private ConnectableObject connectedConnectableObject;
	public void ConnectTo(ConnectableObject atmObj)
	{
		if (cr.res <= 0) return;

		cr.res--;

		connectedConnectableObject = atmObj;

		controller.objList.Add(atmObj);
		atmObj.spawn = true;
		
		DrawConnection(atmObj.gameObject);
	}

	public bool isConnected()
	{
		if (connectedConnectableObject == null)
		{
			return false;
		}
		else
		{
			return true;
		}
	}

	/* ConnectableObj Connectivity Render */

	private GameObject cyl;
	private GameObject cylPrefab;
	private const float CYL_SCALE = 0.25f;
	public string cylPath = "Mesh/Cylinder";
	public void LoadPrefab()
	{
		cylPrefab = Resources.Load<GameObject>(cylPath);
	}

	public void DrawConnection(GameObject atomP)
	{
		Vector3 offset = atomP.transform.position - this.transform.position;
		Vector3 scale = new Vector3(CYL_SCALE, offset.magnitude / 2.0f, CYL_SCALE);
		Vector3 position = this.transform.position + (offset /2.0f);
		
		cyl = (GameObject)Instantiate(cylPrefab, position, Quaternion.identity);
		cyl.transform.up = offset;
		cyl.transform.localScale = scale;
		cyl.transform.parent = controller.objectParent;

		cyl.renderer.material.shader = Shader.Find("Transparent/Diffuse");
		cyl.renderer.material.color = new Color(1.0f,1.0f,1.0f,cr.linkAlpha);

		cr.conList.Add(cyl);
	}

	/* Connectable Obj Animations */
	
	private float yInd = 0.0f;
	private Vector3 yVec;
	private void ConnectableObjectHop()
	{
		yInd = 0.05f * Mathf.Sin(Time.time * Mathf.PI);
		
		yVec = new Vector3(transform.position.x,
		                   yInd,
		                   transform.position.z);
		
		transform.position = yVec;
	}

	private bool fadedIn = false;
	private float alpha = 0.0f;
	private void FadeIn()
	{
		if (!fadedIn)
		{
			alpha += Time.deltaTime * 5.0f;

			if (alpha >= 1.0f)
			{
				alpha = 1.0f;
				fadedIn = true;

				renderer.material.shader = Shader.Find("Transparent/Cutout/Diffuse");
			}

			renderer.material.color = new Color(renderer.material.color.r,
			                                    renderer.material.color.g,
			                                    renderer.material.color.b,
			                                    alpha);
		}
	}

	/* Connectable Obj Reproduction */

	public bool spawn = false;
	private float passedTime = 0.0f;
	private void SpawnConnectableObject()
	{
		if (spawn)
		{
			passedTime += Time.deltaTime;
			if (passedTime >= controller.spawnTime)
			{
				Vector3 randomVec = new Vector3(Random.Range(transform.position.x + controller.minDist, transform.position.x + controller.maxDist),
				                                0,
				                                Random.Range(transform.position.z + controller.minDist, transform.position.z + controller.maxDist) );
				
				
				controller.CreateConnectableObject(randomVec);
				
				spawn = false;
			}
		}
	}
}
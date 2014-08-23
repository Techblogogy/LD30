using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConObj
{
	public Transform objectParent;
	private GameObject parentGo;

	public Material objMat;

	public List<ConnectableObject> objList = new List<ConnectableObject>();

	private static GameObject objectPrefab;

	public float spawnTime;
	
	ConResources cr;

	public float minDist;
	public float maxDist;
	
	public ConObj(GameObject go, string prefabPath, float spawnTime, float minDist, float maxDist)
	{
		parentGo = go;

		objectPrefab = Resources.Load<GameObject>(prefabPath);
		
		objectParent = parentGo.transform; //Set Public Parent
		CreateConnectableObject(new Vector3(0,0,0)); //Create Starter Object

		this.spawnTime = spawnTime;

		cr = parentGo.AddComponent<ConResources>();
		cr.co = this;
		cr.spawnTime = spawnTime;

		this.minDist = minDist;
		this.maxDist = maxDist;
	}

	public void CreateConnectableObject(Vector3 pos)
	{
		GameObject conObj = (GameObject)MonoBehaviour.Instantiate(objectPrefab, Vector3.zero, Quaternion.identity);
		conObj.name = "ConnetableObject";

		/*Rigidbody conBody = conObj.gameObject.AddComponent<Rigidbody>();
		conBody.useGravity = false;
		conBody.drag = Mathf.Infinity;
		conBody.angularDrag = Mathf.Infinity;*/

		conObj.renderer.material.shader = Shader.Find("Transparent/Diffuse");
		conObj.renderer.material.color = new Color(1.0f,1.0f,1.0f,0.0f);

		ConnectableObject conObjComp = conObj.AddComponent<ConnectableObject>();
		
		conObj.transform.parent = objectParent;
		conObj.transform.position = pos;

		conObjComp.controller = this;
		
		if (objList.Count == 0)
		{
			objList.Add(conObjComp);
			conObjComp.spawn = true;
		}

		Camera.main.GetComponents<AudioSource>()[1].Play();
	}	
}
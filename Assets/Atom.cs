using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Atom:MonoBehaviour
{
	private Atom connectedAtom;

	private GameObject cylPrefab;
	private GameObject cyl;

	private const float CYL_SCALE = 0.25f;

	private Vector3 yVec;

	void Start()
	{
		//AddSphereComponent();
		cylPrefab = Resources.Load<GameObject>("Cylinder");

		Debug.Log(cylPrefab);
	}

	void Update()
	{
		AtomHop();
		//ReDrawConnection();

		SpawnAtom();
	}

	float yInd = 0.0f;
	private void AtomHop()
	{
		yInd = 0.1f * Mathf.Sin(Time.time * Mathf.PI /10);

		yVec = new Vector3(transform.position.x,
		                   yInd,
		                   transform.position.z);

		transform.position = yVec;
	}

	public void AtomHit()
	{
		Debug.Log("AtomHit");
	}

	public void ConnectTo(Atom atmObj)
	{
		connectedAtom = atmObj;

		//atmObj.transform.parent = this.transform;
		AtomSpawner.atoms.Add(atmObj);
		atmObj.allowSpawn = true;

		DrawConnection(atmObj.gameObject);
	}

	public void DrawConnection(GameObject atomP)
	{
		Vector3 offset = atomP.transform.position - this.transform.position;
		Vector3 scale = new Vector3(CYL_SCALE, offset.magnitude / 2.0f, CYL_SCALE);
		Vector3 position = this.transform.position + (offset /2.0f);

		cyl = (GameObject)Instantiate(cylPrefab, position, Quaternion.identity);
		cyl.transform.up = offset;
		cyl.transform.localScale = scale;
	}

	public void ReDrawConnection()
	{
		if (cyl != null)
		{
			Vector3 offset = connectedAtom.gameObject.transform.position - this.transform.position;
			Vector3 scale = new Vector3(CYL_SCALE, offset.magnitude / 2.0f, CYL_SCALE);
			Vector3 position = this.transform.position + (offset /2.0f);

			cyl.transform.up = offset;
			cyl.transform.localScale = scale;
		}
	}

	public bool isConnected()
	{
		if (connectedAtom == null)
		{
			return false;
		}
		else
		{
			return true;
		}
	}

	public bool allowSpawn = false;
	private bool spawned = false;
	private float passedTime = 0.0f;
	private void SpawnAtom()
	{
		if (allowSpawn && !spawned)
		{
			passedTime += Time.deltaTime;
			if (passedTime >= 0.5f)
			{
				Vector3 randomVec = new Vector3( Random.Range(transform.position.x + 5.0f, transform.position.x - 5.0f) ,
				                                0,
				                                 Random.Range(transform.position.z + 5.0f, transform.position.z - 5.0f) );


				AtomSpawner.CreateAtom(randomVec);

				spawned = true;
			}
		}
	}
}
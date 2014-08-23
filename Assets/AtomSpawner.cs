using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AtomSpawner:MonoBehaviour
{
	public static int connetions = 0;
	public static Transform armSpwnr;
	public static List<Atom> atoms = new List<Atom>();

	private int atomCount = 0;

	void Start()
	{
		armSpwnr = this.transform;
		CreateAtom(new Vector3(0,0,0));

		/*for (int i=0; i<3; i++)
		{
			CreateAtom(new Vector3(Random.Range(0.0f,10.0f), 
			                       0, 
			                       Random.Range(0.0f,10.0f)));
		}*/
	}

	void Update()
	{
		//GenerateAtom();
	}

	public static void CreateAtom(Vector3 pos)
	{
		GameObject atom = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		atom.name = "Atom";
		Atom atomComp = atom.AddComponent<Atom>();

		atom.transform.parent = armSpwnr;
		atom.transform.position = pos;

		if (atoms.Count == 0)
		{
			atoms.Add(atomComp);
			atomComp.allowSpawn = true;
		}
	}

	private float passedTime = 0.0f;
	private void GenerateAtom()
	{
		if (atoms.Count > 0)
			passedTime += Time.deltaTime;
		
		if (passedTime >= 5.0f)
		{
			foreach (Atom atm in atoms)
			{
				CreateAtom(new Vector3(Random.Range(0.0f,10.0f), 
				                       0, 
				                       Random.Range(0.0f,10.0f)));
			}

			passedTime = 0.0f;
		}
	}
}
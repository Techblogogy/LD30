using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoodEnd:GameState
{
	public GoodEnd(string objName, string meshP, string clipPath, float minDist, float maxDist, float spawnTime) : base(objName,meshP,clipPath,minDist,maxDist,spawnTime)
	{
		
	}
	
	public override void StageStart ()
	{
		//base.StageStart ();

		CameraGUI.setGoodEnd = true;
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
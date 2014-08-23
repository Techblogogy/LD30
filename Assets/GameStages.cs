using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStages:MonoBehaviour
{
	public List<GameState> states = new List<GameState>();
	private int curStateId = 0;

	void Start()
	{
		InitStates();

		states[curStateId].StageStart();
	}

	void Update()
	{
		states[curStateId].StageUpdate();
	}

	private void InitStates()
	{
		states.Add(new GameState("Atom", "Mesh/Atom", "Music/Cycle1", 5.0f, -5.0f, 5.0f));
		states.Add(new GameState("Cell", "Mesh/SphereL", "Music/Cycle2", 5.0f, -5.0f, 5.0f));
		states.Add(new GoodEnd("GoodEnd", null, null, 0, 0, 0));
		states.Add(new BadEnding("EndBad", null, null, 0, 0, 0));
	}

	public void NextState()
	{
		states[curStateId].StageEnd();
		curStateId++;
		states[curStateId].StageStart();
	}

	public void SetBadEnd()
	{
		states[curStateId].StageEnd();
		curStateId=states.Count-1;
		states[curStateId].StageStart();
	}

	public void SetGoodEnd()
	{

	}
}
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
		states.Add(new MenuState("SoundFX/Voice/Voice1"));
		states.Add(new GameState("Atom", "Mesh/Atom", "Music/Cycle1", -5.0f, 5.0f, 5.0f, 5, "SoundFX/Voice/Voice2"));
		states.Add(new GameState("Molecule", "Mesh/Molecule", "Music/Cycle2", 5.0f, -5.0f, 10.0f, 10, "SoundFX/Voice/Voice3"));
		states.Add(new GameState("Cell", "Mesh/Cell", "Music/Cycle3", 5.0f, -5.0f, 15.0f, 15, "SoundFX/Voice/Voice4"));
		states.Add(new GameState("Organism", "Mesh/Organism", "Music/Cycle4", 5.0f, -5.0f, 20.0f, 20, "SoundFX/Voice/Sound5"));
		states.Add(new GoodEnd("Music/Cycle5", "SoundFX/Voice/Sound6"));
		states.Add(new BadEnding());

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
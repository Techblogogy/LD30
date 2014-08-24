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

public class CameraGUI:MonoBehaviour
{
	private Texture2D guiTexture;

	public static bool drawHealButton;
	public static bool buttonPressed = false;

	public static bool setBadEnd = false;
	public static bool setGoodEnd = false;

	public static bool setMenu = false;

	private GUISkin gs;

	private Texture2D logoTex;
	private Texture2D nameTex;

	void Start()
	{
		LoadTexture();

		gs = Resources.Load<GUISkin>("GameGUI");
		logoTex = Resources.Load<Texture2D>("Logo/TB-Symbol");
		nameTex = Resources.Load<Texture2D>("Logo/HOCMAlogo");
	}

	void Update()
	{

	}

	private void LoadTexture()
	{
		guiTexture = Resources.Load<Texture2D>("GUI/GUIBar");
	}

	void OnGUI()
	{
		GUI.skin = gs;

		if (!setBadEnd && !setGoodEnd && !setMenu)
		{
			//GUI.DrawTexture(new Rect(0,0,Screen.width,guiTexture.height/2), guiTexture);
		}
		else if (setBadEnd)
		{
			GUI.Label(new Rect(Screen.width/2-500/2,Screen.height/2-100/2, 500,100), "Game Over \nWatch 'Link Health' Closely Next Time");
		}
		else if (setGoodEnd)
		{
			GUI.TextArea(new Rect(0,0,100,30), "You Won");
		}
		else if (setMenu)
		{
			GUI.DrawTexture(new Rect(Screen.width-96-10,Screen.height-96,96,96), logoTex);
			GUI.Label(new Rect(Screen.width/2-120/2,0,120,50), "H.O.C.M.A");
			GUI.Label(new Rect(Screen.width/2-500/2,Screen.height-50, 500,50), "WARNING: Patience and Thinking are required");

			if (GUI.Button(new Rect(Screen.width/2-50,Screen.height/2-25,100,50), "Play"))
				MenuState.startGame = true;
		}
	}
}
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

	void Start()
	{
		LoadTexture();
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
		if (!setBadEnd && !setGoodEnd)
		{
			GUI.DrawTexture(new Rect(0,0,Screen.width,guiTexture.height/2), guiTexture);
		}
		else if (setBadEnd)
		{
			GUI.TextArea(new Rect(0,0,100,30), "Game Over");
		}
		else if (setGoodEnd)
		{
			GUI.TextArea(new Rect(0,0,100,30), "You Won");
		}
	}
}
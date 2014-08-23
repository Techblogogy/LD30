using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraClick:MonoBehaviour
{
	private float camVel = 2.0f;

	void Start()
	{
		/*cc = gameObject.AddComponent<CharacterController>(); //Add Character Controller
		cc.detectCollisions = false;*/
	}

	void Update()
	{
		MoveCameraAnim();

		CameraRotation();
		if (!allowMoveAnim)
			MoveCamera();

		ObjCheck();
	}

	/* Camera Movement */

	private CharacterController cc;
	private Vector3 transformVector = Vector3.right; //Horizonal Transform
	private Vector3 transformDirection; //Vertical Transform
	private float zImp = 0.0f;
	private void MoveCamera()
	{
		if (Input.GetKey(KeyCode.A))
		{
			transformVector = Vector3.left;
			camVel = 12.0f;
		}
		else if (Input.GetKey(KeyCode.D))
		{
			transformVector = Vector3.right;
			camVel = 12.0f;
		}
		else
		{
			camVel = 0.0f;
		}

		if (Input.GetKey(KeyCode.W))
		{
			zImp = 5.0f;
		}
		else if (Input.GetKey(KeyCode.S))
		{
			zImp = -5.0f;
		}
		else
		{
			zImp = 0.0f;
		}
		
		transformDirection = new Vector3(0,0,zImp);
		transformDirection = transform.TransformDirection(Vector3.forward);
		transformDirection *= zImp; 
		
		transform.Translate(transformDirection * Time.deltaTime);
	}

	/* Camera Rotation */

	private Vector3 centerObj = new Vector3(0,0,0);
	private void CameraRotation()
	{
		transform.LookAt(centerObj);

		if (!allowMoveAnim)
			transform.Translate(transformVector * (Time.deltaTime*camVel));
	}

	/* Camera Interactions */

	private GameObject rayObj;
	private GameObject CamCast(float dist)
	{
		Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit rh;
		if (Physics.Raycast(camRay, out rh,dist) && Input.GetButtonDown("Fire1"))
		{
			return rh.collider.gameObject;
		}
		else return null;
	}

	/* Raycast Object Switching */
	
	private void ObjCheck()
	{
		rayObj = CamCast(100.0f);
		
		if (rayObj == null) return;
			
		if (rayObj.GetComponent<ConnectableObject>() != null)
			ObjectConnection();
	}

	private ConnectableObject tempObject;
	private bool selectedObject = false;
	private void ObjectConnection()
	{
		if (!selectedObject)
		{
			if (!rayObj.GetComponent<ConnectableObject>().isConnected())
			{
				tempObject = rayObj.GetComponent<ConnectableObject>();
				tempObject.gameObject.renderer.material.color = new Color(0,1.0f,0,1.0f); //TEMP: Change Selected Material Color
				selectedObject = true;

				//centerObj = Vector3.MoveTowards(centerObj, tempObject.transform.position, Time.deltaTime *2.0f); //TEMP: Move Camera Rotation Point
				moveTo = tempObject.transform.position;
				allowMoveAnim = true;
			}
		}
		else
		{
			if (tempObject != rayObj.GetComponent<ConnectableObject>() && !rayObj.GetComponent<ConnectableObject>().isConnected())
			{
				tempObject.ConnectTo(rayObj.GetComponent<ConnectableObject>());

				moveTo = rayObj.transform.position;
				allowMoveAnim = true;
			}

			tempObject.gameObject.renderer.material.color = new Color(1.0f,1.0f,1.0f,1.0f); //TEMP: Change Selected Material Color
			
			tempObject = null;
			selectedObject = false;
		}
	}

	/* Camera Animation */
	public static Vector3 moveTo = Vector3.zero;
	public bool allowMoveAnim = false;
	private void MoveCameraAnim()
	{
		if (allowMoveAnim)
		{
			centerObj = Vector3.MoveTowards(centerObj, moveTo, Time.deltaTime*10.0f);
			transform.position = new Vector3(centerObj.x+5,
			                                 centerObj.y+5,
			                                 centerObj.z+5);
			
			Vector3 dist = moveTo - centerObj;
			if (dist.normalized == Vector3.zero) allowMoveAnim = false;
		}
	}
}
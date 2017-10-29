using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour {

	private Transform _xForm_Camera;
	private Transform _xForm_Parent;

	private Vector3 _LocalRotation;
	private float _CameraDistance = 10f;

	public float mouseSens = 4f;
	public float mouseScroll = 2f;
	public float OrbitSpeed = 10f;
	public float ScrollSpeed = 6f;

	public bool CameraDisabled = false;

	// Use this for initialization
	void Start () {
		this._xForm_Camera = this.transform;
		this._xForm_Parent = this.transform.parent;

	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			CameraDisabled = !CameraDisabled;
		}
		if (!CameraDisabled) {
			if (Input.GetAxis ("Mouse X") != 0 || Input.GetAxis ("Mouse Y") != 0) {
				_LocalRotation.x += Input.GetAxis ("Mouse X") * mouseSens;
				_LocalRotation.y -= Input.GetAxis ("Mouse Y") * mouseSens;
				//pivot lock
				_LocalRotation.y = Mathf.Clamp (_LocalRotation.y, 0f, 90f);
			}
			//zoom via scroll
			if(Input.GetAxis("Mouse ScrollWheel") != 0f){
				float ScrollAmount = Input.GetAxis ("Mouse ScrollWheel") * ScrollSpeed;
				ScrollAmount *= (this._CameraDistance * 0.3f);

				this._CameraDistance += ScrollAmount * -1f;
				//scroll between 1.5 m and 100m.
				this._CameraDistance = Mathf.Clamp (this._CameraDistance, 1.5f, 100f);
			}
		}

		//actual camera orientation
		Quaternion QT = Quaternion.Euler(_LocalRotation.y,_LocalRotation.x,0);
		//ease out effect.
		this._xForm_Parent.rotation = Quaternion.Lerp (this._xForm_Parent.rotation, QT, Time.deltaTime * OrbitSpeed);

		if (this._xForm_Camera.localPosition.z != this._CameraDistance * -1f) {
			this._xForm_Camera.localPosition = new Vector3(0f,0f,Mathf.Lerp(this._xForm_Camera.localPosition.z,this._CameraDistance * -1f,Time.deltaTime * ScrollSpeed));
		}
	}
}

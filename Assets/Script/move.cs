using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {

	Rigidbody body;
	float forceScale = 1000;

	float slowScale = 0.9f;

	bool stopX = true;
	bool stopZ = true;
	
	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		if(Input.GetKeyDown(KeyCode.W)){
			body.AddForce(0, 0, forceScale);
			stopZ = false;
		}
		else if(Input.GetKeyUp(KeyCode.W)){
			stopZ = true;
		}
		if(Input.GetKeyDown(KeyCode.S)){
			body.AddForce(0, 0, -forceScale);
			stopZ = false;
		}
		else if(Input.GetKeyUp(KeyCode.S)){
			stopZ = true;
		}
		if(Input.GetKeyDown(KeyCode.A)){
			body.AddForce(-forceScale, 0, 0);
			stopX = false;
		}
		else if(Input.GetKeyUp(KeyCode.A)){
			stopX = true;
		}
		if(Input.GetKeyDown(KeyCode.D)){
			body.AddForce(forceScale, 0, 0);
			stopX = false;
		}
		else if(Input.GetKeyUp(KeyCode.D)){
			stopX = true;
		}

		if(stopX && body.velocity.x != 0){
			float nx = body.velocity.x*slowScale;
			if(Mathf.Approximately(nx,0))
				nx = 0;
			body.velocity = new Vector3(nx,body.velocity.y, body.velocity.z);
		}
		if(stopZ && body.velocity.z != 0){
			float nz = body.velocity.z*slowScale;
			if(Mathf.Approximately(nz,0))
				nz = 0;
			body.velocity = new Vector3(body.velocity.x,body.velocity.y, nz);
		}

		
	}
}

﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float maxSpeed = 5f;
	public float rotSpeed = 180f;

	float shipBoundaryRadius = 0.5f;

	public Animator[] rocketfires;

	void Start () {
	
	}
	
	void Update () {

		// ROTATE the ship.

		// Grab our rotation quaternion
		Quaternion rot = transform.rotation;

		// Grab the Z euler angle
		float z = rot.eulerAngles.z;

		// Change the Z angle based on input
		z -= Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;

		// Recreate the quaternion
		rot = Quaternion.Euler( 0, 0, z );

		// Feed the quaternion into our rotation
		transform.rotation = rot;

		// MOVE the ship.
		Vector3 pos = transform.position;
		 
		Vector3 velocity = new Vector3(0, Input.GetAxis("Vertical") * maxSpeed * Time.deltaTime, 0);
		float currPlayerSpeed = Input.GetAxis("Vertical") * maxSpeed;
		pos += rot * velocity;

		// RESTRICT the player to the camera's boundaries!

		// First to vertical, because it's simpler
		if(pos.y+shipBoundaryRadius > Camera.main.orthographicSize) {
			pos.y = Camera.main.orthographicSize - shipBoundaryRadius;
		}
		if(pos.y-shipBoundaryRadius < -Camera.main.orthographicSize) {
			pos.y = -Camera.main.orthographicSize + shipBoundaryRadius;
		}

		// Now calculate the orthographic width based on the screen ratio
		float screenRatio = (float)Screen.width / (float)Screen.height;
		float widthOrtho = Camera.main.orthographicSize * screenRatio;

		// Now do horizontal bounds
		if(pos.x+shipBoundaryRadius > widthOrtho) {
			pos.x = widthOrtho - shipBoundaryRadius;
		}
		if(pos.x-shipBoundaryRadius < -widthOrtho) {
			pos.x = -widthOrtho + shipBoundaryRadius;
		}

		// Finally, update our position!!
		transform.position = pos;

		foreach (Animator fire in rocketfires) {
			fire.SetFloat("speed", currPlayerSpeed);
		}

	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.CompareTag("powerup")) {
		
			powers power = collision.gameObject.GetComponent<powerUP>().currentPowerup;
			Debug.Log(power.ToString());
			switch (power){
				case powers.health:
					GetComponent<DamageHandler>().heal();
					return;
				case powers.shield:
					GetComponent<DamageHandler>().shields();
					return;
				case powers.heavymachine:
					GetComponent<PlayerShooting>().HeavyMachineGun();
					return;
			}
		}
    }
}

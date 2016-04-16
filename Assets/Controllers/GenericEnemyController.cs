﻿using UnityEngine;
using System.Collections;

public class GenericEnemyController : MonoBehaviour {

	private Vector3 SMPos;
	private Vector3 SMDestPos;
	private int SMLastMove = 0;
	private float SMNextMove = 0.0f;
	private float nextFireTime = 0.0f;
	private float swayRotation = 0.0f;
	private Vector3 bulletOffset = new Vector3( 0, 0, -1 );

	public bool CanShoot = true;
	public int ShootStyle = 0;
	public bool FlyTowardsPlayer = false;
	public bool SwayMovement = false;
	public bool StrangeMovement = false;
	public float fireRate = .2f;
	public float movementSpeed = 25.0f;

	public GameObject bulletPrefab;
	public GameObject playerObject;

	GameObject CreateBullet( Vector3 pos ) {
		return CreateBullet (pos, Quaternion.LookRotation (pos - playerObject.transform.position) );
	}

	GameObject CreateBullet( Vector3 pos, Quaternion angle ) {

		var gameBullet = (GameObject)Instantiate (bulletPrefab, pos, Quaternion.Euler( 0, 0, 0 ) );
		gameBullet.transform.rotation = angle;
		gameBullet.GetComponent<BulletController> ().movementSpeed = gameBullet.GetComponent<BulletController> ().movementSpeed + movementSpeed + this.transform.parent.GetComponent<MovingSceneController> ().movementSpeed;
		return gameBullet;

	}

	// Use this for initialization
	void Start () {
		nextFireTime = Time.time + fireRate;
		SMPos = SMDestPos = this.transform.position;
		SMNextMove = Time.time + 1.0f;
	}

	void LateUpdate() { 
		if (StrangeMovement) {
			if (SMNextMove < Time.time) {
				SMNextMove = Time.time + 0.8f;
				int typeMovement = Random.Range( 0, 4 );
				Vector3 diff = new Vector3 (0, 0, 0);
				float moveDist = movementSpeed * .25f;

				if (SMPos.y > 5) {
					typeMovement = 0;
				}

				if (SMPos.y < -5) {
					typeMovement = 1;
				}

				if (SMLastMove < 2)
					typeMovement = 2;

				switch (typeMovement) {
					case 0:
					diff.y = moveDist; 
						break;
					case 1:
					diff.y = -moveDist;
						break;
					default:
					diff.z = moveDist;
						break;

				}

				SMLastMove = typeMovement;

				SMDestPos = SMPos - diff;
			}
			SMPos = Vector3.Lerp (SMPos, SMDestPos, .5f);
			this.transform.position = SMPos;
		}
	}

	void FixedUpdate() { 

		if (SwayMovement) {

			Vector3 curPos = this.transform.position;
			swayRotation =  ( ( swayRotation > 360.0f )? swayRotation - 360.0f : swayRotation ) + ( movementSpeed * 25.0f ) * Time.deltaTime;
			curPos.y = curPos.y + .45f * Mathf.Sin( Mathf.Deg2Rad * swayRotation );
			this.transform.position = curPos;

		}

	}

	// Update is called once per frame
	void Update () {

		bool pastPlayer = (this.transform.position.z - 5 < playerObject.transform.position.z);
		bool withinShootingRange = (this.transform.position.z - 45 < playerObject.transform.position.z );

		this.transform.Translate(0f, 0f, -Time.deltaTime * movementSpeed);

		if (!pastPlayer) {

			if (FlyTowardsPlayer) {

				this.transform.rotation = Quaternion.LookRotation (this.transform.position - playerObject.transform.position);

			}

		}

		if (CanShoot && withinShootingRange && Time.time > nextFireTime) {
			nextFireTime = Time.time + fireRate;
			Vector3 bulletCenterPos = this.transform.position + bulletOffset;
			switch (ShootStyle) {
				case 1:
					if( pastPlayer )
						CreateBullet (bulletCenterPos, Quaternion.Euler( 0,0,0 ));
					else
						CreateBullet (bulletCenterPos);
					break;
				case 2:
					CreateBullet (bulletCenterPos, Quaternion.Euler (20, 0, 0));
					CreateBullet (bulletCenterPos, Quaternion.Euler (0, 0, 0));
					CreateBullet (bulletCenterPos, Quaternion.Euler (-20, 0, 0));
					break;
				case 3:
					CreateBullet (bulletCenterPos, Quaternion.Euler (0, -20, 0));
					CreateBullet (bulletCenterPos, Quaternion.Euler (0, 0, 0));
					CreateBullet (bulletCenterPos, Quaternion.Euler (0, 20, 0));
					break;
				case 4:
					for (int j = 0; j < 8; j++) {
						CreateBullet (bulletCenterPos, Quaternion.Euler (j * 45, 0, 0));
					}
					break;
				case 5:
					for (int j = 0; j < 8; j++) {
						CreateBullet (bulletCenterPos, Quaternion.Euler (0, j * 45, 0));
					}	
					break;
				default:
					CreateBullet (bulletCenterPos, Quaternion.Euler( 0,0,0 ));
					break;
			}
		}


	}
}
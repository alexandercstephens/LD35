using UnityEngine;
using System.Collections;

public class GenericEnemyController : MonoBehaviour {

	private Vector3 SMPos;
	private Vector3 SMDestPos;
    private bool SMinit = false;
	private int SMLastMove = 0;
	private float SMNextMove = 0.0f;
	private float nextFireTime = 0.0f;
	private float swayRotation = 0.0f;
	private float totalSpeed = 0.0f;
	private float SceneSpeed = 0.0f;
	private Vector3 bulletOffset = new Vector3( 0, 0, -1 );
	private CameraController gameCamera;

	public bool CanShoot = true;
	public float swaySpeed = 10.0f;
	public int ShootStyle = 0;
	public bool FlyTowardsPlayer = false;
	public bool SwayMovement = false;
	//public bool StrangeMovement = false;
	public bool ShootWithBeat = false;
	public float fireRate = .2f;
	public float movementSpeed = 25.0f;

	public GameObject bulletPrefab;
	private GameObject playerObject;
    
    private BeatController beatController;
    private MovingSceneController checkpoint = null;

	GameObject CreateBullet( Vector3 pos ) {
		return CreateBullet (pos, Quaternion.LookRotation (pos - playerObject.transform.position) );
	}

	GameObject CreateBullet( Vector3 pos, Quaternion angle ) {

		var gameBullet = (GameObject)Instantiate (bulletPrefab, pos, Quaternion.Euler( 0, 0, 0 ) );
		gameBullet.transform.rotation = angle * this.transform.rotation;
		gameBullet.GetComponent<BulletController> ().movementSpeed = gameBullet.GetComponent<BulletController> ().movementSpeed + movementSpeed + SceneSpeed;
		return gameBullet;

	}

	// Use this for initialization
	void Start () {

		nextFireTime = Time.time + fireRate;
		
		SMNextMove = Time.time + 1.0f;
		gameCamera = GameObject.Find ("CameraManager").GetComponent<CameraController> ();
		playerObject = GameObject.Find ("Player");

        beatController = GameObject.Find("BeatController").GetComponent<BeatController>();
    }

	void LateUpdate() { 
		//if (StrangeMovement) {
        //
        //    if (!SMinit)
        //    {
        //        SMinit = true;
        //        SMPos = SMDestPos = this.transform.position;
        //    }
        //
		//	if (SMNextMove < Time.time) {
		//		SMNextMove = Time.time + 0.9f;
		//		int typeMovement = Random.Range( 0, 8 );
		//		Vector3 diff = new Vector3 (0, 0, 0);
		//		float moveDist = totalSpeed * .25f;
		//		bool top = gameCamera.checkIsTopDown ();
        //
		//		if( SMLastMove > 1 )
		//			typeMovement = Random.Range( 0, 2 );
		//		
        //
		//		if ((!top && SMPos.y > 5)  || (top && SMPos.x > 7)) {
		//			typeMovement = 0;
		//		}
        //
		//		if ((!top && SMPos.y < -5) || (top && SMPos.x < -7 )) {
		//			typeMovement = 1;
		//		}
        //
		//		if (SMLastMove < 2)
		//			typeMovement = 2;
		//			
		//		switch (typeMovement) {
		//			case 0:
		//			if (top)
		//				diff.x = moveDist;
		//			else
		//				diff.y = moveDist;
		//				break;
		//		case 1:
		//			if (top)
		//				diff.x = -moveDist;
		//			else
		//				diff.y = -moveDist;
		//				break;
		//			default:
		//			diff.z = moveDist * 1.4f;
		//				break;
        //
		//		}
        //
		//		SMLastMove = typeMovement;
        //
		//		SMDestPos = SMPos - diff;
		//	}
        //    
		//	SMPos = Vector3.Lerp (SMPos, SMDestPos, .4f);
		//	this.transform.position = SMPos;
        //
		//}

        
	}

	void FixedUpdate() { 

		if (SwayMovement) {

			Vector3 curPos = this.transform.position;
			float delta = (swaySpeed * 25.0f);

			swayRotation =  ( ( swayRotation > 360.0f )? swayRotation - 360.0f : swayRotation ) + delta * Time.deltaTime;
			curPos.y = curPos.y + .08f * Mathf.Sin( Mathf.Deg2Rad * swayRotation );
            curPos.x = curPos.x + .08f * Mathf.Sin(Mathf.Deg2Rad * swayRotation);
			this.transform.position = curPos;
           
		}

	}

	// Update is called once per frame
	void Update () {
        //if (checkpoint == null) checkpoint = GameObject.FindWithTag("Checkpoint").GetComponent<MovingSceneController>();
        //SceneSpeed = checkpoint.movementSpeed;
		//totalSpeed = SceneSpeed + movementSpeed;
        //
		bool pastPlayer = (this.transform.position.z - 5 < playerObject.transform.position.z);
		bool withinShootingRange = (this.transform.position.z - 65 < playerObject.transform.position.z );
        //
        if( this.transform.position.z - 35 < playerObject.transform.position.z )
			this.transform.Translate(0f, 0f, -Time.deltaTime * movementSpeed);
        //
		//if (!pastPlayer) {
        //
		//	if (FlyTowardsPlayer) {
        //
		//		this.transform.rotation = Quaternion.LookRotation (this.transform.position - playerObject.transform.position);
        //
		//	}
        //
		//}

		bool beatShot = beatController.IsOnBeat ();


		if (CanShoot && withinShootingRange && ( Time.time > nextFireTime || ( ShootWithBeat && beatShot ) ) ) {
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
				CreateBullet (bulletCenterPos, Quaternion.Euler ( 20, 0, 0));
				CreateBullet (bulletCenterPos, Quaternion.Euler ( 0, 0, 0));
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

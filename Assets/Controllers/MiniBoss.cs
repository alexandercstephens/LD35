using UnityEngine;
using System.Collections;

public class MiniBoss : MonoBehaviour
{
	public GameObject miniBossShot;

	public Transform miniBossProtector;
	private Vector3 goalProtectorPosition;
	private Vector3 protectorVelocity;

	private BeatController beatController;
	private string state;

	private Vector3 goalPosition;
	private Vector3 velocity;

	private int beatCount = -4;

	void Start ()
	{
		goalPosition = transform.localPosition + new Vector3 (0f, 0f, -15f);
		goalProtectorPosition = miniBossProtector.localPosition + new Vector3 (0f, 0f, -15f);
		Move ();
		MoveProtector ();
		beatController = GameObject.Find ("BeatController").GetComponent<BeatController> ();
	}

	void Update ()
	{
		if (beatController.IsOnBeat ()) {
			beatCount += 1;
			if (beatCount <= 19) {
				switch (beatCount) {
				case 0:
				case 1:
				case 2:
				case 3:
				case 4:
				case 5:
				case 6:
				case 7:
				case 12:
				case 14:
					StartCoroutine ("AttackEasy");
					break;
				case 16:
				case 17:
				case 18:
				case 19:
					StartCoroutine ("Attack1");
					break;
				case 8:
				case 10:
				case 15:
					StartCoroutine ("Attack3");
					break;
				case 9:
				case 11:
				case 13:
					StartCoroutine ("Attack4");
					break;
				}
			} else {
				goalPosition = transform.localPosition + new Vector3 (0f, 0f, 40f);
				Move ();
			}
		}

		transform.localPosition += velocity * Time.deltaTime;
		miniBossProtector.localPosition += protectorVelocity * Time.deltaTime;
		switch (state) {
		case "waking":
				
			break;
		}
	}

	IEnumerator Attack1 ()
	{
		var firstShot = (GameObject)Instantiate (miniBossShot, transform.position, transform.rotation);
		firstShot.transform.parent = transform.parent;

		var firstShotComponent = firstShot.GetComponent<MiniBossShot> ();
		firstShotComponent.MoveTo (new Vector3 (0f, 0f, 7.1f), 0.444444444f);
		firstShotComponent.Fire (1.7777777777f);

		var randomAtPlayer = beatCount % 5;
		if (randomAtPlayer == 4)
			randomAtPlayer = beatCount % 4;
		
		for (var i = 0; i < 4; i++) {
			var shot = (GameObject)Instantiate (miniBossShot, transform.position, transform.rotation);
			shot.transform.parent = transform.parent;

			var shotComponent = shot.GetComponent<MiniBossShot> ();

			if (i == 0)
				shotComponent.MoveTo (new Vector3 (14f, 10f, 7.35f), 0.444444444f);
			else if (i == 1)
				shotComponent.MoveTo (new Vector3 (7f, 5f, 7.35f), 0.444444444f);
			else if (i == 2)
				shotComponent.MoveTo (new Vector3 (-7f, -5f, 7.35f), 0.444444444f);
			else if (i == 3)
				shotComponent.MoveTo (new Vector3 (-14f, -10f, 7.35f), 0.444444444f);

			if (i == randomAtPlayer)
				shotComponent.FireAtPlayer ();

			shotComponent.Fire ((4 - i) * 0.44444444f);
			yield return new WaitForSeconds (0.444444444f);
		}
	}

	IEnumerator Attack2 ()
	{
		var randomAtPlayer = beatCount % 9;
		if (randomAtPlayer == 8)
			randomAtPlayer = beatCount % 8;
		//var randomAtPlayer2 = (beatCount * 2) % 9;
		for (var i = 0; i < 9; i++) {
			var shot = (GameObject)Instantiate (miniBossShot, transform.position, transform.rotation);
			shot.transform.parent = transform.parent;

			var shotComponent = shot.GetComponent<MiniBossShot> ();

			if (i == 0)
				shotComponent.MoveTo (new Vector3 (10f, 10f, 7f), 0.222222222222f);
			else if (i == 1)
				shotComponent.MoveTo (new Vector3 (5f, 5f, 7f), 0.222222222222f);
			else if (i == 2)
				shotComponent.MoveTo (new Vector3 (-5f, -5f, 7f), 0.222222222222f);
			else if (i == 3)
				shotComponent.MoveTo (new Vector3 (-10f, -10f, 7f), 0.222222222222f);
			else if (i == 4)
				shotComponent.MoveTo (new Vector3 (10f, -10f, 11f), 0.222222222222f);
			else if (i == 5)
				shotComponent.MoveTo (new Vector3 (5f, -5f, 11f), 0.222222222222f);
			else if (i == 6)
				shotComponent.MoveTo (new Vector3 (-5f, 5f, 11f), 0.222222222222f);
			else if (i == 7)
				shotComponent.MoveTo (new Vector3 (-10f, 10f, 11f), 0.222222222222f);

			if (i == randomAtPlayer)// || i == randomAtPlayer2)
				shotComponent.FireAtPlayer ();

			shotComponent.Fire ((8 - i) * 0.222222222f);
			yield return new WaitForSeconds (0.2222222222f);
		}
	}

	IEnumerator Attack3 ()
	{
		for (var i = 0; i < 12; i++) {
			for (var j = 0; j < 4; j++) {
				var shot = (GameObject)Instantiate (miniBossShot, transform.position, transform.rotation);
				shot.transform.parent = transform.parent;

				var shotComponent = shot.GetComponent<MiniBossShot> ();

				shotComponent.MoveTo (new Vector3 (j * 8f - 12f, i * 2f - 11f, j * 1f + 7f), 0.148148148148148f);

				shotComponent.Fire ((12 - i) * 0.148148148148148f);
			}
			yield return new WaitForSeconds (0.148148148148148f);
		}
	}

	IEnumerator Attack4 ()
	{
		for (var i = 0; i < 16; i++) {
			for (var j = 0; j < 4; j++) {
				var shot = (GameObject)Instantiate (miniBossShot, transform.position, transform.rotation);
				shot.transform.parent = transform.parent;

				var shotComponent = shot.GetComponent<MiniBossShot> ();

				shotComponent.MoveTo (new Vector3 (i * 2f - 15f, j * 6.66666666666f - 10f, j * 1f + 7f), 0.111111111111111f);

				shotComponent.Fire ((16 - i) * 0.111111111111111f);
			}
			yield return new WaitForSeconds (0.111111111111111f);
		}
	}

	IEnumerator AttackEasy ()
	{
		var randomAtPlayer = beatCount % 5;
		var randomNoAttack = (2 * beatCount) % 5;
		if (randomNoAttack == randomAtPlayer) {
			randomNoAttack = (randomNoAttack + 1) % 5;
		}

		var j = 0;
		for (var i = 0; i < 5; i++) {
			if (i == randomNoAttack) {
				continue;
			}

			var shot = (GameObject)Instantiate (miniBossShot, transform.position, transform.rotation);
			shot.transform.parent = transform.parent;

			var shotComponent = shot.GetComponent<MiniBossShot> ();
			shotComponent.MoveTo (new Vector3 (14f - 7f * i, 10f - 5f * i, 7.35f), 0.44444444f);

			if (i == randomAtPlayer)
				shotComponent.FireAtPlayer ();

			shotComponent.Fire ((4 - j) * 0.44444444f);
			j += 1;
			yield return new WaitForSeconds (0.444444444f);
		}
	}

	private void Move ()
	{
		velocity = (goalPosition - transform.localPosition) / 7.111111111111f;
		Invoke ("StopMove", 7.111111111111f);
	}

	private void StopMove ()
	{
		velocity = Vector3.zero;
		transform.localPosition = goalPosition;
	}

	private void MoveProtector ()
	{
		protectorVelocity = (goalProtectorPosition - miniBossProtector.localPosition) / 7.111111111111f;
		Invoke ("StopProtector", 7.111111111111f);
	}

	private void StopProtector ()
	{
		protectorVelocity = Vector3.zero;
		miniBossProtector.localPosition = goalProtectorPosition;
	}
}

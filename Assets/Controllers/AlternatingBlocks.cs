using UnityEngine;
using System.Collections;

public class AlternatingBlocks : MonoBehaviour
{
	public GameObject alternate;
	public bool startAlternate = false;
	public float timeBetweenSwitch;
	public Material inactiveMaterial;

	private Collider thisCollider;
	private Renderer thisRenderer;
	private Material thisMaterial;

	private Collider alternateCollider;
	private Renderer alternateRenderer;
	private Material alternateMaterial;

	private bool isAlternate;

	void Awake ()
	{
		thisCollider = GetComponent<Collider> ();
		thisRenderer = GetComponent<Renderer> ();
		thisMaterial = thisRenderer.material;

		alternateCollider = alternate.GetComponent<Collider> ();
		alternateRenderer = alternate.GetComponent<Renderer> ();
		alternateMaterial = alternateRenderer.material;
	}

	void Start ()
	{
		InvokeRepeating ("Alternate", 0f, timeBetweenSwitch);
		isAlternate = !startAlternate;
		Alternate ();
	}

	private void Alternate ()
	{
		if (isAlternate) {
			thisCollider.enabled = true;
			thisRenderer.material = thisMaterial;
			alternateCollider.enabled = false;
			alternateRenderer.material = inactiveMaterial;
			isAlternate = false;
		} else {
			thisCollider.enabled = false;
			thisRenderer.material = inactiveMaterial;
			alternateCollider.enabled = true;
			alternateRenderer.material = thisMaterial;
			isAlternate = true;
		}
	}
}

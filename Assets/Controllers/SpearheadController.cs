using UnityEngine;
using System.Collections;

public class SpearheadController : MonoBehaviour {
    private bool isActive = false;
    private float timeTurnedActive; //needed to prevent isActive from immediately switching off
    private Vector3 originalPosition;
    private Vector3 originalScale;

	// Use this for initialization
	public void Spear ()
    {
        transform.localPosition = new Vector3(0f, 0f, 10f + 0.5f);
        transform.localScale = new Vector3(1f, 1f, 20f);

        isActive = true;
        timeTurnedActive = Time.time;
    }

    void Awake()
    {
        originalPosition = transform.localPosition;
        originalScale = transform.localScale;
    }

    void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, 0.1f);
        transform.localScale = Vector3.Lerp(transform.localScale, originalScale, 0.1f);
        if (transform.localPosition.x - originalPosition.x < 0.01f && isActive && Time.time - timeTurnedActive > 0.1f)
        {
            isActive = false;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (isActive && collider.tag == "HurtsPlayer")
        {
            Destroy(collider.gameObject);//TODO make enemy killing more robust
        }
    }
}

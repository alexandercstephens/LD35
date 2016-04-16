using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float movementSpeed = 1f;

    private bool isTopDown;

    // Use this for initialization
    void Awake () {
        Transform(true);
    }
	
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Transform(!isTopDown);
        }
        Move();
    }

    //transforms player between vertical alignment (top-down view)
    //and horizontal alignment (side view)
    private void Transform (bool itd) //isTopDown
    {
        this.transform.localScale = itd ? new Vector3(1f, 20f, 1f) : new Vector3(20f, 1f, 1f);
        if (itd)
        {
            this.transform.localPosition = new Vector3(transform.localPosition.x, 0f, transform.localPosition.z);
        }
        else
        {
            this.transform.localPosition = new Vector3(0f, transform.localPosition.y, transform.localPosition.z);
        }
        isTopDown = itd;
    }

    //checks for movement input and moves character
    private void Move ()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        Vector3 movementVector;
        if (isTopDown)
        {
            movementVector = new Vector3(horizontalMovement, 0f, verticalMovement);
        }
        else
        {
            movementVector = new Vector3(0f, verticalMovement, horizontalMovement);
        }
        this.transform.Translate(movementSpeed * Time.deltaTime * movementVector);
    }
}

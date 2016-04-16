using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float movementSpeed = 1f;

    private CameraController cameraController;

    private bool isTopDown;
    private Vector3 movementVector;

    void Awake ()
    {
        cameraController = GameObject.Find("CameraManager").GetComponent<CameraController>();
    }

    // Use this for initialization
    void Start () {
        Transform(false);
    }
	
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.T))
        {
            isTopDown = !isTopDown;
            if (isTopDown)
            {
                cameraController.SetTopDownView();
            }
            else
            {
                cameraController.SetSideView();
            }
        }
        if (isTopDown)
        {
            this.transform.localScale = Vector3.Lerp(this.transform.localScale, new Vector3(1f, 20f, 1f), 0.1f);
            //this.transform.localPosition = new Vector3(transform.localPosition.x, 0f, transform.localPosition.z);
        } else
        {
            this.transform.localScale = Vector3.Lerp(this.transform.localScale, new Vector3(20f, 1f, 1f), 0.1f);
        }
        Move();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "HurtsPlayer")
        {
            Debug.Log("You're dead");
            Destroy(collider.gameObject);
        }
    }

    //transforms player between vertical alignment (top-down view)
    //and horizontal alignment (side view)
    private void Transform (bool itd) //isTopDown
    {
        if (itd)
        {
            this.transform.localScale = new Vector3(1f, 20f, 1f);
            this.transform.localPosition = new Vector3(transform.localPosition.x, 0f, transform.localPosition.z);
            cameraController.SetTopDownView();
        }
        else
        {
            this.transform.localScale = new Vector3(20f, 1f, 1f);
            this.transform.localPosition = new Vector3(0f, transform.localPosition.y, transform.localPosition.z);
            cameraController.SetSideView();
        }
        isTopDown = itd;
    }

    //checks for movement input and moves character
    private void Move ()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

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

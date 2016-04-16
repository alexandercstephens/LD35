using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float movementSpeed = 1f;
    public BoxCollider boundaryBox;

    private CameraController cameraController;
    private BoxCollider playerBox;

    private bool isTopDown;
    private Vector3 movementVector;

    void Awake ()
    {
        cameraController = GameObject.Find("CameraManager").GetComponent<CameraController>();
        playerBox = GetComponent<BoxCollider>();
    }

    // Use this for initialization
    void Start () {
        isTopDown = false;
        SetScale();
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
        SetScale();
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

    private void SetScale ()
    {
        if (isTopDown)
        {
            this.transform.localScale = Vector3.Lerp(this.transform.localScale, new Vector3(1f, 15.5f, 1f), 0.1f);
        }
        else
        {
            this.transform.localScale = Vector3.Lerp(this.transform.localScale, new Vector3(20f, 1f, 1f), 0.1f);
        }
    }

    //checks for movement input and moves player
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

        //keep player inside bounds -gotta be a better way to do this
        if (playerBox.bounds.min.x < boundaryBox.bounds.min.x)
        {
            this.transform.Translate(new Vector3(boundaryBox.bounds.min.x - playerBox.bounds.min.x, 0f, 0f));
        }
        else if (playerBox.bounds.max.x > boundaryBox.bounds.max.x)
        {
            this.transform.Translate(new Vector3(boundaryBox.bounds.max.x - playerBox.bounds.max.x, 0f, 0f));
        }

        if (playerBox.bounds.min.y < boundaryBox.bounds.min.y)
        {
            this.transform.Translate(new Vector3(0f, boundaryBox.bounds.min.y - playerBox.bounds.min.y, 0f));
        }
        else if (playerBox.bounds.max.y > boundaryBox.bounds.max.y)
        {
            this.transform.Translate(new Vector3(0f, boundaryBox.bounds.max.y - playerBox.bounds.max.y, 0f));
        }

        if (playerBox.bounds.min.z < boundaryBox.bounds.min.z)
        {
            this.transform.Translate(new Vector3(0f, 0f, boundaryBox.bounds.min.z - playerBox.bounds.min.z));
        }
        else if (playerBox.bounds.max.z > boundaryBox.bounds.max.z)
        {
            this.transform.Translate(new Vector3(0f, 0f, boundaryBox.bounds.max.z - playerBox.bounds.max.z));
        }
    }
}

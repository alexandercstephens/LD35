using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 1f;
    public BoxCollider boundaryBox;
    public BoxCollider hitBox;

    private CameraController cameraController;
    private ParticleSystem[] engineParticles;

    private bool isTopDown;
    private Vector3 movementVector;
    private SpearheadController spearHead;

    void Awake()
    {
        cameraController = GameObject.Find("CameraManager").GetComponent<CameraController>();
        engineParticles = GetComponentsInChildren<ParticleSystem>();
        spearHead = GetComponentInChildren<SpearheadController>();
    }

    // Use this for initialization
    void Start()
    {
        isTopDown = false;
        SetScale();
    }

    // Update is called once per frame
    void Update()
    {
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
        if (Input.GetKeyDown(KeyCode.P)) //TODO remove; for debugging only
        {
            cameraController.addShake(1f);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spearHead.Spear();
        }
        SetScale();
        Move();
    }

    private void SetScale()
    {
        if (isTopDown)
        {
            this.transform.localScale = Vector3.Lerp(this.transform.localScale, new Vector3(1f, 14.5f, 1f), 0.1f);
        }
        else
        {
            this.transform.localScale = Vector3.Lerp(this.transform.localScale, new Vector3(19f, 1f, 1f), 0.1f);
        }
    }

    //checks for movement input and moves player
    private void Move()
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

        foreach (var particleSystem in engineParticles)
        {
            //TODO engine sound effect change volume
            //TODO don't hardcode values
            particleSystem.startSize = 0.2f * (1.2f + 0.6f * movementVector.z);
        }

        //keep player inside bounds -gotta be a better way to do this
        if (hitBox.bounds.min.x < boundaryBox.bounds.min.x)
        {
            this.transform.Translate(new Vector3(boundaryBox.bounds.min.x - hitBox.bounds.min.x, 0f, 0f));
        }
        else if (hitBox.bounds.max.x > boundaryBox.bounds.max.x)
        {
            this.transform.Translate(new Vector3(boundaryBox.bounds.max.x - hitBox.bounds.max.x, 0f, 0f));
        }

        if (hitBox.bounds.min.y < boundaryBox.bounds.min.y)
        {
            this.transform.Translate(new Vector3(0f, boundaryBox.bounds.min.y - hitBox.bounds.min.y, 0f));
        }
        else if (hitBox.bounds.max.y > boundaryBox.bounds.max.y)
        {
            this.transform.Translate(new Vector3(0f, boundaryBox.bounds.max.y - hitBox.bounds.max.y, 0f));
        }

        if (hitBox.bounds.min.z < boundaryBox.bounds.min.z)
        {
            this.transform.Translate(new Vector3(0f, 0f, boundaryBox.bounds.min.z - hitBox.bounds.min.z));
        }
        else if (hitBox.bounds.max.z > boundaryBox.bounds.max.z)
        {
            this.transform.Translate(new Vector3(0f, 0f, boundaryBox.bounds.max.z - hitBox.bounds.max.z));
        }
    }
}

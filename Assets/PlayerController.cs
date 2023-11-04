using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Pri, update the move and rotation variables from the editor to change the speed in real-time!
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;
    // You don't need to attach anything to this in the editor, because we take care of this variable in the Start() method
    public Game game;
    private Vector3 targetPosition;
    private bool isMoving = false;

    // Executes only once
    void Start()
    {
        game = GameObject.FindObjectOfType<Game>().GetComponent<Game>();
    }

    // Homework: look into the life-cycles of a Unity video game. It's Awake() -> Start() -> Update(). Update is our game-loop.
    void Update()
    {
        // Check for touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                SetTargetPosition(touch.position);
            }
        }

        // Check for mouse input
        if (Input.GetMouseButton(0))
        {
            SetTargetPosition(Input.mousePosition);
        }

        // Move and rotate only if isMoving is set to true
        if (isMoving)
        {
            MoveAndRotatePlayer();
        }
    }

    void SetTargetPosition(Vector2 inputPosition)
    {
        // Convert input position to world space and update targetPosition
        targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(inputPosition.x, inputPosition.y, Camera.main.transform.position.y - transform.position.y));
        targetPosition.y = transform.position.y; // Keep the target y position constant
        isMoving = true; // Set isMoving to true to start moving towards the new target
    }

    void MoveAndRotatePlayer()
    {
        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Rotate towards the target position
        Vector3 targetDirection = targetPosition - transform.position;
        if (targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Check if the player has reached the target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            isMoving = false; // Stop moving
        }
    }

    // Check if collider got triggered
    void OnTriggerEnter(Collider other)
    {
        // If the collider triggered another collider with a tag of "Coin", then remove the coin and add a point
        if (other.gameObject.tag == "Coin")
        {
            Destroy(other.gameObject);
            game.AddPoint();
        }
    }
}

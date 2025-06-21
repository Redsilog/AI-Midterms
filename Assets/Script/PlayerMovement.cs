using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float runSpeed = 10f;

    [Header("Mouse Look Settings")]
    [SerializeField] float mouseSensitivity = 1f;
    [SerializeField] float verticalLookLimit = 80f;

    [Header("Run Limit Settings")]
    [SerializeField] float runDuration = 0f; 
    [SerializeField] float runCooldown = 0f;
    [SerializeField] float staminaRegenRate = 1f;

    public Transform cameraTransform;

    private Rigidbody rb;
    private float rotationY;
    private float rotationX;

    private float currentRunTime = 0f;
    private bool canRun = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationY += mouseX;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -verticalLookLimit, verticalLookLimit);

        rb.MoveRotation(Quaternion.Euler(0f, rotationY, 0f));
        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 direction = (forward * moveZ + right * moveX).normalized;

        bool isTryingToRun = Input.GetKey(KeyCode.LeftShift);
        bool isMoving = moveX != 0 || moveZ != 0;

        float moveSpeed = walkSpeed;

        if (isTryingToRun && canRun && isMoving)
        {
            moveSpeed = runSpeed;
            currentRunTime += Time.fixedDeltaTime;

            if (currentRunTime >= runDuration)
            {
                canRun = false;
            }
        }
        else
        {
            moveSpeed = walkSpeed;

            if (!isTryingToRun || !isMoving)
            {
                currentRunTime -= staminaRegenRate * Time.fixedDeltaTime;

                if (currentRunTime <= 0f)
                {
                    currentRunTime = 0f;
                    canRun = true;
                }
            }
        }

        rb.linearVelocity = direction * moveSpeed + new Vector3(0f, rb.linearVelocity.y, 0f);
    }
}

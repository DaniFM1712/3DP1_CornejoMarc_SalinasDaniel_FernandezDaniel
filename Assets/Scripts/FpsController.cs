using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsController : MonoBehaviour
{

    [Header("Mouse Debug")]

    [SerializeField]
    public KeyCode angleLockKey = KeyCode.I; 

    [SerializeField]
    public KeyCode mouseLockKey = KeyCode.O;

    private bool angleLocked = false;

    float yaw = 0;
    float pitch = 0;
    Vector3 direction = new Vector3(0, 0, 0);

    [Header("Rotation")]
    [SerializeField] float yawSpeed = 5.0f;
    [SerializeField] float pitchSpeed = 5.0f;
    [SerializeField] bool invertPitch;
    [SerializeField] bool invertYaw;
    [SerializeField] float minPitch;
    [SerializeField] float maxPitch;
    [SerializeField] Transform pitchController;

    [Header("Planar Movement")]
    [SerializeField] CharacterController characterController;
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] KeyCode runKey = KeyCode.LeftShift;
    [SerializeField] KeyCode forwardKey = KeyCode.W;
    [SerializeField] KeyCode leftKey = KeyCode.A;
    [SerializeField] KeyCode backwardsKey = KeyCode.S;
    [SerializeField] KeyCode rightKey = KeyCode.D;
    float currSpeed;

    [Header("Vertical Movement")]
    float verticalSpeed = 0;
    float gravity;
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    float jumpSpeed;
    [SerializeField] float maxHeightJump;
    [SerializeField] float jumpTime;
    [SerializeField] bool onGround;
    bool onCeiling;

    private void Awake()
    {
        Cursor.lockState = (Cursor.lockState == CursorLockMode.Locked) ? CursorLockMode.None : CursorLockMode.Locked;
        yaw = transform.rotation.eulerAngles.y;
        pitch = transform.rotation.eulerAngles.x;

        gravity = (-2 * maxHeightJump) / (jumpTime * jumpTime);
        jumpSpeed = -gravity * jumpTime;
    }

    private void FixedUpdate()
    {
        if (!angleLocked)
            rotate();
        move();
    }

    void Update()
    {
        inputUpdate();
        updateLockKeyState();
    }

    void updateLockKeyState()
    {
        if (Input.GetKeyDown(angleLockKey))
            angleLocked = !angleLocked;

        if (Input.GetKeyDown(mouseLockKey))
            Cursor.lockState = (Cursor.lockState == CursorLockMode.Locked) ? CursorLockMode.None : CursorLockMode.Locked;
    }

    void rotate()
    {
        float xMouse = Input.GetAxis("Mouse X");
        yaw += xMouse * yawSpeed * (invertYaw ? -1 : 1);

        float yMouse = Input.GetAxis("Mouse Y");
        pitch -= yMouse * pitchSpeed * (invertPitch ? -1 : 1);

        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);


        transform.rotation = Quaternion.Euler(0, yaw, 0);
        pitchController.localRotation = Quaternion.Euler(pitch, 0, 0);
    }

    void move()
    {
        verticalSpeed += gravity * Time.deltaTime;

        Vector3 movement = direction.normalized * Time.deltaTime * currSpeed;

        movement.y = verticalSpeed * Time.deltaTime;

        CollisionFlags colFlags = characterController.Move(movement);
        onGround = (colFlags & CollisionFlags.Below) != 0;
        onCeiling = (colFlags & CollisionFlags.Above) != 0;

        if (onGround || onCeiling) verticalSpeed = gravity * Time.deltaTime;

    }

    void inputUpdate()
    {
        Vector3 forward = getForward();
        Vector3 right = getRight();
        direction = new Vector3(0, 0, 0);
        if (Input.GetKey(forwardKey))
        {
            direction += forward;
        }
        if (Input.GetKey(leftKey))
        {
            direction -= right;
        }
        if (Input.GetKey(rightKey))
        {
            direction += right;
        }
        if (Input.GetKey(backwardsKey))
        {
            direction -= forward;
        }

        if (Input.GetKey(runKey))
        {
            currSpeed = runSpeed;
        }
        else
        {
            currSpeed = walkSpeed;
        }

        if (Input.GetKeyDown(jumpKey) && onGround)
        {
            verticalSpeed = jumpSpeed;
        }
    }

    Vector3 getForward()
    {
        return new Vector3(Mathf.Sin(yaw * Mathf.Deg2Rad), 0.0f, Mathf.Cos(yaw * Mathf.Deg2Rad));
    }

    Vector3 getRight()
    {
        return new Vector3(Mathf.Sin((yaw + 90.0f) * Mathf.Deg2Rad), 0.0f, Mathf.Cos((yaw + 90.0f) * Mathf.Deg2Rad));
    }
}
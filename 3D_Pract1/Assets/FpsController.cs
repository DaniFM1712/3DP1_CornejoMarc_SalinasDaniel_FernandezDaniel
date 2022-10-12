using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsController : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Mouse Debug")]

    [SerializeField]
    public KeyCode angleLockKey = KeyCode.I; //para bloquear el angulo de rotación (literalmente no rota)

    [SerializeField]
    public KeyCode mouseLockKey = KeyCode.O; //para esconder el ratón(solo se esconde si le das click a la pantalla) y lo pone en el centro de la pantalla

    private bool angleLocked = true;

    float yaw = 0;
    float pitch = 0;
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
    [SerializeField] KeyCode forwardKey = KeyCode.W;
    [SerializeField] KeyCode backwardKey = KeyCode.S;
    [SerializeField] KeyCode rightKey = KeyCode.D;
    [SerializeField] KeyCode leftKey = KeyCode.A;
    [SerializeField] KeyCode runKey = KeyCode.LeftShift;
    Vector3 direction;
    float currentSpeed;


    [Header("Vertical Movement")]
    float verticalSpeed = 0f;
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] bool onGround;
    [SerializeField] bool onCeiling;
    [SerializeField] float maxJumpHeight = 20.0f;
    [SerializeField] float jumpTime = 3.0f;
    [SerializeField] float gravity = -9.8f; //no hauria de ser serializable
    [SerializeField] float jumpSpeed = 20f; //no hauria de ser serializable

    private void Awake()
    {
        yaw = transform.rotation.eulerAngles.y;
        pitch = transform.rotation.eulerAngles.x;

        jumpSpeed = (2 * maxJumpHeight) / jumpTime;
        gravity = (-2 * maxJumpHeight) / (jumpTime * jumpTime);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!angleLocked)
            rotate();
        
        Move();
    }
    void Update()
    {
        updateLockKeyState();
        InputUpdate();
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
        pitch -= yMouse * pitchSpeed * (invertPitch ? -1 : 1); //si invert pitch activated, -1 else 1

        pitch = Mathf.Clamp(pitch, minPitch, maxPitch); //cualquier valor te lo filtra entre dos valores


        transform.rotation = Quaternion.Euler(0, yaw, 0); //esto se hace para que el personaje cuando mire arriba y abajo no se incline
                                                          //y solo sea la cámara
        pitchController.localRotation = Quaternion.Euler(pitch, 0, 0); //se hace la local rotarion, porque pilla la del padre
    }

    void Move()
    {
        
        verticalSpeed += gravity * Time.fixedDeltaTime;

        Vector3 movement = direction.normalized * Time.fixedDeltaTime * currentSpeed;
        movement.y = verticalSpeed * Time.fixedDeltaTime;

        CollisionFlags colFlags = characterController.Move(movement);
        onGround = (colFlags & CollisionFlags.Below) != 0;
        onCeiling = (colFlags & CollisionFlags.Above) != 0;

        if (onGround || onCeiling) verticalSpeed = 0.0f;
    }

    void InputUpdate()
    {
        Vector3 fw = getForward();
        Vector3 right = getRight();
        direction = new Vector3(0, 0, 0);

        if (Input.GetKey(forwardKey))
            direction += fw;

        if (Input.GetKey(backwardKey))
            direction += -fw;

        if (Input.GetKey(rightKey))
            direction += right;

        if (Input.GetKey(leftKey))
            direction += -right;

        if (Input.GetKeyDown(jumpKey) && onGround)
            verticalSpeed = jumpSpeed;

        if (Input.GetKey(runKey))
            currentSpeed = runSpeed;
        else
            currentSpeed = walkSpeed;

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
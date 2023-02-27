using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, PlayerInput.IMainActions
{
    #region Essentials
    private Vector3 _move;
    private Rigidbody _rb;
    #endregion

    #region MoveStats
    [Header("MoveSpeed")]
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed = 1f;
    [SerializeField] private float _crouchSpeed = 1f;
    [Header("JumpForce")]
    [SerializeField] private float _jumpForce;
    [Header("Detection")]
    [SerializeField] private float _detectionDistanceWhenWalking;
    [SerializeField] private float _detectionDistanceWhenRunning;
    [SerializeField] private float _detectionDistanceWhenCrouching;
    #endregion

    #region Movement Bools
    private bool _isCrouching;
    private bool _isRunning;
    private bool _isWalking;
    #endregion

    #region PlayerStats
    #endregion

    #region PlayerHealth
    #endregion

    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = new PlayerInput();

        _rb= GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }
    private void OnDisable()
    {
        playerInput.Disable();
    }


    public void OnCrouch(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {   
        //throw new System.NotImplementedException();
    }

    public void OnJump(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Debug.Log("move");
        _move = context.ReadValue<Vector3>();
        
    }

    public void OnRun(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Debug.Log("run");
        //throw new System.NotImplementedException();
    }

    public void OnUse(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        if(!_isCrouching && !_isRunning && _move.magnitude>0) { _isWalking= true; _crouchSpeed = 1f; _runSpeed = 1f; }
        if (_isCrouching) { _crouchSpeed = 0.5f; _runSpeed= 1f; }
        if (_isRunning) { _runSpeed = 2.75f; _crouchSpeed= 1f; }
    }

    void FixedUpdate()
    {
        Move();
    }
    void GetInput()
    {
        _move = playerInput.Main.Move.ReadValue<Vector3>();
        if (_move.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(_move.x, _move.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f,targetAngle,0f);
        }

        //getting button values 'cause we need to keep pushing the button
        _isCrouching = playerInput.Main.Crouch.ReadValue<float>()>0;
        _isRunning = playerInput.Main.Run.ReadValue<float>()>0;

        //triggered saves the info once on the frame it is called on
        if (playerInput.Main.Jump.triggered)
        {
            Jump();//Debug.Log("jump");
        }
        if (playerInput.Main.Use.triggered)
        {
            Debug.Log("use");
        }
        if (_isCrouching)
        {
            Debug.Log("crouch");
        }
        if (_isRunning)
        {
            Debug.Log("run");

        }

        //_move = playerInput.Main.Move.ReadValue<Vector2>();
    }
    private void Move()
    {
        //here i put all of the speeds, makes them all relative to walk
        //i don't know if it is clever or never yet
        //guess we'll see
        _rb.velocity = new Vector3(_move.normalized.x * _walkSpeed * _crouchSpeed * _runSpeed * Time.fixedDeltaTime, _rb.velocity.y, _move.normalized.z * _walkSpeed * _crouchSpeed * _runSpeed * Time.fixedDeltaTime);
        
        
    }
    private void Jump()
    {
        _rb.AddForce(0, _jumpForce, 0,  ForceMode.Impulse);
    }

    private void Use()
    {
        Debug.Log("use");
    }
}

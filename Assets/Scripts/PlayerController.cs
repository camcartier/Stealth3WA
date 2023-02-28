using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour  //, PlayerInput.IMainActions
{
    #region Essentials
    //move est l'input recupéré par le new input system
    private Vector3 _move;
    //direction est le vecteur recomposé avec le forward/right et les inputs
    private Vector3 _direction; 
    //ce component est requis par le script
    private Rigidbody _rb;
    private Animator _animator;
    //camera est dans essentials car on va utiliser son forward pour agir sur le forward du player
    private Transform _cameraTransform;
    #endregion

    #region Camera
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Transform cameraTransform;
    #endregion

    #region MoveStats
    [Header("MoveSpeed")]
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed = 1f;
    [SerializeField] private float _crouchSpeed = 1f;
    [Header("Rotation-Camera")]
    [SerializeField] private float _rotationSpeed;
    [Header("JumpForce")]
    [SerializeField] private float _jumpForce;
    [Header("Detection")]
    [SerializeField] private float _detectionDistanceWhenWalking;
    [SerializeField] private float _detectionDistanceWhenRunning;
    [SerializeField] private float _detectionDistanceWhenCrouching;
    #endregion

    #region Mouse (testing)
    [Header("Mouse")]
    [SerializeField] private float sensitivity; 
    private float rotY;
    private float rotX;
    private GameObject _camera;
    #endregion

    #region Rotation (testing)
    private float _turnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;
    #endregion

    #region Movement Bools
    private bool _isCrouching;
    private bool _isRunning;
    private bool _isWalking;
    private bool _isGrounded;
    private bool _canJump;
    private int _jumpNumber;
    #endregion

    #region GroundChecker
    [Header("Groundchecker")]
    [SerializeField] private Vector3 _boxDimension;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private LayerMask _groundMask;
    public Collider[] groundColliders;
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
        _animator = GetComponent<Animator>();
        _camera = GameObject.Find("Main Camera");
        //_groundChecker = transform.Find("GroundChecker").transform;
    }

    private void OnEnable() { playerInput.Enable(); }
    private void OnDisable() { playerInput.Disable(); }


    // Start is called before the first frame update
    void Start()
    {
        _cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        if (!_isCrouching && !_isRunning && _move.magnitude > 0) { _isWalking = true; _crouchSpeed = 1f; _runSpeed = 1f; }
        if (_isCrouching) { _crouchSpeed = 0.5f; _runSpeed = 1f; }
        if (_isRunning) { _runSpeed = 2f; _crouchSpeed = 1f; }

        //
        groundColliders = Physics.OverlapBox(_groundChecker.position, _boxDimension, Quaternion.identity, _groundMask);
        if (groundColliders.Length > 0)
        {
            Debug.Log(groundColliders[0].name);
            _isGrounded = true;
        }
        else { 
            _isGrounded = false; 
        }
        if(_rb.velocity.y < 0)
        {
            _jumpNumber = 0;
        }
    }

    void FixedUpdate()
    {
        //InvertMouse();

        Move();/*
        if(_direction.magnitude>0)
        {
            Vector3 lookDirection = _cameraTransform.forward; ;
            lookDirection.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookDirection);
            rotation = Quaternion.RotateTowards(_rb.rotation, rotation, _rotationSpeed * Time.fixedDeltaTime);
            _rb.MoveRotation(rotation);
        }
        */

    }
    void GetInput()
    {
        _move = playerInput.Main.Move.ReadValue<Vector3>();


        //getting button values 'cause we need to keep pushing the button
        _isCrouching = playerInput.Main.Crouch.ReadValue<float>()>0;
        _isRunning = playerInput.Main.Run.ReadValue<float>()>0;

        //triggered saves the info once on the frame it is called on
        if (playerInput.Main.Jump.triggered && _isGrounded)
        {
            Debug.Log("has jumped " + groundColliders.Length);
            _isGrounded = false;
            Debug.Log("grounded false");
            Jump();//Debug.Log("jump");
        }
        if (playerInput.Main.Use.triggered)
        {
            _animator.SetTrigger("use");
            Debug.Log("use");
        }
        if (_isCrouching)
        {
            _animator.SetBool("crouching", true);
            Debug.Log("crouch");
        }
        if (_isRunning)
        {
            _animator.SetBool("running", true);
            Debug.Log("run");
        }

        //_move = playerInput.Main.Move.ReadValue<Vector2>();
    }

    //this is currently working except for camera rotation which are inverted
    private void Move()
    {

        _direction = _cameraTransform.transform.forward * _move.z + _cameraTransform.transform.right * _move.x;
        if(_direction.magnitude >= 0.1f)
        {
            //this piece of code is from brackey
            //it makes the character face the direction he is going towards
            float targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
            //this line makes the rotation smooth
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);


            //here i put all of the speeds, makes them all relative to walk
            _rb.velocity = new Vector3(_direction.normalized.x * _walkSpeed * _crouchSpeed * _runSpeed * Time.fixedDeltaTime, _rb.velocity.y, _direction.normalized.z * _walkSpeed * _crouchSpeed * _runSpeed * Time.fixedDeltaTime);
        }
        else { _rb.velocity = new Vector3(0, _rb.velocity.y, 0); }



    }


    //did not work :p
    private void InvertMouse()
    {
        //big nope here
        /*
        rotY = Input.GetAxis("Mouse X") * sensitivity;
        rotX = Input.GetAxis("Mouse X") * sensitivity;
        _camera.transform.Rotate(rotX, rotY, 0);
        */
    }

    private void Jump()
    {
        //_canJump = false;
        _animator.SetTrigger("jump");
        _rb.AddForce(0, _jumpForce, 0,  ForceMode.Impulse);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_groundChecker.position, _boxDimension);
    }

    private void Use()
    {
        Debug.Log("use");
    }
}


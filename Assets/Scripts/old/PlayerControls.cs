using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    #region MoveData
    private Rigidbody _rb;
    private Vector2 _move;
    #endregion
    #region Bools
    private bool _jumping;
    private bool _running;
    private bool _crouching;
    #endregion

    [SerializeField] PlayerInput playerInput;
    //[SerializeField] InputActionReference Move, Jump, Run, Crouch, Use;

    private void Awake()
    {
       // playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }
    private void OnDisable()
    {
        playerInput.Disable();
        //playerInput.Main.Use.started -= Use;
    }

    private void Start()
    {
        //playerInput.Main.Use.performed += Use;

    }

    // Update is called once per frame
    private void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        //Vector2 _move = playerInput.Main.Move.ReadValue<Vector2>();
        //if (playerInput.Main.Jump.triggered) { Debug.Log("jumped"); }
        

       // _move = Move.action.ReadValue<Vector2>();
        Debug.Log(_move);

    }





}

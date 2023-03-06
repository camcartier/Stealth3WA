using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorStateMachine : MonoBehaviour
{
    private PlayerController _playerController;
    private Animator _animator;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _animator = GetComponent<Animator>();
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerController._move.magnitude > 0.05)
        {
            _animator.SetBool("walking", true);
        }
        else { _animator.SetBool("walking", false);  }

        if (_playerController._isRunning)
        {
            _animator.SetBool("running", true);
        }
        else { _animator.SetBool("running", false); }

        if (_playerController._isCrouching)
        {
            _animator.SetBool("crouching", true);
        }
        else { _animator.SetBool("crouching", false); }
    }
}

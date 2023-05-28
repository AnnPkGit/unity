using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    private CharacterController _characterController;
    private Animator _animator;
    private Vector3 _moveVector;
    private float _moveSpeed = 800f;
    private bool isRunning = false;
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float factor = _moveSpeed * Time.deltaTime;
        float ix = Input.GetAxis("Horizontal");
        float iy = Input.GetAxis("Vertical");
        //_moveVector = new Vector3(ix, 0, iy);
        _moveVector = this.transform.forward * iy +
            this.transform.right * ix;
        if(_moveVector.magnitude > 1)
        {
            _moveVector = _moveVector.normalized;   
        }
        _moveVector *= factor;
        if(_moveVector.magnitude > _characterController.minMoveDistance)
        {

            if (!isRunning)
            {
                _animator.SetInteger("MoveState", 0);
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (!isRunning)
                {
                    isRunning = true;
                    _animator.SetInteger("MoveState", 2);
                }
                else
                {
                    isRunning = false;
                    _animator.SetInteger("MoveState", 0);
                }
            }
        }
        else 
        {
            _animator.SetInteger("MoveState", 1);
        }

        _characterController.SimpleMove(_moveVector);
    }
}


//if (!isRunning)
//{
//    _animator.SetInteger("MoveState", 0);

//    _animator.SetInteger("MoveState", 2);
//    isRunning = true;
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Animator _animator;
    private CharacterController _characterController;

    public float speed = 6.0f;
    public float rotationSpeed = 25;
    public float jumpSpeed = 7.5f;
    public float gravity = 20.0f;
    
    private Vector3 inputVector;
    private Vector3 targetDirection;
    private Vector3 moveDirection = Vector3.zero;
    
    
    
    void Start()
    {
        Time.timeScale = 1;
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = -(Input.GetAxis("Vertical"));
        float z = (Input.GetAxisRaw("Horizontal"));
        inputVector = new Vector3(x,0,z);
        
        _animator.SetFloat("Input X",z);
        _animator.SetFloat("Input Z", -(x));
        if (x != 0 || z != 0)
        { 
            _animator.SetBool("Running",true);
            _animator.SetBool("Moving",true);
        }
        else
        {
            _animator.SetBool("Running",false);
            _animator.SetBool("Moving",false);
        }
        
        //Jump
        if (_characterController.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection *= speed;
        }

        _characterController.Move(moveDirection * Time.deltaTime);
        UpdateMovement();

       
        
        void UpdateMovement()
        {
            Vector3 motion = inputVector;
            motion *= (Mathf.Abs(inputVector.x) == 1 && Mathf.Abs(inputVector.z) == 1) ? .7f : 1;
            RotateTowardMovment();
            GetCameraRerative();
        }

        void RotateTowardMovment()
        {
            if (inputVector != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDirection),
                    Time.deltaTime * rotationSpeed);

            }
        }

        void GetCameraRerative()
        {
            Transform cameraTransform = Camera.main.transform;
            Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
            forward.y = 0;
            forward = forward.normalized;

            Vector3 right = new Vector3(forward.z, 0, forward.x);
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");
            targetDirection = (h * right) + (v * forward);
        }
        
    }
}

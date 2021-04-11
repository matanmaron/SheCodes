using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject CameraHolder = null;
    [SerializeField] float MouseSensitivity = 1;
    [SerializeField] float Speed = 15;
    float camera_minimumY = -10;
    float camera_maximumY = 50;
    float player_rotationX = 0;
    float camera_rotationY = 0;
    float camera_rotationX = 0;
    Animator _animator = null;
    bool _isrunning = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (!GameManager.Instance.IsPlayerActive)
        {
            if (_isrunning)
            {
                StopRunning();
            }
            return;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            GameManager.Instance.Swap();
        }
        MovePlayer();
        MoveCamera();
    }

    void MovePlayer()
    {
        var inputY = Input.GetAxis("Vertical");
        var inputX = Input.GetAxis("Horizontal");
        if (inputY==0 && inputX==0 && _isrunning)
        {
            StopRunning();
        }
        else if (!_isrunning && (inputY > 0 || inputX > 0))
        {
            StartRunning();
        }
        transform.position += transform.forward * Speed * inputY * Time.deltaTime;
        transform.position += transform.right * Speed * inputX * Time.deltaTime;
        player_rotationX += Input.GetAxis("Mouse X") * MouseSensitivity;
        transform.localEulerAngles = new Vector3(0, player_rotationX, 0);
    }

    private void StartRunning()
    {
        Debug.Log("player running");
        _isrunning = true;
        _animator.SetBool("running", true);
    }

    private void StopRunning()
    {
        Debug.Log("player idle");
        _isrunning = false;
        _animator.SetBool("running", false);
    }

    void MoveCamera()
    {
        camera_rotationY += Input.GetAxis("Mouse Y") * MouseSensitivity;
        camera_rotationX += Input.GetAxis("Mouse X") * MouseSensitivity;
        camera_rotationY = Mathf.Clamp(camera_rotationY, camera_minimumY, camera_maximumY);
        CameraHolder.transform.localEulerAngles = new Vector3(-camera_rotationY, camera_rotationX, 0);
        CameraHolder.transform.position = transform.position;
    }

}
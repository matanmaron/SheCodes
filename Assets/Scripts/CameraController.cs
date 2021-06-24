using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraController : MonoBehaviour
{
    [Header("Change")]
    [SerializeField] private float cameraSpeed = 0.001f;
    
    [Space(10)]
    [Header("Do Not Change")]
    private Vector2 touchStart;
    [SerializeField] private Camera cam;

    bool gameOver = true;

    void Start()
    {
        LevelManager.OnLevelEnd += OnLevelEnd;
    }

    private void OnLevelEnd()
    {
        gameOver = true;
    }

    void Update()
    {
        if (gameOver)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Input.mousePosition;
        }
        

        if (Input.GetMouseButton(0))
        {
            MoveCamera();
        }
    }

    private void MoveCamera()
    {
        
        Vector2 mousePos = Input.mousePosition;

        Vector3 moveVector = Vector3.zero;
        float moveDis = cameraSpeed * Time.deltaTime;
        
        //move x
        if (mousePos.x > touchStart.x)
        {
            if (transform.position.x + moveDis < GameManager.Instance.LevelManager.blocksList[GameManager.Instance.LevelManager.levelGridX-1].transform.position.x)
            {
                moveVector.x = moveDis;
            }
        }
        else if (mousePos.x < touchStart.x)
        {
            if (transform.position.x - moveDis > GameManager.Instance.LevelManager.blocksList[0].transform.position.x)
            {
                moveVector.x = -moveDis;
            }
        }
        


        //move z
        if (mousePos.y > touchStart.y)
        {
            if (transform.position.z + moveDis < 0)
            {
                moveVector.z = moveDis;
            }
        }
        else if (mousePos.y < touchStart.y)
        {
            if (transform.position.z - moveDis > -(GameManager.Instance.LevelManager.blocksList[GameManager.Instance.LevelManager.blocksList.Count-1].transform.position.z - GameManager.Instance.LevelManager.blocksList[0].transform.position.z))
            {
                moveVector.z = -moveDis;
            }
        }
        
        transform.Translate(moveVector, Space.World);
    }



}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region singletone
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    [SerializeField] Transform PlayerCamera = null;
    [SerializeField] Transform AICamera = null;
    [SerializeField] Transform Player = null;
    [SerializeField] AIController AIController = null;
    internal bool IsPlayerActive = true;

    internal void Swap()
    {
        Debug.Log("Ai now Active");
        PlayerCamera.gameObject.SetActive(false);
        AICamera.gameObject.SetActive(true);
        AIController.SetTarget(Player.position);
        IsPlayerActive = false;
    }

    internal void SwapFinish()
    {
        Debug.Log("Player now Active");
        PlayerCamera.gameObject.SetActive(true);
        AICamera.gameObject.SetActive(false);
        IsPlayerActive = true;
    }
}

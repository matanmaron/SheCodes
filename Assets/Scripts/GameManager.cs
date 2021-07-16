using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject DemoVideoPrefab = null;
    DateTime lastClick = DateTime.Now;
    bool isDemoRunning= false;

    [Header("Change (not for every level)")]
    [SerializeField] public float playerYOffset = 0.72f;
    [SerializeField] public float variaballYGeneralOffset = 0.3f;
    [SerializeField] public float variaballYPedestalOffset = 1f;

    //[SerializeField] public GameObject player = null;
    [SerializeField] public float playerMoveDuration = 0.2f;
    
    public static GameManager Instance { get; private set; } //singleton
    //public Player Player { get; internal set; }
    public LevelManager LevelManager { get; internal set; }
    public bool IsDemoActive = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        IsDemoActive = PlayerPrefs.GetInt("DemoMode", 0) == 0 ? false : true;
    }

    private void Update()
    {
        if (IsDemoActive)
        {
            if (!isDemoRunning)
            {
                if (Input.anyKey)
                {
                    lastClick = DateTime.Now;
                }
                if (DateTime.Now > lastClick.AddMinutes(2))
                {
                    Debug.Log("DEMO START");
                    isDemoRunning = true;
                    Instantiate(DemoVideoPrefab);
                }
            }
        }
    }

    internal void StopDemo()
    {
        isDemoRunning = false;
        lastClick = DateTime.Now;
        SceneManager.LoadScene(0);
    }
}

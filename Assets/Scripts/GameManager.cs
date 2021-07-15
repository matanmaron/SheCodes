using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Change (not for every level)")]
    [SerializeField] public float playerYOffset = 0.72f;
    [SerializeField] public float variaballYGeneralOffset = 0.3f;
    [SerializeField] public float variaballYPedestalOffset = 1f;

    //[SerializeField] public GameObject player = null;
    [SerializeField] public float playerMoveDuration = 0.2f;
    
    public static GameManager Instance { get; private set; } //singleton
    //public Player Player { get; internal set; }
    public LevelManager LevelManager { get; internal set; }
    public bool IsDemo = false;

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
        IsDemo = PlayerPrefs.GetInt("isdemo", 0) == 0 ? false : true;
        Debug.Log($"GameManager - DEMO IS NOW - {IsDemo}");
    }
}

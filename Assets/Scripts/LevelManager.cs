using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    
    public static event Action OnLevelEnd;
    public static event Action OnLevelFail;
    public static event Action OnOutOfMoves;
    public int maxMoves = 3;
    public int playerStartBlockIndex;
    private Vector3 playerStartPos;
    public Vector3 startPos;
    public int movesLeft = 0;
    //[HideInInspector] public int levelGridX;
    public int levelGridX;
    [HideInInspector] public int levelGridY;
    [SerializeField] public List<Block> blocksList = new List<Block>();
    [SerializeField] public List<Variaball> variaballsList = new List<Variaball>();
    [SerializeField] private CodeBuilder cb = null;
    [SerializeField] LevelCreator levelCreator = null;
    [SerializeField] Player player = null;
    //public float groundZ = 0;

    private void OnEnable()
    {
        Player.OnAnyAction += MoveTaken;
    }

    private void OnDisable()
    {
        Player.OnAnyAction -= MoveTaken;
    }

    void Start()
    {
        GameManager.Instance.LevelManager = this;
        AudioManager.Instance?.NextMusicFile();

        levelCreator = GameObject.Find("LevelCreator").GetComponent<LevelCreator>();
        //player = GameObject.Find("Player").GetComponent<Player>();
        cb = GameObject.Find("CodeBuilder").GetComponent<CodeBuilder>();


        maxMoves = levelCreator.levelMaxMoves;
        movesLeft = maxMoves;
        UI_Manager.Instance.UpdateMovesLeftText();
        
        levelGridX = levelCreator.levelGridX;
        levelGridY = levelCreator.levelGridY;

        PlacePlayer();
        UI_Manager.Instance.GetPlayer();

        //groundZ = levelCreator.emptyBlockPrefab.GetComponentInChildren<MeshRenderer>().bounds.extents.y;
        SetVariaballIds();
    }

    void Update()
    {
        
    }

    private void PlacePlayer()
    {
        
        
        playerStartPos = blocksList[playerStartBlockIndex].transform.position + new Vector3(0, blocksList[playerStartBlockIndex].GetComponentInChildren<MeshRenderer>().bounds.extents.y + GameManager.Instance.playerYOffset, 0);
        player = Instantiate(levelCreator.playerPrefab, playerStartPos, Quaternion.identity).GetComponent<Player>();
        //GameObject player = Instantiate(levelCreator.playerPrefab, playerStartPos, Quaternion.identity);
        //GameManager.Instance.Player = player.GetComponent<Player>();
        player.transform.position = playerStartPos;

    }


    private void SetVariaballIds ()
    {
        for (int i=0; i< variaballsList.Count; i++)
        {
            variaballsList[i].SetID(i);
            Debug.Log("Variaball Id: " + variaballsList[i].GetID());

            variaballsList[i].GetPlayer();

        }
    }

    public List<Variaball> GetVariaballsList()
    {
        return variaballsList;
    }

    public void EndLevel()
    {
        OnLevelEnd?.Invoke();
        Debug.Log("level ended");
    }

    public void FailLevel()
    {
        OnLevelFail?.Invoke();
        Debug.Log("level failed");
    }

    private void MoveTaken()
    {
        movesLeft--;
        UI_Manager.Instance.UpdateMovesLeftText();

        if (movesLeft < 0)
        {
            FailLevel();
            Debug.Log("no moves left");
            OnOutOfMoves?.Invoke();
            cb.BuildCode();
        }
        
    }
 
}

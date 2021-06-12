using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI movesLeftText;
    [SerializeField] private GameObject directionButtons = null;
    [SerializeField] private GameObject forButton = null;
    [SerializeField] private GameObject pressButton = null;
    [SerializeField] private GameObject pickupDropButton = null;
    [SerializeField] private GameObject VariaballUI = null;
    [SerializeField] private GameObject useMachineButton = null;
    [SerializeField] private Text variableText = null;
    [SerializeField] private GameObject SettingsPanel = null;

    LevelCreator levelCreator = null;
    Player player = null;

    public static UI_Manager Instance { get; private set; } //singleton
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
    }
    
    void Start()
    {
        levelCreator = GameObject.Find("LevelCreator").GetComponent<LevelCreator>();

        SetupLevelUI();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateMovesLeftText()
    {
        movesLeftText.text = GameManager.Instance.LevelManager.movesLeft.ToString();
    }

    private void SetupLevelUI()
    {
        directionButtons.SetActive(levelCreator.directionsButtonsEnabled);
        forButton.SetActive(levelCreator.forButtonEnabled);
        pressButton.SetActive(levelCreator.pressButtonEnabled);
        pickupDropButton.SetActive(levelCreator.pickupDropButtonEnabled);
        useMachineButton.SetActive(levelCreator.useMachineButtonEnabled);

        VariaballHeld(false);
    }

    public void GetPlayer()
    {
        player = FindObjectOfType<Player>();
    }
    public void VariaballHeld(bool isHeld)
    {
        
        VariaballUI.SetActive(isHeld);
        Debug.Log("isHeld: " + isHeld);

        if (isHeld)
        {
            //Player player = GameObject.Find("Player").GetComponent<Player>();
            
            if (player.myVariaball.isNull)
            {
                variableText.text = "Null";
            }
            else
            {
                variableText.text = player.myVariaball.myInt.ToString();
            }
        }
    }

    public void VariaballModified()
    {
        //Player player = GameObject.Find("Player").GetComponent<Player>();
        
        variableText.text = player.myVariaball.myInt.ToString();
    }

    public void DirectionPressed(int directionNum)
    {
        //Debug.Log(GameObject.Find("Player").GetComponent<Player>());

        //Player player = GameObject.Find("Player").GetComponent<Player>();
        
        
        player.DirectionPressed(directionNum);
    }

    public void PressSwitchPressed()
    {
        //Player player = GameObject.Find("Player").GetComponent<Player>();
        
        //player = GameObject.Find("Player").GetComponent<Player>();
        
        player.PressSwitchInitial();
    }

    public void PickupOrDropPressed()
    {
        //Player player = GameObject.Find("Player").GetComponent<Player>();
        
        player.PickUpOrDropInitial();
    }

    public void UseMachinePressed()
    {
        //Player player = GameObject.Find("Player").GetComponent<Player>();
        
        player.UseMachineInitial();
    }

    public void OnSettingsButton()
    {
        SettingsPanel.SetActive(true);
    }

    public void OnPlayButton()
    {
        SettingsPanel.SetActive(false);
    }

    public void OnExitButton()
    {
        SceneManager.LoadScene(0);
    }

    public void OnResetButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

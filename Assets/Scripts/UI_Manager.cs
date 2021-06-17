﻿using System;
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
    [SerializeField] private GameObject EndPanel = null;
    [SerializeField] private TextMeshProUGUI EndText = null;
    [SerializeField] private Sprite ButtonIdle;
    [SerializeField] private Sprite ButtonPressed;
    LevelCreator levelCreator = null;
    Player player = null;
    ButtonType prevButton = ButtonType.None;

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
        LevelManager.OnLevelEnd += OnLevelEnd;
    }

    void OnLevelEnd()
    {
        EndText.text = CodeBuilder.Instance.BuildCode();
        EndPanel.SetActive(true);
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

        SetPressedButton(ButtonType.None);
        player.DirectionPressed(directionNum);
    }

    public void PressSwitchPressed()
    {
        //Player player = GameObject.Find("Player").GetComponent<Player>();

        //player = GameObject.Find("Player").GetComponent<Player>();
        SetPressedButton(ButtonType.PressSwitch);
        player.PressSwitchInitial();
    }

    public void PickupOrDropPressed()
    {
        //Player player = GameObject.Find("Player").GetComponent<Player>();
        SetPressedButton(ButtonType.PickupOrDrop);
        player.PickUpOrDropInitial();
    }

    private void SetPressedButton(ButtonType btype)
    {
        if (prevButton == btype)
        {
            SetImage(btype, ButtonIdle);
            btype = ButtonType.None;
        }
        else
        {
            SetImage(prevButton, ButtonIdle);
            SetImage(btype, ButtonPressed);
        }
        prevButton = btype;
    }

    private void SetImage(ButtonType btype, Sprite buttonSprite)
    {
        switch (btype)
        {
            case ButtonType.PickupOrDrop:
                pickupDropButton.GetComponent<Image>().sprite = buttonSprite;
                break;
            case ButtonType.PressSwitch:
                pressButton.GetComponent<Image>().sprite = buttonSprite;
                break;
            case ButtonType.UseMachine:
                useMachineButton.GetComponent<Image>().sprite = buttonSprite;
                break;
            default:
                break;
        }
    }

    public void UseMachinePressed()
    {
        //Player player = GameObject.Find("Player").GetComponent<Player>();
        SetPressedButton(ButtonType.UseMachine);
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnNextButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}

public enum ButtonType
{
    None,
    PickupOrDrop,
    PressSwitch,
    UseMachine
}
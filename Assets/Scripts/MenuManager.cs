using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject MenuPanel;
    [SerializeField] private GameObject LevelsPanel;
    [SerializeField] private Button ContinueBTN = null;
    [SerializeField] private GameObject CreditsPanel = null;
    [SerializeField] private GameObject OptionsPanel = null;
    [SerializeField] private GameObject HowToPlayPanel = null;

    private int lastLevel = 0;

    public void Start()
    {
        OnBack();
        lastLevel = PlayerPrefs.GetInt("level", 0);
        if (lastLevel == 0)
        {
            Debug.Log("no saved level");
            ContinueBTN.interactable = false;
        }
    }

    private void ShowLevels()
    {
        MenuPanel.SetActive(false);
        LevelsPanel.SetActive(true);
    }

    public void OnBack()
    {
        MenuPanel.SetActive(true);
        LevelsPanel.SetActive(false);
        CreditsPanel.SetActive(false);
        OptionsPanel.SetActive(false);
        HowToPlayPanel.SetActive(false);
    }

    public void LoadLevel(int number)
    {
        SceneManager.LoadScene(number.ToString("D2"));
    }

    public void OnStart()
    {
        ShowLevels();
    }

    public void OnContinue()
    {
        SceneManager.LoadScene(lastLevel.ToString("D2"));
    }

    public void OnHowToPlay()
    {
        HowToPlayPanel.SetActive(true);
        MenuPanel.SetActive(false);
    }

    public void OnOptions()
    {
        OptionsPanel.SetActive(true);
        MenuPanel.SetActive(false);
    }

    public void OnCredits()
    {
        CreditsPanel.SetActive(true);
        MenuPanel.SetActive(false);
    }
}

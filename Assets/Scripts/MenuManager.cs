using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject Menu;
    [SerializeField] private GameObject Levels;

    public void Start()
    {
        Menu.SetActive(true);
        Levels.SetActive(false);
    }

    public void OnStart()
    {
        ShowLevels();
    }

    private void ShowLevels()
    {
        Menu.SetActive(false);
        Levels.SetActive(true);
    }

    public void OnBack()
    {
        Menu.SetActive(true);
        Levels.SetActive(false);
    }

    public void LoadLevel(int number)
    {
        SceneManager.LoadScene(number.ToString("D2"));
    }
}

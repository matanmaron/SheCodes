using Shecodes.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Button BtnExit;
    [SerializeField] Button BtnReset;
    [SerializeField] TextMeshProUGUI TmpSteps;

    void Start()
    {
        BtnExit.onClick.AddListener(OnExit);
        BtnReset.onClick.AddListener(OnReset);
    }

    private void OnReset()
    {
        GameManager.Instance.ResetLevel();
    }

    private void OnExit()
    {
        GameManager.Instance.LoadLevel(0);
    }

    internal void SetSteps(int number)
    {
        TmpSteps.text = number.ToString();
    }
}

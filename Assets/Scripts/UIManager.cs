using Shecodes.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Shecodes.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] Button BtnExit;
        [SerializeField] Button BtnNext;
        [SerializeField] Button BtnReset;
        [SerializeField] Button BtnResetOver;
        [SerializeField] TextMeshProUGUI TmpSteps;
        [SerializeField] GameObject GameOverPanel;
        [SerializeField] GameObject WinPanel;

        void Start()
        {
            BtnExit.onClick.AddListener(OnExit);
            BtnNext.onClick.AddListener(OnWin);
            BtnReset.onClick.AddListener(OnReset);
            BtnResetOver.onClick.AddListener(OnResetOver);
        }

        private void OnResetOver()
        {
            GameOverPanel.SetActive(false);
            GameManager.Instance.ResetLevel();
        }

        private void OnReset()
        {
            GameManager.Instance.ResetLevel();
        }

        private void OnWin()
        {
            WinPanel.SetActive(false);
            OnExit();
        }

        private void OnExit()
        {
            GameManager.Instance.LoadLevel(0);
        }

        internal void SetSteps(int number)
        {
            TmpSteps.text = number.ToString();
        }

        internal void GameOver()
        {
            GameOverPanel.SetActive(true);
        }

        internal void YouWin()
        {
            WinPanel.SetActive(true);
        }
    }
}
using Shecodes.Frame;
using Shecodes.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shecodes.Levels
{
    public class Level3 : MonoBehaviour
    {
        [SerializeField] Button ButtonLeft;
        [SerializeField] Button ButtonUp;
        [SerializeField] Button ButtonDown;
        [SerializeField] Button ButtonFor;
        [SerializeField] TextMeshProUGUI ButtonForText;
        [SerializeField] int MaxSteps;
        [SerializeField] Vector2Int PlayerPosition;
        [SerializeField] Vector2Int GoalPosition;
        [SerializeField] List<int> ForRange;
        int forIndex = 0;

        void Start()
        {
            GameManager.Instance.SetPlayer(PlayerPosition.x, PlayerPosition.y);
            GameManager.Instance.SetGoal(GoalPosition.x, GoalPosition.y);
            GameManager.Instance.SetSteps(MaxSteps);
            ButtonLeft.onClick.AddListener(OnButtonClickLeft);
            ButtonUp.onClick.AddListener(OnButtonClickUp);
            ButtonDown.onClick.AddListener(OnButtonClickDown);
            ButtonFor.onClick.AddListener(OnButtonClickFor);
            ButtonForText.text = $"For: {ForRange[forIndex]}";
        }

        private void OnButtonClickLeft()
        {
            GameManager.Instance.ForMovePlayer(Direction.Left, ForRange[forIndex]);
            forIndex = 0;
            ButtonForText.text = $"For: {ForRange[forIndex]}";
        }

        private void OnButtonClickDown()
        {
            GameManager.Instance.ForMovePlayer(Direction.Down, ForRange[forIndex]);
            forIndex = 0;
            ButtonForText.text = $"For: {ForRange[forIndex]}";
        }

        private void OnButtonClickUp()
        {
            GameManager.Instance.ForMovePlayer(Direction.Up, ForRange[forIndex]);
            forIndex = 0;
            ButtonForText.text = $"For: {ForRange[forIndex]}";
        }

        private void OnButtonClickFor()
        {
            forIndex++;
            if (forIndex > ForRange.Count-1)
            {
                forIndex = 0;
            }
            ButtonForText.text = $"For: {ForRange[forIndex]}";
        }
    }
}
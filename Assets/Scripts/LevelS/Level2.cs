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
    public class Level2 : MonoBehaviour
    {
        [SerializeField] Button ButtonRight;
        [SerializeField] Button ButtonUp;
        [SerializeField] Button ButtonDown;
        [SerializeField] Button ButtonFor;
        [SerializeField] TextMeshProUGUI ButtonForText;
        [SerializeField] int MaxSteps;
        [SerializeField] Vector2Int PlayerPosition;
        [SerializeField] Vector2Int GoalPosition;
        [SerializeField] Vector2Int ForRange;

        private int forInt = 0;

        void Start()
        {
            GameManager.Instance.SetPlayer(PlayerPosition.x, PlayerPosition.y);
            GameManager.Instance.SetGoal(GoalPosition.x, GoalPosition.y);
            GameManager.Instance.SetSteps(MaxSteps);
            ButtonRight.onClick.AddListener(OnButtonClickRight);
            ButtonUp.onClick.AddListener(OnButtonClickUp);
            ButtonDown.onClick.AddListener(OnButtonClickDown);
            ButtonFor.onClick.AddListener(OnButtonClickFor);
            forInt = ForRange.x;
            ButtonForText.text = $"For: {forInt}";
        }

        private void OnButtonClickRight()
        {
            GameManager.Instance.ForMovePlayer(Direction.Right, forInt);
        }

        private void OnButtonClickDown()
        {
            GameManager.Instance.ForMovePlayer(Direction.Down, forInt);
        }

        private void OnButtonClickUp()
        {
            GameManager.Instance.ForMovePlayer(Direction.Up, forInt);
        }

        private void OnButtonClickFor()
        {
            forInt++;
            if (forInt > ForRange.y)
            {
                forInt = ForRange.x;
            }
            ButtonForText.text = $"For: {forInt}";
        }
    }
}
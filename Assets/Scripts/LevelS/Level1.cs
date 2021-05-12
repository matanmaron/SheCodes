using Shecodes.Frame;
using Shecodes.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shecodes.Levels
{
    public class Level1 : MonoBehaviour
    {
        [SerializeField] Button ButtonRight;
        [SerializeField] Button ButtonUp;
        [SerializeField] int MaxSteps;
        [SerializeField] Vector2Int PlayerPosition;
        [SerializeField] Vector2Int GoalPosition;

        void Start()
        {
            GameManager.Instance.SetPlayer(PlayerPosition.x, PlayerPosition.y);
            GameManager.Instance.SetGoal(GoalPosition.x, GoalPosition.y);
            GameManager.Instance.SetSteps(MaxSteps);
            ButtonRight.onClick.AddListener(OnButtonClickRight);
            ButtonUp.onClick.AddListener(OnButtonClickUp);
        }

        private void OnButtonClickRight()
        {
            GameManager.Instance.MovePlayer(Direction.Right);
        }

        private void OnButtonClickUp()
        {
            GameManager.Instance.MovePlayer(Direction.Up);
        }
    }
}
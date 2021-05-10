using Shecodes.Frame;
using Shecodes.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shecodes.Levels
{
    public class Level1 : MonoBehaviour
    {
        [SerializeField] Button ButtonRight;
        [SerializeField] int MaxSteps;

        void Start()
        {
            GameManager.Instance.SetPlayer(-7, 3);
            GameManager.Instance.SetGoal(0,3);
            GameManager.Instance.SetSteps(MaxSteps);
            ButtonRight.onClick.AddListener(OnButtonClickRight);
        }

        private void OnButtonClickRight()
        {
            GameManager.Instance.MovePlayer(Direction.Right);
        }
    }
}
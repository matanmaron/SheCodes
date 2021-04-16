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
        [SerializeField] Button ButtonDown;
        [SerializeField] Button ButtonRight;
        [SerializeField] int MaxStemps;

        void Start()
        {
            GameManager.Instance.SetPlayer(-7, 3);
            GameManager.Instance.SetSteps(MaxStemps);
            ButtonDown.onClick.AddListener(OnButtonClickDown);
            ButtonRight.onClick.AddListener(OnButtonClickRight);
        }

        private void OnButtonClickDown()
        {
            GameManager.Instance.MovePlayer(Direction.Down);
        }

        private void OnButtonClickRight()
        {
            GameManager.Instance.MovePlayer(Direction.Right);
        }
    }
}
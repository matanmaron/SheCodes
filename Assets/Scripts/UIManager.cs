using Shecodes.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shecodes.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] Button BtnLeft;
        [SerializeField] Button BtnRight;
        [SerializeField] Button BtnUp;
        [SerializeField] Button BtnDown;

        void Start()
        {
            BtnLeft.onClick.AddListener(delegate { OnBuittonClick(Direction.Left); });
            BtnRight.onClick.AddListener(delegate { OnBuittonClick(Direction.Right); });
            BtnUp.onClick.AddListener(delegate { OnBuittonClick(Direction.Up); });
            BtnDown.onClick.AddListener(delegate { OnBuittonClick(Direction.Down); });
        }

        void OnBuittonClick(Direction direction)
        {
            GameManager.Instance.MovePlayer(direction);
        }
    }
}
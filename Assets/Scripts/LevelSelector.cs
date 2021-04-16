using Shecodes.Frame;
using Shecodes.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shecodes.Levels
{
    public class LevelSelector : MonoBehaviour
    {
        [SerializeField] List<Button> Buttons;

        void Start()
        {
            int i = 1;
            foreach (var btn in Buttons)
            {
                int locali = i;
                btn.onClick.AddListener(delegate { OnButtonClick(locali); });
                i++;
            }
        }

        private void OnButtonClick(int number)
        {
            GameManager.Instance.LoadLevel(number);
        }
    }
}
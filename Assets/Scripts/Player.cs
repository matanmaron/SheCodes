using Shecodes.Frame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shecodes.Managers
{
    public class Player : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == Consts.GOAL)
            {
                GameManager.Instance.YouWin();
            }
        }
    }
}
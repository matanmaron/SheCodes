using Shecodes.Frame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shecodes.Managers
{
    public class Player : MonoBehaviour
    {
        bool goingBack = true;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == Consts.GOAL)
            {
                GameManager.Instance.GameOverWin();
            }
            else if (collision.gameObject.tag == Consts.FREESPOT)
            {
                if (goingBack)
                {
                    goingBack = false;
                    return;
                }
                GameManager.Instance.MoveSuccessful();
            }
            else
            {
                goingBack = true;
                GameManager.Instance.GoBack();
            }
        }
    }
}
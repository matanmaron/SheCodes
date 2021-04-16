using Shecodes.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shecodes.Managers
{
    public class GameManager : MonoBehaviour
    {
        #region singletone
        private static GameManager _instance;
        public static GameManager Instance { get { return _instance; } }
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }
        #endregion

        [SerializeField] Transform Player;

        internal void MovePlayer(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    Player.position += Vector3.left;
                    break;
                case Direction.Right:
                    Player.position += Vector3.right;
                    break;
                case Direction.Up:
                    Player.position += Vector3.up;
                    break;
                case Direction.Down:
                    Player.position += Vector3.down;
                    break;
                default:
                    break;
            }
        }
    }
}
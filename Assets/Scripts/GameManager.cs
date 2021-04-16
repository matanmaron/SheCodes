using Shecodes.Frame;
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
        private GameObject currentLevel;
        private int currentLevelNumber;

        private void Start()
        {
            currentLevel = Instantiate(Resources.Load(Consts.LEVEL_SELECTOR) as GameObject);
        }

        internal void ResetPlayer()
        {
            if (Player != null)
            {
                Player.position = Vector3.zero;
            }
        }
        internal void ResetLevel()
        {
            LoadLevel(currentLevelNumber);
        }

        internal void LoadLevel(int number)
        {
            Destroy(currentLevel);
            if (number == 0)
            {
                currentLevel = Instantiate(Resources.Load($"{Consts.LEVEL_SELECTOR}") as GameObject);
            }
            else
            {
                currentLevel = Instantiate(Resources.Load($"{Consts.LEVEL}{number}") as GameObject);
            }
            ResetPlayer();
            currentLevelNumber = number;
        }

        internal void MovePlayer(Direction direction)
        {
            if (CanMove(direction))
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
        internal bool CanMove(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                case Direction.Right:
                    if (Mathf.Abs(Player.position.y)<4)
                    {
                        return true;
                    }
                    break;
                case Direction.Up:
                case Direction.Down:
                    if (Mathf.Abs(Player.position.x) < 8)
                    {
                        return true;
                    }
                    break;
                default:
                    break;
            }
            return false;
        }
    }
}
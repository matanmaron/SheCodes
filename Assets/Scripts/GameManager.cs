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

        [SerializeField] UIManager UIManager;
        [SerializeField] Transform Player;
        private GameObject currentLevel;
        private int currentLevelNumber;
        private int maxSteps;

        private void Start()
        {
            currentLevel = Instantiate(Resources.Load(Consts.LEVEL_SELECTOR) as GameObject);
        }

        internal void SetPlayer(int x, int y)
        {
            if (Player != null)
            {
                Player.position = new Vector3(x, y, 0);
            }
        }

        internal void SetSteps(int stemps)
        {
            maxSteps = stemps;
            UIManager.SetSteps(maxSteps);
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
                maxSteps--;
                if (maxSteps == 0)
                {
                    GameOver();
                }
                UIManager.SetSteps(maxSteps);
            }
        }

        internal void YouWin()
        {
            UIManager.YouWin();
        }

        private void GameOver()
        {
            UIManager.GameOver();
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
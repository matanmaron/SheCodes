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
        [SerializeField] Transform Goal;
        private GameObject currentLevel;
        private int currentLevelNumber;
        private int maxSteps;
        private Vector3 prevPosition;
        bool gameOver = true;

        private void Start()
        {
            currentLevel = Instantiate(Resources.Load(Consts.LEVEL_SELECTOR) as GameObject);
        }

        internal void SetPlayer(int x, int y)
        {
            if (Player != null)
            {
                Player.position = new Vector3(x, y, -5);
            }
        }

        internal void SetGoal(int x, int y)
        {
            if (Goal != null)
            {
                Goal.position = new Vector3(x, y, -5);
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
            gameOver = false;
        }

        internal void MovePlayer(Direction direction)
        {
            if (gameOver)
            {
                return;
            }
            Debug.Log("[Move] Player Moving");
            prevPosition = Player.position;
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

        internal void GoBack()
        {
            Debug.Log("[Move] Can't Move, Go Back");
            Player.position = prevPosition;
        }

        internal void MoveSuccessful()
        {
            Debug.Log("[Move] Move Successful");
            maxSteps--;
            if (maxSteps == 0)
            {
                GameOverLose();
            }
            UIManager.SetSteps(maxSteps);
        }

        internal void GameOverWin()
        {
            Debug.Log("[Game] Game Over Win");
            gameOver = true;
            UIManager.YouWin();
        }

        private void GameOverLose()
        {
            Debug.Log("[Game] Game Over Lose");
            gameOver = true;
            UIManager.GameOver();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnSetting
{
    Player1,
    Player2,
    Computer,
}

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager GetInstance()
    {
        return _instance;
    }


    [Header("References")]
    public UIController UIController;
    public Computer computer;

    private bool gameStart;
    private char[,] board;
    public char[,] GetBoard()
    {
        return board;
    }
    private bool xTurn;
    private TurnSetting[] turnSettings;

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        board = new char[3, 3] {
            { '-', '-', '-' },
            { '-', '-', '-' },
            { '-', '-', '-' }
        };
    }

    /// <summary>
    /// Start game, set ui state
    /// </summary>
    /// <param name="pvp">if this game is pvp</param>
    public void StartGame(bool pvp = true)
    {
        gameStart = true;
        UIController.GameStart();
        xTurn = true;
        if (pvp)
        {
            turnSettings = new TurnSetting[2] { TurnSetting.Player1, TurnSetting.Player2 };
        }
        else
        {
            bool playerFirst = Random.Range(0, 2) == 0;
            UIController.SetInfo($"{(playerFirst ? TurnSetting.Player1 : TurnSetting.Computer)} First", 2);
            turnSettings = new TurnSetting[2] { playerFirst ? TurnSetting.Player1 : TurnSetting.Computer, playerFirst ? TurnSetting.Computer : TurnSetting.Player1 };
        }
        NextTurn();
    }

    private void NextTurn()
    {
        xTurn = !xTurn;
        UIController.SetTurn(turnSettings[xTurn ? 1 : 0], xTurn);
        if (turnSettings[xTurn ? 1 : 0] == TurnSetting.Computer)
        {
            UIController.DisableAllCell();
            var move = computer.GetBestMove(board, xTurn);
            SetCell(move.x * 3 + move.y);
        }
        else
            UIController.EnableValidCell();
    }

    /// <summary>
    /// Reset game, clear board
    /// </summary>
    public void Reset()
    {
        gameStart = false;
        board = new char[3, 3] {
            { '-', '-', '-' },
            { '-', '-', '-' },
            { '-', '-', '-' }
        };
        UIController.ResetCells();
    }

    /// <summary>
    /// Put chess at position
    /// </summary>
    /// <param name="position"></param>

    public void SetCell(int position)
    {
        if (!gameStart)
            return;
        board[position / 3, position % 3] = xTurn ? 'x' : 'o';
        UIController.SetCell(position, xTurn ? 'x' : 'o');
        if (computer.IsFull(board) || computer.Evaluate(board, true) != 0)
        {
            switch(computer.Evaluate(board, true))
            {
                case -1:
                    UIController.SetInfo($"{turnSettings[0]} Wins! ", -1);
                    break;
                case 1:
                    UIController.SetInfo($"{turnSettings[1]} Wins! ", -1);
                    break;
                default:
                    UIController.SetInfo("Awww, it's a tie! ", -1);
                    break;
            }
            UIController.DisableAllCell();
        }
        else
        {
            NextTurn();
        }
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

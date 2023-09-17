using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    /// <summary>
    /// Determine whether player/computer wins in this board
    /// </summary>
    /// <param name="board"></param>
    /// <param name="useX">true if player/computer using X</param>
    /// <returns>win: 1, lose: -1, tie or not finish: 0</returns>
    public int Evaluate(char[,] board, bool useX)
    {
        for (var row = 0; row < 3; row++)
        {
            if (board[row, 0] == board[row, 1] && board[row, 2] == board[row, 1])
            {
                if (board[row, 0] == (useX ? 'x' : 'o'))
                    return 1;
                if (board[row, 0] == (useX ? 'o' : 'x'))
                    return -1;
            }
        }
        for (var col = 0; col < 3; col++)
        {
            if (board[0, col] == board[1, col] && board[2, col] == board[1, col])
            {
                if (board[0, col] == (useX ? 'x' : 'o'))
                    return 1;
                if (board[0, col] == (useX ? 'o' : 'x'))
                    return -1;
            }
        }
        if (board[0, 0] == board[1, 1] && board[2, 2] == board[1, 1])
        {
            if (board[0, 0] == (useX ? 'x' : 'o'))
                return 1;
            if (board[0, 0] == (useX ? 'o' : 'x'))
                return -1;
        }
        if (board[0, 2] == board[1, 1] && board[2, 0] == board[1, 1])
        {
            if (board[0, 2] == (useX ? 'x' : 'o'))
                return 1;
            if (board[0, 2] == (useX ? 'o' : 'x'))
                return -1;
        }
        return 0;
    }

    /// <summary>
    /// Minimax algorithm
    /// </summary>
    /// <param name="board"></param>
    /// <param name="useX">true if current move using X</param>
    /// <param name="isMax">true if this step is max</param>
    /// <returns></returns>
    public int MinMax(char[,] board, bool useX, bool isMax)
    {
        if (Evaluate(board, useX) != 0 || IsFull(board))
            return Evaluate(board, useX);

        if (isMax)
        {
            int maxValue = -2;

            for (var row = 0; row < 3; row++)
            {
                for (var col = 0; col < 3; col++)
                {
                    if (board[row, col] == '-')
                    {
                        board[row, col] = useX ? 'x' : 'o';
                        maxValue = Mathf.Max(maxValue, MinMax(board, useX, !isMax));
                        //Debug.Log($"Max {maxValue}\n{board[0, 0]},{board[0, 1]},{board[0, 2]}\n{board[1, 0]},{board[1, 1]},{board[1, 2]}\n{board[2, 0]},{board[2, 1]},{board[2, 2]}");
                        board[row, col] = '-';
                    }
                }
            }

            return maxValue;
        }
        else
        {
            int minValue = 2;

            for (var row = 0; row < 3; row++)
            {
                for (var col = 0; col < 3; col++)
                {
                    if (board[row, col] == '-')
                    {
                        board[row, col] = useX ? 'o' : 'x';
                        minValue = Mathf.Min(minValue, MinMax(board, useX, !isMax));
                        //Debug.Log($"Min {minValue}\n{board[0, 0]},{board[0, 1]},{board[0, 2]}\n{board[1, 0]},{board[1, 1]},{board[1, 2]}\n{board[2, 0]},{board[2, 1]},{board[2, 2]}");
                        board[row, col] = '-';
                    }
                }
            }

            return minValue;
        }
    }

    /// <summary>
    /// Get best move using minimax algorithm
    /// </summary>
    /// <param name="board"></param>
    /// <param name="useX">true if this move is X</param>
    /// <returns></returns>
    public Vector2Int GetBestMove(char[,] board, bool useX)
    {
        Vector2Int move = new Vector2Int(-1, -1);

        var maxValue = -2;
        var currentMoveScore = 2;
        for (var row = 0; row < 3; row++)
        {
            for (var col = 0; col < 3; col++)
            {
                if (board[row, col] == '-')
                {
                    board[row, col] = useX ? 'x' : 'o';
                    currentMoveScore = MinMax(board, useX, false);
                    board[row, col] = '-';
                    if (currentMoveScore > maxValue)
                    {
                        move = new Vector2Int(row, col);
                        maxValue = currentMoveScore;
                    }
                }
            }
        }

        return move;
    }

    /// <summary>
    /// Return if game finish
    /// </summary>
    /// <param name="board"></param>
    /// <returns>true if game finish</returns>
    public bool IsFull(char[,] board)
    {
        for (var row = 0; row < 3; row++)
        {
            for (var col = 0; col <3; col++)
            {
                if (board[row, col] == '-')
                    return false;
            }
        }
        return true;
    }
}

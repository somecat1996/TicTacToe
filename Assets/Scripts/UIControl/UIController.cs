using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("UI References")]
    public Transform cellGroup;
    public TMP_Text turnInfo;
    public GameObject pvpButton;
    public GameObject pveButton;
    public GameObject resetButton;
    public GameObject resultPanel;

    private List<TMP_Text> cellList;
    // Start is called before the first frame update
    void Start()
    {
        cellList = new List<TMP_Text>();
        // Load all board cells
        for (var i = 0; i < cellGroup.childCount; i++)
        {
            cellList.Add(cellGroup.GetChild(i).GetChild(0).GetComponent<TMP_Text>());
        }
        ResetCells();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Reset board to empty
    /// </summary>
    public void ResetCells()
    {
        turnInfo.text = "";
        pvpButton.SetActive(true);
        pveButton.SetActive(true);
        resetButton.SetActive(false);
        resultPanel.SetActive(false);
        foreach (var i in cellList)
        {
            i.text = "";
        }
    }

    /// <summary>
    /// Set cell
    /// </summary>
    /// <param name="index"></param>
    /// <param name="ch"></param>
    public void SetCell(int index, char ch)
    {
        cellGroup.GetChild(index).GetComponent<Button>().enabled = false;
        if (ch == 'o')
            cellList[index].text = "O";
        else
            cellList[index].text = "X";
    }

    /// <summary>
    /// Disable all cells in board
    /// </summary>
    public void DisableAllCell()
    {
        for (var i = 0; i < cellGroup.childCount; i++)
        {
            cellGroup.GetChild(i).GetComponent<Button>().enabled = false;
        }
    }

    /// <summary>
    /// Enable valid cells in board
    /// </summary>
    public void EnableValidCell()
    {
        for (var i = 0; i < cellGroup.childCount; i++)
        {
            if (GameManager.GetInstance().GetBoard()[i / 3, i % 3] == '-')
                cellGroup.GetChild(i).GetComponent<Button>().enabled = true;
        }
    }

    /// <summary>
    /// Start game
    /// hide pvp & pve button, show reset button
    /// </summary>
    public void GameStart()
    {
        pvpButton.SetActive(false);
        pveButton.SetActive(false);
        resetButton.SetActive(true);
    }

    /// <summary>
    /// Set turn info text
    /// </summary>
    /// <param name="turn"></param>
    /// <param name="isX"></param>
    public void SetTurn(TurnSetting turn, bool isX)
    {
        switch (turn)
        {
            case TurnSetting.Player1:
                turnInfo.text = $"Current Turn: Player1 ({(isX?"X":"O")})";
                break;
            case TurnSetting.Player2:
                turnInfo.text = $"Current Turn: Player2 ({(isX ? "X" : "O")})";
                break;
            case TurnSetting.Computer:
                turnInfo.text = $"Current Turn: Computer ({(isX ? "X" : "O")})";
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Show info panel
    /// </summary>
    /// <param name="text"></param>
    /// <param name="showTime"></param>
    public void SetInfo(string text, float showTime)
    {
        resultPanel.SetActive(true);
        resultPanel.GetComponent<ResultPanel>().SetInfo(text, showTime);
    }
}

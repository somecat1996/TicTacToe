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
        cellGroup.GetComponent<GridLayoutGroup>().cellSize = new Vector2((Screen.height / 485 * 1080 - 6) / 3, (Screen.height / 485 * 1080 - 6) / 3);
        cellGroup.GetComponent<GridLayoutGroup>().spacing = new Vector2(3, 3);
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

    public void SetCell(int index, char ch)
    {
        cellGroup.GetChild(index).GetComponent<Button>().enabled = false;
        if (ch == 'o')
            cellList[index].text = "O";
        else
            cellList[index].text = "X";
    }

    public void DisableAllCell()
    {
        for (var i = 0; i < cellGroup.childCount; i++)
        {
            cellGroup.GetChild(i).GetComponent<Button>().enabled = false;
        }
    }

    public void EnableValidCell()
    {
        for (var i = 0; i < cellGroup.childCount; i++)
        {
            if (GameManager.GetInstance().GetBoard()[i / 3, i % 3] == '-')
                cellGroup.GetChild(i).GetComponent<Button>().enabled = true;
        }
    }

    public void GameStart()
    {
        pvpButton.SetActive(false);
        pveButton.SetActive(false);
        resetButton.SetActive(true);
    }

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

    public void SetInfo(string text, float showTime)
    {
        resultPanel.SetActive(true);
        resultPanel.GetComponent<ResultPanel>().SetInfo(text, showTime);
    }
}

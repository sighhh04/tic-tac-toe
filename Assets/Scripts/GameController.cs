using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public int whosTurn; // x is 0 and o is 1
    public int turnCount; //counts the number of turns played
    public GameObject[] turnIcons; //displays whos turn it is
    public Sprite[] playerIcons; //0 is x and 1 is O icon
    public Button[] tictactoesSpaces; //playable space for our space
    public int[] markedSpaces; //identifies which spaces have been marked by which player
    public Text winnerText; //hold text component of winner text
    public GameObject[] winningLines; //hold all the different lines to show winner
    public GameObject winnerPanel;
    public int xScore;
    public int oScore;
    public Text xScoreText;
    public Text oScoreText;

    // Start is called before the first frame update
    void Start()
    {
        GameSetup();
    }

    void GameSetup()
    {
        whosTurn = 0;
        turnCount = 0;
        turnIcons[0].SetActive(true);
        turnIcons[1].SetActive(false);

        for (int i = 0; i < tictactoesSpaces.Length; i++)
        {
            tictactoesSpaces[i].interactable = true;
            tictactoesSpaces[i].GetComponent<Image>().sprite = null;
        }
        for (int i = 0; i < markedSpaces.Length; i++)
        {
            markedSpaces[i] = -100;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TicTacToeButton(int whichNumber)
    {
        Debug.Log(whichNumber);
        tictactoesSpaces[whichNumber].image.sprite = playerIcons[whosTurn];
        tictactoesSpaces[whichNumber].interactable = false;

        markedSpaces[whichNumber] = whosTurn + 1;
        turnCount++;
        if (turnCount > 4)
        {
            bool isWinner = WinnerCheck();
            if (turnCount == 9 && isWinner == false)
            {
                Draw();
            }
        }

        if (whosTurn == 0)
        {
            whosTurn = 1;
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);

        }
        else
        {
            whosTurn = 0;
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);

        }
    }

    bool WinnerCheck()
    {
        int s1 = markedSpaces[0] + markedSpaces[1] + markedSpaces[2];
        int s2 = markedSpaces[3] + markedSpaces[4] + markedSpaces[5];
        int s3 = markedSpaces[6] + markedSpaces[7] + markedSpaces[8];
        int s4 = markedSpaces[0] + markedSpaces[3] + markedSpaces[6];
        int s5 = markedSpaces[1] + markedSpaces[4] + markedSpaces[7];
        int s6 = markedSpaces[2] + markedSpaces[5] + markedSpaces[8];
        int s7 = markedSpaces[0] + markedSpaces[4] + markedSpaces[8];
        int s8 = markedSpaces[2] + markedSpaces[4] + markedSpaces[6];
        var solutions = new int[] { s1, s2, s3, s4, s5, s6, s7, s8 };

        for (int i = 0; i < solutions.Length; i++)
        {
            if (solutions[i] == 3 * (whosTurn + 1))
            {
                winnerDisplay(i);
                return true;
            }
        }
        return false;
    }

    void winnerDisplay(int indexIn) 
    {
        winnerPanel.gameObject.SetActive(true);
        if (whosTurn == 0)
        {
            xScore++;
            xScoreText.text = xScore.ToString();
            winnerText.text = "Player X Wins!";
        }
        else if(whosTurn == 1)
        {
            oScore++;
            oScoreText.text = oScore.ToString();
            winnerText.text = "Player O Wins!";
        }
        winningLines[indexIn].SetActive(true);
    }

    public void Rematch()
    {
        GameSetup();
        for (int i = 0; i < winningLines.Length; i++)
        {
            winningLines[i].SetActive(false);
        }
        winnerPanel.SetActive(false);
    }

    public void Restart()
    {
        Rematch();
        xScore = 0;
        oScore = 0;
        xScoreText.text = "0";
        oScoreText.text = "0";
    }

    void Draw()
    {
        winnerPanel.SetActive(true);
        winnerText.text = "DRAW!";
    }
}

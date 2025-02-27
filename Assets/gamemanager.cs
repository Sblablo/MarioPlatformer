using TMPro;
using UnityEngine;

public class gamemanager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI pointsText;

    private int coins = 0;
    private bool isGameOver = false;
    private int points = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int timeleft = 100 - (int)Time.time;
        if (!isGameOver && timeleft <= 0)
        {
            isGameOver = true;
            Debug.Log("Level Failed!");
        }
        timerText.text = "Time: " + timeleft;
    }

    public void addCoin(int amount = 1)
    {
        coins += amount;
        coinsText.text = $"x{coins:D2}";
    }

    public void addPoints(int amount = 100)
    {
        points += amount;
        pointsText.text = $"Mario\n{points:D6}";
    }
}

using TMPro;
using UnityEngine;

public class gamemanager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI coinsText;

    private int coins = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int timeleft = 300 - (int)Time.time;
        timerText.text = "Time: " + timeleft;

        coinsText.text = $"x{coins}";
    }

    public void addCoin()
    {
        coins += 1;
    }
}

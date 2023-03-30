using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
  
     public GameObject Restart;
    public static Controller instance;
    public static int money;
    public int StartMoney;
    public static Text CoinText;
    public Text Cointext;

    private void Start()
    {
        Restart.SetActive(false);
        instance = this;    
        money = StartMoney;
        CoinText = Cointext;
        Cointext.text = "Coin : " + money;
    }

    void FixedUpdate()
    {
        Cointext.text = "Coin : " + money;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}

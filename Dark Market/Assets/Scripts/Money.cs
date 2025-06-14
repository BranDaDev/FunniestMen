using UnityEngine;
using TMPro;

public class Money : MonoBehaviour
{
    public TMP_Text moneyText;
    private int money;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    
    public void AddMoney()
    {
        money++;
        moneyText.text = "Money: " + money;
    }

}

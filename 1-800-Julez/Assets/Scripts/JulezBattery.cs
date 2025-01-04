using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JulezBattery : MonoBehaviour
{

    public static JulezBattery instance;

    public static int number;

    public int currentBattery, maxBattery;

    public Image PowerBar;

    private void Awake()
    {
        instance = this; 
    }

    // Start is called before the first frame update
    void Start()
    {
      currentBattery = maxBattery;  

    }

    // Update is called once per frame
    void Update()
    {
        PowerBar.fillAmount = Mathf.Clamp(currentBattery / maxBattery, 0, 1);
    }

    public void DealDamage()
    {
        currentBattery -= 1;

        if(currentBattery <= 0)
        {
            gameObject.SetActive(false);

        }

    }

}

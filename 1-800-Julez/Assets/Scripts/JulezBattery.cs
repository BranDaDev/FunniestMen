using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JulezBattery : MonoBehaviour
{

    public static JulezBattery instance;

    public static float number;

    public float currentBattery, maxBattery;

    public Image PowerBar;

    public float invincibleLength;

    private float invincibleCounter;

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

        if (invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;
        }
    }

    public void DealDamage()
    {
        if (invincibleCounter <= 0)
        {
            currentBattery -= 1;

            if (currentBattery <= 0)
            {
                gameObject.SetActive(false);

            } else
            {
                invincibleCounter = invincibleLength;
            }

        }
    }

}

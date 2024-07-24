using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeatBar : MonoBehaviour
{

    public Slider slider;
    public float heatTickInterval = 1f;
    public int heatIncrement = 1;
    public GameObject player;
    private IDamageable playerDamageable;
    // Start is called before the first frame update
    public void setHeat(int health)
    {
        slider.value = health;
        StopAllCoroutines();
        StartHeat();
        
    }
    private void Start()
    {
        playerDamageable = player.GetComponent<IDamageable>();
        setMinHeat();
        StartHeat();
    }
    public void StartHeat()
    {
        Debug.Log("heat started");
        StartCoroutine(BeginHeating());
    }

    IEnumerator BeginHeating()
    {
        while (slider.value < slider.maxValue)
        {
            yield return new WaitForSeconds(heatTickInterval);
            IncreaseHeat(heatIncrement);
        }
    }

    public void IncreaseHeat(int amount)
    {
        if (slider.value + amount < slider.maxValue)
        {
            slider.value += amount;
        }
        else
        {
            slider.value = slider.maxValue;
            playerDamageable.OnHit(100);
        }
    }

    public void setMaxHeat(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }
    public void setMinHeat()
    {
        slider.value = 0;
    }
}


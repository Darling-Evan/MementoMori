// Written by Evan Darling
// 9/12/2023
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public PlayerHealth playerHealthScript;
    public Image fillImage;
    private Slider slider;

    // Start is called before the first frame update
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {

        if (slider.value <= slider.minValue)
        {
            fillImage.enabled = false;
        }
        if (slider.value > slider.minValue && !fillImage.enabled)
        {
            fillImage.enabled = true;
        }

        float fillValue = playerHealthScript.currentHealth / (float)playerHealthScript.maxHealth;




        slider.value = fillValue;
    }
}

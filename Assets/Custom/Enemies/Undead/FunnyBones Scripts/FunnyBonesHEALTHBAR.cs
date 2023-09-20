using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FunnyBonesHEALTHBAR : MonoBehaviour
{
    public FunnyBonesHealth funnyBonesHealth;
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

        float fillValue = funnyBonesHealth.currentHealth / (float)funnyBonesHealth.maxHealth;

        slider.value = fillValue;
    }
}

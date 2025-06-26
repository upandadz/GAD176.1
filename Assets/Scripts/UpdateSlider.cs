using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateSlider : MonoBehaviour
{
    [SerializeField] private Throw throwClass; // to get holdown time value
    [SerializeField] private Slider slider;

    private float maxHoldDownTime;

    void Start()
    {
        maxHoldDownTime = throwClass.GetMaxHoldDownTime();
        slider.maxValue = maxHoldDownTime;
    }

    void Update()
    {
        slider.value += Time.deltaTime;
    }

    private void OnDisable()
    {
        slider.value = 0;
    }
}

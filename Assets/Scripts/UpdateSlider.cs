using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateSlider : MonoBehaviour
{
    [SerializeField] private Throw throwClass;
    [SerializeField] private Slider slider;
    [SerializeField] private Transform sliderTransform;

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

    public void EnableSlider()
    {
        slider.enabled = true;
    }
    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        slider.value = 0;
    }
}

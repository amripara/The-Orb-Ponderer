using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Singleton class to handle the Time Slow Meter.
/// Use the UpdateMeter method to update the current and max values of the meter.
/// </summary>
public class TimeSlowMeterManager : MonoBehaviour
{
    #region Singleton
    private static TimeSlowMeterManager _instance;
    public static TimeSlowMeterManager Instance { get => _instance; }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        } else
        {
            Destroy(this);
        }
    }
    #endregion

    [SerializeField] private Slider slider;

    public void UpdateMeter(float value, float maxValue = 0)
    {
        if (slider == null)
        {
            slider = GetComponent<Slider>();
        }
        if (maxValue != 0)
        {
            slider.maxValue = maxValue;
        }
        slider.value = value;
    }
}

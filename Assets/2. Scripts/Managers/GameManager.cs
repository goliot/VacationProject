using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }

    [Header("# ObjectPool")]
    public PoolManager pool;
    public Spawner spawner;

    [Header("# InGame")]
    public float spawnTime;

    [Header("# CaptureSlider")]
    public Slider redSlider;
    public Slider blueSlider;

    [Header("# CaptureZone")]
    public CaptureZoneController redCaptureZone;
    public CaptureZoneController blueCaptureZone;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        redSlider.value = redCaptureZone.GetComponent<CaptureZoneController>().redCaptureValue;
        blueSlider.value = blueCaptureZone.GetComponent<CaptureZoneController>().blueCaptureValue;
    }
}

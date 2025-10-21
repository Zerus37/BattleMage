using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PostProcessManager : MonoBehaviour
{
    private static PostProcessManager instance;

    [SerializeField] private Volume _volume;
    [SerializeField] private VolumeProfile _basePfile;
    [SerializeField] private VolumeProfile _speedUpPfile;

    void Start()
    {
        instance = this;
    }

    public static void Base()
	{
        instance._volume.profile = instance._basePfile;
    }

    public static void SpeedUp()
	{
        instance._volume.profile = instance._speedUpPfile;
    }
}

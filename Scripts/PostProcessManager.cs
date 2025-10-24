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
    [SerializeField] private Volume _teleportVolume;

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

    public static void Teleport(float time)
	{
        if (time <= 0)
            return;

        instance.StartCoroutine(instance.TeleportRoutine(time));
    }

    private IEnumerator TeleportRoutine(float time)
    {
        _teleportVolume.enabled = true;
        _teleportVolume.weight = 1;
        _volume.weight = 0;

        for (float t = 1; t > 0; t -= Time.deltaTime / time)
		{
            _teleportVolume.weight = t;
            _volume.weight = 1 - t;
            yield return null;
        }

        _volume.weight = 1;
        _teleportVolume.weight = 0;
        _teleportVolume.enabled = false;
    }

	private void OnDisable()
	{
        StopAllCoroutines();
	}
}

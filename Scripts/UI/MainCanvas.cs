using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    private static MainCanvas instance;

    public static Transform Transform => instance.transform;

    void Start()
    {
        instance = this;
    }
}
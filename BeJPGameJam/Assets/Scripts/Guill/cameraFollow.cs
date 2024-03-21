using System;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float timeOffSet;
    [SerializeField] private Vector3 posOffSet;

    private Vector3 velocity;
    public static GameObject instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        if (instance == null)
        {
            instance = gameObject;
        }
    }

    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + posOffSet, ref velocity, timeOffSet);
    }
}

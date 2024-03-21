using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] public bool windPowerActive;
    [SerializeField] public bool earthPowerActive;
    [SerializeField] public bool firePowerActive;
    [SerializeField] public bool waterPowerActive;
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
}

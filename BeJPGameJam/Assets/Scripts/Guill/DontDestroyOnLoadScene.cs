using UnityEngine;

public class DontDestroyOnLoadScene : MonoBehaviour
{
    [SerializeField] private GameObject[] objects;
    public static GameObject instance;
    void Awake()
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
        
        foreach (var element in objects)
        {
            DontDestroyOnLoad(element);
        }
    }
}

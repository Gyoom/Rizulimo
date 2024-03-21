using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawn : MonoBehaviour
{
    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject cam = GameObject.FindGameObjectWithTag("PlayerCamera");
        string activeSceneName = SceneManager.GetActiveScene().name;
        player.transform.position = transform.position;
        if (!activeSceneName.Equals("Hub_Remy"))
        {
            player.GetComponent<PlayerMovement>().enabled = true;
            player.GetComponent<MenuMovement>().enabled = false;
            player.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
        
        if (activeSceneName.Equals("Sakuramochi_St4"))
        {
            cam.SetActive(false);
        }
    }
}

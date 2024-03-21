using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectMovement : MonoBehaviour
{

    void Start()
    {
        string activeSceneName = SceneManager.GetActiveScene().name;
        if (activeSceneName.Equals("Hub_Remy") || activeSceneName.Equals("Sakuramochi_St4"))
        {
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<MenuMovement>().enabled = true;
            GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }

    public void ChangeMovement(string scene)
    {
        if (scene.Equals("Hub_Remy") || scene.Equals("Sakuramochi_St4"))
        {
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<MenuMovement>().enabled = true;
            GetComponent<Rigidbody2D>().gravityScale = 0;
        } else
        {
            GetComponent<PlayerMovement>().enabled = true;
            GetComponent<MenuMovement>().enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }
}

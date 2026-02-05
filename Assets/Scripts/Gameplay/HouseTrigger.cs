using Platformer.Mechanics;
using UnityEngine;

public class HouseTrigger : MonoBehaviour
{
    public GameObject gameOverUI;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision != null)
        {
            if(collision.gameObject.tag == "Player")
            {
                gameOverUI.SetActive(true);
                    Invoke("ReloadScene", 2f);
            }
        }
    }

    private void ReloadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}

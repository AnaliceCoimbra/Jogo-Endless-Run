using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisionReset : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Colidiu com obst�culo, reiniciando...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

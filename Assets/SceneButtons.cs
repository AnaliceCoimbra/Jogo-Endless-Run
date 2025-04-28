using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void Menu() 
    {
        SceneManager.LoadScene(0);

    }

    public void Controls()
    {
        SceneManager.LoadScene(3);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class Global : MonoBehaviour
{
    public GameObject menuScreen;
    public bool startPaused;
    
    void Start()
    {
        Screen.SetResolution(1920, 1080, true);

        if (startPaused)
             Pause();
    }
    
    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape))
            return;
        
        if (!menuScreen.activeSelf)
            Pause();
        else
            Play();
    }
    
    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        menuScreen.SetActive(true);
        Time.timeScale = 0;
    }
    
    public void Play()
    {
        Cursor.lockState = CursorLockMode.Locked;
        menuScreen.SetActive(false);
        Time.timeScale = 1;
    }
    
    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene("BobTheEngineer");
    }
    
    public void LoadLoop()
    {
        SceneManager.LoadScene("LoopScene"); //, LoadSceneMode.Additive);
    }
}
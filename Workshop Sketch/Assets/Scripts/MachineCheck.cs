using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MachineCheck : MonoBehaviour
{
    [SerializeField] private Global global;
    [SerializeField] private Transform player;
    [SerializeField] private RectTransform menuScreen;
    [SerializeField] private Transform machine;
    [SerializeField] private Transform extraMachine;

    private int currentCount = 0;
    private int goalCount = 0;
    
    private float oldSpeed;
    private float oldSens;
    
    public void CheckResult()
    {
        oldSpeed = player.GetComponent<PlayerMovement>().speed;
        oldSens = player.GetChild(0).GetComponent<MouseLook>().mouseSensitivity;
        
        player.GetComponent<PlayerInventory>().Busy = true;
        player.GetComponent<PlayerMovement>().speed = 0f;
        player.GetChild(0).GetComponent<MouseLook>().mouseSensitivity = 0f;
        
        for (int i = 0; i < machine.childCount; i++)
        {
            goalCount++;
            if (machine.GetChild(i).childCount > 1)
            {
                if (machine.GetChild(i).name == machine.GetChild(i).GetChild(1).name)
                    currentCount++;
            }
        }
        
        if (goalCount == currentCount)
            Victory();
        else
            Defeat();
        
        player.GetComponent<PlayerMovement>().speed = oldSpeed;
        player.GetChild(0).GetComponent<MouseLook>().mouseSensitivity = oldSens;
        player.GetComponent<PlayerInventory>().Busy = false;
    }
    
    private void Victory()
    {
        machine.gameObject.SetActive(false);
        extraMachine.gameObject.SetActive(true);
        
        menuScreen.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/egg2");
        menuScreen.GetChild(3).gameObject.SetActive(true);
        global.Pause();
    }
    
    private void Defeat()
    {
        SceneManager.LoadScene("LoopScene");
    }
}

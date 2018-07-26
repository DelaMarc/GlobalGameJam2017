using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class buttonHandler : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject controls;
    private bool activator = true;

	public void Play()
    {
        SceneManager.LoadScene("simpleFollowScene", LoadSceneMode.Single);        
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void SwitchMenus()
    {
        activator = !activator;
        mainMenu.SetActive(activator);
        controls.SetActive(!activator);
    }
        
}

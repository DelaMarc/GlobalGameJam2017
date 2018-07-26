using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DisplayLevelMenu : MonoBehaviour {
    public GameObject upgradeMenu;
    public GameObject lifeBar;
    public GameObject deadMenu;
    private bool activator = false;
    private bool end = false;
	// Use this for initialization
	void Start () {
	
	}
	
    void KeyHandler()
    {
        if (!end && Input.GetKeyDown("e"))
        {
            activator = !activator;
            upgradeMenu.SetActive(activator);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void End()
    {
        end = true;
        upgradeMenu.SetActive(false);
        lifeBar.SetActive(false);
        deadMenu.SetActive(true);
    }

	// Update is called once per frame
	void Update () {
        KeyHandler();
	}
}

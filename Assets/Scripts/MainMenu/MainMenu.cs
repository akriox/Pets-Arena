using UnityEngine;

public class MainMenu : MonoBehaviour {

    public GameObject optionsPanel;
    public GameObject quitPopUp;

    public void Update()
    {
        if (Input.GetAxis("Cancel") != 0)
        {
            ClosePanels();
        }
    }

    public void Play()
    {
        Application.LoadLevel(1);
    }

    public void showOptionsPanel(bool b)
    {
        optionsPanel.SetActive(b);
    }

    public void showQuitPopUp(bool b)
    {
        quitPopUp.SetActive(b);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void ClosePanels()
    {
        showOptionsPanel(false);
        showQuitPopUp(false);
    }
}

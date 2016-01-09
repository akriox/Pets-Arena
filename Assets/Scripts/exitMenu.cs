using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class exitMenu: MonoBehaviour {

    public Canvas quitMenu;
    public Button exitText;
    public Button noText;

    // Use this for initialization
    void Start () {
        quitMenu = quitMenu.GetComponent<Canvas>();
        exitText = exitText.GetComponent<Button>();
        noText = noText.GetComponent<Button>();
    }

	public void LoadScene (int level) {
		Application.LoadLevel (level);
	}


    public void ExitGame() {
        Application.Quit ();
    }
}
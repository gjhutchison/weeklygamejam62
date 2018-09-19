using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour {

    public Button _startButton;
    public Button _creditsButton;
    public Button _exitButton;
    public Button _backButton;

    public GameObject _mainMenuPanel;
    public GameObject _creditsPanel;

	// Use this for initialization
	void Start () {
        _startButton.onClick.AddListener(clickStart);
        _creditsButton.onClick.AddListener(clickCredits);
        _backButton.onClick.AddListener(clickBack);
        _exitButton.onClick.AddListener(clickExit);

        _mainMenuPanel.SetActive(true);
        _creditsPanel.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    private void clickStart() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level_1");
    }

    private void clickCredits() {
        _mainMenuPanel.SetActive(false);
        _creditsPanel.SetActive(true);
    }

    private void clickBack() {
        _mainMenuPanel.SetActive(true);
        _creditsPanel.SetActive(false);
    }

    private void clickExit() {
        Application.Quit();
    }
}

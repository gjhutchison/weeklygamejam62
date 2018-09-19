using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUIScript : MonoBehaviour {

    // Use this for initialization
    private PlayerController _playerController;
    private Text _gameOverText;

    // Use this for initialization
    void Start() {
        _playerController = GameObject.Find("Wolf").GetComponent<PlayerController>();
        _gameOverText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update() {
        if (!_playerController.isDead()) {
            _gameOverText.text = "";
            return;
        }

        _gameOverText.text = "GAME OVER\nPRESS R TO RESTART";

        if (Input.GetKeyUp(KeyCode.R)) {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
    }
}

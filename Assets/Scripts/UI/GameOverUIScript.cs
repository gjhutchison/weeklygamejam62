﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUIScript : MonoBehaviour {

    // Use this for initialization
    private PlayerController _playerController;
    private LevelController _levelController;
    private Text _gameOverText;

    // Use this for initialization
    void Start() {
        _levelController = GameObject.Find("Level Controller").GetComponent<LevelController>();
        _playerController = GameObject.Find("Wolf").GetComponent<PlayerController>();
        _gameOverText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update() {
        if (!_playerController.isDead() || _levelController.wonGame()) {
            _gameOverText.text = "";
            return;
        }

        _gameOverText.text = "GAME OVER\nPRESS R TO RESTART";

        if (Input.GetKeyUp(KeyCode.R)) {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
    }
}

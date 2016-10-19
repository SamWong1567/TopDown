using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    public GameObject gameOverscreen;
    bool gameOver;
    float gameOverScreenEnterStartTime;
    float secondsBeforeAllowButtonPress = 2;
    // Use this for initialization
    void Start() {
        FindObjectOfType<Player>().OnPlayerDeath += OnGameOver;


    }

    // Update is called once per frame
    void Update() {     

        if (gameOver) {
            gameOverscreen.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(gameOverscreen.GetComponent<CanvasGroup>().alpha, 1, 2*Time.deltaTime);

            if (gameOverScreenEnterStartTime + secondsBeforeAllowButtonPress < Time.time) {


                if (Input.GetKeyDown(KeyCode.Space)) {
                    SceneManager.LoadScene(0);
                }
                if (Input.touchCount == 1) {
                    if (Input.GetTouch(0).phase == TouchPhase.Began) {
                        SceneManager.LoadScene(0);
                    }
                }

                if (Input.GetMouseButton(0)) {
                    SceneManager.LoadScene(0);
                }
            }
       }

    }

    void OnGameOver() {
        gameOverScreenEnterStartTime = Time.time;
        gameOverscreen.SetActive(true);       
        gameOver = true;
    }
}

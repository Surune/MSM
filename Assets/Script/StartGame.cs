using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    private Button StartGameButton;

    // Start is called before the first frame update
    void Start()
    {
        StartGameButton = this.GetComponent<Button>();
        StartGameButton.onClick.AddListener(StartGameFunction);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartGameFunction()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }
}

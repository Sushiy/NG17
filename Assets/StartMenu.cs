using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

	public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void SetLives(int lives)
    {

    }
}

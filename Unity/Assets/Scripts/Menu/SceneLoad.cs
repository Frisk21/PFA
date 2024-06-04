using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{

    public string MyScene;

    // Start is called before the first frame update
    public void LoadScene()
    {
        SceneManager.LoadScene(MyScene);
    }

}

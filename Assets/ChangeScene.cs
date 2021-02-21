using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
            }
    public void LoadStart()
    {
        SceneManager.LoadScene("FinalGameScene");
    }

    public void Start()
    {
        Invoke("LoadStart", 3f);
    }
}

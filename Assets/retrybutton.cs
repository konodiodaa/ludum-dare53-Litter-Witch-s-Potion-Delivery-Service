using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class retrybutton : MonoBehaviour
{
    private Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(Retry);
    }

    private void Retry()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Potion Deliver Service");
    }
}

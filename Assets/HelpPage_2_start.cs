using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HelpPage_2_start : MonoBehaviour
{
    [SerializeField]
    private Button btn;
    // Start is called before the first frame update
    void Awake()
    {
        btn = GetComponentInChildren<Button>();
        btn.onClick.AddListener(EndPage);
    }

    private void EndPage()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Play");
    }
}

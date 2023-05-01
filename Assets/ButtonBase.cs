using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonBase : MonoBehaviour
{

    [SerializeField]
    private GameObject helpPanel;
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClickFunc);
    }

    void OnClickFunc()
    {
        helpPanel.SetActive(true);
    }    
}

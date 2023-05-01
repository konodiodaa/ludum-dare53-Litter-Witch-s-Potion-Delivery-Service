using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssenceGenerator : MonoBehaviour
{
    [SerializeField]
    private float coolTime;

    [SerializeField]
    private Essence m_type;

    private AudioSource generateSource;
    [SerializeField]
    private GameObject fxPrefab;

    private float Timer;

    private void Awake()
    {
        Timer = coolTime;
        generateSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Timer -= Time.deltaTime;
        if(Timer <= 0)
        {
            generateSource.Play();
            fxPrefab.SetActive(true);
            EventCenter.Broadcast(EventDefine.dropEssence,
                this.transform.position, m_type);
            Timer = coolTime;
        }
    }
}

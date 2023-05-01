using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionPanel : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> potionDecs;
    
    [SerializeField]
    private GameObject[] SFXs;

    [SerializeField]
    private Text count;

    private int revealedCount;

    private AudioSource audioReveal;

    private Vector3[] offset =
    {
        new Vector3(50,0,0),
        new Vector3(100,50,0),
        new Vector3(-100,-50,0),
        new Vector3(-50, 50,0),

    };



    private void Awake()
    {
        revealedCount = 0;
        audioReveal = GetComponent<AudioSource>();
        EventCenter.AddListener<int>(EventDefine.Reveal, RevealPotion);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener<int>(EventDefine.Reveal, RevealPotion);
    }

    private void Update()
    {
        count.text = revealedCount.ToString();
    }

    private void RevealPotion(int index)
    {
        if(!potionDecs[index].GetComponent<PotionDec>().getReveal())
        {
            audioReveal.Play();
            revealedCount++;
            if(revealedCount >= 10)
            {
                EventCenter.Broadcast(EventDefine.Win);
            }

            potionDecs[index].GetComponent<PotionDec>().Reveal();
            SFXs[0].transform.position = potionDecs[index].transform.position + offset[0];
            SFXs[1].transform.position = potionDecs[index].transform.position + offset[1];
            SFXs[2].transform.position = potionDecs[index].transform.position + offset[2];
            SFXs[3].transform.position = potionDecs[index].transform.position + offset[3];
            SFXs[0].SetActive(true);
            SFXs[1].SetActive(true);
            SFXs[2].SetActive(true);
            SFXs[3].SetActive(true);
           // ice_SFX.transform.position = potionDecs[index].transform.position;
           // ice_SFX.SetActive(true);
        }

    }
}

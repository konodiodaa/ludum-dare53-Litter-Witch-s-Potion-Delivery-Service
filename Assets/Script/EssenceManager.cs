using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssenceManager : MonoBehaviour
{
    [SerializeField]
    private GameObject essence;
    private void Awake()
    {
        EventCenter.AddListener<Vector3, Essence>(EventDefine.dropEssence,EssenceCreate);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<Vector3, Essence>(EventDefine.dropEssence, EssenceCreate);
    }

    private void EssenceCreate(Vector3 pos,Essence type)
    {
        GameObject go = Instantiate(essence, pos, Quaternion.identity);
        go.GetComponent<EssenceBall>().Init(type);
    }
}

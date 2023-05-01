using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportManager : MonoBehaviour
{
    [SerializeField]
    private Transform heroTop;
    [SerializeField]
    private Transform heroBottom;

    [SerializeField]
    private Vector3 offsetY;

    private void Awake()
    {
        EventCenter.AddListener<bool, Transform>(EventDefine.Transport, Transmit);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<bool, Transform>(EventDefine.Transport, Transmit);
    }

    private void Transmit(bool isTop,Transform transform)
    {
        if(isTop)
        {
            transform.position = transform.position - offsetY;
            transform.gameObject.GetComponent<Enemy>().Init(heroBottom,!isTop);
        }
        else
        {
            transform.position = transform.position + offsetY;
            transform.gameObject.GetComponent<Enemy>().Init(heroTop, !isTop);
        }
    }
}

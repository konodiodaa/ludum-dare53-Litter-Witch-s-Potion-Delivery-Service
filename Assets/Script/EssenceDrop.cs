using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Essence
{
    Green,
    Red,
    Blue
};


public class EssenceDrop : MonoBehaviour
{
    [SerializeField]
    private Essence m_type;

    private bool isBanned;
    private bool isUniversal;

    private void Awake()
    {
        isBanned = false;
    }

    public void DropEssence()
    {
        if (isUniversal)
        {
            Essence[] types = { Essence.Green, Essence.Red, Essence.Blue };
            int r = Random.Range(0, types.Length);
            Essence e = types[r];
            EventCenter.Broadcast(EventDefine.dropEssence,
                this.transform.position, e);
        }

        if (!isBanned)
            EventCenter.Broadcast(EventDefine.dropEssence,this.transform.position,m_type);

    }

    public void Ban()
    {
        isBanned = true;
    }

    public void UnBan()
    {
        isBanned = false;
    }

    public void Universal()
    {
        isUniversal = true;
    }

    public void UnUniversal()
    {
        isUniversal = false;
    }
}

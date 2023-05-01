using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssenceBall : MonoBehaviour
{
    [SerializeField]
    private Essence m_type;

    private SpriteRenderer sr;

    public void Init(Essence type)
    {
        m_type = type;
        sr = GetComponent<SpriteRenderer>();
        switch (m_type)
        {
            case Essence.Red:
                sr.color = Color.red;
                break;
            case Essence.Green:
                sr.color = Color.green;
                break;
            case Essence.Blue:
                sr.color = Color.blue;
                break;
        }
    }

    public Essence GetEssenceType()
    {
        return m_type;
    }

}

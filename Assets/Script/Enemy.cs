using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EnemyState
{
    moveToTarget,
    attack,
    idel
};


public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int HP;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float AttackRange;
    [SerializeField]
    private int ATK;
    [SerializeField]
    public float coolTime;
    [SerializeField]
    private float attackTimer;
    private bool attackReady;

    [SerializeField]
    private bool isTop;

    private EssenceDrop ed;

    private Rigidbody2D rb;
    private Transform targetTransform;


    [SerializeField]
    private EnemyState m_state;

    private void Awake()
    {
        ed = GetComponent<EssenceDrop>();
        attackReady = false;
        attackTimer = coolTime;
        rb = GetComponent<Rigidbody2D>();
        m_state = EnemyState.idel;
    }

    private void Update()
    {
        StateHandler();

        AttackCoolDownHandler();
    }

    private void StateHandler()
    {
        if(targetTransform == null)
        {
            m_state = EnemyState.idel;
        }
        else
        {
            m_state = EnemyState.moveToTarget;
        }

        if (m_state == EnemyState.moveToTarget)
        {
            MoveToTarget();
        }
        if(m_state == EnemyState.attack)
        {
            
            Attack();
        }
    }

    private void AttackCoolDownHandler()
    {
        if (!attackReady)
        {
            attackTimer -= Time.deltaTime;
        }

        if (attackTimer < 0 && !attackReady)
        {
            attackReady = true;
            attackTimer = coolTime;
        }
    }
    private void MoveToTarget()
    {
        Vector3 dir = targetTransform.position - transform.position;
        rb.velocity = dir.normalized * speed;
        if(dir.magnitude <= AttackRange)
        {
           
            m_state = EnemyState.attack;
        }
    }

    private void Attack()
    {
        Vector3 dir = targetTransform.position - transform.position;

        if (dir.magnitude > AttackRange)
        {
            m_state = EnemyState.moveToTarget;
        }
        else
        {
            if(attackReady)
            {
                targetTransform.gameObject.GetComponent<HeroUnit>().TakenDamage(ATK);
                attackReady = false;
            }

            rb.velocity = Vector2.zero;
        }
    }

    public void Init(Transform transform,bool isTop)
    {
        targetTransform = transform;
        this.isTop = isTop;
    }

    public void takenDMG(int atk)
    {
        HP -= atk;
        if(HP <= 0)
        {
            ed.DropEssence();
            Destroy(gameObject);
        }
    }

    public void Transport()
    {
        EventCenter.Broadcast(EventDefine.Transport, isTop, transform);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
enum MinionBaseState
{
    patrolling,
    attack,
    idel,
    dizzy,
};

public class HeroUnit : MonoBehaviour
{
    [SerializeField]
    private int HP;
    [SerializeField]
    private float speed;
    [SerializeField]
    private int ATK;
    [SerializeField]
    private float AttackRange;
    [SerializeField]
    public float coolTime;
    [SerializeField]
    private float attackTimer;
    private bool attackReady;
    private int flip;

    private Rigidbody2D rb;
    [SerializeField]
    private GameObject attackFX;
    [SerializeField]
    private GameObject roundFX;
    [SerializeField]
    private Text UI_HP;
    [SerializeField]
    private Text UI_ATK;

    [SerializeField]
    private MinionBaseState m_state;

    [SerializeField]
    private Transform[] patrollingDest;
    [SerializeField]
    private bool m_patrollReverse;

    //potion effects
    public bool oceanEffect;
    public bool strEffect;
    public bool AgEffect;
    public bool isImmunity;
    public bool isUniversal;

    //audio
    private AudioSource audioAttack;
    private void Start()
    {
        flip = 1;
        oceanEffect = false;
        isImmunity = false;
        attackReady = false;
        strEffect = false;
        attackTimer = coolTime;
        rb = GetComponent<Rigidbody2D>();
        m_state = MinionBaseState.patrolling;
        audioAttack = GetComponent<AudioSource>();
        //UI_HP = GetComponentInChildren<Text>();
    }

    private void Update()
    {
        StateHandler();

        AttackCoolDownHandler();

        UI_HP.text = HP.ToString();
        UI_ATK.text = ATK.ToString();

        if(m_patrollReverse)
            GetComponent<SpriteRenderer>().flipX = true;
        else
            GetComponent<SpriteRenderer>().flipX = false;
    }

    private void StateHandler()
    {
        if(m_state == MinionBaseState.patrolling)
        {
            Patrolling();
        }

        if(m_state == MinionBaseState.attack)
        {
            Attack();
        }

        if(m_state == MinionBaseState.dizzy)
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void Patrolling()
    {
        Vector3 dir = m_patrollReverse ? (patrollingDest[1].position - transform.position).normalized 
            : (patrollingDest[0].position - transform.position).normalized;

        rb.velocity = dir * speed;
        if( m_patrollReverse && (patrollingDest[1].position - transform.position).magnitude < 0.1)
        {
            m_patrollReverse = !m_patrollReverse ;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if(!m_patrollReverse && (patrollingDest[0].position - transform.position).magnitude < 0.1)
        {
            m_patrollReverse = !m_patrollReverse;
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if(Physics2D.OverlapCircleAll(transform.position, AttackRange, LayerMask.GetMask("EnemyLayer")).Length > 0)
        {
            m_state = MinionBaseState.attack;
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

    private void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, AttackRange,LayerMask.GetMask("EnemyLayer"));
        if (colliders.Length == 0)
        {
            m_state = MinionBaseState.patrolling;
            return;
        }
        else
        {
            bool isReverse = false;
            if(colliders[0].transform.position.x - transform.position.x >0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = true;
                isReverse = true;
            }

            if(attackReady)
            {
                audioAttack.Play();
                attackFX.transform.position = (colliders[0].transform.position + this.transform.position) /2;
                attackFX.transform.position= new Vector3(attackFX.transform.position.x, attackFX.transform.position.y,0);
                if (isReverse) attackFX.transform.localScale = new Vector3(-1, 1, 1);
                else attackFX.transform.localScale = Vector3.one;

                if(isUniversal)
                {
                    colliders[0].GetComponent<EssenceDrop>().Universal();
                    colliders[0].GetComponent<EssenceDrop>().UnBan();
                    colliders[0].GetComponent<Enemy>().takenDMG(ATK);
                    attackFX.SetActive(true);
                }
                else if(oceanEffect)
                {
                    colliders[0].GetComponent<Enemy>().Transport();
                    attackFX.SetActive(true);
                }
                else if(strEffect)
                {
                    colliders[0].GetComponent<EssenceDrop>().UnUniversal();
                    colliders[0].GetComponent<EssenceDrop>().Ban();
                    colliders[0].GetComponent<Enemy>().takenDMG(ATK);
                    attackFX.SetActive(true);
                }
                else if(AgEffect)
                {
                    foreach(var collider in colliders)
                    {
                        colliders[0].GetComponent<EssenceDrop>().UnUniversal();
                        collider.GetComponent<EssenceDrop>().UnBan();
                        collider.GetComponent<Enemy>().takenDMG(ATK);
                    }
                    roundFX.SetActive(true);
                }
                else
                {
                    colliders[0].GetComponent<EssenceDrop>().UnUniversal();
                    colliders[0].GetComponent<EssenceDrop>().UnBan();
                    colliders[0].GetComponent<Enemy>().takenDMG(ATK);
                    attackFX.SetActive(true);
                }

                attackReady = false;                    

            }
            rb.velocity = Vector2.zero;
        }

    }

    public void TakenDamage(int atk)
    {
        if (isImmunity) return ;
        HP -= atk;
        if(HP <= 0)
        {
            EventCenter.Broadcast(EventDefine.Die);
            Destroy(gameObject);
        }
    }

    public void ATKChange(int value)
    {
        ATK += value;
    }

    public int ATKGet()
    {
        return ATK;
    }

    public void ATKSet(int value)
    {
        ATK = value;
    }

    public void Stun()
    {
        m_state = MinionBaseState.dizzy;
    }

    public void UnStun()
    {
        m_state = MinionBaseState.patrolling;
    }


}

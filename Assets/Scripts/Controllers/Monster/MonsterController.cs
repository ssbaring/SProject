using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MonsterController : BaseController
{
    public float SearchRange = 7.0f;
    public float AttackRange = 2.5f;

    protected Vector3 respawnPos;
    protected PlayerController _player;

    protected GameObjectState State
    {
        get { return _state; }
        set
        {
            switch(value)
            {
                case GameObjectState.Idle:
                    animator.SetBool("move", false);
                    animator.SetBool("attack", false);
                    animator.SetBool("hit", false);
                    animator.SetBool("dead", false);
                    break;
                case GameObjectState.Move:
                    animator.SetBool("move", true);
                    animator.SetBool("attack", false);
                    break;
                case GameObjectState.Attack:
                    animator.SetBool("move", false);
                    animator.SetBool("attack", true);
                    break;
                case GameObjectState.Hit:
                    animator.SetBool("hit", true);
                    animator.SetBool("attack", false);
                    animator.SetBool("move", false);
                    break;
                case GameObjectState.Dead:
                    animator.SetBool("move", false);
                    animator.SetBool("attack", false);
                    animator.SetBool("hit", false);
                    animator.SetBool("dead", true);
                    break;
            }
            _state = value;
        }
    }

    private bool isJump = false;
    private bool isHit = false;
    private bool isAttack = false;

    [SerializeField]
    private float angle;
    private Vector2 perp;
    private bool isSlope = false;
    private float distance = 1.0f;

    protected override void Init()
    {
        base.Init();

        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        Damage = 10.0f;
        respawnPos = transform.position;
    }

    void Update()
    {
        if (State == GameObjectState.Dead)
            return;

        RaycastHit2D hit = Physics2D.Raycast(Feet.position, Vector2.down, distance, LayerMask.GetMask("Platform"));
        if (hit)
        {
            perp = Vector2.Perpendicular(hit.normal).normalized;
            angle = Vector2.Angle(hit.normal, Vector2.up);

            if (angle != 0)
                isSlope = true;
            else
                isSlope = false;
        }
    }

    void FixedUpdate()
    {
        if (State == GameObjectState.Dead)
            return;

        if (!TargetInDistance())
        {
            State = GameObjectState.Idle;
            return;
        }

        float dist = (_player.transform.position - transform.position).magnitude;
        Vector3 dir = (_player.transform.position - transform.position).normalized;

        if(dist <= AttackRange)
        {
            if(!isHit && !isAttack)
            {
                isAttack = true;
                StartCoroutine("Attack");
                State = GameObjectState.Attack;
            }
        }
        else
        {
            if (!isHit)
            {
                State = GameObjectState.Move;

                if (isSlope && !isJump)
                    rigid2D.velocity = perp * Speed * dir.x * -1.0f;
                else if (!isSlope && !isJump)
                    rigid2D.velocity = new Vector2(dir.x * Speed, 0);
                else
                    rigid2D.velocity = new Vector2(dir.x * Speed, rigid2D.velocity.y);
            }
        }

        if (dir.x > 0)
            _spriteRenderer.flipX = false;
        else if (dir.x < 0)
            _spriteRenderer.flipX = true;
    }

    IEnumerator Attack()
    {
        _player.OnDamaged(this);
        yield return new WaitForSeconds(2.3f);
        isAttack = false;
    }

    protected bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, _player.transform.position) < SearchRange;
    }

    public override void OnDamaged(BaseController attacker)
    {
        base.OnDamaged(attacker);
        State = GameObjectState.Hit;
        isHit = true;
        StartCoroutine("UpdateHitState");
    }

    IEnumerator UpdateHitState()
    {
        yield return new WaitForSeconds(0.3f);
        if (State == GameObjectState.Hit)
        {
            isHit = false;
            if (CurHp > 0)
            {
                State = GameObjectState.Idle;
            }
            else
            {
                State = GameObjectState.Dead;
                StartCoroutine("UpdateDeadState");
            }
        }
    }

    IEnumerator UpdateDeadState()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}

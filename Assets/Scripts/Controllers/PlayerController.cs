using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static Define;

public class PlayerController : BaseController
{

    [SerializeField]
    private float jumpPower;

    [SerializeField]
    private float distance;

    [SerializeField]
    private float angle;

    private float hor;

    private Vector2 perp;

    private bool isJump = false;
    private bool isSlope = false;
    private bool isAttack = false;

    public GameObjectState State
    {
        get { return _state; }
        set
        {
            switch (value)
            {
                case GameObjectState.Idle:
                    animator.SetBool("move", false);
                    animator.SetBool("jump", false);
                    animator.SetBool("attack", false);
                    break;
                case GameObjectState.Move:
                    animator.SetBool("move", true);
                    break;
                case GameObjectState.Dead:
                    break;
                case GameObjectState.Attack:
                    animator.SetBool("attack", true);
                    break;
                case GameObjectState.Jump:
                    animator.SetBool("jump", true);
                    break;
                default:
                    break;
            }

            _state = value;
        }
    }

    protected override void Init()
    {
        base.Init();
    }

    void Update()
    {
        if (State == GameObjectState.Dead)
            return;

        Jump();
       
        if (Input.GetButtonUp("Horizontal"))
            rigid2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

        if (Input.GetButtonDown("Horizontal"))
            rigid2D.constraints = RigidbodyConstraints2D.FreezeRotation;

        if (Input.GetKeyDown(KeyCode.LeftControl))
            Attack();

        Move();

        if (hor > 0)
        {
            _spriteRenderer.flipX = false;

            if (State != GameObjectState.Jump)
                State = GameObjectState.Move;
        }
        else if(hor < 0)
        {
            _spriteRenderer.flipX = true;

            if (State != GameObjectState.Jump)
                State = GameObjectState.Move;
        }
        else
        {
            if (State == GameObjectState.Move)
                State = GameObjectState.Idle;
        }

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

    private void FixedUpdate()
    {
        if (rigid2D.velocity.y < 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, 1.0f, LayerMask.GetMask("Platform"));
            if (hit.collider != null && hit.distance < 0.6f && State == GameObjectState.Jump)
            {
                isJump = false;
                State = GameObjectState.Idle;
            }

            rigid2D.velocity -= _gravity * 20 * Time.deltaTime;
        }
    }

    void Move()
    {
        hor = Input.GetAxisRaw("Horizontal");
        if (isSlope && !isJump)
            rigid2D.velocity = perp * Speed * hor * -1.0f;
        else if (!isSlope && !isJump)
            rigid2D.velocity = new Vector2(hor * Speed, 0);
        else
            rigid2D.velocity = new Vector2(hor * Speed, rigid2D.velocity.y);
    }

    void Jump()
    {
        if (!isJump && Input.GetKeyDown(KeyCode.Space))
        {
            isJump = true;
            State = GameObjectState.Jump;
            rigid2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    void Attack()
    {
        State = GameObjectState.Attack;
        isAttack = true;
        StartCoroutine("UpdateState");

        RaycastHit2D hit = Physics2D.Raycast(transform.position, _spriteRenderer.flipX ? Vector3.left : Vector3.right, 1.3f, LayerMask.GetMask("Enemy"));
        if (hit.collider != null)
            hit.collider.GetComponent<MonsterController>().OnDamaged(this);
    }

    IEnumerator UpdateState()
    {
        yield return new WaitForSeconds(0.3f);
        if (State == GameObjectState.Attack)
        {
            State = GameObjectState.Idle;
            isAttack = false;
        }
    }

    public override void OnDamaged(BaseController attacker)
    {
        base.OnDamaged(attacker);

        if (CurHp <= 0)
            State = GameObjectState.Dead;
    }
}

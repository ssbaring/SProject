using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class BaseController : MonoBehaviour
{
    public Transform Feet;
    public float MaxHp = 100.0f;
    public float CurHp = 100.0f;
    public float Damage = 0.0f;
    public float Speed;

    protected GameObjectState _state = GameObjectState.Idle;
    protected Vector2 _gravity;
    protected Rigidbody2D rigid2D;
    protected Animator animator;
    protected SpriteRenderer _spriteRenderer;

    protected HpBar _hpBar;

    void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        _gravity = new Vector2(0, -Physics2D.gravity.y);
        _spriteRenderer = GetComponent<SpriteRenderer>();
        rigid2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        GameObject go = Managers.Resource.Instantiate("UI/HpBar", transform);
        go.transform.localPosition = new Vector3(0, 0.7f, 0);
        go.name = "HpBar";
        _hpBar = go.GetComponent<HpBar>();
    }

    public virtual void OnDamaged(BaseController attacker) 
    {
        float ratio = 0.0f;
        CurHp -= attacker.Damage;
        if (CurHp > 0)
            ratio = CurHp / MaxHp;

        _hpBar.SetHpBar(ratio);
    }
}

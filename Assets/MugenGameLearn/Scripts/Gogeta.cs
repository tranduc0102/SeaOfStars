using System.Collections;
using UnityEngine;

public class Gogeta : MonoBehaviour
{
    [Header("Movement")]
    public float speedMove;
    private Vector2 dir;
    private bool canMove = true;

    [Header("Camera Limit")]
    public float maxStartMoveCam;
    [SerializeField] private Camera _camera;

    [Header("Components")]
    private Rigidbody2D rb;
    private Animator _animator;

    [Header("Combo")]
    private int comboStep = 0;
    private bool isAttacking = false;
    private float comboTimer = 0f;
    public float comboResetTime = 1f;

    private bool show;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private int countDownSpace = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && countDownSpace == 0)
        {
            _animator.Play("Show");
            countDownSpace = 1;
            StartCoroutine(StartShow());
        } 
        if(!show) return;
        if (canMove && !isAttacking)
        {
            HandleInput();
        }

        HandleComboReset();
    }

    private void FixedUpdate()
    {
        if (canMove)
            rb.velocity = dir * speedMove * Time.deltaTime;
        else
            rb.velocity = Vector2.zero;
    }

    private void HandleInput()
    {
        dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        HandleAnimation(dir.x);

        if (Input.GetKeyDown(KeyCode.K))
        {
            Kame();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            DoCombo();
        }
    }

    private IEnumerator StartShow()
    {
        yield return new WaitForSeconds(3f);
        show = true;
    }

    private void HandleAnimation(float horizontal)
    {
        if (horizontal == 0)
        {
            _animator.Play("idle");
        }
        else if (horizontal > 0)
        {
            _animator.Play("MoveR");
        }
        else
        {
            _animator.Play("MoveL");
        }
    }

    private void DoCombo()
    {
        if (isAttacking) return;

        comboStep++;
        if (comboStep > 4) comboStep = 1;

        _animator.Play("combo" + comboStep);
        isAttacking = true;
        canMove = false;
        comboTimer = 0f;

        StartCoroutine(EndComboAttack(0.5f));
    }

    private IEnumerator EndComboAttack(float delay)
    {
        yield return new WaitForSeconds(delay);
        isAttacking = false;
        canMove = true;
    }

    private void HandleComboReset()
    {
        if (comboStep > 0)
        {
            comboTimer += Time.deltaTime;
            if (comboTimer >= comboResetTime)
            {
                comboStep = 0;
                comboTimer = 0;
            }
        }
    }

    private void Kame()
    {
        canMove = false;
        isAttacking = true;
        _animator.Play("kame");
        StartCoroutine(EndComboAttack(1f));
    }
}

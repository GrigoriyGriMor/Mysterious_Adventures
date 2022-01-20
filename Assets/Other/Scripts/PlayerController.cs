using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    [Header("���� ���� ��������� ��������")]
    [SerializeField] private FloatingJoystick _joystick;
    [Header("���� ���� ��������� ������ ������")]
    [SerializeField] private Button jumpButton;

    [Header("�������� �������� ���������. ���� 0, �� ����� ������")]
    [SerializeField] private float _moveSpeed;
    [Header("�������� ������ ���������.")]
    [SerializeField] private float _jumpSpeed;

    //����� ���� ��������
    private float distance = 3f;

    [Header("�������� ����� ��������/�������")]
    [SerializeField] private float jumpCooldown = 1f;
    private GameObject spriteToTurn;

    [Header("������ �� ��������")]
    [SerializeField]
    private Animator animator;

    private bool cooldown = false, IsGrounded;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();//�������� Rigidbody � �����, ����� � ���������� ����� ���� ������� ���
        spriteToTurn = transform.GetChild(0).gameObject;//�������� ������ �� ������, ������� ����� �������
        jumpButton.onClick.AddListener(Debg);//���������� � UI-������ ������� ������
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_joystick.Horizontal * _moveSpeed, _rigidbody.velocity.y);//������ ����������� �������� ���� ����� � ������� ��������� ���������� � ��������� ����� ��������� �� ������
        IsGrounded = IsOnGround();//���������, ����������� �� �������� � ������� ������ ����


        //���� �������� ����������� �����, ������������ ������ ��������� �����
        if (_joystick.Horizontal < 0 )
        {
            spriteToTurn.transform.rotation = Quaternion.Euler(new Vector2(0,180));
            animator.SetBool("Run", true);
        }
       
        //���� �������� ����������� ������, ������������ ������ ��������� ������
        else if (_joystick.Horizontal > 0)
        {
            spriteToTurn.transform.rotation = Quaternion.Euler(new Vector2(0, 0));
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }


        //if (Input.GetKey(KeyCode.Space))
        //{
        //    Debg();
        //}
    }

    /// <summary>
    /// �������� ���������� ��������� �� �����.
    /// </summary>
    /// <returns></returns>
    private bool IsOnGround()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.down, distance);

        if (ray.collider)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// ���������� ������ � �������� ������� � ��������� ����� ���������. ��������� �������� � ����������
    /// </summary>
    /// <returns></returns>
    private IEnumerator Jump()
    {
        if(cooldown == false)
        {
            _rigidbody.velocity = new Vector2(0, _jumpSpeed * 10);
            animator.SetTrigger("Jump");

        }
        cooldown = true;
        yield return new WaitForSeconds(jumpCooldown);
        cooldown = false;
    }

    /// <summary>
    /// ������� ������� ������. 
    /// </summary>
    public void Debg()
    {
        if (IsGrounded && cooldown == false)
        {
            StartCoroutine("Jump");
        }
    }

}

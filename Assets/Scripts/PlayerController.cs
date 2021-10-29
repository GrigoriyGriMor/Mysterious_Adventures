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
    private bool cooldown = false, IsGrounded;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        spriteToTurn = transform.GetChild(0).gameObject;
        jumpButton.onClick.AddListener(Debg);
    }

    // Start is called before the first frame update
    private void FixedUpdate()
    {
        //Debug.DrawRay(transform.position, Vector2.down * distance, Color.black);
        _rigidbody.velocity = new Vector2(_joystick.Horizontal * _moveSpeed, _rigidbody.velocity.y);
        IsGrounded = IsOnGround();

        if (_joystick.Horizontal < 0 )
        {
            spriteToTurn.transform.rotation = Quaternion.Euler(new Vector2(0,180));
        }
        else if(_joystick.Horizontal > 0)
        {

            spriteToTurn.transform.rotation = Quaternion.Euler(new Vector2(0, 0));
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Debg();
        }
    }


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

    private IEnumerator Jump()
    {
        if (cooldown == false)
        {
           
            Debug.Log("������ ���������");
        }
        cooldown = true;
        yield return new WaitForSeconds(jumpCooldown);
        cooldown = false;
    }


    public void Debg()
    {
        if (IsGrounded && cooldown == false)
        {

            StartCoroutine("Jump");
            _rigidbody.velocity = new Vector2(0, _jumpSpeed * 10);
        }
    }

}

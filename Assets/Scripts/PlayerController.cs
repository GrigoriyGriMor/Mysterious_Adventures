using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    [Header("Сюда надо поставить джойстик")]
    [SerializeField] private FloatingJoystick _joystick;
    [Header("Скорость движения персонажа. Если 0, то будет стоять")]
    [SerializeField] private float _moveSpeed;
    [Header("Скорость прыжка персонажа.")]
    [SerializeField] private float _jumpSpeed;
    [Header("Высота прыжка персонажа.")]
    [SerializeField] private float distance = 3f;
    [Header("Задержка между прыжками/кулдаун")]
    [SerializeField] private float jumpCooldown = 1f;
    private GameObject spriteToTurn;
    private bool cooldown = false;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        spriteToTurn = transform.GetChild(0).gameObject;
    }


   
    // Start is called before the first frame update
    private void FixedUpdate()
    {
        //Debug.DrawRay(transform.position, Vector2.down * distance, Color.black);
        _rigidbody.velocity = new Vector2(_joystick.Horizontal * _moveSpeed, 0);
        bool IsGrounded = IsOnGround();

        if (_joystick.Horizontal < 0 )
        {
            spriteToTurn.transform.rotation = Quaternion.Euler(new Vector2(0,180));
        }
        else if(_joystick.Horizontal > 0)
        {

            spriteToTurn.transform.rotation = Quaternion.Euler(new Vector2(0, 0));
        }

        if (Input.GetKey(KeyCode.Space) && IsGrounded&&cooldown ==false) 
        {
           
            StartCoroutine ("Jump");
            
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
        if(cooldown==false)
        _rigidbody.velocity += new Vector2(0, _jumpSpeed*10);
        cooldown = true;
        yield return new WaitForSeconds(jumpCooldown);
        cooldown = false;
    }
    
}

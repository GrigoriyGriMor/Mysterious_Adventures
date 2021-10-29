using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    [Header("Сюда надо поставить джойстик")]
    [SerializeField] private FloatingJoystick _joystick;
    [Header("Сюда надо поставить кнопку прыжка")]
    [SerializeField] private Button jumpButton;

    [Header("Скорость движения персонажа. Если 0, то будет стоять")]
    [SerializeField] private float _moveSpeed;
    [Header("Скорость прыжка персонажа.")]
    [SerializeField] private float _jumpSpeed;

    //Длина луча рейкаста
    private float distance = 3f;

    [Header("Задержка между прыжками/кулдаун")]
    [SerializeField] private float jumpCooldown = 1f;
    private GameObject spriteToTurn;
    private bool cooldown = false, IsGrounded;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();//получаем Rigidbody с героя, чтобы в дальнейшем можно было двигать его
        spriteToTurn = transform.GetChild(0).gameObject;//получаем ссылку на спрайт, который будем вращать
        jumpButton.onClick.AddListener(Debg);//подключаем к UI-кнопке функцию прыжка
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_joystick.Horizontal * _moveSpeed, _rigidbody.velocity.y);//меняем направление скорости тела героя с помощью получения информации о положении ручки джойстика на экране
        IsGrounded = IsOnGround();//проверяем, приземлился ли персонаж в текущий момент игры


        //если джойстик отклоняется влево, поворачиваем спрайт персонажа влево
        if (_joystick.Horizontal < 0 )
        {
            spriteToTurn.transform.rotation = Quaternion.Euler(new Vector2(0,180));
        }
       
        //если джойстик отклоняется впрапо, поворачиваем спрайт персонажа вправо
        else if (_joystick.Horizontal > 0)
        {
            spriteToTurn.transform.rotation = Quaternion.Euler(new Vector2(0, 0));
        }

        //if (Input.GetKey(KeyCode.Space))
        //{
        //    Debg();
        //}
    }

    /// <summary>
    /// Проверка нахождения персонажа на земле.
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
    /// Выполнение прыжка с заданной высотой и задержкой между попытками. Параметры меняются в инспекторе
    /// </summary>
    /// <returns></returns>
    private IEnumerator Jump()
    {
        if(cooldown == false)
        {
            _rigidbody.velocity = new Vector2(0, _jumpSpeed * 10);
        }
        cooldown = true;
        yield return new WaitForSeconds(jumpCooldown);
        cooldown = false;
    }

    /// <summary>
    /// Главная функция прыжка. 
    /// </summary>
    public void Debg()
    {
        if (IsGrounded && cooldown == false)
        {
            StartCoroutine("Jump");
        }
    }

}

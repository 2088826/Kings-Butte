using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputManager : MonoBehaviour
{
    private float speed = 5f;
    private Vector2 targetPos = Vector2.zero;
    private float xOffset = 0;
    private float yOffset = 0;

    private bool _isAbility = false;
    private bool _hasPowerUp = false;

    private Rigidbody2D rb2d;
    private Animator pAnim;

    // Input System
    private InputActionAsset inputAsset;
    private InputActionMap player;
    private InputAction aim;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        pAnim = GetComponent<Animator>();

        inputAsset = this.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");
    }

    /// <summary>
    /// I believe this is called on value change for inputs when pressed.
    /// </summary>
    private void OnEnable()
    {
        player.FindAction("Ability1").started += DoAbility1;
        player.FindAction("Ability2").started += DoAbility2;
        player.FindAction("PowerUp").started += DoPowerUp;
        player.FindAction("BasicAttack").started += DoBasicAttack;
        aim = player.FindAction("Aim");
        //player.FindAction("AttackNorth").started += AttackNorth;
        //player.FindAction("AttackSouth").started += AttackSouth;
        //player.FindAction("AttackWest").started += AttackWest;
        //player.FindAction("AttackEast").started += AttackEast;
        player.Enable();
    }

    /// <summary>
    /// I believe this is called on value change when released (back to neutral positions).
    /// </summary>
    private void OnDisable()
    {
        player.FindAction("Ability1").started -= DoAbility1;
        player.FindAction("Ability2").started -= DoAbility2;
        player.FindAction("PowerUp").started -= DoPowerUp;
        player.FindAction("BasicAttack").started -= DoBasicAttack;
        //player.FindAction("AttackNorth").started -= AttackNorth;
        //player.FindAction("AttackSouth").started -= AttackSouth;
        //player.FindAction("AttackWest").started -= AttackWest;
        //player.FindAction("AttackEast").started -= AttackEast;
        player.Disable();
    }

    /// <summary>
    /// Called on button press and uses ability 1.
    /// </summary>
    /// <param name="obj">obj Callback context when action is triggered</param>
    private void DoAbility1(InputAction.CallbackContext obj)
    {
        if (!_isAbility) // Slam
        {
            Debug.Log("Ability1");
            _isAbility = true;
            rb2d.bodyType = RigidbodyType2D.Static;
            pAnim.SetTrigger("ability1");
        }
    }

    /// <summary>
    /// Called on button press and uses ability 2.
    /// </summary>
    /// <param name="obj">obj Callback context when action is triggered</param>
    private void DoAbility2(InputAction.CallbackContext obj)
    {

        if (!_isAbility && aim.inProgress) // Jump (move 2 spaces)
        {
            float valueX = aim.ReadValue<Vector2>().x;
            float valueY = aim.ReadValue<Vector2>().y;

            Vector2 currentPos = gameObject.transform.position;

            if (valueY > 0.1 && valueY > valueX) // Up(y) = 1
            {
                Debug.Log("Jump North");
                _isAbility = true;

                xOffset = 1f;
                yOffset = 0.5f;
                targetPos = new Vector2 (currentPos.x + xOffset, currentPos.y + yOffset);
                MoveToTarget();

                rb2d.bodyType = RigidbodyType2D.Static;
                pAnim.SetTrigger("ability2");
            }
            else if (valueY < -0.1 && valueY < valueX) // Down(y) = -1
            {
                Debug.Log("Jump South");
                _isAbility = true;

                xOffset = -1f;
                yOffset = -0.5f;
                targetPos = new Vector2(currentPos.x + xOffset, currentPos.y + yOffset);
                MoveToTarget();

                rb2d.bodyType = RigidbodyType2D.Static;
                pAnim.SetTrigger("ability2");
            }
            else if (valueX < -0.1 && valueX < valueY) // Left(x) = -1
            {
                Debug.Log("Jump West");
                _isAbility = true;

                xOffset = -1f;
                yOffset = 0.5f;
                targetPos = new Vector2(currentPos.x + xOffset, currentPos.y + yOffset);
                MoveToTarget();

                rb2d.bodyType = RigidbodyType2D.Static;
                pAnim.SetTrigger("ability2");
            }
            else if (valueX > 0.1 && valueX > valueY) // Right(x) = 1
            {
                Debug.Log("Jump East");
                _isAbility = true;

                xOffset = 1f;
                yOffset = -0.5f;
                targetPos = new Vector2(currentPos.x + xOffset, currentPos.y + yOffset);
                MoveToTarget();

                rb2d.bodyType = RigidbodyType2D.Static;
                pAnim.SetTrigger("ability2");
            }
        }
    }

    /// <summary>
    /// Called on button press and uses powerup if available.
    /// </summary>
    /// <param name="obj">obj Callback context when action is triggered</param>
    private void DoPowerUp(InputAction.CallbackContext obj)
    {
        if (!_isAbility && _hasPowerUp) // Powerup
        {
            Debug.Log("PowerUp");
            _isAbility = true;
            rb2d.bodyType = RigidbodyType2D.Static;
            pAnim.SetTrigger("powerUp1");
        }
    }

    /// <summary>
    /// Button press uses a basic attack in the direction of the aim.
    /// </summary>
    /// <param name="obj">obj Callback context when action is triggered</param>
    private void DoBasicAttack(InputAction.CallbackContext obj)
    {

        if (!_isAbility && aim.inProgress) // Basic Attack
        {
            float valueX = aim.ReadValue<Vector2>().x;
            float valueY = aim.ReadValue<Vector2>().y;

            if (valueY > 0.1 && valueY > valueX) // Up(y) = 1
            {
                Debug.Log("Basic Attack North");

                _isAbility = true;
                rb2d.bodyType = RigidbodyType2D.Static;
                pAnim.SetTrigger("Nattack");
            }
            else if (valueY < -0.1 && valueY < valueX) // Down(y) = -1
            {
                Debug.Log("Basic Attack South");

                _isAbility = true;
                rb2d.bodyType = RigidbodyType2D.Static;
                pAnim.SetTrigger("Sattack");
            }
            else if (valueX < -0.1 && valueX < valueY) // Left(x) = -1
            {
                Debug.Log("Basic Attack West");
                _isAbility = true;
                rb2d.bodyType = RigidbodyType2D.Static;
                pAnim.SetTrigger("Wattack");
            }
            else if (valueX > 0.1 && valueX > valueY) // Right(x) = 1
            {
                Debug.Log("Basic Attack East");
                _isAbility = true;
                rb2d.bodyType = RigidbodyType2D.Static;
                pAnim.SetTrigger("Eattack");
            }
        }
    }

    private void MoveToTarget()
    {
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, targetPos, speed * Time.deltaTime);
    }

    /// <summary>
    /// Reset the player animation state to idle.
    /// </summary>
    public void idleAnim()
    {
        pAnim.SetTrigger("idle");
        _isAbility = false;
        rb2d.bodyType = RigidbodyType2D.Dynamic;
    }
}

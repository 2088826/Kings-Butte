using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputManager : MonoBehaviour
{
    private bool _isAbility = false;

    private Rigidbody2D rb2d;
    private Animator pAnim;

    // Input System
    private InputActionAsset inputAsset;
    private InputActionMap player;
    private InputAction move;

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
        player.FindAction("PowerUp1").started += PowerUp1;
        player.FindAction("PowerUp2").started += PowerUp2;
        player.FindAction("AttackNorth").started += AttackNorth;
        player.FindAction("AttackSouth").started += AttackSouth;
        player.FindAction("AttackWest").started += AttackWest;
        player.FindAction("AttackEast").started += AttackEast;
        player.Enable();
    }

    /// <summary>
    /// I believe this is called on value change when released (back to neutral positions).
    /// </summary>
    private void OnDisable()
    {
        player.FindAction("Ability1").started -= DoAbility1;
        player.FindAction("Ability2").started -= DoAbility2;
        player.FindAction("PowerUp1").started -= PowerUp1;
        player.FindAction("PowerUp2").started -= PowerUp2;
        player.FindAction("AttackNorth").started -= AttackNorth;
        player.FindAction("AttackSouth").started -= AttackSouth;
        player.FindAction("AttackWest").started -= AttackWest;
        player.FindAction("AttackEast").started -= AttackEast;
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
        if (!_isAbility) // Jump (move 2 spaces)
        {
            Debug.Log("Ability2");
            _isAbility = true;
            rb2d.bodyType = RigidbodyType2D.Static;
            pAnim.SetTrigger("ability2");
        }
    }

    /// <summary>
    /// Called on button press and uses ability 3.
    /// </summary>
    /// <param name="obj">obj Callback context when action is triggered</param>
    private void PowerUp1(InputAction.CallbackContext obj)
    {
        if (!_isAbility) // Powerup 1
        {
            Debug.Log("PowerUp1");
            _isAbility = true;
            rb2d.bodyType = RigidbodyType2D.Static;
            //pAnim.SetTrigger("powerUp1");
        }
    }

    /// <summary>
    /// Called on button press and uses ability 4.
    /// </summary>
    /// <param name="obj">obj Callback context when action is triggered</param>
    private void PowerUp2(InputAction.CallbackContext obj)
    {
        if (!_isAbility) // Powerup 2
        {
            Debug.Log("PowerUp2");
            _isAbility = true;
            rb2d.bodyType = RigidbodyType2D.Static;
            //pAnim.SetTrigger("powerUp2");
        }
    }

    /// <summary>
    /// Called on north button press and uses an attack.
    /// </summary>
    /// <param name="obj">obj Callback context when action is triggered</param>
    private void AttackNorth(InputAction.CallbackContext obj)
    {
        if (!_isAbility)
        {

            _isAbility = true;
            rb2d.bodyType = RigidbodyType2D.Static;
            pAnim.SetTrigger("Nattack");
        }
    }

    /// <summary>
    /// Called on south button press and uses an attack.
    /// </summary>
    /// <param name="obj">obj Callback context when action is triggered</param>
    private void AttackSouth(InputAction.CallbackContext obj)
    {
        if (!_isAbility)
        {
            _isAbility = true;
            rb2d.bodyType = RigidbodyType2D.Static;
            pAnim.SetTrigger("Sattack");
        }
    }

    /// <summary>
    /// Called on west button press and uses an attack.
    /// </summary>
    /// <param name="obj">obj Callback context when action is triggered</param>
    private void AttackWest(InputAction.CallbackContext obj)
    {
        if (!_isAbility)
        {
            _isAbility = true;
            rb2d.bodyType = RigidbodyType2D.Static;
            pAnim.SetTrigger("Wattack");
        }
    }

    /// <summary>
    /// Called on east button press and uses an attack.
    /// </summary>
    /// <param name="obj">obj Callback context when action is triggered</param>
    private void AttackEast(InputAction.CallbackContext obj)
    {
        if (!_isAbility)
        {
            _isAbility = true;
            rb2d.bodyType = RigidbodyType2D.Static;
            pAnim.SetTrigger("Eattack");
        }
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

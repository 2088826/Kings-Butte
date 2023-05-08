using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerActions : MonoBehaviour
{
    // Serialized Fields
    [SerializeField] private float attackSpeed = 1.5f;

    // Numerics
    private float nextBasic = 0;
    private float defaultAttackSpeed;

    // Boolean
    private bool _isAbility = false;
	private bool _hasPowerUp = false;

    // Components
    private Rigidbody2D rb2d;
    private Animator pAnim;
    private PlayerController controller;
    private AbilityCooldown abilities;

    // Input System
    private InputActionAsset inputAsset;
    private InputActionMap player;
    private InputActionMap ui;
    private InputAction move;
    private InputAction aim;

    // Public Properties
    public InputAction Move { get { return move; } }
    public InputAction Aim { get { return aim; } }
    public bool IsAbility { get { return _isAbility; } }

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        pAnim = GetComponent<Animator>();
        controller = GetComponent<PlayerController>();
        abilities = GetComponent<AbilityCooldown>();

        inputAsset = this.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");
        ui = inputAsset.FindActionMap("UI");

        defaultAttackSpeed = attackSpeed;

        ToggleDisable();
    }

    /// <summary>
    /// Called on button press and uses ability 1.
    /// </summary>
    /// <param name="obj">obj Callback context when action is triggered</param>
    private void DoAbility1(InputAction.CallbackContext obj)
    {
        if (!_isAbility && !abilities.IsAbility1 && !controller.IsMoving) // Slam
        {
            abilities.IsAbility1 = true;
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
        if (!_isAbility && !abilities.IsAbility2 && aim.inProgress && !controller.IsMoving) // Jump (move 2 spaces)
        {
            abilities.IsAbility2 = true;
            controller.IsMoving = true;
            _isAbility = true;
            rb2d.bodyType = RigidbodyType2D.Static;
            pAnim.SetTrigger("ability2");

            float valueX = aim.ReadValue<Vector2>().x;
            float valueY = aim.ReadValue<Vector2>().y;
            
            if (valueY > 0.1 && valueY > valueX && valueY > valueX * -1) // Up(y) = 1
            {
                controller.Move2Adjacent("North");
            }
            else if (valueY < -0.1 && valueY < valueX && valueY < valueX * -1) // Down(y) = -1
            {
                controller.Move2Adjacent("South");
            }
            else if (valueX < -0.1 && valueX < valueY && valueX < valueY * -1) // Left(x) = -1
            {
                controller.Move2Adjacent("West");
            }
            else if (valueX > 0.1 && valueX > valueY && valueX > valueY * -1) // Right(x) = 1
            {
                controller.Move2Adjacent("East");
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
        if (!_isAbility && aim.inProgress && !controller.IsMoving && Time.time > nextBasic) // Basic Attack
        {
            nextBasic = Time.time + attackSpeed;
            _isAbility = true;
            rb2d.bodyType = RigidbodyType2D.Static;

            float valueX = aim.ReadValue<Vector2>().x;
            float valueY = aim.ReadValue<Vector2>().y;

            if (valueY > 0.1 && valueY > valueX && valueY > valueX * -1) // Up(y) = 1
            {
                pAnim.SetTrigger("Nattack");
            }
            else if (valueY < -0.1 && valueY < valueX && valueY < valueX * -1) // Down(y) = -1
            {
                pAnim.SetTrigger("Sattack");
            }
            else if (valueX < -0.1 && valueX < valueY && valueX < valueY * -1) // Left(x) = -1
            {
                pAnim.SetTrigger("Wattack");
            }
            else if (valueX > 0.1 && valueX > valueY && valueX > valueY * -1) // Right(x) = 1
            {
                pAnim.SetTrigger("Eattack");
            }
        }
    }

    /// <summary>
    /// The basic attack cooldown changes based on the multiplier.
    /// </summary>
    /// <param name="multiplier">Amount of speed buffs currently held</param>
    public void ChangeMoveCooldown(float multiplier)
    {
        if (multiplier > 0)
        {
            attackSpeed = defaultAttackSpeed - (1 * multiplier);

        }
        else
        {
            attackSpeed = defaultAttackSpeed;
        }
    }

    /// <summary>
    /// Pause the Game
    /// </summary>
    /// <param name="obj">obj Callback context when action is triggered</param>
    private void DoPause(InputAction.CallbackContext obj)
    {
        GameManager.IsPaused = true;
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

    /// <summary>
    /// Toggle on the UI action map.
    /// </summary>
    public void ToggleUI()
    {
        // Enable UI
        //ui.Enable();

        // Disable Player
        player.FindAction("Pause").started -= DoPause;
        player.FindAction("Ability1").started -= DoAbility1;
        player.FindAction("Ability2").started -= DoAbility2;
        player.FindAction("PowerUp").started -= DoPowerUp;
        player.FindAction("BasicAttack").started -= DoBasicAttack;
        player.Disable();
    }

    /// <summary>
    /// Toggle on the Player action map.
    /// </summary>
    public void TogglePlayer()
    {
        // Enable Player
        player.FindAction("Pause").started += DoPause;
        player.FindAction("Ability1").started += DoAbility1;
        player.FindAction("Ability2").started += DoAbility2;
        player.FindAction("PowerUp").started += DoPowerUp;
        player.FindAction("BasicAttack").started += DoBasicAttack;
        aim = player.FindAction("Aim");
        move = player.FindAction("Movement");
        player.Enable();

        // Disable UI
        //ui.Disable();
    }

    /// <summary>
    /// Toggle off all action maps.
    /// </summary>
    public void ToggleDisable()
    {
        // Disable UI
        //ui.Disable();

        // Disable Player
        player.FindAction("Pause").started -= DoPause;
        player.FindAction("Ability1").started -= DoAbility1;
        player.FindAction("Ability2").started -= DoAbility2;
        player.FindAction("PowerUp").started -= DoPowerUp;
        player.FindAction("BasicAttack").started -= DoBasicAttack;
        player.Disable();
    }
}

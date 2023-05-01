using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProtoController : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private int playerIndex = 0;

    private bool _isAbility = false;
    private Vector2 movement = Vector2.zero;

    private Rigidbody2D rb2d;
    private Animator pAnim;
    
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

    private void OnEnable()
    {
        player.FindAction("Ability1").started += DoAbility1;
        move = player.FindAction("Movement");
        player.Enable();
    }

    private void OnDisable()
    {
        player.FindAction("Ability1").started -= DoAbility1;
        player.Disable();        
    }

    public void SetInputVector(Vector2 direction)
    {

    }

    private void FixedUpdate()
    {
        Debug.Log(move);
        if (move != null)
        {
            float inputX = move.ReadValue<Vector2>().x * speed;
            float inputY = move.ReadValue<Vector2>().y * speed;
            rb2d.velocity = new Vector2(inputX, inputY);
        }
    }

    private void DoAbility1(InputAction.CallbackContext obj)
    {
        if (!_isAbility)
        {
            _isAbility = true;
            rb2d.bodyType = RigidbodyType2D.Static;
            pAnim.SetTrigger("shockwave");
        }
    }

    public int GetPlayerIndex()
    {
        return playerIndex;
    }

    public void idleAnim()
    {
        pAnim.SetTrigger("idle");
        _isAbility = false;
        rb2d.bodyType = RigidbodyType2D.Dynamic;
    }
}

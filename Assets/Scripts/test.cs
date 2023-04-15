using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;

public class test : MonoBehaviour
{
    public Grid grid;
    public Tilemap playableTileMap;
    public Tilemap unPlayableTileMap;
    Vector3Int pos; 
    public Vector3 PlayerPos;  // Given
    [SerializeField] private float m_speed = 2f;
    Rigidbody2D rb2d;
    Animator pAnim;
    GameObject player;

    private bool isAttacking = false;


    /* 
        -- Joystick buttons --
        Joystick 0 = B button
        Joystick 1 = A button
        Joystick 2 = Y button
        Joystick 3 = X button
        Joystick 4 = L1 button
        Joystick 5 = R1 button
        Joystick 6 = L2 button
        Joystick 7 = R2 button
        Joystick 8 = L3 button
        Joystick 9 = R3 button
     */

    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        pAnim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // -- Handle input and movement --
        if(gameObject.name == "Player1" && !isAttacking)
        {
            float inputX = Input.GetAxis("Horizontal");
            float inputY = Input.GetAxisRaw("Vertical");
            rb2d.velocity = new Vector2(inputX * m_speed, inputY * m_speed);

            if(Input.GetAxisRaw("Jump") > 0)
            {
                isAttacking = true;
                rb2d.bodyType = RigidbodyType2D.Static;
                pAnim.SetTrigger("shockwave");
                Debug.Log("Shockwave");
            }
        }

        if (gameObject.name == "Player2" && !isAttacking)
        {
            float inputX = Input.GetAxis("Horizontal");
            float inputY = Input.GetAxisRaw("Vertical");
            rb2d.velocity = new Vector2(inputX * m_speed, inputY * m_speed);

            if (Input.GetAxisRaw("Jump") > 0)
            {
                isAttacking = true;
                rb2d.bodyType = RigidbodyType2D.Static;
                pAnim.SetTrigger("shockwave");
                Debug.Log("Shockwave");
            }
        }


        // Swap direction of sprite depending on walk direction
        //if (inputX > 0)
        //{
        //    GetComponent<SpriteRenderer>().flipX = false;
        //}

        //else if (inputX < 0)
        //{
        //    GetComponent<SpriteRenderer>().flipX = true;
        //}


        //player.transform.position = PlayerPos;

        //if (playableTileMap.WorldToCell(PlayerPos) != null)
        //{
        //    Vector3Int lPos = playableTileMap.WorldToCell(PlayerPos);
        //    Debug.Log(playableTileMap.HasTile(lPos));
        //    Debug.Log("name : " + playableTileMap.GetTile(lPos) + " & position : " + lPos);
        //}
        //else if (unPlayableTileMap.WorldToCell(PlayerPos) != null)
        //{
        //    Vector3Int lPos = playableTileMap.WorldToCell(PlayerPos);
        //    Debug.Log("name : " + playableTileMap.GetTile(lPos) + " & position : " + lPos);
        //    Debug.Log("Dead");
        //}
    }

    public void idleAnim()
    {
        pAnim.SetTrigger("idle");
        isAttacking = false;
        rb2d.bodyType = RigidbodyType2D.Dynamic;
    }
}

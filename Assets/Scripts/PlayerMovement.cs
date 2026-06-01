using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator anim;

    [SerializeField] private bool walking;
    
    Vector2 movementInput;
    [SerializeField] private float moveSpeed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = movementInput * moveSpeed;
        Animate();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
        //Animate();
    }

    public void Animate()
    {
        if (movementInput.magnitude > 0.1f || movementInput.magnitude < -0.1f)
        {
            walking = true;
        }
        else
            walking = false;

        if (walking)
        {
            anim.SetFloat("X", movementInput.x);
            anim.SetFloat("Y", movementInput.y);
        }

        anim.SetBool("Walking", walking);

    }
}

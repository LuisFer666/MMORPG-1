using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movimientoX;
    public float movimientoY;
    public int velocidadMov;
    public int velocidadRot;
    public CharacterController characterController;

    public bool blocking;
    public bool atacking;
    public bool kicking;
    public bool running;
    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movimientoX = Input.GetAxis("Horizontal");
        movimientoY = Input.GetAxis("Vertical");

        // Presionar click derecho
        if(Input.GetMouseButtonDown(1)) blocking = true;
        // Soltar click derecho
        if (Input.GetMouseButtonUp(1)) blocking = false;

        // Presionar click izquierdo
        if (Input.GetMouseButtonDown(0)) atacking = true;
        // Soltar click izquierdo
        if (Input.GetMouseButtonUp(0)) atacking = false;

        // Presionar tecla F
        if (Input.GetKeyDown(KeyCode.F)) kicking = true;
        // Soltar tecla F
        if (Input.GetKeyUp(KeyCode.F)) kicking = false;

        // Presionar tecla Left Shift
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            running = true;
            velocidadMov = 40;
        }
        // Soltar tecla Left Shift
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            running = false;
            velocidadMov = 20;
        }

        Vector3 input = new(0, 0, movimientoY);

        Vector3 forward = characterController.transform.forward;
        Vector3 right = characterController.transform.right;

        Vector3 movimiento = input.x * right + input.z * forward;
        characterController.Move(movimiento * velocidadMov * Time.deltaTime);
        characterController.transform.Rotate(new Vector3(0,movimientoX*velocidadRot*Time.deltaTime));
        
        animator.SetFloat("movX", movimientoX);
        animator.SetFloat("movY", movimientoY);
        animator.SetBool("isBlocking", blocking);
        animator.SetBool("isAtacking", atacking);
        animator.SetBool("isKicking", kicking);
        animator.SetBool("isRunning", running);
    }
}

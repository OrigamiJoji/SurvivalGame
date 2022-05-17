using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public sealed class PlayerMove : MonoBehaviour {

    [SerializeField] private float moveSpeed;

    [Header("Mobility Stats")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpForce;

    [Header("Technical Stats")]
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float groundDrag;
    [SerializeField] private float airDrag;



    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private Transform orientation;
    [SerializeField] private LayerMask whatIsGround;

    private Rigidbody rb;
    private float xInput;
    private float zInput;
    private Vector3 moveDir;

    private bool isGrounded;

    private float lockMultiplier;

    private void Awake() {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    private void Start() {
        LockMovement(false);
    }

    void Update() {
        GetAxis();
        moveDir = xInput * orientation.right + zInput * orientation.forward;
        CheckGround();

        DragControl();
        SprintControl();

        if(Input.GetKeyDown(KeyCode.Space)) {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void GetAxis() {
        xInput = Input.GetAxisRaw("Horizontal");
        zInput = Input.GetAxisRaw("Vertical");
    }
    private void ApplyMovement() {
        rb.AddForce(moveDir.normalized * moveSpeed * lockMultiplier, ForceMode.Acceleration);
    }

    private void DragControl() {
        if (isGrounded) { rb.drag = groundDrag; }
        else { rb.drag = airDrag; }
    }

    private void CheckGround() {
        if (Physics.CheckSphere(groundCheckPos.position, groundCheckRadius, whatIsGround)) {
            isGrounded = true;
        }
        else { isGrounded = false; }
    }
    private void SprintControl() {
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded) {
            moveSpeed = Mathf.Lerp(moveSpeed, runSpeed, acceleration * Time.deltaTime);
        }
        else { moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime); }
    }

    public void LockMovement(bool locked) {
        if (locked) {
            lockMultiplier = 0;
        }
        else {
            lockMultiplier = 1;
        }
    }

    private void Jump() {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(groundCheckPos.position, groundCheckRadius);
    }
}

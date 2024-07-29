using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // �������� ��������
    public float idleTimeThreshold = 2f; // ����� ������� �� �������� � idle3
    private Animator animator;
    private Rigidbody2D rb;
    private bool isFacingRight = true;
    private bool isButtonDestroyed = false;
    private float idleTimer = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");

        if (!isButtonDestroyed)
        {
            // �� ����������� ������: ������ ������������ �������� idle1
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
            animator.SetBool("isRunning", false);
            animator.SetBool("isIdle3", false);

            if (moveInput != 0)
            {
                // ������� �������
                if (moveInput < 0 && isFacingRight)
                {
                    Flip();
                }
                else if (moveInput > 0 && !isFacingRight)
                {
                    Flip();
                }
            }
        }
        else
        {
            // ����� ����������� ������
            if (moveInput != 0)
            {
                rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
                animator.SetBool("isRunning", true);
                animator.SetBool("isIdle3", false);
                idleTimer = 0f; // ����� ������� �������

                // ������� �������
                if (moveInput < 0 && isFacingRight)
                {
                    Flip();
                }
                else if (moveInput > 0 && !isFacingRight)
                {
                    Flip();
                }
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                animator.SetBool("isRunning", false);

                idleTimer += Time.deltaTime;
                if (idleTimer >= idleTimeThreshold)
                {
                    animator.SetBool("isIdle3", true);
                }
                else
                {
                    animator.SetBool("isIdle3", false);
                }
            }
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void SetButtonDestroyed(bool destroyed)
    {
        isButtonDestroyed = destroyed;
    }
}

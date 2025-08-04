using UnityEngine;
using System.Collections;

public enum PotionType { DoubleScore, AddLife, SlowTimer }

public class BallController : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isDragging = false;
    private Vector3 offset;
    private GameManager gameManager;
    private Camera cam;
    private bool hasHitGround = false;

    public Sprite soccerSprite;
    public Sprite basketballSprite;
    public Sprite tennisSprite;

    private SpriteRenderer sr;




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
        cam = Camera.main;
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        #if UNITY_ANDROID
        HandleTouchInput();
        #endif

        UpdateBallSprite();
    }

    void UpdateBallSprite()
    {
        int score = gameManager.GetScore();

        if (score >= 20 && sr.sprite != tennisSprite)
        {
            sr.sprite = tennisSprite;
        }
        else if (score >= 10 && score < 20 && sr.sprite != basketballSprite)
        {
            sr.sprite = basketballSprite;
        }
        else if (score < 10 && sr.sprite != soccerSprite)
        {
            sr.sprite = soccerSprite;
        }
    }

    void OnMouseDown()
    {
        Vector3 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        offset = transform.position - mouseWorldPos;
        isDragging = true;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        gameManager.AddScore(1);
        hasHitGround = false;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0;
            transform.position = mouseWorldPos + offset;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
        rb.gravityScale = 20;
        rb.AddForce(new Vector2(0, 500));
    }

    void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchWorldPos = cam.ScreenToWorldPoint(touch.position);
            touchWorldPos.z = 0;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (Vector2.Distance(touchWorldPos, transform.position) < 1f)
                    {
                        offset = transform.position - touchWorldPos;
                        isDragging = true;
                        rb.gravityScale = 0;
                        rb.velocity = Vector2.zero;
                        gameManager.AddScore(1);
                        hasHitGround = false;
                    }
                    break;

                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    if (isDragging)
                    {
                        transform.position = touchWorldPos + offset;
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if (isDragging)
                    {
                        isDragging = false;
                        rb.gravityScale = 9;
                        rb.AddForce(new Vector2(0, 500));
                    }
                    break;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !hasHitGround)
        {
            hasHitGround = true;
            gameManager.BallHitGround();
        }
    }
}

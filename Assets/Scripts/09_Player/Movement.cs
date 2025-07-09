using System.Collections;
using UnityEngine;

[System.Serializable]

public enum SIDE { Left, Mid, Right }

public class Movement : MonoBehaviour
{
    [SerializeField] GameObject midTarget;
    [SerializeField] GameObject leftTarget;
    [SerializeField] GameObject rightTarget;

    public SIDE midSide = SIDE.Mid;

    [Header("References")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private CharacterVFX characterVFX;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip rollSidesClip;
    [SerializeField] private AudioClip rollClip;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip crushTrunkClip;
    [SerializeField] private AudioClip cruchObstacleClip;
    [SerializeField] private AudioClip damageBurdClip;

    [Header("Player Settings")]
    [SerializeField] private AnimationCurve dodgeCurve;

    [Range(0.1f, 10f)]
    public float dodgeDuration = 0.5f;

    public float jumpPower = 8;
    public float rollDelay = 1f;
    public float landingSpeed = 25f;
    public float damageTimeDelay = 2f;

    [HideInInspector]
    public bool swipeLeft, swipeRight, swipeUp, swipeDown;
    public bool inJump;
    public bool inRoll;
    public bool isDamage = false;

    public bool isDead;

    public Animator animator;

    public AudioSource shootSound;

    private float xPos;
    private float yPos;

    private float dodgeTime;

    private float newXPos;
    private float xPosMid;
    private float xPosLeft;
    private float xPosRight;
    private float xPosLast;
    private float colHeight;
    private float colCenterY;

    private float lastHorizontalPressTime;
    private float keyCooldown = 0.3f;


    private bool obstacleClash;
    private bool isPaused;

    private SIDE lastSide;

    private KeyCode lastHorizontalKey = KeyCode.None;

    private CharacterController characterController;

    private LevelGenerator levelGenerator;

    private BulletWeapon bulletWeapon;

    public void InitAnim(Animator animator)
    {
        this.animator = animator;
    }

    public void InitSound(AudioSource audioSource)
    {
        this.shootSound = audioSource;
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
        bulletWeapon = FindFirstObjectByType<BulletWeapon>();

        colHeight = characterController.height;
        colCenterY = characterController.center.y;
        transform.position = Vector3.zero;

        xPosMid = midTarget.transform.position.x;
        xPosLeft = leftTarget.transform.position.x;
        xPosRight = rightTarget.transform.position.x;

        animator.SetBool("Grounded", true);

        isDead = false;
        obstacleClash = false;
        isPaused = false;
    }

    void Update()
    {
        if (levelGenerator.readyToStart)
        {
            PlayerMove();
            Fire();
        }
    }

    private void PlayerMove()
    {
        Move(Vector3.zero);
        animator.SetBool("Run_b", true);
        animator.SetTrigger("Run_t");
    }

    private void Move(Vector3 dir)
    {
        if (levelGenerator.readyToStart && !isDead && !obstacleClash)
        {
            bool leftPressed = Input.GetKeyDown(KeyCode.LeftArrow);
            bool rightPressed = Input.GetKeyDown(KeyCode.RightArrow);

            if (leftPressed || rightPressed)
            {
                KeyCode currentKey = leftPressed ? KeyCode.LeftArrow : KeyCode.RightArrow;
                float timeSinceLastPress = Time.time - lastHorizontalPressTime;

                if (lastHorizontalKey == KeyCode.None ||
                currentKey != lastHorizontalKey ||
                timeSinceLastPress >= keyCooldown)
                {
                    swipeLeft = leftPressed;
                    swipeRight = rightPressed;
                    lastHorizontalKey = currentKey;
                    lastHorizontalPressTime = Time.time;
                }
                else
                {
                    swipeLeft = false;
                    swipeRight = false;
                }
            }
            else
            {
                swipeLeft = false;
                swipeRight = false;
            }

            swipeUp = Input.GetKeyDown(KeyCode.UpArrow);
            swipeDown = Input.GetKeyDown(KeyCode.DownArrow);
        }

        HandleMovement();
        MoveVectorChar();
        Jump();
        Roll();
    }

    private void HandleMovement()
    {
        if (swipeLeft)
        {
            MoveLeft();
        }
        else if (swipeRight)
        {
            MoveRight();
        }
    }

    private void MoveLeft()
    {
        switch (midSide)
        {
            case SIDE.Mid:
                xPosLast = newXPos;
                lastSide = midSide;
                newXPos = xPosLeft;
                midSide = SIDE.Left;
                animator.Play("RollLeft");
                audioSource.clip = rollSidesClip;
                audioSource.Play();
                StartDodge(xPos, newXPos);
                break;

            case SIDE.Right:
                xPosLast = newXPos;
                lastSide = midSide;
                newXPos = xPosMid;
                midSide = SIDE.Mid;
                animator.Play("RollLeft");
                audioSource.clip = rollSidesClip;
                audioSource.Play();
                StartDodge(xPos, newXPos);
                break;
        }
    }

    private void MoveRight()
    {
        switch (midSide)
        {
            case SIDE.Mid:
                xPosLast = newXPos;
                lastSide = midSide;
                newXPos = xPosRight;
                midSide = SIDE.Right;
                animator.Play("RollRight");
                audioSource.clip = rollSidesClip;
                audioSource.Play();
                StartDodge(xPos, newXPos);

                break;

            case SIDE.Left:
                xPosLast = newXPos;
                lastSide = midSide;
                newXPos = xPosMid;
                midSide = SIDE.Mid;
                animator.Play("RollRight");
                audioSource.clip = rollSidesClip;
                audioSource.Play();
                StartDodge(xPos, newXPos);
                break;
        }
    }

    private void MoveVectorChar()
    {
        dodgeTime += Time.deltaTime / dodgeDuration;
        float curveValue = dodgeCurve.Evaluate(dodgeTime);

        xPos = Mathf.Lerp(xPos, obstacleClash ? xPosLast : newXPos, curveValue);
        Vector3 moveVector = new Vector3(xPos - transform.position.x, yPos * Time.deltaTime, 0);
        characterController.Move(moveVector);
    }


    public void StartDodge(float currentX, float nextX)
    {
        xPosLast = currentX;
        newXPos = nextX;

        dodgeTime = 0f;
    }

    public void Jump()
    {
        if (characterController.isGrounded)
        {
            animator.SetBool("Grounded", true);
            animator.SetBool("Fall_b", false);

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fall"))
            {
                animator.Play("Land");
                inJump = false;
            }
            if (swipeUp)
            {
                yPos = jumpPower;
                animator.CrossFadeInFixedTime("Jump", 0.1f);
                animator.SetBool("Grounded", false);
                animator.SetBool("Fall_b", true);
                inJump = true;

                audioSource.clip = jumpClip;
                audioSource.Play();
            }
        }
        else
        {
            yPos -= jumpPower * 2 * Time.deltaTime;
            if (characterController.velocity.y < -0.1f && !inRoll)
            {
                animator.SetBool("Fall_b", true);
            }
        }
    }

    public void Roll()
    {
        if (!inRoll)
            if (swipeDown)
            {
                StartCoroutine(RollDelay(rollDelay));
                yPos -= landingSpeed;
                characterController.center = new Vector3(0, colCenterY / 2f, 0);
                characterController.height = colHeight / 2f;
                inRoll = true;
                inJump = false;

                audioSource.clip = rollClip;
                audioSource.Play();

                animator.CrossFadeInFixedTime("Roll", 0.1f);
            }
    }

    public void RollBack()
    {
        if (midSide == SIDE.Left || midSide == SIDE.Mid && xPos > 1)
        {
            if (!isDead)
            {
                animator.Play("RollLeft");
            }
            else
            {
                animator.Play("HitLeft");
            }
        }
        else if (midSide == SIDE.Right || midSide == SIDE.Mid && xPos < 1)
        {
            if (!isDead)
            {
                animator.Play("RollRight");
            }
            else
            {
                animator.Play("HitRight");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            isDead = true;

            audioSource.clip = cruchObstacleClip;
            audioSource.Play();

            cameraShake.ShakeCamera();

            animator.Play("Knockdown");
            StartCoroutine(LoadScene(3f));
        }

        else if (other.CompareTag("Truck") && !isPaused)
        {
            if (!isDamage)
            {
                audioSource.clip = crushTrunkClip;
                audioSource.Play();
                StartCoroutine(PauseGame(0.09f));

                cameraShake.ShakeCamera();

                HandleFirstCollision();
            }
            else
            {
                isDead = true;

                audioSource.clip = crushTrunkClip;
                audioSource.Play();

                cameraShake.ShakeCamera();

                StartCoroutine(LoadScene(3f));
                midSide = lastSide;
                newXPos = xPosLast;
                obstacleClash = true;
                RollBack();
            }
        }
        else if (other.CompareTag("Enemy"))
        {
            StartCoroutine(PauseGame(0.09f));
        }

    }

    private void Fire()
    {
        if (Input.GetKey(KeyCode.Space) && levelGenerator.readyToStart && !isDead)
        {
            bulletWeapon.Fire();
        }
    }

    private void HandleFirstCollision()
    {
        midSide = lastSide;
        newXPos = xPosLast;
        obstacleClash = true;
        isDamage = true;
        
        characterVFX.DamageCooldownVFX();

        audioSource.PlayOneShot(damageBurdClip);

        RollBack();

        StartCoroutine(ResetFlagAfterDelay(() => obstacleClash = false, 0.2f));
        StartCoroutine(ResetFlagAfterDelay(() => isDamage = false, damageTimeDelay));
    }

    private IEnumerator ResetFlagAfterDelay(System.Action resetAction, float delay)
    {
        yield return new WaitForSeconds(delay);
        resetAction();
    }

    private IEnumerator RollDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        characterController.center = new Vector3(0, colCenterY, 0);
        characterController.height = colHeight;
        inRoll = false;
    }

    private IEnumerator PauseGame(float seconds)
    {
        isPaused = true;
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(seconds);
        Time.timeScale = 1f;
        isPaused = false;
    }

    private IEnumerator LoadScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneLoader.Instance.LoadScene(0);
    }

}

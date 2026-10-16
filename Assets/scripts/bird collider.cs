using UnityEngine;
using DG.Tweening;

public class BirdController : MonoBehaviour
{
    [Header("Flap")]
    public float flapForce = 5f;

    [Header("Rotation")]
    public float rotationSpeed = 5f;
    public float maxUpRotation = 25f;
    public float maxDownRotation = -90f;

    [Header("Audio")]
    public AudioSource flapAudio;
    public AudioClip deathClip;

    [Header("Camera Shake - DOTween")]
    public Camera mainCamera;
    public float shakeDuration = 0.25f;
    public float shakeStrength = 0.15f;
    public int shakeVibrato = 10;
    public float shakeRandomness = 90f;

    [Header("Death Flash")]
    public DeathFlash deathFlash;

    [Header("Death Particles")]
    public ParticleSystem deathParticles;

    private Rigidbody2D rb;
    private bool hasDied = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (hasDied)
            return;

        // Keyboard input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Flap();
        }

        // Mouse input
        if (Input.GetMouseButtonDown(0))
        {
            Flap();
        }

        // Natural bird rotation
        RotateBird();
    }

    // =========================
    // FLAP
    // =========================

    void Flap()
    {
        // Velocity-based jump
        rb.linearVelocity = new Vector2(0f, flapForce);

        // Flap sound
        if (flapAudio != null)
        {
            flapAudio.Play();
        }
    }

    // =========================
    // BIRD ROTATION
    // =========================

    void RotateBird()
    {
        float velocity = rb.linearVelocity.y;

        float targetAngle = Mathf.Lerp(
            maxDownRotation,
            maxUpRotation,
            Mathf.InverseLerp(-5f, 5f, velocity)
        );

        Quaternion targetRotation =
            Quaternion.Euler(0f, 0f, targetAngle);

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    // =========================
    // COLLISION / DEATH
    // =========================

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Prevent death effects from triggering multiple times
        if (hasDied)
            return;

        hasDied = true;

        Debug.Log("BIRD COLLISION!");

        // Stop normal upward movement
        rb.linearVelocity = new Vector2(0f, -5f);

        // Spin bird
        rb.angularVelocity = -500f;

        // =========================
        // DEATH SOUND
        // =========================

        if (deathClip != null)
        {
            AudioSource.PlayClipAtPoint(
                deathClip,
                transform.position,
                1f
            );
        }

        // =========================
        // DOTWEEN CAMERA SHAKE
        // =========================

        if (mainCamera != null)
        {
            mainCamera
                .DOShakePosition(
                    shakeDuration,
                    shakeStrength,
                    shakeVibrato,
                    shakeRandomness
                )
                .SetUpdate(true);
        }

        // =========================
        // DEATH FLASH
        // =========================

        if (deathFlash != null)
        {
            deathFlash.Flash();
        }

        // =========================
        // DEATH PARTICLES
        // =========================

        if (deathParticles != null)
        {
            deathParticles.Play();
        }

        // =========================
        // GAME OVER
        // =========================

        if (GameManager.Instance != null)
        {
            GameManager.Instance.GameOver();
        }
    }
}
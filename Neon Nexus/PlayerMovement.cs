using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        Rigidbody rb;
        AudioSource audioSource;
        PlayerJump jump;
        [SerializeField] float footstepCooldown = 0.5f;
        float currentFootstepCooldown = 0.0f;
        [SerializeField] List<AudioClip> runSFX = new();

        [Tooltip("Normal speed the player runs at")]
        [SerializeField] float runSpeed = 10.0f;
        [Range(1, 10)]
        [SerializeField] float runDrag = 6.0f;
        [Range(0.1f, 1)]
        [SerializeField] float airDrag = 0.0f;
        public float airSpeed = 0.3f;
        internal float startAirSpeed = 0.3f;

        Vector3 direction;
        [HideInInspector]  public float speed;
        internal bool canRun = true;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
            speed = runSpeed;
            jump = GetComponent<PlayerJump>();
            startAirSpeed = airSpeed;
            audioSource = GetComponent<AudioSource>();
            audioSource.Pause();
        }

        void Update()
        {
            if (canRun)
            {
                FindMoveDir();
            }
            else
            {
                direction = Vector3.zero;
            }

            if (jump)
            {
                bool isGrounded = jump.Grounded();
                rb.drag = isGrounded ? runDrag : airDrag; //to optimise, use jump events
                speed = isGrounded ? runSpeed : airSpeed;
                currentFootstepCooldown -= Time.deltaTime;
                if (isGrounded && rb.velocity.magnitude > 3f && Time.timeScale > 0.1f && currentFootstepCooldown <= 0)
                {
                    audioSource.Stop();
                    AudioClip clip = runSFX[Random.Range(0, runSFX.Count)];
                    currentFootstepCooldown = footstepCooldown;
                    audioSource.PlayOneShot(clip);
                }
            }
        }

        void FindMoveDir()
        {
            direction = transform.forward * Input.GetAxisRaw("Vertical") + transform.right * Input.GetAxisRaw("Horizontal");
        }

        void FixedUpdate()
        {
            if (canRun)
            {
                rb.AddForce(direction.normalized * speed, ForceMode.Acceleration);
            }
        }

        public void IncreaseSpeed(float amt)
        {
            runSpeed += amt;
        }
    }
}
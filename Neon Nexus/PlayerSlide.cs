using UnityEngine;

namespace Player
{
    public class PlayerSlide : MonoBehaviour
    {
        Rigidbody rb;
        PlayerMovement playerMove;
        PlayerJump jump;

        [SerializeField] float maxSlideTime;
        float currentSlideTime = 0;
        [SerializeField] float slideForce;
        [SerializeField] float slideScale;
        [SerializeField] float downForce;
        Transform cam;
        float startingScale;
        bool sliding = false;
        Vector3 dir;
        [SerializeField] float rayUpHeight;
        [SerializeField] float distToHead;

        void Start()
        {
            startingScale = transform.localScale.y;
            rb = GetComponent<Rigidbody>();
            playerMove = GetComponent<PlayerMovement>();
            cam = Camera.main.transform;
            jump = GetComponent<PlayerJump>();
        }

        void Update()
        {
            if (Input.GetButtonDown("Slide") && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) && jump.Grounded())
            {
                StartSliding();
            }

            if (Input.GetButtonUp("Slide") && sliding && Physics.Raycast(transform.position + new Vector3(0, distToHead, 0), transform.up, out _, rayUpHeight))
            {
                StopSliding();
            }
        }

        void FixedUpdate()
        {
            if (sliding)
            {
                SlidingMovement();
            }
        }

        void StartSliding()
        {
            sliding = true;
            transform.localScale = new Vector3(transform.localScale.x, slideScale, transform.localScale.z);
            dir = transform.forward * Input.GetAxisRaw("Vertical") + transform.right * Input.GetAxisRaw("Horizontal");
            rb.AddForce(Vector3.down * downForce, ForceMode.Impulse);
            currentSlideTime = maxSlideTime;
            playerMove.canRun = false;
        }

        void StopSliding()
        {
            sliding = false;
            transform.localScale = new Vector3(transform.localScale.x, startingScale, transform.localScale.z);
            playerMove.canRun = true;
        }

        void SlidingMovement()
        {
            currentSlideTime -= Time.fixedDeltaTime;
            rb.AddForce(dir.normalized * slideForce, ForceMode.Force);
            if (currentSlideTime <= 0)
            {
                StopSliding();
            }
        }

        public void IncreaseSlideForce(float amt)
        {
            slideForce += amt;
        }
    }
}


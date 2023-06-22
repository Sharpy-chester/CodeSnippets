using UnityEngine;

namespace Player
{
    public class PlayerWallRun : MonoBehaviour
    {
        Rigidbody rb;
        PlayerMovement movement;
        public bool isWallRunning = false;
        [SerializeField] float wallRunDist;
        [Range(0, 1)]
        [SerializeField] float wallRunGravity = 0.1f;
        [SerializeField] float minSpeed = 2f;

        internal WallRunDirection wallRunDir = WallRunDirection.none;

        public delegate void StartWallrun();
        public event StartWallrun startWallrun;

        internal enum WallRunDirection
        {
            left,
            right,
            none
        }


        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            movement = GetComponent<PlayerMovement>();
        }

        void Update()
        {
            CheckWallRun();
        
        }

        void FixedUpdate()
        {
            if (isWallRunning)
            {
                UpdateWallRun();
            }
        }

        void CheckWallRun()
        {
            if (Physics.Raycast(transform.position, -transform.right, out RaycastHit leftHit, wallRunDist) && rb.velocity.magnitude > minSpeed)
            {
                wallRunDir = WallRunDirection.left;
                StartWallRun();
            }
            else if (Physics.Raycast(transform.position, transform.right, out RaycastHit rightHit, wallRunDist) && rb.velocity.magnitude > minSpeed)
            {
                wallRunDir = WallRunDirection.right;
                StartWallRun();
            }
            else if (isWallRunning)
            {
                EndWallRun();
            }
        }

        void StartWallRun()
        {
            isWallRunning = true;
            rb.useGravity = false;
        }

        void UpdateWallRun()
        {
            rb.AddForce(transform.up * (Physics.gravity.y * wallRunGravity), ForceMode.Acceleration);
        }

        public void EndWallRun()
        {
            isWallRunning = false;
            rb.useGravity = true;
            wallRunDir = WallRunDirection.none;
        }
    }
}


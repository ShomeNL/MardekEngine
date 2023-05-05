using UnityEngine;
using MARDEK.Core;

namespace MARDEK.Movement
{
    public class FollowMovementController : MovementController
    {
        [SerializeField] Movable followedMovement;
        bool shouldFollow = false;

        public bool ShouldFollow
        {
            get 
            {
                return shouldFollow;
            }
            set 
            {
                shouldFollow = value;
            }
        }

        private void OnEnable()
        {
            if (followedMovement)
                followedMovement.OnStartMove += OnFollowedMovementMoved;
        }

        private void OnDisable()
        {
            if (followedMovement)
                followedMovement.OnStartMove -= OnFollowedMovementMoved;
        }

        void OnFollowedMovementMoved()
        {
            shouldFollow = true;
            //assures that follower-follower movement will happen in the same frame.
            if (followedMovement.CompareTag("Player") == false)
                MoveToFollowed();
        }

        void MoveToFollowed()
        {
            Vector2 desiredDelta = followedMovement.lastPosition - (Vector2)transform.position;
            MoveDirection followDirection = ApproximanteDirectionByVector2(desiredDelta);
            if (followDirection)
            {
                SendDirection(followDirection);
            }
        }

        private void Update()
        {
            if (movement.isMoving == false && shouldFollow)
            {                
                if (Utilities2D.AreCloseEnough(followedMovement.lastPosition, transform.position))
                    shouldFollow = false;
                else
                    MoveToFollowed();
            }
        }
    }
}
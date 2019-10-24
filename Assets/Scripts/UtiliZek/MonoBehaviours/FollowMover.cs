using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtiliZek
{
    [RequireComponent(typeof(Rigidbody))]
    public class FollowMover : MonoBehaviour
    {
        public Transform target;
        public Vector3 targetOffset;
        public float speed = 1500;
        public float speedBonus = 1;
        public float horizontalDrag = 500;
        public bool followY = true;
        public float bonusGravity = 1000;

        private new Rigidbody rigidbody;

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            Vector3 relativePosition = target.position + targetOffset - transform.position;

            if (!followY)
                relativePosition = NoY(relativePosition);

            if (relativePosition.sqrMagnitude < 1 && relativePosition.sqrMagnitude > 0.1f)
                relativePosition = relativePosition.normalized;

            Vector3 downVector = Vector3.down * bonusGravity;
            Vector3 toTargetVector = relativePosition * speedBonus * speed;
            Vector3 dragVector = NoY(rigidbody.velocity * horizontalDrag);
            rigidbody.AddForce(Time.deltaTime * (downVector + toTargetVector - dragVector));

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 1, 0), 0.02f);
        }

        private Vector3 NoY(Vector3 vector3)
        {
            return new Vector3(vector3.x, 0, vector3.z);
        }
    }
}
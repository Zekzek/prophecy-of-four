using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtiliZek
{
    [RequireComponent(typeof(Rigidbody))]
    public class FollowCamera : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset;
        public float speed = 1000;

        private static FollowCamera instance;
        private new Rigidbody rigidbody;

        public static float DistanceToTarget { get { return instance.offset.magnitude; } }

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            rigidbody.useGravity = false;
        }

        private void Update()
        {
            Vector3 relativePosition = target.position + offset - transform.position;
            rigidbody.velocity = relativePosition * Time.deltaTime * speed;
        }
    }
}
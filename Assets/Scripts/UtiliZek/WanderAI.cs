using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtiliZek
{
    [RequireComponent(typeof(Rigidbody))]
    public class WanderAI : MonoBehaviour
    {
        public float baseAttentionSpan = 1f;
        public float speed = 20;
        public float maxVelocity = 25;

        private new Rigidbody rigidbody;
        private float sqrMaxVelocity;
        private Vector3 chosenDirection;
        private float attentiveLevel = 0f;

        private void start()
        {
            rigidbody = GetComponent<Rigidbody>();
            sqrMaxVelocity = maxVelocity * maxVelocity;
        }

        private void Update()
        {
            if (Random.value > 0.5f)
                attentiveLevel -= Time.deltaTime;
            if (attentiveLevel <= 0)
                PickDirection();
            else
                MoveInDirection(chosenDirection);
        }

        private void MoveInDirection(Vector3 positionDelta)
        {
            rigidbody.velocity += positionDelta * Time.deltaTime * speed;
            if (rigidbody.velocity.sqrMagnitude > sqrMaxVelocity)
                rigidbody.velocity = rigidbody.velocity.normalized * maxVelocity;

            transform.LookAt(transform.position + positionDelta);
        }

        private void PickDirection()
        {
            chosenDirection = new Vector3(2 * Random.value - 1, 0, 2 * Random.value - 1);
            attentiveLevel = baseAttentionSpan;
        }
    }
}
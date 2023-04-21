using UnityEngine;

namespace AssemblyActorCore
{
    public class MovablePhysic : Movable
    {
        private Rigidbody _rigidbody;

        private new void Awake()
        {
            base.Awake();

            _rigidbody = GetComponent<Rigidbody>();
        }

        public override void Enable(bool state)
        {
            if (state == true)
            {
                _rigidbody.MovePosition(_rigidbody.position);
                _rigidbody.velocity = Vector3.zero;
                _rigidbody.constraints = RigidbodyConstraints.None;
                _rigidbody.freezeRotation = true;
                _rigidbody.useGravity = false;
                _rigidbody.isKinematic = false;
            }
            else
            {
                _rigidbody.MovePosition(_rigidbody.position);
                _rigidbody.velocity = Vector3.zero;
                _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                _rigidbody.isKinematic = true;
            }
        }

        protected override void Move()
        {
            _rigidbody.MovePosition(_rigidbody.position + velocity);
            _rigidbody.AddForce(Physics.gravity * gravity, ForceMode.Acceleration);
        }

        protected override void Force(ref float force) 
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(Vector3.up * force, ForceMode.Impulse);

            force = 0;
        }
    }
}
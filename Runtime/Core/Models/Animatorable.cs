using System.Collections.Generic;
using UnityEngine;

namespace Actormachine
{
    /// <summary> Model - to control the Animator. </summary>
    public class Animatorable : ActorBehaviour
    {          
        // Parameters
        private float _speed = 0;
        private Vector3 _direction = Vector3.zero;
        private string _previousName = "None";

        public Animator _animator = null;

        private RuntimeAnimatorController _previousAnimatorController = null;

        private new void Awake()
        {
            base.Awake();

            _animator = RootTransform.GetComponentInChildren<Animator>();
        }

        public float Speed
        {
            get => _speed;

            set
            {
                _speed = value;

                if (_animator)
                {
                    _animator.SetFloat("Speed", _speed);
                }
            }
        }

        public Vector3 Direction
        {
            get => _direction;

            set
            {
                _direction = value;

                if (_animator)
                {
                    _animator.SetFloat("DirectionX", _direction.x);
                    _animator.SetFloat("DirectionY", _direction.y);
                    _animator.SetFloat("DirectionZ", _direction.z);
                }
            }
        }

        public void Enter(RuntimeAnimatorController controller)
        {
            if (controller == null)
            {
                return;
            }

            _previousAnimatorController = _animator.runtimeAnimatorController;
            _animator.runtimeAnimatorController = controller;

            _previousName = "Enter";
        }

        public void Exit()
        {
            if (_previousAnimatorController == null)
            {
                return;
            }

            _animator.runtimeAnimatorController = _previousAnimatorController;
            
            _previousName = "Exit";
        }

        public void Play(string name, float fade = 0.025f)
        {
            if (_animator)
            {
                if (name != _previousName)
                {
                    Debug.Log(name);
                    _animator.CrossFade(name, fade, 0, 0);
                    _animator.speed = 1;
                    _previousName = name;
                }
            }
        }

        public void Stop()
        {
            if (_animator)
            {
                _animator.speed = 0;
                _animator.SetFloat("Speed", 0);
            }

            _previousName = "Stop";
        }
    }
}
using UnityEngine;

namespace AssemblyActorCore
{
    public class InteractionController : Controller
    {
        public float Duration = 1;
        private float _speed => 1 / Duration;
        private float _timer = 0;

        protected Animatorable animatorable;

        protected new void Awake()
        {
            base.Awake();

            animatorable = GetComponentInParent<Animatorable>();
        }

        public override void Enter()
        {
            animatorable.Play(Name, _speed);
            _timer = 0;
        }

        public override void UpdateLoop()
        {
            _timer += Time.deltaTime;

            if (_timer >= Duration)
            {
                controllerMachine.Deactivate(gameObject);
            }
        }

        public override void FixedLoop() { }

        public override void Exit() { }
    }
}
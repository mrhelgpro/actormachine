using UnityEngine;

namespace AssemblyActorCore
{
    public abstract class Model : MonoBehaviour
    {
        public Transform GetRoot => mainTransform;

        protected Transform myTransform;
        protected Transform mainTransform;

        protected void Awake()
        {
            Actor actor = GetComponentInParent<Actor>();

            mainTransform = actor == null ? transform : actor.transform;
            myTransform = transform;
        }
    }

    public abstract class Presenter : MonoBehaviour
    {
        public PresenterType Type;
        public string Name = "Presenter";

        protected PresenterMachine presenterMachine;

        protected void Awake() => presenterMachine = GetComponentInParent<PresenterMachine>();

        protected void TryToActivate() => presenterMachine.TryToActivate(gameObject);

        public abstract void Enter();
        public abstract void UpdateLoop();
        public abstract void FixedLoop();
        public abstract void Exit();
    }

    public abstract class Activator : MonoBehaviour
    {
        protected PresenterMachine presenterMachine;
        protected Presenter presenter;
        protected void Awake()
        {
            presenter = GetComponent<Presenter>();

            if (presenter == null)
            {
                gameObject.SetActive(false);

                Debug.LogWarning(gameObject.name + " - <Presenter> is not found");
            }
            else
            {
                presenterMachine = GetComponentInParent<PresenterMachine>();
            }
        }
        protected void TryToActivate() => presenterMachine.TryToActivate(gameObject);

        public abstract void UpdateActivate();
    }
}
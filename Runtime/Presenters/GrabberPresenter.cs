using UnityEngine;

namespace Actormachine
{
    public class GrabberPresenter : Presenter
    {
        // Model Components
        private Interactable _interactable;

        // Presenter Methods
        public override void Initiate()
        {
            // Get components using "GetComponentInRoot" to create them on <Actor>
            _interactable = AddComponentInRoot<Interactable>();
        }

        public override void Enter()
        {

        }

        public override void Exit()
        {

        }
    }
}
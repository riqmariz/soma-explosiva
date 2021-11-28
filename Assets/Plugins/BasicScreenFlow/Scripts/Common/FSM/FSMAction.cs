using UnityEngine;

namespace Common.FSM
{
    public abstract class FSMAction
    {
        private FSMState owner;

        public FSMAction (FSMState owner)
        {
            this.owner = owner;
        }

        protected void SetOwner(FSMState owner)
        {
            this.owner = owner;
        }

        public FSMState GetOwner ()
        {
            return owner;
        }

        public void TryChangeStateTo(string StateID)
        {
            if (!string.IsNullOrEmpty(StateID))
            {
                owner.SendEvent(StateID, owner.Name);
            }
            else
            {
                Debug.LogWarning("Tried to change state with a null or empty ID");
            }
        }

        ///<summary>
        /// Starts the action.
        ///</summary>
        public abstract void OnEnter();

        /// <summary>
        ///  Updates the action.
        /// </summary>
        public abstract void OnUpdate();

        /// <summary>
        ///  Updates the action in a fixed update so you have a fixed update that does not depend on the fps.
        /// </summary>
        public abstract void OnFixedUpdate();

        ///<summary>
        /// Finishes the action.
        ///</summary>
        public abstract void OnExit();
    }
}
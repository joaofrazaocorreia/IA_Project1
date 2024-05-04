using LibGameAI.FSMs;

namespace Entities
{
    /// <summary>
    /// Defines an agent that doesn't move.
    /// </summary>
    public abstract class StaticAgent: Agent
    {
        protected StateMachine StateMachine;

        private void Update()
        {
            StateMachine.Update();
        }
    }
}
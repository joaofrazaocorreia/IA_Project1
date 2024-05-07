using Locals;
using UnityEngine;
using UnityEngine.AI;
using UnityTimer;

namespace Entities
{
    /// <summary>
    /// Base class for agents that have movement.
    /// </summary>
    public abstract class MobileAgent : Agent
    {
        public NavMeshAgent agent;
        public int maxSpeed = 20;
        
        [HideInInspector] public Destination initialDestination;
        
        protected Timer Timer;
        protected Destination CurrentDestination;
        
        private bool _isOnDestination;

        /// <summary>
        /// Hides agents that are inside of the destination area.
        /// </summary>
        protected bool isOnDestination
        {
            get => _isOnDestination;
            set
            {
                GetComponent<Renderer>().enabled = value switch
                {
                    true => false,
                    false => true
                };

                _isOnDestination = value;
            }
        }

        public virtual void Start()
        {
            agent.enabled = true;
        }
    }
}
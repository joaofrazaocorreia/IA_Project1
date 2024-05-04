using Locals;
using UnityEngine.AI;
using UnityTimer;

namespace Entities
{
    public abstract class MobileAgent : Agent
    {
        public NavMeshAgent agent;
        public Destination initialDestination;
        
        protected Timer Timer;
        protected Destination CurrentDestination;

        public abstract Destination GetRandomDestination();

        public virtual void Start()
        {
            CurrentDestination = initialDestination;
        }
    }
}
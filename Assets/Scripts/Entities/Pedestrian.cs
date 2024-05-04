using Locals;
using Manager;
using UnityEngine;
using UnityTimer;

namespace Entities
{
    public class Pedestrian : MobileAgent
    {
        public override void Start()
        {
            base.Start();
            Timer = Timer.Register(SimulationManager.instance.pedestrianDestinationMaxTime, ChooseRandomDestination, 
                isLooped: true);
            CurrentDestination.EnterDestination(this);
        }

        private void ChooseRandomDestination()
        {
            transform.SetPositionAndRotation(CurrentDestination.pedestrianExitPoint.position, 
                CurrentDestination.pedestrianExitPoint.rotation);
            CurrentDestination.LeaveDestination(this);
            
            CurrentDestination = GetRandomDestination();
            agent.Move(CurrentDestination.position);
        }

        public override Destination GetRandomDestination()
        {
            return SimulationManager.instance.allDestinations[Random.Range(0, 
                SimulationManager.instance.allDestinations.Count)];
        }
    }
}
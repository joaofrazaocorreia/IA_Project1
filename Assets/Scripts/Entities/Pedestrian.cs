using LibGameAI.DecisionTrees;
using Locals;
using Manager;
using UnityEngine;
using UnityTimer;
using Random = UnityEngine.Random;

namespace Entities
{
    /// <summary>
    /// Class for the default pedestrian.
    /// </summary>
    public class Pedestrian : MobileAgent, ITrafficLightListener
    {
        public bool isOnCrossroad { get; set; }

        private Vector3 _vel; //navmesh agent's velocity
        private bool _madeDecision; //flag to not repeat decision tree node checking unnecessarily
        
        private IDecisionTreeNode _root;
        private IDecisionTreeNode _current;
        
        public override void Start()
        {
            base.Start();
            agent.speed = maxSpeed * .02f;
            Timer = Timer.Register(SimulationManager.instance.pedestrianDestinationMaxTime, ChooseRandomDestination, 
                isLooped: true);
            isOnDestination = true;
            CurrentDestination.EnterDestination(this);
            _vel = agent.velocity;
        }
        
        
        /// <summary>
        /// Chooses a random destination for the pedestrian to go to.        
        /// It also sets the agent's position and rotation to that of the current destination's exit point, 
        /// then calls LeaveDestination on CurrentDestination.
        /// </summary>
        private void ChooseRandomDestination()
        {
            transform.SetPositionAndRotation(CurrentDestination.pedestrianExitPoint.position, 
                CurrentDestination.pedestrianExitPoint.rotation);
            CurrentDestination.LeaveDestination(this);
            isOnDestination = false;
            
            CurrentDestination = GetRandomDestination();
            agent.Move(CurrentDestination.position);
            Timer.Pause();
        }

        
        /// <summary>
        /// Chooses a random destination.
        /// </summary>
        /// 
        /// <returns> A random destination from the list of all destinations.</returns>
        private Destination GetRandomDestination()
        {
            return SimulationManager.instance.allDestinations[Random.Range(0, 
                SimulationManager.instance.allDestinations.Count)];
        }

        private void Update()
        {
            if (!isOnDestination)
            {
                if (!agent.pathPending)
                {
                    if (agent.remainingDistance <= agent.stoppingDistance)
                    {
                        if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                        {
                            CurrentDestination.EnterDestination(this);
                            isOnDestination = true;
                            Timer.Resume();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Called when the traffic light this pedestrian is affected by goes 'green'.        
        /// It sets the agent's velocity to what it was before pausing.
        /// </summary>
        public void Resume()
        {
            agent.velocity = _vel;
        }

        /// <summary>
        /// Called when the traffic light this pedestrian is affected by goes 'red'.
        /// Sets the agent's velocity to zero, which causes it to stop moving.
        /// </summary> 
        public void Wait()
        {
            agent.velocity = Vector3.zero;
        }

        /// <summary>
        /// Called when the traffic light this pedestrian is affected by goes 'orange'.      
        /// It checks if the agent is on a crossroad and then decides whether it will wait or start crossing,
        /// aka erratic/accident-prone behaviour.
        /// </summary>
        public void SlowDown()
        {
            // possible behaviours:
            //
            // * the agent is crossing the street:
            // - normal: finishes
            // - erratic: stops moving
            // 
            // * the agent isn't crossing the street
            // - normal: stops, waits
            // - erratic: starts crossing the street
            
            if (_madeDecision) 
                return;
            
            _madeDecision = true;
            
            RandomDecisionBehaviour rdb = new RandomDecisionBehaviour(() => Random.Range(0, 1), 
                () => Time.time, 4);

            DecisionNode crossingStreet = new DecisionNode(rdb.RandomDecision, new ActionNode(delegate {  }), 
                new ActionNode(Wait));
            
            DecisionNode notCrossingStreet = new DecisionNode(rdb.RandomDecision, new ActionNode(Wait), 
                new ActionNode(delegate {  }));

            DecisionNode checkCrossingNode = new DecisionNode
            (
                () => isOnCrossroad, 
                crossingStreet,
                notCrossingStreet
            );
            
            _root = checkCrossingNode;
            
            (_root.MakeDecision() as ActionNode)?.Execute();
        }
    }
}
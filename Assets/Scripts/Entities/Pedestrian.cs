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
        private bool _madeDecision; //flag to not repeat DT node checking unnecessarily
        
        private IDecisionTreeNode _root;
        private IDecisionTreeNode _current;
        
        private Destination _nextDestination;

        /// <summary> Sets the speed of the agent to 20% of its maxSpeed,        
        /// registers the timer, and overall initiates the agent.
        /// </summary>
        /// <param name="destination"> The destination to start at.</param>
        public void Init(Destination destination)
        {
            agent.speed = maxSpeed * .2f;
            Timer = Timer.Register(SimulationManager.instance.
                pedestrianDestinationMaxTime, LeaveCurrentDestination);
            
            isOnDestination = true;
            CurrentDestination = destination;
            CurrentDestination.EnterDestination(this);
            _vel = agent.velocity;
            
            agent.enabled = CurrentDestination == null;
            GetComponent<Renderer>().enabled = CurrentDestination == null;
            _nextDestination = GetRandomDestination();
        }
        
        /// <summary>
        /// Called when the pedestrian enters a destination.        
        /// Sets the timer to leave the current destination, and calls
        /// EnterDestination on _nextDestination.
        /// </summary>
        private void EnterDestination()
        {
            if (CurrentDestination == null)
            {
                Debug.LogError("Error: Current destination is null when " +
                    "entering destination.");
                return;
            }
	        
            Debug.Log("Entered destination. Current destination: "+
                $"{CurrentDestination}, " +
                      $"Next destination: {_nextDestination}");
	        
            Timer = Timer.Register(SimulationManager.instance.
                pedestrianDestinationMaxTime, LeaveCurrentDestination);
	        
            _nextDestination.EnterDestination(this);
            
            agent.enabled = false;
            GetComponent<Renderer>().enabled = false;
	        
            CurrentDestination = _nextDestination;
        }

        /// <summary>
        /// Chooses a random destination for the pedestrian to go to.        
        /// It also sets the agent's position and rotation to that of the
        /// current destination's exit point, then calls LeaveDestination on
        /// CurrentDestination.
        /// </summary>
        private void LeaveCurrentDestination()
        {
            transform.SetPositionAndRotation(CurrentDestination.
                pedestrianExitPoint.position, Quaternion.identity);
            GetComponent<Renderer>().enabled = true;
            CurrentDestination.LeaveDestination(this);
            isOnDestination = false;
            
            CurrentDestination = null;
            _nextDestination = GetRandomDestination();
            agent.destination = _nextDestination.position;
        }

        
        /// <summary>
        /// Chooses a random destination.
        /// </summary>
        /// <returns> A random destination from the list of all destinations.
        /// </returns>
        private Destination GetRandomDestination()
        {
            return SimulationManager.instance.allDestinations[Random.Range(0, 
                SimulationManager.instance.allDestinations.Count)];
        }

        private void Update()
        {
            if (agent.enabled && agent.isOnNavMesh)
            {
                if (CurrentDestination == null && _nextDestination != null)
                {
                    float distanceToDestination = Vector3.Distance
                        (transform.position, _nextDestination.position);
            
                    if (distanceToDestination < 5f)
                    {
                        EnterDestination();
                    }
                }
            }
        }

        /// <summary>
        /// Called when the traffic light this pedestrian is affected by goes
        /// 'green'. It sets the agent's velocity to what it was before pausing.
        /// </summary>
        public void Resume()
        {
            agent.velocity = _vel;
        }

        /// <summary>
        /// Called when the traffic light this pedestrian is affected by goes
        /// 'red'. Sets the agent's velocity to zero, which causes it to stop
        /// moving.
        /// </summary> 
        public void Wait()
        {
            agent.velocity = Vector3.zero;
        }

        /// <summary>
        /// Called when the traffic light this pedestrian is affected by goes
        /// 'orange'. It checks if the agent is on a crossroad and then decides
        /// whether it will wait or start crossing.
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
            
            RandomDecisionBehaviour rdb = new RandomDecisionBehaviour(() =>
                Random.Range(0, 1), () => Time.time, 4);

            DecisionNode crossingStreet = new DecisionNode(rdb.RandomDecision,
                new ActionNode(delegate {  }), new ActionNode(Wait));
            
            DecisionNode notCrossingStreet = new DecisionNode(rdb.RandomDecision,
                new ActionNode(Wait), new ActionNode(delegate {  }));

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
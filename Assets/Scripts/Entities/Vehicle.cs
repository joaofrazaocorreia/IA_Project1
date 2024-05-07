using LibGameAI.DecisionTrees;
using Locals;
using Manager;
using UnityEngine;
using UnityTimer;
using Random = UnityEngine.Random;

namespace Entities
{
    /// <summary>
    /// Defines the behaviour of a generic vehicle.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class Vehicle : MobileAgent, ITrafficLightListener
    {
        public float stopDistance = .25f; //distance to prevent 'hitting' others
        
        private RoadWaypoint _currentRoad;
        private RoadConnector _currentConnector;
        
        private bool _madeDecision;
        private bool _isWaiting;
        private Destination _nextDestination;
        
        private IDecisionTreeNode _root;
        private IDecisionTreeNode _current;

        /// <summary>
        /// Sets up the vehicle's initial state, including its destination and 
        /// speed.
        /// </summary>
        /// <param name="destination"> The destination to start at.</param>
        public void Init(Destination destination)
        {
            Timer = Timer.Register(SimulationManager.instance.
                vehicleDestinationMaxTime, LeaveCurrentDestination);

            if (isUncontrolled)
                agent.speed = maxSpeed * 2f;

            else
                agent.speed = maxSpeed * 0.2f;

            CurrentDestination = destination;
            
            agent.enabled = CurrentDestination == null;
            GetComponentInChildren<Renderer>().enabled =
                CurrentDestination == null;
            
            CurrentDestination.EnterDestination(this);
            _nextDestination = GetRandomDestination();
        }

        /// <summary>
        /// Called when the vehicle enters a destination.        
        /// It sets the timer to leave the current destination, and calls
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
	        
            Debug.Log("Entered destination. Current destination: " +
                $"{CurrentDestination}, " +
                      $"Next destination: {_nextDestination}");
	        
            Timer = Timer.Register(SimulationManager.instance.
                vehicleDestinationMaxTime, LeaveCurrentDestination);
	        
            _nextDestination.EnterDestination(this);
            
            agent.enabled = false;
            GetComponentInChildren<Renderer>().enabled = false;
	        
            CurrentDestination = _nextDestination;
            _nextDestination = GetRandomDestination();
        }

        /// <summary>
        /// Sets the current road and connector for the vehicle.        
        /// It also registers this vehicle with that road, and sets its speed to
        /// be either the maximum speed of this vehicle or the speed limit of
        /// that road, whichever is lower.
        /// </summary>
        /// <param name="road"> The road the vehicle is on.</param>
        private void SetOnRoad(RoadWaypoint road)
        {
            if (road == null)
            {
                Debug.Log("Road is null.");
            }
            
            _currentRoad = road;
            _currentRoad.RegisterVehicle(this, true);

            agent.enabled = true;
            agent.speed = Mathf.Min(road.roadSpeedLimit, maxSpeed) * .2f;
        }

        /// <summary>
        /// Chooses a random destination for the vehicle to go to.        
        /// It does this by first setting the position and rotation of the
        /// vehicle to that of its current destination's exit point, then it
        /// sets itself on road using that exit point's waypointToConnect and
        /// itself as parameters. 
        /// </summary>
        private void LeaveCurrentDestination()
        {
            transform.SetPositionAndRotation(CurrentDestination.
                vehicleExitPoint.vehicleSpawnPt.position, Quaternion.identity);
            SetOnRoad(CurrentDestination.vehicleExitPoint);

            GetComponentInChildren<Renderer>().enabled = true;
            
            CurrentDestination.LeaveDestination(this);

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
            if (!_isWaiting && agent.enabled && agent.isOnNavMesh)
            {
                if (CheckCollision())
                {
                    Wait();
                }

                else
                {
                    Resume();

                    if (CurrentDestination == null && _nextDestination != null)
                    {
                        float distanceToDestination = Vector3.Distance
                            (transform.position, _nextDestination.position);
                
                        if (distanceToDestination < 3f)
                        {
                            EnterDestination();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Called when the traffic light this vehicle is affected by goes
        /// 'green'. It sets the agent's velocity to what it was before pausing.
        /// </summary>
        public void Resume()
        {
            _isWaiting = false;
            agent.isStopped = false;
            
            if (isUncontrolled)
                agent.speed = maxSpeed;
        }

        /// <summary>
        /// Called when the traffic light this vehicle is affected by goes
        /// 'red'. Sets the agent's velocity to zero, which causes it to stop
        /// moving.
        /// </summary> 
        public void Wait()
        {
            if (isUncontrolled)
                Resume();
            
            else
            {
                _isWaiting = true;
                agent.isStopped = true;
            }
        }

        /// <summary>
        /// Called when the traffic light this vehicle is affected by goes
        /// 'orange'. It causes the agent to slow down.
        /// </summary>
        public void SlowDown()
        {
            if (isUncontrolled)
                Resume();

            else
                agent.speed = agent.speed * 0.3f;
        }
        
        /// <summary>
        /// Checks if the car is within a certain distance of an object with the
        /// tag "Pedestrian".
        /// </summary>        
        /// <returns> A boolean value. If the car is within a certain distance
        /// of the pedestrian, it returns true.
        /// Otherwise, it returns false.</returns>
        private bool CheckCollision()
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            RaycastHit hit;
            
            if (Physics.Raycast(transform.position, forward, out hit))
            {
                if (Vector3.Distance(transform.position, hit.point) < stopDistance)
                {
                    if (hit.transform.CompareTag("Unit"))
                        return true;
                }
                
                return false;
            }
            
            return false;
        }
        
        /// <summary>
        /// Called when the vehicle enters a trigger collider.        
        /// If the collider has a RoadConnector component, then it will register
        /// itself with that connector's waypoint.
        /// </summary>
        public void OnTriggerEnter(Collider col)
        {
            if (col.CompareTag("Connector"))
            {
                RoadConnector connection = col.GetComponent<RoadConnector>();

                if (connection.waypointToConnect != _currentRoad)
                {
                    _currentRoad.RegisterVehicle(this, false);

                    if (isUncontrolled)
                        agent.speed = maxSpeed;

                    else
                        agent.speed = Mathf.Min(connection.waypointToConnect.
                            roadSpeedLimit, maxSpeed) * .2f;

                    _currentRoad = connection.waypointToConnect;
                    
                    _currentRoad.RegisterVehicle(this, true);
                    _currentConnector = connection.GetConnection();
                    
                    if(_currentConnector != null)
                        agent.destination = _currentConnector.transform.position;
                }
            }
        }
    }
}
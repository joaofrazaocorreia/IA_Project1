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
        public float stopDistance = .25f; //distance to stop from 'hitting' other vehicles
        
        private RoadWaypoint _currentRoad;
        private RoadConnector _currentConnector;
        
        private bool _madeDecision;
        private bool _isWaiting;
        
        private IDecisionTreeNode _root;
        private IDecisionTreeNode _current;

        public override void Start()
        {
            base.Start();
            Timer = Timer.Register(SimulationManager.instance.vehicleDestinationMaxTime, ChooseRandomDestination, 
            isLooped: true);
            CurrentDestination.EnterDestination(this);
        }

        /// <summary>
        /// Sets the current road and connector for the vehicle.        
        /// It also registers this vehicle with that road, and sets its speed to be either
        /// the maximum speed of this vehicle or the speed limit of that road, whichever is lower.
        /// </summary>
        ///
        /// <param name="road"> The road the vehicle is on.</param>
        /// <param name="connector"> The connector that the vehicle is currently on.</param>
        private void SetOnRoad(RoadWaypoint road, RoadConnector connector)
        {
            _currentRoad = road;
            _currentConnector = connector;
            
            road.RegisterVehicle(this, true);
            
            agent.speed = Mathf.Min(road.roadSpeedLimit, maxSpeed) * .02f;
        }

        /// <summary>
        /// Chooses a random destination for the vehicle to go to.        
        /// It does this by first setting the position and rotation of the vehicle to that of its
        /// current destination's exit point, then it sets itself on road using that exit point's
        /// waypointToConnect and itself as parameters. 
        /// </summary>
        private void ChooseRandomDestination()
        {
            transform.SetPositionAndRotation(CurrentDestination.vehicleExitPoint.transform.position, 
                CurrentDestination.vehicleExitPoint.transform.rotation);
            SetOnRoad(CurrentDestination.vehicleExitPoint.waypointToConnect, CurrentDestination.vehicleExitPoint);
            
            CurrentDestination.LeaveDestination(this);
            
            CurrentDestination = GetRandomDestination();
            
            agent.Move(CurrentDestination.position);
        }

        /// <summary>
        /// <see cref="Pedestrian.GetRandomDestination"/>
        /// </summary>
        /// <returns></returns>
        private Destination GetRandomDestination()
        {
            return SimulationManager.instance.allDestinations[Random.Range(0, 
                SimulationManager.instance.allDestinations.Count)];
        }

        private void Update()
        {
            if (!_isWaiting && agent.isOnNavMesh)
            {
                if (CheckCollision())
                {
                    Wait();
                }
                else
                {
                    Resume();
                }
            }
        }

        /// <summary>
        /// <see cref="Pedestrian.Resume"/>
        /// </summary>
        public void Resume()
        {
            agent.isStopped = false;
        }

        /// <summary>
        /// <see cref="Pedestrian.Wait"/>
        /// </summary>
        public void Wait()
        {
            _isWaiting = true;
            agent.isStopped = true;
        }

        /// <summary>
        /// <see cref="Pedestrian.SlowDown"/>
        /// </summary>
        public void SlowDown()
        {
            // possible behaviours:
            //
            // * there are agents passing the street
            // - normal: waits
            // - erratic: starts moving
            // 
            // * no agents on the street
            // - normal: slows down, but crosses if there's enough time
            
            //to-be implemented
        }
        
        /// <summary>
        /// Checks if the car is within a certain distance of an object with the tag "Pedestrian".
        /// </summary>        
        /// <returns> A boolean value. If the car is within a certain distance of the pedestrian, it returns true.
        /// Otherwise, it returns false.</returns>
        private bool CheckCollision()
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            RaycastHit hit;
            
            if (Physics.Raycast(transform.position, forward, out hit))
            {
                if (Vector3.Distance(transform.position, hit.point) < stopDistance)
                {
                    if (hit.transform.CompareTag("Pedestrian"))
                        return true;
                }
                
                return false;
            }
            
            return false;
        }
        
        /// <summary>
        /// Called when the vehicle enters a trigger collider.        
        /// If the collider has a RoadConnector component, then it will register itself
        /// with that connector's waypoint.
        /// </summary>
        public void OnTriggerEnter(Collider col)
        {
            if (col.CompareTag("Connector"))
            {
                RoadConnector connection = col.GetComponent<RoadConnector>();

                if (connection.waypointToConnect != _currentRoad)
                {
                    _currentRoad.RegisterVehicle(this, false);
                    
                    agent.speed = Mathf.Min(connection.waypointToConnect.roadSpeedLimit, maxSpeed) * .02f;
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
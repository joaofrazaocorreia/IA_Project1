using System.Collections.Generic;
using System.Linq;
using Entities;
using TMPro;
using UnityEngine;

namespace Locals
{
    /// <summary>
    /// Defines a destination and it's behaviours.
    /// </summary>
    public class Destination : MonoBehaviour
    {
        public TMP_Text vehicleCounter;
        public TMP_Text pedestrianCounter;
        
        public RoadConnector vehicleExitPoint;
        public Transform pedestrianExitPoint;
        
        private List<Agent> _agentsInside;
        private int _numberOfAgentsInside;
        
        public Vector3 position => transform.position;

        private int NumberOfVehiclesInside() => _agentsInside.Count(x => x.GetType() == typeof(Vehicle));
        private int NumberOfPedestriansInside() => _agentsInside.Count(x => x.GetType() == typeof(Pedestrian));

        private void Update()
        {
            vehicleCounter.text = NumberOfVehiclesInside().ToString();
            pedestrianCounter.text = NumberOfPedestriansInside().ToString();
        }

        /// <summary>
        /// Adds an agent to the list of agents inside the destination.
        /// </summary>  
        public void EnterDestination(Agent agent)
        {
            _agentsInside.Add(agent);
        }

        /// <summary>
        /// Removes the agent from the list of agents inside.
        /// </summary>
        public void LeaveDestination(Agent agent)
        {
            _agentsInside.Remove(agent);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, 0.5f);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Entities;
using TMPro;
using UnityEngine;

namespace Locals
{
    public class Destination : MonoBehaviour
    {
        public TMP_Text vehicleCounter;
        public TMP_Text pedestrianCounter;
        public Transform vehicleExitPoint;
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

        public void EnterDestination(Agent agent)
        {
            _agentsInside.Add(agent);
        }

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
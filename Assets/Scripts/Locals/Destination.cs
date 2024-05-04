using System.Collections.Generic;
using System.Linq;
using Entities;
using UnityEngine;

namespace Locals
{
    public class Destination : MonoBehaviour
    {
        private List<Agent> _agentsInside;

        private int _numberOfAgentsInside;

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
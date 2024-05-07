using UnityEngine;

namespace Locals
{
    /// <summary>
    /// Component that connects a road to another.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class RoadConnector : MonoBehaviour
    {
        public RoadWaypoint waypointToConnect { get; set; }
        public RoadConnector[] otherConnections;
        
        /// <summary>
        /// Gets a random connection connected to this one.
        /// </summary>
        public RoadConnector GetConnection()
        {
            return otherConnections.Length > 0 ? otherConnections[Random.Range(0, otherConnections.Length)] : null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            
            Gizmos.DrawSphere(transform.position - new Vector3(0, 
                transform.localScale.y * 0.5f, 0), 0.05f);
        }
    }
}
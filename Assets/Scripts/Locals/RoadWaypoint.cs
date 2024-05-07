using System.Collections.Generic;
using Entities;
using UnityEngine;

namespace Locals
{
    /// <summary>
    /// Defines a road point.
    /// </summary>
    public class RoadWaypoint: MonoBehaviour
    {
        public Transform vehicleSpawnPt;
        
        public RoadConnector[] connections;
        public int roadSpeedLimit = 20; //max speed vehicles can circulate at, on this road

        private List<Vehicle> activeVehicles { get; set; }

        private void Start()
        {
            activeVehicles = new List<Vehicle>();
            
            foreach (var r in connections)
            {
                r.waypointToConnect = this;
            }
        }

        /// <summary>
        /// Adds or removes a vehicle from the activeVehicles list.
        /// </summary>
        /// <param name="v"> The vehicle that is being added or removed from the list.</param>
        /// <param name="add"> Whether or not the vehicle should be added to the list of active vehicles. 
        /// If it is true, then it will be added. If false, then it will not be added and removed from the list
        /// if already present.</param>
        public void RegisterVehicle(Vehicle v, bool add)
        {
            if (add)
            {
                activeVehicles.Add(v);
            }
            else
            {
                if (activeVehicles.Contains(v))
                {
                    activeVehicles.Remove(v);
                }
            }
        }
    }
}
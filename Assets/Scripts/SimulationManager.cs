using System.Collections.Generic;
using Entities;
using Locals;
using UnityEngine;
using UnityTimer;
using Random = UnityEngine.Random;

namespace Manager
{
    /// <summary>
    /// A singleton all-purpose manager for the simulation.
    /// </summary>
    public class SimulationManager : MonoBehaviour
    {
        public static SimulationManager instance { get; private set; }
        
        public int numberOfVehicles;
        public int numberOfPedestrians;
        public float vehicleDestinationMaxTime;
        public float pedestrianDestinationMaxTime;
        public float accidentMaxTime;
        public float uncontrolledMaxTime;

        public List<Destination> allDestinations;
        
        public GameObject pedestrianPrefab;
        public GameObject vehiclePrefab;

        private Timer _accidentTimer;
        private Timer _uncontrolledTimer;
        
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this);
                return;
            }

            instance = this;
        }

        /// <summary>
        /// Spawns the number of pedestrians and vehicles determined on the
        /// simulation's parameters.
        /// </summary>
        private void Start()
        {
            for (int p = 0; p < numberOfPedestrians; p++)
            {
                Destination randomDestination = allDestinations[Random.Range(0, allDestinations.Count)];
                Pedestrian pedestrian = Instantiate(pedestrianPrefab, randomDestination.position, 
                    Quaternion.identity).GetComponent<Pedestrian>();
                pedestrian.Init(randomDestination);
            }
            
            for (int v = 0; v < numberOfVehicles; v++)
            {
                Destination randomDestination = allDestinations[Random.Range(0, allDestinations.Count)];
                Vehicle vehicle = Instantiate(vehiclePrefab, randomDestination.position, 
                    Quaternion.identity).GetComponent<Vehicle>();
                vehicle.Init(randomDestination);
            }
        }
    }
}
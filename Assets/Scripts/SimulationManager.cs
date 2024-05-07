using System.Collections.Generic;
using Entities;
using Locals;
using UnityEngine;
using System.Linq;
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
        public Material accidentColor;

        private List<Agent> agents;
        
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
            agents = new List<Agent>();

            for (int p = 0; p < numberOfPedestrians; p++)
            {
                Destination randomDestination = allDestinations[Random.Range(0, allDestinations.Count)];
                Pedestrian pedestrian = Instantiate(pedestrianPrefab, randomDestination.position, 
                    Quaternion.identity).GetComponent<Pedestrian>();
                agents.Add(pedestrian);
                pedestrian.Init(randomDestination);
            }
            
            for (int v = 0; v < numberOfVehicles; v++)
            {
                Destination randomDestination = allDestinations[Random.Range(0, allDestinations.Count)];
                Vehicle vehicle = Instantiate(vehiclePrefab, randomDestination.position, 
                    Quaternion.identity).GetComponent<Vehicle>();
                agents.Add(vehicle);
                vehicle.Init(randomDestination);
            }

            foreach (TrafficLight tl in gameObject.
                GetComponentsInChildren<TrafficLight>())
            {
                agents.Add(tl);
            }
        }

        /// <summary>
        /// Chooses a random agent in the scene to become Uncontrolled.
        /// </summary>
        public void UncontrolRandomAgent()
        {
            List<Agent> stableAgentsList = agents.Where
                (a => !a.isUncontrolled).ToList();

            if(stableAgentsList.Count > 0)
            {
                stableAgentsList[Random.Range(0, stableAgentsList.Count)].
                    BecomeUncontrolled();
            }
        }
    }
}
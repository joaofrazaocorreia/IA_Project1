using System.Collections.Generic;
using Locals;
using UnityEngine;
using UnityTimer;

namespace Manager
{
    public class SimulationManager : MonoBehaviour
    {
        public int numberOfVehicles;
        public int numberOfPedestrians;
        public float vehicleDestinationMaxTime;
        public float pedestrianDestinationMaxTime;
        public float accidentMaxTime;
        public float uncontrolledMaxTime;

        public List<Destination> allDestinations;

        private Timer _accidentTimer;
        private Timer _uncontrolledTimer;
    }
}
using Entities;
using UnityEngine;

namespace Locals
{
    /// <summary>
    /// Defines a crossroad, for pedestrian checking purposes.
    /// </summary>
    public class Crossroad : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Pedestrian>(out var p))
            {
                p.isOnCrossroad = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<Pedestrian>(out var p))
            {
                p.isOnCrossroad = false;
            }
        }
    }
}
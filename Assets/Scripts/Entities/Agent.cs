using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using Unity.VisualScripting;

namespace Entities
{
    /// <summary>
    /// Base class. Defines an Agent, and contains its uncontrolled state.
    /// </summary>
    public abstract class Agent : MonoBehaviour
    {
        protected UnityTimer.Timer uncontrolledTimer;
        
        public bool isUncontrolled;

        /// <summary>
        /// Causes this agent to become uncontrolled for a random amount of time
        /// up to the defined value in the Simulation Manager.
        /// </summary>
        public void BecomeUncontrolled()
        {
            float prevSpeed = 0;
            isUncontrolled = true;

            if (this is MobileAgent)
                prevSpeed = (this as MobileAgent).agent.speed;
            
            uncontrolledTimer = UnityTimer.Timer.Register(Random.Range(0f,
                SimulationManager.instance.uncontrolledMaxTime),
                    () =>
                    {
                        isUncontrolled = false;

                        if (this is MobileAgent)
                            (this as MobileAgent).agent.speed = prevSpeed;
                    });

            StartCoroutine(BlinkAgent(this));
        }

        private IEnumerator BlinkAgent(Agent a)
        {
            Renderer renderer;
            if(a.transform.GetComponent<Renderer>() != null)
                renderer = a.transform.GetComponent<Renderer>();
            
            else renderer = a.transform.GetComponentInChildren<Renderer>();
            
            List<Material> materials = new List<Material>();
            renderer.GetMaterials(materials);

            Material originalColor = materials[0];

            bool blink = false;

            while(!uncontrolledTimer.isDone)
            {
                blink = !blink;

                if(blink)
                {
                    renderer.material = SimulationManager.instance.
                        accidentColor;
                }

                else
                {
                    renderer.material = originalColor;
                }

                yield return new WaitForSeconds(0.5f);
            }

            if(uncontrolledTimer.isDone)
                renderer.material = originalColor;
        }
    }
}

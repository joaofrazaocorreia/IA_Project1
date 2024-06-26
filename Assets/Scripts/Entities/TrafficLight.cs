﻿using System.Collections;
using System.Collections.Generic;
using LibGameAI.FSMs;
using UnityEngine;
using UnityEngine.UI;

namespace Entities
{
    /// <summary>
    /// Defines the behaviour for the traffic lights on the simulation.
    /// </summary>
    public class TrafficLight: StaticAgent
    {
        public Image signal;
        public float maxDistance;

        private Color _evaluateColor;
        
        private float _stateEnterTime;
        public float timeBeforeStateChange = 20f; // Seconds between states

        private void Start()
        {
            _stateEnterTime = Time.time;
            
            //Init states

            State greenState = new State
            (
                "Green", 
                () => OnEnterState(TrafficColors.Green), 
                () => OnUpdateState(), 
                () => OnExitState()
                );
            
            State orangeState = new State
            (
                "Orange", 
                () => OnEnterState(TrafficColors.Orange), 
                () => OnUpdateState(), 
                () => OnExitState()
            );
            
            State redState = new State
            (
                "Red", 
                () => OnEnterState(TrafficColors.Red), 
                () => OnUpdateState(), 
                () => OnExitState()
            );
            
            //Init transitions
            
            greenState.AddTransition(new Transition(ShouldChangeState, () =>
                _stateEnterTime = Time.time, orangeState));
            orangeState.AddTransition(new Transition(ShouldChangeState, () =>
                _stateEnterTime = Time.time, redState));
            redState.AddTransition(new Transition(ShouldChangeState, () =>
                _stateEnterTime = Time.time, greenState));

            StateMachine = new StateMachine(greenState);
        }
        
        /// <summary>
        /// Checks if the time since entering the current state is greater than
        /// or equal to the duration of that state. Shorter time is applied if
        /// it's yellow.
        /// </summary>
        private bool ShouldChangeState()
        {
            if(isUncontrolled)
                return Time.time - _stateEnterTime >= 0.15;

            else
            {
                if (_evaluateColor == Color.yellow)
                    return Time.time - _stateEnterTime >= 
                        timeBeforeStateChange * 0.15f;

                else
                    return Time.time - _stateEnterTime >= timeBeforeStateChange;
            }
        }

        /// <summary>
        /// Called when the state machine exits a state.
        /// </summary>
        private void OnExitState()
        {
            
        }

        /// <summary>
        /// Called every frame. It checks the current color of the traffic light
        /// and then changes the state of all vehicles and pedestrians in range
        /// accordingly.
        /// </summary>
        private void OnUpdateState()
        {
            Ray ray = new Ray();
            RaycastHit[] colliders = Physics.RaycastAll(ray, maxDistance);
            
            List<Vehicle> vehiclesAffected = new();
            List<Pedestrian> pedestriansAffected = new();
            
            foreach (var v in colliders)
            {
                if (v.collider.TryGetComponent(out Vehicle c))
                {
                    vehiclesAffected.Add(c);
                }

                if (v.collider.TryGetComponent(out Pedestrian p))
                {
                    pedestriansAffected.Add(p);
                }
            }

            if (_evaluateColor == Color.green)
            {
                foreach (Vehicle car in vehiclesAffected) 
                {
                    car.Resume();
                }

                foreach (Pedestrian pedestrian in pedestriansAffected)
                {
                    pedestrian.Wait();
                }
            }
            else if (_evaluateColor == Color.yellow)
            {
                foreach (Vehicle car in vehiclesAffected) 
                {
                    car.SlowDown();
                }

                foreach (Pedestrian pedestrian in pedestriansAffected)
                {
                    pedestrian.Wait();
                }
            }
            else if (_evaluateColor == Color.red)
            {
                foreach (Vehicle car in vehiclesAffected) 
                {
                    car.Wait();
                }

                foreach (Pedestrian pedestrian in pedestriansAffected)
                {
                    pedestrian.Resume();
                }
            }
        }

        /// <summary>
        /// Called when the state machine enters a new state.
        /// The function sets the _evaluateColor variable to be equal to one of
        /// three colors depending on what value was passed into it.
        /// </summary>
        private void OnEnterState(TrafficColors trafficColor)
        {
            if(isUncontrolled)
            {
                signal.color = Random.Range(0, 3) switch
                {
                    0 => Color.yellow,
                    1 => Color.red,
                    2 => Color.green,
                    _ => Color.green
                };
            }

            else
            {    
                _evaluateColor = trafficColor switch
                {
                    TrafficColors.Green => Color.green,
                    TrafficColors.Orange => Color.yellow,
                    TrafficColors.Red => Color.red,
                    _ => _evaluateColor
                };

                signal.color = _evaluateColor;
            }

            
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dylan
{


    public class UIBehaviour : MonoBehaviour
    {

        private GameObject canvas;
        private Slider Dispersion;
        private Slider Cohesion;
        private Slider Alignment;
        private Slider offset;

        private Button addFlock;
        private Button removeFlock;

        private FlockBehaviour flock;



        
        void Start()
        {
            canvas = GameObject.Find("USCanvas");
            Dispersion = GameObject.Find("DispersionSlider").GetComponent<Slider>();
            Cohesion = GameObject.Find("CohesionSlider").GetComponent<Slider>();
            Alignment = GameObject.Find("AlignmentSlider").GetComponent<Slider>();

            addFlock = GameObject.Find("AddFlockButton").GetComponent<Button>();
            removeFlock = GameObject.Find("RemoveFlockButton").GetComponent<Button>();

            flock = GameObject.FindObjectOfType<FlockBehaviour>();


            Dispersion.maxValue = 100.0f;
            Cohesion.maxValue = 100.0f;
            Alignment.maxValue = 100.0f;
            offset.maxValue = 100.0f;



            Dispersion.minValue = 0.0f;
            Cohesion.minValue = 0.0f;
            Alignment.minValue = 0.0f;
            offset.minValue = 1.0f;

            Dispersion.value = 1.0f;
            Cohesion.value = 1.0f;
            Alignment.value = 1.0f;
            offset.value = 1.0f;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            flock.DispersionCo = Dispersion.value;
            flock.CohesionCo = Cohesion.value;
            flock.AlignmentCo = Alignment.value;
            flock._distanceapart = offset.value;
        }
    }
}
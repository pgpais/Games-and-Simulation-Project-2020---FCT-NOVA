using System;
using System.Collections;
using UnityEngine;

namespace Activatables
{
    /// <summary>
    /// Object that performs a translation movement when activated
    /// </summary>
    public class SlidingWall : Activatable
    {
        public float travelTime;
        [SerializeField]
        private Vector3 distanceToTravel;
        [SerializeField]
        private float gizmoSphereRadius = 0.5f;
        [SerializeField]
        public AnimationCurve travelCurve;

        private float travelCurrentTime = 0f;
        private Vector3 destination;
        private Vector3 initialPosition;
        private Transform trans;
        [SerializeField]
        private GameObject objectToShow;

        protected override void Start()
        {
            base.Start();
            trans = GetComponent<Transform>();
            initialPosition = trans.position;
            destination = initialPosition + distanceToTravel;
        }
        
        public override void Activate()
        {
            StartCoroutine(Slide());
        }

        private IEnumerator Slide()
        {
            
            while (travelCurrentTime <= travelTime)
            {
                transform.position = Vector3.Lerp(initialPosition, destination, travelCurve.Evaluate(travelCurrentTime/travelTime));
                //Debug.Log("Time = " + travelCurrentTime/travelTime + " | Evaluate result = " + travelCurve.Evaluate(travelCurrentTime/travelTime));
                travelCurrentTime += Time.fixedDeltaTime;
                yield return new WaitForSeconds(Time.fixedDeltaTime);
            }
            transform.position = Vector3.Lerp(initialPosition, destination, 1);
        }

        private void OnDrawGizmosSelected()
        {
            if (objectToShow == null)
            {
                objectToShow = gameObject;
            }
            Gizmos.color = Color.green;
            var position = transform.position;
            Gizmos.DrawLine(position, position + distanceToTravel);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(position + distanceToTravel, gizmoSphereRadius);
            Gizmos.DrawWireMesh(objectToShow.GetComponent<MeshFilter>().sharedMesh, position + distanceToTravel, objectToShow.GetComponent<Transform>().rotation, objectToShow.GetComponent<Transform>().lossyScale);
        }
    }
}

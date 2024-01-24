using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.lineact.lit.FSM
{

    public class PatrolPointsConfig : MonoBehaviour
    {
        [SerializeField] private List<Vector3> points;
        
        public Vector3 TargetPoint { get; private set; }
        //private Vector3 targetPoint;
        private int index;

        private void Start()
        {
            index = 0;
            TargetPoint = points[index];
        }
        public Vector3 GetTargetPointDirection()
        {
            return (TargetPoint - transform.position).normalized;
        }

        public bool HasReachedPoint()
        {
            return (Vector3.Distance(transform.position, TargetPoint) <= 0.10f) ? true : false;
        }

        public void SetNextTargetPoint()
        {
            if (points.Count <= 0) return;
            index = (index+1)% points.Count;
            TargetPoint = points[index];
        }        
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.lineact.lit.FSM
{
    /// <summary>
    /// This class
    /// </summary>
    public class WaitActivityConfig : MonoBehaviour
    {
        public float Timer;

        // Start is called before the first frame update
        void Awake()
        {
            Timer = 0;
        }

        public void Reset()
        {
            Timer = 0;
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pendopo.Character
{
    public class Order : MonoBehaviour
    {
        public bool IsOrdering { get; private set; }

        [Header("Reference")]
        private CustomerMovement customerMovement;

        private void Awake()
        {
            customerMovement = GetComponent<CustomerMovement>();
        }

        public void Ordering()
        {
            IsOrdering = true;
        }

        public bool GiveOrder()
        {
            IsOrdering = false;

            return true;
        }
    }
}
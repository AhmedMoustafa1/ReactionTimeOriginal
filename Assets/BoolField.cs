using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kandooz{ 
[CreateAssetMenu(menuName = "Kandooz/bool Field")]



    public class BoolField : ScriptableObject
    {
        [SerializeField]
        private bool value;


        public virtual bool Value
        {
            get
            {
                return value;
            }

            set
            {

                this.value = value;

            }
        }

    }
}

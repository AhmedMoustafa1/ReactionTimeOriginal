
using UnityEngine;

    [CreateAssetMenu]

    public class IntField : ScriptableObject
    {
        [SerializeField]
        private int value;
      

        public virtual int Value
        {
            get
            {
                return value;
            }

            set
            {
                if (this.value != value)
                {
                    this.value = value;
                  
                }
            }
        }
       
       
    }

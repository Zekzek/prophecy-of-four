using System;
using UnityEngine;

namespace UtiliZek
{
    [Serializable]
    public class Resource
    {
        public string Type { get; set; }

        public Action<float> OnFull;
        public Action<float> OnEmpty;
        public Action<float, float> OnChange;
        public Action<float, float> OnMore;
        public Action<float, float> OnLess;

        [SerializeField]
        private float current;
        [SerializeField]
        private float max = 100;
        private float min = 0;

        public float Current { get { return current; } set { Change(value - current); } }
        public float Max { get { return max; } set { max = value; Init(); } }
        public float Min { get { return min; } set { min = value; Init(); } }
        public bool IsFull { get { return Utilizek.CloseEnough(current, max); } }
        public bool IsEmpty { get { return Utilizek.CloseEnough(current, min); } }

        public void Init()
        {
            current = Mathf.Clamp(current, min, max);
        }

        public void Change(float amount)
        {
            float updatedAmount = Mathf.Clamp(current + amount, min, max);

            if (Utilizek.CloseEnough(updatedAmount, amount))
            {
                // Nothing changed
                return;
            }

            current = updatedAmount;
            float percent = OnChange != null || OnMore != null || OnLess != null ? (current - min) / (max - min) : 0;

            if (OnChange != null)
                OnChange(current, percent);

            if (amount > 0)
            {
                if (OnMore != null)
                    OnMore(current, percent);
                if (Utilizek.CloseEnough(current, max))
                {
                    current = max;
                    if (OnFull != null)
                        OnFull(current);
                }
            }
            else
            {
                if (OnLess != null)
                    OnLess(current, percent);
                if (Utilizek.CloseEnough(current, min))
                {
                    current = min;
                    if (OnEmpty != null)
                        OnEmpty(current);
                }
            }
        }
    }
}
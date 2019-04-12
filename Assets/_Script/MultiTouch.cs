using UnityEngine;

namespace _Script
{
    /// <inheritdoc />
    /// <summary>
    /// Allow multitouch .
    /// </summary>
    public class MultiTouch : MonoBehaviour
    {
        /// <summary>
        /// Allow multi touch .
        /// </summary>
        [Header("Allow multi touch.")]
        public bool allowMultiTouch = false;
        /// <summary>
        /// Awake the instance ./
        /// </summary>
        private void Awake()
        {
            //Multi touch.
            Input.multiTouchEnabled = allowMultiTouch;
        }
    }
}

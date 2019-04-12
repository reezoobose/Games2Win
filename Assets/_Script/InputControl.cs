using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Script
{
    /// <inheritdoc cref="InputControl" />
    /// <summary>
    /// Input controller .
    /// </summary>
    public class InputControl : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        //============================Event Handler=================================//
        public void OnDrag(PointerEventData eventData)
        {
            Drag(eventData);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Drag(eventData);
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            Drag(eventData);
        }
        //============================Event Handler=================================//
        /// <summary>
        /// Drag .
        /// </summary>
        /// <param name="eventData"></param>
        private void Drag(PointerEventData eventData)
        {
            Vector3 dragVectorDirection = (eventData.position - eventData.pressPosition).normalized;
            var direction = GetDragDirection(dragVectorDirection);
            CameraOrbit.cameraOrbitReference.Rotate(direction);
        }
        /// <summary>
        /// Get touch direction.
        /// </summary>
        /// <param name="dragVector"></param>
        /// <returns></returns>
        private TouchDirection GetDragDirection(Vector3 dragVector)
        {
            var positiveX = Mathf.Abs(dragVector.x);
            var positiveY = Mathf.Abs(dragVector.y);
            if (positiveX > positiveY)
            {
                return (dragVector.x > 0) ? TouchDirection.Right : TouchDirection.Left;
            }
            else
            {
                return (dragVector.y > 0) ? TouchDirection.Up : TouchDirection.Down;
            }
        }

    }
}
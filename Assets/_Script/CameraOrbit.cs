﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Script
{
    /// <summary>
    /// Touch direction.
    /// </summary>
    public enum TouchDirection
    {
        Left,
        Right,
        Up,
        Down
    }
    /// <inheritdoc />
    /// <summary>
    /// Camera orbit.
    /// </summary>
    public class CameraOrbit : MonoBehaviour
    {
        /// <summary>
        /// Target .
        /// </summary>
        [Header("Target Transform.")]public Transform target;
        /// <summary>
        /// X speed.
        /// </summary>
        [Header("Speed Of move.")]public float xSpeed = 0.1f;
        /// <summary>
        /// Reset camera .
        /// </summary>
        [Header("Reset Camera ")]public Button resetCamera;
        /// <summary>
        /// Auto rotate .
        /// </summary>
        [Header("Autorotate in Update")]public bool autoRotate;
        /// <summary>
        /// Static reference.
        /// </summary>
        public static CameraOrbit cameraOrbitReference;
        /// <summary>
        /// Camera default
        /// </summary>
        private Vector3 _defaultPosition;
        private Quaternion _defaultRotation;

        /// <summary>
        /// Awake the instance.
        /// </summary>
        private void Awake()
        {
            //single tone.
            if (cameraOrbitReference == null)
                cameraOrbitReference = this;
            //set default transform.
            _defaultPosition = transform.localPosition;
            _defaultRotation = transform.localRotation;
            //null check.
            if(resetCamera == null) return;
            //Check reset .
            if (resetCamera.gameObject.activeInHierarchy) { resetCamera.gameObject.SetActive(false); }
            //Reset camera position .
            resetCamera.onClick.AddListener(() =>
            {
                CameraOrbit.cameraOrbitReference.PositionReset();
            });
        }

        /// <summary>
        /// Update.
        /// </summary>
        private void Update()
        {
            if(autoRotate)
            transform.RotateAround(target.position, Vector3.up, xSpeed);
        }

        /// <summary>
        /// Reset all .
        /// </summary>
        /// <returns></returns>
        public void PositionReset()
        {
            transform.localPosition = _defaultPosition;
            transform.localRotation = _defaultRotation;
            if (resetCamera.gameObject.activeInHierarchy) { resetCamera.gameObject.SetActive(false); }
        }

        /// <summary>
        /// Rotate Around .
        /// </summary>
        /// <param name="direction"></param>
        public void RotateAround(TouchDirection direction)
        {
            switch (direction)
            {
                case TouchDirection.Left:
                    transform.RotateAround(target.position,Vector3.up,xSpeed);
                    if (!resetCamera.gameObject.activeInHierarchy)
                    {
                        resetCamera.gameObject.SetActive(true);
                    }
                    break;
                case TouchDirection.Right:
                    transform.RotateAround(target.position, Vector3.up, -xSpeed);
                    if (!resetCamera.gameObject.activeInHierarchy)
                    {
                        resetCamera.gameObject.SetActive(true);
                    }
                    break;
                case TouchDirection.Up:
                    break;
                case TouchDirection.Down:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("direction", direction, null);
            }
        }

        /// <summary>
        /// Rotate .
        /// </summary>
        /// <param name="direction"></param>
        public void Rotate(TouchDirection direction)
        {
            switch (direction)
            {
                case TouchDirection.Left:
                    transform.Rotate(0f, xSpeed, 0f);
                    if (!resetCamera.gameObject.activeInHierarchy)
                    {
                        resetCamera.gameObject.SetActive(true);
                    }

                    break;
                case TouchDirection.Right:
                    transform.Rotate(0f, -xSpeed, 0f);
                    if (!resetCamera.gameObject.activeInHierarchy)
                    {
                        resetCamera.gameObject.SetActive(true);
                    }

                    break;
                case TouchDirection.Up:
                    break;
                case TouchDirection.Down:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("direction", direction, null);
            }
        }


    }
}
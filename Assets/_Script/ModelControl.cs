using System;
using UnityEngine;

namespace _Script
{
    /// <summary>
    /// Model View status.
    /// </summary>
    public enum ModelViewStatus
    {
        /// <summary>
        /// Single model one at a time .
        /// </summary>
        SingleView,
        /// <summary>
        /// Side by model view .
        /// </summary>
        SideBySideView
    }
    /// <summary>
    /// Model Identification .
    /// </summary>
    public enum ModelIdentification
    {
        /// <summary>
        /// Male 
        /// </summary>
        Male,
        /// <summary>
        /// Female .
        /// </summary>
        Female
    }
    /// <inheritdoc />
    /// <summary>
    /// Model controller.
    /// ModelControl
    /// </summary>
    public class ModelControl : MonoBehaviour
    {
        /// <summary>
        /// female model .
        /// </summary>
        [Header("female model")]public GameObject femaleModel;
        /// <summary>
        /// Male model object .
        /// </summary>
        [Header("male model")]public GameObject maleModel;
        /// <summary>
        /// Model Scale .
        /// </summary>
        [Header("Model full Scale.")]public Vector3 modelFullScale;
        /// <summary>
        /// Model side by side scale .
        /// </summary>
        [Header("Model side by side scale.")]public Vector3 modelSideBySideScale;
        /// <summary>
        /// Model full position .
        /// </summary>
        [Header("Model full position.")]public Vector3 modelFullPosition;
        /// <summary>
        /// Model side by side down position .
        /// </summary>
        [Header("Model side by side down position.")]public Vector3 modelSideBySideDownPosition;
        /// <summary>
        /// Model side by side up position .
        /// </summary>
        [Header("Model side by side up position.")]public Vector3 modelSideBySideUpPosition;
        /// <summary>
        /// Default model .
        /// </summary>
        [Header("Default Model")] public ModelIdentification defaultModel;
        /// <summary>
        /// Model view status.
        /// </summary>
        [Header("Model View")]public ModelViewStatus defaultView;
        /// <summary>
        /// Model view status.
        /// </summary>
        private ModelViewStatus _currentViewStatus;
        /// <summary>
        /// Static reference .
        /// </summary>
        public static ModelControl ModelControlReference;
        /// <summary>
        /// Current Model.
        /// </summary>
        private ModelIdentification _currentModel;

        /// <summary>
        /// Awake the instance .
        /// </summary>
        private void Awake()
        {
            //set single tone.
            if (ModelControlReference == null)
            {
                ModelControlReference = this;

            }
            //set up view.
            _currentViewStatus = defaultView;
            //Set up current model.
            _currentModel = defaultModel;
            //Activate view
            ActivateView(defaultView);
        }

        /// <summary>
        /// Switch model.
        /// </summary>
        public void SwitchedModel()
        {
            switch (_currentModel)
            {
                case ModelIdentification.Male:
                    ActivateModel(ModelIdentification.Female);
                    break;
                case ModelIdentification.Female:
                    ActivateModel(ModelIdentification.Male);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

        /// <summary>
        /// Activate model.
        /// </summary>
        /// <param name="identification"></param>
        private void ActivateModel(ModelIdentification identification)
        {
            //check status.
            if(_currentViewStatus != ModelViewStatus.SingleView) return;
            //Set up current model .
            _currentModel = identification;
            //Activate that model.
            switch (identification)
            {
                case ModelIdentification.Male:
                    if (!maleModel.activeInHierarchy) { maleModel.SetActive(true);}
                    if (femaleModel.activeInHierarchy) { femaleModel.SetActive(false);}
                    break;
                case ModelIdentification.Female:
                    if (maleModel.activeInHierarchy) { maleModel.SetActive(false); }
                    if (!femaleModel.activeInHierarchy) { femaleModel.SetActive(true); }
                    break;
                default:
                    throw new ArgumentOutOfRangeException("identification", identification, null);
            }
        }

        /// <summary>
        /// Activate view.
        /// </summary>
        public void ActivateView(ModelViewStatus status)
        {
            //Already playing .
            if (_currentViewStatus == status) return;
            //change status.
            _currentViewStatus = status;
            //Get the both model.
            if (maleModel.activeInHierarchy) { maleModel.SetActive(false); }
            if (femaleModel.activeInHierarchy) { femaleModel.SetActive(false); }
            //Switch
            switch (status)
            {
                case ModelViewStatus.SingleView:
                    //set up single view .
                    SingleViewModelSetup();
                    //Activate
                    ActivateModel(_currentModel);
                    break;
                case ModelViewStatus.SideBySideView:
                    //set up side view .
                    SideBySideViewSetup();
                    //Activate both model.
                    if (!maleModel.activeInHierarchy) { maleModel.SetActive(true); }
                    if (!femaleModel.activeInHierarchy) { femaleModel.SetActive(true); }
                    break;
                default:
                    throw new ArgumentOutOfRangeException("status", status, null);
            }
        }

        /// <summary>
        /// Set up for single view
        /// </summary>
        private void SingleViewModelSetup()
        {
            //set up scale.
            maleModel.transform.localScale = modelFullScale;
            femaleModel.transform.localScale = modelFullScale;
            //Set up the position
            maleModel.transform.localPosition = modelFullPosition;
            femaleModel.transform.localPosition = modelFullPosition;


        }

        /// <summary>
        /// Side by side view set up .
        /// </summary>
        private void SideBySideViewSetup()
        {
            //set up scale.
            maleModel.transform.localScale = modelSideBySideScale;
            femaleModel.transform.localScale = modelSideBySideScale;
            //Check last activate single model.
            switch (_currentModel)
            {
                case ModelIdentification.Male:

                    //Set up the position
                    maleModel.transform.localPosition = modelSideBySideDownPosition;
                    femaleModel.transform.localPosition = modelSideBySideUpPosition;
                    break;
                case ModelIdentification.Female:
                    //Set up the position
                    femaleModel.transform.localPosition = modelSideBySideDownPosition;
                    maleModel.transform.localPosition = modelSideBySideUpPosition;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Script
{
    /// <inheritdoc />
    /// <summary>
    /// Menu controller .
    /// </summary>
    public class MenuControl : MonoBehaviour
    {
        /// <summary>
        /// Single button .
        /// </summary>
        public Button singleButton;

        /// <summary>
        /// Side by side button .
        /// </summary>
        public Button sideBySideButton;

        /// <summary>
        /// Change model.
        /// </summary>
        public Button changeModel;

        /// <summary>
        /// Forward animation button.
        /// </summary>
        public Button forwardButton;

        /// <summary>
        /// Back animation button.
        /// </summary>
        public Button backWardButton;

        /// <summary>
        /// Sprite for selection .
        /// </summary>
        public Sprite selectedSprite;

        /// <summary>
        /// Sprite for not selection.
        /// </summary>
        public Sprite notSelectedSprite;

        /// <summary>
        /// Speed slider .
        /// </summary>
        public Slider speedSlider;

        /// <summary>
        /// Speed text .
        /// </summary>
        public Text speed;
        /// <summary>
        /// Help button.
        /// </summary>
        public Button helpButton;
        /// <summary>
        /// hELP POP UP.
        /// </summary>
        public GameObject helpPopUp;

        /// <summary>
        /// Help pop up close button.
        /// </summary>
        public Button helpPopupClose;


        /// <summary>
        /// Awake the instance .
        /// </summary>
        private void Awake()
        {
            //Change model .
            changeModel.onClick.AddListener(() =>
            {
                ModelControl.ModelControlReference.SwitchedModel();
                AnimationControl.animationControlReference.PlayAnimation(Direction.Current);
            });
            //Change to single view.
            singleButton.onClick.AddListener(() =>
            {
                //Activate change model.
                if (!changeModel.interactable)
                {
                    changeModel.interactable = true;

                }

                ModelControl.ModelControlReference.ActivateView(ModelViewStatus.SingleView);
                AnimationControl.animationControlReference.PlayAnimation(Direction.Current);
                singleButton.image.sprite = selectedSprite;
                sideBySideButton.image.sprite = notSelectedSprite;
            });
            //Change to multiple view.
            sideBySideButton.onClick.AddListener(() =>
            {
                //Deactivate change model.
                changeModel.interactable = false;
                ModelControl.ModelControlReference.ActivateView(ModelViewStatus.SideBySideView);
                AnimationControl.animationControlReference.PlayAnimation(Direction.Current);
                sideBySideButton.image.sprite = selectedSprite;
                singleButton.image.sprite = notSelectedSprite;
            });

            //Forward button.
            forwardButton.onClick.AddListener(() =>
            {
                AnimationControl.animationControlReference.PlayAnimation(Direction.Forward);
            });
            //backWard button.
            backWardButton.onClick.AddListener(() =>
            {
                AnimationControl.animationControlReference.PlayAnimation(Direction.Backward);
            });
           
            //Speed slider.
            speedSlider.onValueChanged.AddListener((changedSpeed) =>
            {
                //Set text
                speed.text = "Speed: "+changedSpeed.ToString("0.0") + " X";
                AnimationControl.animationControlReference.SetAnimationSpeed(changedSpeed);
            });
            
            //Help button.
            helpButton.onClick.AddListener(() =>
            {
                if (!helpPopUp.activeInHierarchy)
                {
                    helpPopUp.SetActive(true);
                }
            });
            //Help pop up close .
            helpPopupClose.onClick.AddListener(() =>
            {
                if (helpPopUp.activeInHierarchy)
                {
                    helpPopUp.SetActive(false);
                }
            });
           
        }

        /// <summary>
        /// Start the instance .
        /// </summary>
        private void Start()
        {
            //default value .
            speedSlider.normalizedValue = 0.5f;
        }
    }
}

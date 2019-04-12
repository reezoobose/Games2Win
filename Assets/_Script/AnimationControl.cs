using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _Script
{
    /// <summary>
    ///     Animations .
    /// </summary>
    public enum AnimationsType
    {
        Idle,
        Walk,
        Kick,
        Run,
        Dance
    }

    /// <summary>
    ///     Direction .
    /// </summary>
    public enum Direction
    {
        /// <summary>
        ///     Next animation will be played by forward .
        /// </summary>
        Forward,

        /// <summary>
        ///     Previous animation will be played by backward.
        /// </summary>
        Backward,
        /// <summary>
        /// Current Animation.
        /// </summary>
        Current
    }

    /// <inheritdoc />
    /// <summary>
    ///     Animation controller.
    /// </summary>
    public class AnimationControl : MonoBehaviour
    {
        /// <summary>
        ///     Male Animator.
        /// </summary>
        [Header("Male Animator")] public Animator maleAnimator;

        /// <summary>
        ///     Female animator .
        /// </summary>
        [Header("Female Animator")] public Animator femaleAnimator;

        /// <summary>
        /// Whic animation is playing.
        /// </summary>
        [Header("Description text")]public Text description;

        /// <summary>
        ///     Animation follow this order list index to play .
        /// </summary>
        [Header("Animation list order")] public List<AnimationsType> animationsTypeList;


        /// <summary>
        /// Lowest Animation Speed Possible.
        /// </summary>
        [Space(50f)] [Range(0,1)][Header("Animation Speed Lower Bound")]public float animationSpeedLowerBound;
        /// <summary>
        /// Highest Animation speed possible .
        /// </summary>
        [Range(0,3)][Header("Animation Speed higher Bound")] public float animationSpeedUpperBound;


        /// <summary>
        ///     Animation control reference .
        /// </summary>
        public static AnimationControl animationControlReference;

        /// <summary>
        ///     Previous animation state..
        /// </summary>
        private AnimationsType _previousAnimation;

        /// <summary>
        ///     Next animation state .
        /// </summary>
        private AnimationsType _currentAnimation;

        /// <summary>
        ///     Default state .
        /// </summary>
        private AnimationsType _defaultState;

        /// <summary>
        ///     Number of animation.
        /// </summary>
        private int _animationListCount;

        /// <summary>
        ///     current animation of the list index .
        /// </summary>
        private int _currentAnimationPlayedIndex;

        /// <summary>
        ///     Animation control bool cached.
        /// </summary>
        private static readonly int Idle = Animator.StringToHash("IsIdle");
        private static readonly int Walk = Animator.StringToHash("IsWalk");
        private static readonly int Kick = Animator.StringToHash("IsKick");
        private static readonly int Run = Animator.StringToHash("IsRun");
        private static readonly int Dance = Animator.StringToHash("IsDance");

        /// <summary>
        ///     Awake the instance .
        /// </summary>
        private void Awake()
        {
            //Single tone .
            if (animationControlReference == null) animationControlReference = this;
            //animation list count .
            _animationListCount = animationsTypeList.Count;
            //Set up idle animation.
            _previousAnimation = _currentAnimation = _defaultState = AnimationsType.Idle;
            //play default.
            PlayAnimation(_defaultState);

        }

        /// <summary>
        ///     play animation.
        /// </summary>
        /// <param name="direction"></param>
        public void PlayAnimation(Direction direction)
        {
            switch (direction)
            {
                case Direction.Forward:
                    PlayAnimation(GetNextAnimation());
                    break;
                case Direction.Backward:
                    PlayAnimation(GetPreviousAnimation());
                    break;
                case Direction.Current:
                    PlayAnimation(_currentAnimation);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("direction", direction, null);
            }
        }

        /// <summary>
        ///     Get next Animation.
        /// </summary>
        /// <returns></returns>
        private AnimationsType GetNextAnimation()
        {
            //get current .
            var current = _currentAnimationPlayedIndex + 1;
            //if check.
            if (current > _animationListCount - 1) current = 0;
            //Set up .
            _currentAnimationPlayedIndex = current;
            return animationsTypeList[current];
        }

        /// <summary>
        ///     get previous Animation .
        /// </summary>
        /// <returns></returns>
        private AnimationsType GetPreviousAnimation()
        {
            //get previous .
            var current = _currentAnimationPlayedIndex - 1;
            //if check.
            if (current < 0) current = _animationListCount - 1;
            //Set up
            _currentAnimationPlayedIndex = current;

            return animationsTypeList[current];
        }

        /// <summary>
        ///     Get Animation hash.
        /// </summary>
        /// <returns></returns>
        private int GetAnimationHash(AnimationsType type)
        {
            switch (type)
            {
                case AnimationsType.Idle:
                    return Idle;
                case AnimationsType.Walk:
                    return Walk;
                case AnimationsType.Kick:
                    return Kick;
                case AnimationsType.Run:
                    return Run;
                case AnimationsType.Dance:
                    return Dance;
                default:
                    throw new ArgumentOutOfRangeException("type", type, null);
            }
        }

        /// <summary>
        ///     Animation control Switch
        /// </summary>
        private void PlayAnimation(AnimationsType type)
        {
            //Set previous
            _previousAnimation = _currentAnimation;
            //Set text.
            description.text = "Playing Animation " + type;
            //set next.
            _currentAnimation = type;
            //Activate animation
            maleAnimator.SetBool(GetAnimationHash(_previousAnimation), false);
            femaleAnimator.SetBool(GetAnimationHash(_previousAnimation), false);
            maleAnimator.SetBool(GetAnimationHash(_currentAnimation), true);
            femaleAnimator.SetBool(GetAnimationHash(_currentAnimation), true);
        }
        /// <summary>
        /// Set animation speed.
        /// </summary>
        public void SetAnimationSpeed(float currentValue)
        {
            var speed = Mathf.Lerp(animationSpeedLowerBound,animationSpeedUpperBound,currentValue);
            maleAnimator.speed = femaleAnimator.speed = speed;
        }
    }
}
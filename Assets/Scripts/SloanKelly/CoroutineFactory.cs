using System;
using System.Collections;
using UnityEngine;

namespace SloanKelly.GameLib
{
    /// <summary>
    /// Factory class to build coroutines using actions.
    /// </summary>
    public static class CoroutineFactory
    {
        /// <summary>
        /// Create a coroutine.
        /// </summary>
        /// <param name="duration">Length of the action</param>
        /// <param name="action">Action to be performed each tick</param>
        /// <param name="postCondition">Post-condition action to be performed</param>
        /// <returns></returns>
        public static IEnumerator Create(float duration, Action<float> action, Action postCondition = null)
        {
            float time = 0f;

            while (time < 1f)
            {
                time += Time.deltaTime / duration;
                action(time);

                yield return null;
            }

            action(1f);

            if (postCondition != null)
            {
                postCondition();
            }
        }
    }
}
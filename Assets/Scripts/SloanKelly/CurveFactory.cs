using UnityEngine;

namespace SloanKelly.GameLib
{
    /// <summary>
    /// Factory class to build animation curves.
    /// </summary>
    public static class CurveFactory
    {
        /// <summary>
        /// Create a linear animation between two values.
        /// </summary>
        /// <param name="startValue">Start value</param>
        /// <param name="endValue">End value</param>
        /// <returns></returns>
        public static AnimationCurve Create(float startValue, float endValue)
        {
            var curve = new AnimationCurve();
            curve.AddKey(0f, startValue);
            curve.AddKey(1f, endValue);

            return curve;
        }
    }
}
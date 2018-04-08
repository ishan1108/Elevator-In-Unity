using UnityEngine;
using System.Collections;

namespace SloanKelly.GameLib
{
    /// <summary>
    /// Abstract class for scriptable behaviours.
    /// </summary>
    public abstract class ScriptableBehaviour : MonoBehaviour
    {
        /// <summary>
        /// Run the scriptable. This method MUST be implemented in child classes for this type.
        /// </summary>
        /// <returns>IEnumerator</returns>
        public abstract IEnumerator Run();
    }
}
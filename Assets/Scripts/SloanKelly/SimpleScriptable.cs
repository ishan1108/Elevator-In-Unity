using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SloanKelly.GameLib
{
    /// <summary>
    /// From the components attached to the object, this class will gather each
    /// component and run them in turn.
    /// </summary>
    public class SimpleScriptable : MonoBehaviour
    {
        IEnumerable<ScriptableBehaviour> scriptables;

        [Tooltip("The scriptables will execute when the object start")]
        public bool runOnStartup = false;

        /// <summary>
        /// Run the scriptables.
        /// </summary>
        /// <returns>IEnumerator</returns>
        public IEnumerator RunScriptables()
        {
            foreach (var scriptable in scriptables)
            {
                yield return scriptable.Run();
            }
        }

        /// <summary>
        /// Awaken the instance.
        /// </summary>
        void Awake()
        {
            scriptables = GetComponents<ScriptableBehaviour>();
        }

        IEnumerator Start()
        {
            if (!runOnStartup)
            {
                yield break;
            }
            else
            {
                yield return RunScriptables();
            }
        }
    }
}
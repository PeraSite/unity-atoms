using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// None generic base class of Lists. Inherits from `BaseAtom`.
    /// </summary>
    [EditorIcon("atom-icon-piglet")]
    public abstract class BaseAtomValueList : BaseAtom
    {
        /// <summary>
        /// Event for when the list is cleared.
        /// </summary>
        public AtomEventBase Cleared;
        public abstract IList IList { get; }

        /// <summary>
        /// Whether the list should start cleared
        /// </summary>
        [SerializeField]
        protected bool _startCleared;

        /// <summary>
        /// Clear the list.
        /// </summary>
        public void Clear()
        {
            IList.Clear();
            if (null != Cleared)
            {
                Cleared.Raise();
            }
        }

        private void OnEnable()
        {
            if (_startCleared)
            {
#if UNITY_EDITOR
                if (UnityEditor.EditorSettings.enterPlayModeOptionsEnabled)
                {
                    _instances.Add(this);
                }
#endif
                Clear();
            }
        }

#if UNITY_EDITOR
        /// <summary>
        /// Set of all AtomVariable instances in editor.
        /// </summary>
        private static HashSet<BaseAtomValueList> _instances = new();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetInstance()
        {
            foreach (var instance in _instances) {
                instance.Clear();
            }
        }
#endif

        public abstract void Add(object obj);
    }
}

using JetBrains.Annotations;
using UnityEngine;

namespace UMF.Core.Implementation
{
    public class AnimatorUtil : MonoBehaviour
    {
        [UsedImplicitly]
        public void SetInteger(int value)
        {
            animator.SetInteger(AniamtorIntFieldName, value);
        }

        [UsedImplicitly]
        public void SetTrigger(string value)
        {
            animator.SetTrigger(value);
        }
#pragma warning disable 0649
        [SerializeField] private Animator animator;

        [SerializeField] private string AniamtorIntFieldName;
#pragma warning restore 0649
    }
}
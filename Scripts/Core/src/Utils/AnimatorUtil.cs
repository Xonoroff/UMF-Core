using JetBrains.Annotations;
using UnityEngine;

namespace Core.src.Utils
{
    public class AnimatorUtil : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        [SerializeField]
        private string AniamtorIntFieldName;
        
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
    }
}
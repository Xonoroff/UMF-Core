using System;
using UnityEngine;

namespace UMF.Core.Infrastructure
{
    [Serializable]
    public class BaseEntity : IValidatable
    {
        [field: SerializeField] public string Id { get; set; }

        public virtual bool IsValid()
        {
            return !string.IsNullOrEmpty(Id);
        }
    }
}
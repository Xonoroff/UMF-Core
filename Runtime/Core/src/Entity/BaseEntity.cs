using System;
using Core.src.Infrastructure;
using UnityEngine;

namespace Core.src.Entity
{
    [Serializable]
    public class BaseEntity : IValidatable
    {
        [field: SerializeField]
        public string Id { get; set; }
        
        public virtual bool IsValid()
        {
            return !string.IsNullOrEmpty(Id);
        }
    }
}

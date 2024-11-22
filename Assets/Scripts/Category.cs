using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BodySystem
{
    public class Category : MonoBehaviour
    {
        public enum Type
        {
            Long,
            Short,
            Flat,
            Irregular
        }
        public Type boneType;
    }
}

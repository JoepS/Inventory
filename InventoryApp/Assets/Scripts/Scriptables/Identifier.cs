using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptables
{

    [CreateAssetMenu(fileName = "New Identifier", menuName = "Identifiers/New", order = 0)]
    public class Identifier : ScriptableObject
    {
        public new string name;
    }

}

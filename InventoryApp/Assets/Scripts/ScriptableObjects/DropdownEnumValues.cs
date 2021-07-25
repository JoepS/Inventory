using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enum values", menuName = "Helpers/New Enum Value List", order = 1)]
public class DropdownEnumValues : ScriptableObject
{
    public List<string> enumValues = new List<string>();
}

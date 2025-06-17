using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class CharacterEvents_Old
{
    // character damaged and damage value
    public static UnityEvent<GameObject, int> characterDamaged = new UnityEvent<GameObject, int>();
    // segun el tuto UnityACtion tambien vale y el lo usa, pero si salio con unityEvent

    // character healed and amount healed
    public static UnityEvent<GameObject, int> characterHealed = new UnityEvent<GameObject, int>();

}

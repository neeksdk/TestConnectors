using System;
using UnityEngine;
using UnityEngine.UI;

public class TestMenu : MonoBehaviour
{

    [SerializeField] private Toggle method1Toggle, method2Toggle;
    public enum UICommandType
    {
        Randomize,
        Method1,
        Method2,
        ClearConnections,
        TwoMethods,
        NoneMethods
    }

    public static Action<UICommandType> OnUIPressed = delegate { };

    public void ClearConnections() {
        OnUIPressed(UICommandType.ClearConnections);
    }
    
    public void RandomizePressed() {
        OnUIPressed(UICommandType.Randomize);
    }

    public void SelectMethod() {
        if (method1Toggle.isOn) {
            OnUIPressed(method2Toggle.isOn ? UICommandType.TwoMethods : UICommandType.Method1);
        } else {
            OnUIPressed(method2Toggle.isOn ? UICommandType.Method2 : UICommandType.NoneMethods);
        }
    }
}

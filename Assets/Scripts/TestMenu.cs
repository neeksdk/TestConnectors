using System;
using UnityEngine;

public class TestMenu : MonoBehaviour
{
    public enum UICommandType
    {
        Randomize,
        Method1,
        Method2
    }

    public static Action<UICommandType> OnUIPressed = delegate { };

    public void RandomizePressed() {
        OnUIPressed(UICommandType.Randomize);
    }

    public void SelectMethod1(bool isToggleOn) {
        if (isToggleOn) OnUIPressed(UICommandType.Method1);
    }

    public void SelectMethod2(bool isToggleOn) {
        if (isToggleOn) OnUIPressed(UICommandType.Method2);
    }
}

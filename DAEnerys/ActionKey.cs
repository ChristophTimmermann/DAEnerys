using System.Collections.Generic;
using System.Windows.Forms;

namespace DAEnerys
{
    class ActionKey
    {
        //---------------------------------- STATIC PART ---------------------------------//
        public static Dictionary<Action, ActionKey> ActionKeys = new Dictionary<Action, ActionKey>();
        private static Keys pressedKeys;
        private static Keys pressedModifiers;

        public static void Init()
        {
            //Initialize hotkeys with default values
            new ActionKey("Toggle orthographic", Action.TOGGLE_ORTHOGRAPHIC, Keys.NumPad5);

            new ActionKey("Front view", Action.VIEW_FRONT, Keys.NumPad1);
            new ActionKey("Back view", Action.VIEW_BACK, Keys.NumPad1, Keys.Control);

            new ActionKey("Left view", Action.VIEW_LEFT, Keys.NumPad3);
            new ActionKey("Right view", Action.VIEW_RIGHT, Keys.NumPad3, Keys.Control);

            new ActionKey("Top view", Action.VIEW_TOP, Keys.NumPad7);
            new ActionKey("Bottom view", Action.VIEW_BOTTOM, Keys.NumPad7, Keys.Control);
        }

        public static void KeyDown(KeyEventArgs e)
        {
            pressedKeys = e.KeyCode;
            pressedModifiers = e.Modifiers;
        }

        public static void KeyUp(KeyEventArgs e)
        {
            pressedKeys = e.KeyCode;
            pressedModifiers = e.Modifiers;
        }

        public static bool IsDown(Action action)
        {
            if (ActionKeys[action].IsDown())
                return true;

            return false;
        }

        //---------------------------------- INSTANCE PART ---------------------------------//
        public string Name;
        public Action Action;
        public Keys Key;
        public Keys Modifiers;

        //GUI
        public Label Label;
        public CheckBox CheckCTRL;
        public CheckBox CheckALT;
        public Button Button;

        public ActionKey(string name, Action action, Keys key, Keys modifiers = Keys.None)
        {
            Name = name;
            Action = action;
            Key = key;
            Modifiers = modifiers;

            ActionKeys.Add(action, this);
        }

        public bool IsDown()
        {
            if (Key == pressedKeys)
                if (Modifiers == pressedModifiers)
                    return true;

            return false;
        }
    }

    enum Action
    {
        TOGGLE_ORTHOGRAPHIC = 1,
        VIEW_FRONT = 2,
        VIEW_LEFT = 3,
        VIEW_TOP = 4,
        VIEW_BACK = 5,
        VIEW_RIGHT = 6,
        VIEW_BOTTOM = 7,
    }
}

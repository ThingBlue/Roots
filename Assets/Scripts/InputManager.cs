using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Roots
{
    public class InputManager : MonoBehaviour
    {
        // Singleton
        //public static InputManager instance;

        public static Dictionary<string, List<KeyCode>> keyMap;

        private void Start()
        {
            // Initialize key map
            keyMap = new Dictionary<string, List<KeyCode>>();

            addKeyToMap("UP", KeyCode.W);
            addKeyToMap("LEFt", KeyCode.A);
            addKeyToMap("RIGHT", KeyCode.D);
            addKeyToMap("DOWN", KeyCode.S);
        }

        #region Getters

        public static bool getKey(string key)
        {
            if (!keyMap.ContainsKey(key)) return false;

            foreach (KeyCode keyCode in keyMap[key])
            {
                if (Input.GetKey(keyCode)) return true;
            }
            return false;
        }

        public static bool getKeyDown(string key)
        {
            if (!keyMap.ContainsKey(key)) return false;

            foreach (KeyCode keyCode in keyMap[key])
            {
                if (Input.GetKeyDown(keyCode)) return true;
            }
            return false;
        }

        public static bool getKeyUp(string key)
        {
            if (!keyMap.ContainsKey(key)) return false;

            foreach (KeyCode keyCode in keyMap[key])
            {
                if (Input.GetKeyUp(keyCode)) return true;
            }
            return false;
        }

        // Getter for key map
        public static List<KeyCode> getKeysInMap(string key)
        {
            if (!keyMap.ContainsKey(key)) return new List<KeyCode>();

            return keyMap[key];
        }

        // Getter for mouse position in world space
        public static Vector3 getMousePositionInWorld()
        {
            // Create plane at zero
            Plane plane = new Plane(Vector3.back, Vector3.zero);

            // Cast ray onto plane
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Return hit location
            if (plane.Raycast(ray, out float enter)) return ray.GetPoint(enter);
            return Vector3.zero; // Should never execute
        }

        #endregion

        #region Setters

        public static void clearKeyListInMap(string key)
        {
            if (!keyMap.ContainsKey(key)) return;

            keyMap[key].Clear();
        }

        public static void addKeyToMap(string key, KeyCode value)
        {
            // Create new keycode mapping if it doesn't exist
            if (!keyMap.ContainsKey(key)) keyMap.Add(key, new List<KeyCode>());

            // Check if current value already exists in the list
            if (keyMap[key].Contains(value)) return;

            keyMap[key].Add(value);
        }

        public static void setKeyListInMap(string key, List<KeyCode> value)
        {
            // Create new keycode mapping if it doesn't exist
            if (!keyMap.ContainsKey(key)) keyMap.Add(key, value);
            else keyMap[key] = value;
        }

        #endregion
    }
}

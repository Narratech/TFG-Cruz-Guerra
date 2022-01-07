using UnityEngine;

namespace tfg
{
    [System.Serializable]
    public enum Scene
    {
        Menu, Options, Encyclopedia, Credits, SelectLevel, Game
    }

    public class ButtonsScene : MonoBehaviour
    {
        public Scene scene;
    }
}
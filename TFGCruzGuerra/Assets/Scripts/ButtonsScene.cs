using UnityEngine;

namespace tfg
{
    [System.Serializable]
    public enum Scene
    {
        Menu, Options, Encyclopedia, Credits, SelectLevel, Game, Results,
    }

    public class ButtonsScene : MonoBehaviour
    {
        public Scene scene;
    }
}
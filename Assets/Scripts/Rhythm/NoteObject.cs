using Building;
using UnityEngine;

//Following: https://youtu.be/cZzf1FQQFA0
namespace Rhythm
{
    public class NoteObject : MonoBehaviour
    {
        public bool canBePressed;
        private Floor _floor;

        private void Awake()
        {
            _floor = GetComponent<Floor>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Activator"))
            {
                if (_floor.isWeak)
                {
                    canBePressed = true;
                    _floor.ChangeColor(Color.green);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Activator"))
            {
                if (_floor.isWeak)
                {
                    canBePressed = false;
                    _floor.ChangeColor(Color.yellow);
                }
            }
        }
    }
}
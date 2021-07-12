using Core;
using UnityEngine;

//Following: https://youtu.be/cZzf1FQQFA0
namespace Rhythm
{
    public class BeatScroller : Singleton<BeatScroller>
    {
        public float beatTempo;
        public bool hasStarted;

        protected override void Awake()
        {
            base.Awake();
            beatTempo /= 60f;
        }
    }
}

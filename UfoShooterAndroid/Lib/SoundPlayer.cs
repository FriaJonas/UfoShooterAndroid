using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UfoShooterAndroid.Lib
{
    internal class SoundPlayer
    {
        public static void Play(SoundEffect sound)
        {
            SoundEffectInstance se = sound.CreateInstance();
            se.IsLooped = false;
            se.Pitch = 0;
            se.Volume = 1;
            se.Play();
        }
    }
}

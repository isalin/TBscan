using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace TBscan
{
    class Sound
    {
        string prefix;
        public Sound()
        {

            
            //SoundPlayer sound = new SoundPlayer(path);
            //sound.Play();
            MessageBox.Show(getPath());
            prefix = getPath();
        }

        private string getPath()
        {

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var assemblyPath = assembly.GetFiles()[0].Name;
            var assemblyDir = System.IO.Path.GetDirectoryName(assemblyPath);


            return System.IO.Directory.GetCurrentDirectory();
        }

        internal void play(string filename)
        {
            SoundPlayer snd = new SoundPlayer(prefix + @"\act_scan\audio\" + filename + ".wav");
            snd.Play();
        }
    }
}

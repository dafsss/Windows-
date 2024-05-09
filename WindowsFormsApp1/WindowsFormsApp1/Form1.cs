
using NAudio;
using NAudio.Wave;
using NAudio.Vorbis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string[] files;

        List<string> localmusiclist=new List<string> { };
        public Form1()
        {
            InitializeComponent();
        }

        private void musicplay(string filename) 
        {
            string extension=Path.GetExtension(filename);
            if (extension == "ogg") 
            {
                if (File.Exists(filename))
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "打开音频|*.ogg";

                    string oggFilePath = "";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        oggFilePath = openFileDialog.FileName;
                    }

                    using (var vorbisReader = new VorbisWaveReader(oggFilePath))
                    {
                        using (var outputDevice = new WaveOutEvent())
                        {
                            outputDevice.Init(vorbisReader);
                            outputDevice.Play();

                            while (outputDevice.PlaybackState == PlaybackState.Playing)
                            {
                                System.Threading.Thread.Sleep(1000);
                            }
                        }
                    }

                    using (var vorbisStream = new VorbisWaveReader(oggFilePath))
                    {
                        // 创建WaveOutEvent实例来播放音频  
                        using (var outputDevice = new WaveOutEvent())
                        {
                            outputDevice.Init(vorbisStream);
                            outputDevice.Play();

                            // 等待播放完成，或者你可以添加其他逻辑来处理播放过程  
                            while (outputDevice.PlaybackState == PlaybackState.Playing)
                            {
                                System.Threading.Thread.Sleep(1000);
                            }
                        }
                    }

                }
            }
            else
            {
                Console.WriteLine("this is not ogg");
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }

            
        }


        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] files = { };

            openFileDialog1.Filter = "选择音频|*.mp3;*.flac;*.wav;*.ogg";
            //同时打开多个文件
            openFileDialog1.Multiselect = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //清空原有列表
                listBox1.Items.Clear();
                localmusiclist.Clear();

                if (files != null)
                {
                    Array.Clear(files, 0, files.Length);
                }
                files = openFileDialog1.FileNames;
                string[] array = files;
                foreach (string file in array)
                {
                    listBox1.Items.Add(file);
                    localmusiclist.Add(file);
                }
            }
            openFileDialog1.ShowDialog() == DialogResult.OK;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            listBox1.Items.Clear();
            if (files != null) 
            {
                Array.Clear(files, 0, files.Length);
            }
            files=openFileDialog1.FileNames;

            string[] array = files;

            foreach (string x in array)
            {
                listBox1.Items.Add(x); 
                localmusiclist.Add(x);
            }


        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (localmusiclist.Count > 0)
            {
                axWindowsMediaPlayer1.URL= localmusiclist[listBox1.SelectedIndex];
                musicplay(axWindowsMediaPlayer1.URL);
                歌曲列表.Text =Path.GetFileNameWithoutExtension(localmusiclist[listBox1.SelectedIndex]);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.settings.volume = trackBar1.Value;
            label2.Text = trackBar1.Value + "%";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}

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

namespace DiscordEnablerDisabler
{
    public partial class Form1 : Form
    {
        private bool _isDiscordDisabled = false;

        public Form1()
        {
            InitializeComponent();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGO_Click(object sender, EventArgs e)
        {
            var path = GetDsicordPath();


            var DiscordFileName = Path.Combine(path, "Update.exe");
            var RenamedDiscordFileName = Path.Combine(path, "_Update.exe");
            var FakeDiscordFileName = Path.Combine(Environment.CurrentDirectory, "Update.exe");
            if (Directory.Exists(path))
            {
                if (!_isDiscordDisabled)
                {
                    System.IO.File.Move(DiscordFileName, RenamedDiscordFileName);
                    System.IO.File.Copy(FakeDiscordFileName, DiscordFileName);
                }
                else
                {
                    System.IO.File.Delete(DiscordFileName);
                    System.IO.File.Move(RenamedDiscordFileName, DiscordFileName);
                }

                _isDiscordDisabled = !_isDiscordDisabled;
                SetTexts();

            }
            else
            {
                NoDiscordFound();

            }
        }

        private void NoDiscordFound()
        {
            MessageBox.Show("No Discord found");
            label1.Text = "No Discord found";
            btnGO.Visible = false;

        }

        private void SetTexts()
        {
            btnGO.Text = _isDiscordDisabled ? "Enable Discord" : "Disable Discord";
            label1.Text = _isDiscordDisabled ? "Discord is disabled" : "Discord is enabled";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var path = GetDsicordPath();
            if (Directory.Exists(path))
            {
                var RenamedDiscordFileName = Path.Combine(path, "_Update.exe");
                _isDiscordDisabled = System.IO.File.Exists(RenamedDiscordFileName);
                SetTexts();
            }
            else
            {
                NoDiscordFound();
            }
        }

        private string GetDsicordPath()
        {
            var lad = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            //lad = lad.Replace("al_n0005684", "faimbode");
            var path = Path.Combine(lad, "Discord");
            if (!Directory.Exists(path))
            {
                var ad = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                //ad = ad.Replace("al_n0005684", "faimbode");
                path = Path.Combine(ad, "Discord");
            }
            if (!Directory.Exists(path))
            {
                path = Path.Combine(Environment.SpecialFolder.UserProfile.ToString(), @"\AppData\LocalLow\Discord");
            }

            return path;
        }
    }
}


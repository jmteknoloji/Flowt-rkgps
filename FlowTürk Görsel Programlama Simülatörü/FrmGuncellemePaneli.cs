using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlowTürk_Görsel_Programlama_Simülatörü
{
    public partial class FrmGuncellemePaneli : Form
    {
        public FrmGuncellemePaneli()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
            new FrmGuncellemeIndirme().ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmGuncellemePaneli_Load(object sender, EventArgs e)
        {
            label2.Text = "Yeni FlowTürk G.P.S. "+FrmAna.YeniVersiyon+" mevcut.";
            label3.Text = "Sürümünüz: " + Application.ProductVersion;
        }
    }
}

using GamingHub2.WinUI.Igra;
using GamingHub2.WinUI.Konzola;
using GamingHub2.WinUI.Korisnik;
using GamingHub2.WinUI.Zanr;
using GamingHub2.WinUI.Zanrovi;
using GamingHub2.WinUI.Korisnici;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GamingHub2.WinUI.Recenzija;
using GamingHub2.WinUI.Proizvod;
using GamingHub2.WinUI.Izvjestaj;

namespace GamingHub2.WinUI
{
    public partial class frmPocetna : Form
    {
        private int childFormNumber = 0;

        public frmPocetna()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void prikazKorisnikaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmKorisnici frm = new frmKorisnici();
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void prikazZanrovaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPrikazZanrova frm = new frmPrikazZanrova();
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }
        private void dodajUrediZanrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmZanrDodajUredi frm = new frmZanrDodajUredi();
            //frm.MdiParent = this;
            frm.ShowDialog();
            DialogResult = DialogResult.OK;
        }

        private void noviKorisnikToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmKorisniciDetalji frm = new frmKorisniciDetalji();
            frm.Show();
        }

        private void prikaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPrikazIgara frm = new frmPrikazIgara();
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }
        private void novaIgraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIgraDodajUredi frm = new frmIgraDodajUredi();
            frm.MdiParent = this;
            frm.Show();
        }

        private void prikazKonzolaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPrikazKonzola frm = new frmPrikazKonzola();
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void dodajKonzoluToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmKonzolaDodajUredi frm = new frmKonzolaDodajUredi();
            frm.MdiParent = this;
            frm.Show();
        }

        private void ulogeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRoleManager frm = new frmRoleManager();
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void prikazKorisnikaToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            frmKorisnici frm = new frmKorisnici();
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void noviKorisnikToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmKorisniciDetalji frm = new frmKorisniciDetalji();
            frm.Show();
        }

        private void prikazRecenzijaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPrikazRecenzija frm = new frmPrikazRecenzija();
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void novaRecenzijaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDodajUrediRecenzija frm = new frmDodajUrediRecenzija();
            frm.Show();
        }

        private void prikazProizvodaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProizvodi frm = new frmProizvodi();
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void noviProizvodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProizvodDodaj frm = new frmProizvodDodaj();
            frm.Show();
        }

        private void izvještajToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIzvjestaj frm = new frmIzvjestaj();
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }
    }
}

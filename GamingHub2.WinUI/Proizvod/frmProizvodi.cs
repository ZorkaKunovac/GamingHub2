using GamingHub2.Model.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GamingHub2.WinUI.Proizvod
{
    public partial class frmProizvodi : Form
    {
        private readonly APIService _service = new APIService("Proizvod");
        public frmProizvodi()
        {
            InitializeComponent();
        }

        private async void btnPrikazi_Click(object sender, EventArgs e)
        {
            ProizvodSearchRequest request = new ProizvodSearchRequest()
            {
                Naziv = txtNaziv.Text
            };
            dgvProizvodi.AutoGenerateColumns = false;
            dgvProizvodi.DataSource = await _service.Get<List<Model.Proizvod>>(request);
        }

        private async void frmProizvodi_Load(object sender, EventArgs e)
        {
            dgvProizvodi.AutoGenerateColumns = false;
            dgvProizvodi.DataSource = await _service.Get<List<Model.Proizvod>>(null);
        }

        private async void dgvProizvodi_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var proizvodId = int.Parse(dgvProizvodi.SelectedRows[0].Cells[0].Value.ToString());

            frmProizvodUredi frm = new frmProizvodUredi(proizvodId);
            var result = frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                dgvProizvodi.DataSource = await _service.Get<List<Model.Proizvod>>(null);
            }
        }

        private async void btnDodajProizvod_Click(object sender, EventArgs e)
        {
            frmProizvodDodaj frm = new frmProizvodDodaj();
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                dgvProizvodi.DataSource = await _service.Get<List<Model.Proizvod>>(null);
            }
        }
    }
}

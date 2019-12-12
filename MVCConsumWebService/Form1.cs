using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVCConsumWebService
{
    public partial class Form1 : Form
    {
        Repository repo = new Repository();
        public Form1()
        {
            InitializeComponent();
            dataGridView1.DataSource = repo.GetAllContactes();
            dataGridView1.Columns[0].Visible = false;
        }

        private void buttonAfegir_Click(object sender, EventArgs e)
        {
            contacte c = new contacte();
            c.nom = textBoxNom.Text;
            c.cognoms = textBoxCognom.Text;
            c.telefons = new List<telefon>();
            c.emails = new List<email>();
            repo.InsertContacte(c);
            dataGridView1.DataSource = repo.GetAllContactes();
        }

        private void buttonModificar_Click(object sender, EventArgs e)
        {
            contacte c = new contacte();
            c.contacteId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            c.nom = textBoxNom.Text;
            c.cognoms = textBoxCognom.Text;
            repo.UpdateContacte(c);
            dataGridView1.DataSource = repo.GetAllContactes();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                textBoxNom.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBoxCognom.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            }
            catch (Exception ex)
            {
                textBoxNom.Text = "";
                textBoxCognom.Text = "";
            }
        }
    }
}

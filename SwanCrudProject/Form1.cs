using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace SwanCrudProject
{
    public partial class Form1 : Form
        

    {
        BMSDETAILSDBDataContext db;
        public Form1()
        {
            AppCenter.Start("ccbccd56-a393-44ea-950e-14d080ff27e3",
                   typeof(Analytics), typeof(Crashes));
            Analytics.TrackEvent("My custom event");
            InitializeComponent();
        }

        private void BindGridView()
        {
            db = new BMSDETAILSDBDataContext();
            dataGridView.DataSource = db.BMSDETAILs;
        }

        private void ClearTextBoxes()
        {
            foreach(Control ctr in this.Controls)
            {
                if (ctr is TextBox)
                {
                    TextBox txt = ctr as TextBox;
                    txt.Clear();
                }
              
            }
            NametextBox.Focus();
        }


        private void Insertbutton_Click(object sender, EventArgs e)
        {
            if (NametextBox.Text == "" || EmailidtextBox.Text == "" || locationcomboBox.Text == "" || PhonenotextBox.Text=="")
            {
                MessageBox.Show("All fields are Mandatory");
                
            }
            else
            {
                db = new BMSDETAILSDBDataContext();
                BMSDETAIL b = new BMSDETAIL();
                b.name = NametextBox.Text;
                b.phoneno = int.Parse(PhonenotextBox.Text);
                b.emailid = EmailidtextBox.Text;
                b.location = locationcomboBox.Text;
                db.BMSDETAILs.InsertOnSubmit(b); // data save on linq 
                db.SubmitChanges(); // from permently infromation save to use Submitchanges
                MessageBox.Show("Employee Infromation inserted Successfully Done!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearTextBoxes();
                BindGridView(); 
            }
            


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BindGridView();
        }

        private void dataGridView_MouseClick(object sender, MouseEventArgs e)
        {
            NametextBox.Text = dataGridView.SelectedRows[0].Cells[1].Value.ToString();
            PhonenotextBox.Text = dataGridView.SelectedRows[0].Cells[2].Value.ToString();
            EmailidtextBox.Text = dataGridView.SelectedRows[0].Cells[3].Value.ToString();
            locationcomboBox.Text = dataGridView.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void updatebutton_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                db = new BMSDETAILSDBDataContext();
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                BMSDETAIL bms = db.BMSDETAILs.FirstOrDefault(s => s.Id == id);
                bms.name = NametextBox.Text;
                bms.phoneno = int.Parse(PhonenotextBox.Text);
                bms.emailid = EmailidtextBox.Text;
                bms.location = locationcomboBox.Text;
                db.SubmitChanges();
                MessageBox.Show("Employee Infromation updated Successfully Done!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearTextBoxes();
                BindGridView();
            }
            else
            {
                MessageBox.Show("Please Select for update");
            }
            

        }

        private void Deletebutton_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                db = new BMSDETAILSDBDataContext();
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                BMSDETAIL bms = db.BMSDETAILs.FirstOrDefault(s => s.Id == id);
                db.BMSDETAILs.DeleteOnSubmit(bms);
                db.SubmitChanges();
                MessageBox.Show("Employee Infromation Deleted Successfully Done!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearTextBoxes();
                BindGridView();
            }
            else
            {
                MessageBox.Show("Please Select for Delete");
            }
            
        }
    }
}

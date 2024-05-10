using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace Universitet
{
    public partial class Form1 : Form
    {
        private List<University> universities;

        public Form1()
        {
            InitializeComponent();
            LoadUniversities();
            BindData();
        }

        private void LoadUniversities()
        {
            universities = new List<University>();

           
            string filePath = "Universities.txt";

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        
                        string[] parts = line.Split(',');                     
                        int id = int.Parse(parts[0]);
                        string name = parts[1];
                        string city = parts[2];
                        

                        University university = new University
                        {
                            Id = id,
                            Name = name,
                            City = city,                           
                        };

                        universities.Add(university);
                    }
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show($"Ошибка при загрузке данных университетов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void BindData()
        {
            
            dataGridView1.DataSource = universities;

            comboBox1.DataSource = universities.Select(u => u.City).Distinct().ToList();
        }

      
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedCity = comboBox1.SelectedItem.ToString();
            var filteredUniversities = universities.Where(u => u.City == selectedCity).ToList();
            dataGridView1.DataSource = filteredUniversities;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                University selectedUniversity = dataGridView1.SelectedRows[0].DataBoundItem as University;
                textBoxname.Text = selectedUniversity.Name;
                textBoxcity.Text = selectedUniversity.City;
                
            }
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedUniversity = dataGridView1.SelectedRows[0].DataBoundItem as University;
            universities.Remove(selectedUniversity);
            BindData();
        }

        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult res;
            res = MessageBox.Show("Вы хотите выйти?", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                Application.Exit();
            }
            else { this.Show(); }
        }
    }
    
}
    
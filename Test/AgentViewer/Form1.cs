// Copyright (C) Upperbay Systems, LLC - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and confidential
// Written by Dave Hardin <dave@upperbay.com>, 2001-2020

using System;
using System.Windows.Forms;


namespace AgentViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'upperbayAgentsDataSet.CurrentValues' table. You can move, or remove it, as needed.
            this.currentValuesTableAdapter.Fill(this.upperbayAgentsDataSet.CurrentValues);
        }

        private void button1_Click(object sender, EventArgs e)
        {
                this.currentValuesTableAdapter.Fill(this.upperbayAgentsDataSet.CurrentValues);
                

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.currentValuesTableAdapter.Fill(this.upperbayAgentsDataSet.CurrentValues);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }




    }
}

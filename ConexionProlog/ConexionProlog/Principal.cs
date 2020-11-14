using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SbsSW.SwiPlCs;

namespace ConexionProlog
{
	public partial class Principal : Form
	{
		public Principal()
		{
			InitializeComponent();
			this.CenterToScreen();
			this.Text = "Proyecto C# conectado con Prolog";
		}

        private void Principal_Load(object sender, EventArgs e)
        {
			Environment.SetEnvironmentVariable("SWI_HOME_DIR", @"C:\Program Files (x86)\swipl");
			Environment.SetEnvironmentVariable("Path", @"C:\Program Files (x86)\swipl\bin");
			string[] p = { "-q", "-f", @"practica.pl" };

			// Connect to Prolog Engine
			PlEngine.Initialize(p);

		}

        private void button1_Click(object sender, EventArgs e)
        {

		

			PlQuery consulta = new PlQuery("max(6,5,X)");

			foreach (PlQueryVariables z in consulta.SolutionVariables)
				Console.WriteLine(z["X"].ToString());
			//listBox1.Items.Add();	


			var dataTable = new DataTable();
			var tamannoMatriz = int.Parse(tbMatriz.Text.ToString());
			for (int i = 0; i < tamannoMatriz; i++)
			{
				dataTable.Columns.Add(" " + i);
			}

			for (int i = 0; i < tamannoMatriz; i++)
			{
				dataTable.Rows.Add();
			}

			tabla.DataSource = dataTable;
			tabla.CellClick += listenerrDataGridViewOnClick;
			Console.WriteLine(dataTable.Rows[0]);
		}

        private void label1_Click(object sender, EventArgs e)
        {

        }
		private void listenerrDataGridViewOnClick(object sender, DataGridViewCellEventArgs e)
		{
			DataGridViewCell cell = (DataGridViewCell)tabla.Rows[e.RowIndex].Cells[e.ColumnIndex];
			Console.WriteLine(cell.Value);
			Console.WriteLine("Funciona");
			if (cell.Value == "sewagram express")
			{
			
			}
		}

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

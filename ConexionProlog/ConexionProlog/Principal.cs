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
		Boolean flag = false;
		DataTable dataTable = new DataTable();
		int tamannoMatriz = 0;
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
			string[] p = { "-q", "-f", @"proyecto.pl" };

			// Connect to Prolog Engine
			PlEngine.Initialize(p);
		
			tabla.CellClick += listenerrDataGridViewOnClick;
			tabla.ColumnHeadersVisible = false;
			tabla.RowHeadersVisible = false;
			tabla.AllowUserToAddRows = false;
			tabla.AllowUserToResizeRows = false;
			tabla.AllowUserToResizeColumns = false;
			tabla.AllowDrop = false;

		}

        private void button1_Click(object sender, EventArgs e)
        {


			dataTable.Rows.Clear();
			dataTable.Columns.Clear();

			PlQuery consulta = new PlQuery("punto(2,X)");

			foreach (PlQueryVariables z in consulta.SolutionVariables)
				Console.WriteLine(z["X"].ToString());
			

			flag = false;


			
			tamannoMatriz = int.Parse(tbMatriz.Text.ToString());
			for (int i = 0; i < tamannoMatriz; i++)
			{
				dataTable.Columns.Add();
			}

			for (int i = 0; i < tamannoMatriz; i++)
			{
				dataTable.Rows.Add();
			}

			tabla.DataSource = dataTable;

		}

        private void label1_Click(object sender, EventArgs e)
        {

        }
		private void listenerrDataGridViewOnClick(object sender, DataGridViewCellEventArgs e)
		{
           DataGridViewCell cell = (DataGridViewCell)tabla.Rows[e.RowIndex].Cells[e.ColumnIndex];
		   Console.WriteLine(cell.Value);
		   Console.WriteLine("Funciona");
		   if (flag == true)
		    {
				cell.Style.BackColor = System.Drawing.Color.Red;
		    }
		   else
			{
				if (cell.Value == "O")
				{
					cell.Value = " ";
				}
				else
				{
					cell.Value = "O";
				}
			}		
		}

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
		
		}

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
	

			Random rnd = new Random();
			int totalValues = rnd.Next(0, tamannoMatriz*tamannoMatriz);

			for (int i = 0; i < tamannoMatriz; i++)
			{
				for (int j = 0; j < tamannoMatriz; j++)
				{
					dataTable.Rows[i][j] = " ";
				}
			}

			Console.WriteLine(totalValues);
			Console.WriteLine(tamannoMatriz);
			for (int i = 0; i <= totalValues; i++)
			{
				int colums = rnd.Next(0, tamannoMatriz );
				int rows = rnd.Next(0, tamannoMatriz);
				dataTable.Rows[colums][rows] = "O";
			}

		
				
			
		}

        private void button2_Click_1(object sender, EventArgs e)
        {
			flag = true;

        }
    }
}

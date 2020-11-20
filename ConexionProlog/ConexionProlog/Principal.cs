using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
			deleteFile();
			dataTable.Rows.Clear();
			dataTable.Columns.Clear();

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
		  
		   if (flag == true)
		    {
				if (cell.Value == "O")
                {
					String x = e.ColumnIndex.ToString();
					String y = e.RowIndex.ToString();
					foundGroup(x, y);
					MessageBox.Show("Usted esta en la posición x =" + x + " y = " + y);
				}
				else
				{
					MessageBox.Show("Usted esta en la posición x =" + e.ColumnIndex.ToString() + " y = " + e.RowIndex.ToString());
				}
			}
		   else
			{
				if (cell.Value == "O")
				{
					MessageBox.Show("Usted esta en la posición x =" + e.ColumnIndex.ToString() + " y = " + e.RowIndex.ToString());
				}
				else
				{
					String x = e.ColumnIndex.ToString();
					String y = e.RowIndex.ToString();
					String z = "punto(" + x.ToString() + "," + y.ToString() + ").";
					addPoint(z);
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

			cleanMatriz();
			for (int i = 0; i <= totalValues; i++)
			{
				int colums = rnd.Next(0, tamannoMatriz);
				int rows = rnd.Next(0, tamannoMatriz);
				dataTable.Rows[colums][rows] = "O";
				String z = "punto(" + colums.ToString() + "," + rows.ToString() + ").";
				addPoint(z);
			}		
		}

        private void button2_Click_1(object sender, EventArgs e)
        {
			flag = true;

        }

	

		private void listing()
        {
			PlQuery consulta = new PlQuery("listing(X).");
			foreach (PlQueryVariables z in consulta.SolutionVariables)
			{
				Console.WriteLine(z["X"].ToString());
			}
			
		}
		private void startProlog()
        {
			PlQuery consulta = new PlQuery("start.");
			Console.WriteLine(consulta.NextSolution());
		}

		private void addPoint(String consult)
        {
			using (StreamWriter outputFile = new StreamWriter("C:\\Users\\edubi\\OneDrive - Estudiantes ITCR\\Cuarto Semestre\\Lenguajes de programación\\Proyectos\\Proyecto 3\\ProyectoCsharpProlog\\ConexionProlog\\ConexionProlog\\bin\\Debug\\bdp.pl", true))
			{
					outputFile.WriteLine(consult);
			}
		}

		private void deleteFile()
        {
			File.Delete("C:\\Users\\edubi\\OneDrive - Estudiantes ITCR\\Cuarto Semestre\\Lenguajes de programación\\Proyectos\\Proyecto 3\\ProyectoCsharpProlog\\ConexionProlog\\ConexionProlog\\bin\\Debug\\bdp.pl");
			addPoint(":- dynamic punto/2.");
		}

        private void clean_Click(object sender, EventArgs e)
        {
			cleanMatriz();
			deleteFile();
		}

		private void cleanMatriz()
        {
			for (int i = 0; i < tamannoMatriz; i++)
			{
				for (int j = 0; j < tamannoMatriz; j++)
				{
					dataTable.Rows[i][j] = " ";
				}
			}
		}

        private void btnConsult_Click(object sender, EventArgs e)
        {
			PlQuery consulta = new PlQuery("grupo([0,9],L).");
			String xd = " ";
			  foreach (PlQueryVariables z in consulta.SolutionVariables)
			{
				Console.WriteLine("X = " + z["L"].ToString());
				xd = z["L"].ToString();
				break;
			}	 

			Console.WriteLine(consulta.NextSolution());
			consulta.Dispose();
			List<string> f = new List<string>(xd.Split(','));

			Console.WriteLine(xd.Length);
			Console.WriteLine(f[1]);
		}


		private void foundGroup(String X, String Y)
		{
			/**
			 * 
			 * PlQuery consulta = new PlQuery("grupos(X,"+Y+").");
			foreach (PlQueryVariables z in consulta.SolutionVariables)
			{
				Console.WriteLine("Conectados con y");
				Console.WriteLine("X = " + z["X"].ToString() + " Y = " + Y);
			}
			Console.WriteLine(consulta.NextSolution());
			consulta.Dispose();

			consulta = new PlQuery("grupos(" + X+",Y).");
			foreach (PlQueryVariables z in consulta.SolutionVariables)
			{
				Console.WriteLine("Conectados con X");
				Console.WriteLine("X = " + X + " Y = " + z["Y"].ToString());
			}
			Console.WriteLine(consulta.NextSolution());
			consulta.Dispose();
			 
			*/

			ArrayList al = new ArrayList();
			PlQuery consulta = new PlQuery("grupo([" + X+","+ Y+"],L).");

			foreach (PlQueryVariables z in consulta.SolutionVariables)
			{
				Console.WriteLine("X = " + z["L"].ToString());
				
			}

			Console.WriteLine(consulta.NextSolution());
			consulta.Dispose();

		}
	}
}

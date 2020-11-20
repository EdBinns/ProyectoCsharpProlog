﻿using System;
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
					List<List<int>> list = foundGroup(x, y);
					MessageBox.Show("Usted esta en la posición x =" + x + " y = " + y);

					foreach(List<int> subList in list)
                    {
						DataGridViewCell color = (DataGridViewCell)tabla.Rows[subList[1]].Cells[subList[0]];
						color.Style.BackColor = System.Drawing.Color.Coral;
                    }

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
				dataTable.Rows[rows][colums] = "O";
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
			tabla.DataSource = null;
			tabla.Rows.Clear();
			tabla.DataSource = dataTable;
		}

        private void btnConsult_Click(object sender, EventArgs e)
        {
			PlQuery consulta = new PlQuery("grupo([1,4],L).");
			String stringConsult = " ";
			  foreach (PlQueryVariables z in consulta.SolutionVariables)
			{
				Console.WriteLine("X = " + z["L"].ToString());
				stringConsult = z["L"].ToString();
				break;
			}	 

			Console.WriteLine(consulta.NextSolution());
			consulta.Dispose();

	
            stringConsult = stringConsult.Replace("],[", "].[");
			stringConsult = stringConsult.Replace("[[", "[");
			stringConsult = stringConsult.Replace("]]", "");
			List<string> listConsult = new List<string>(stringConsult.Split('.'));
			List<string> sublista;
			List<List<int>> listOfPoint  = new List<List<int>>();
			foreach (String i in listConsult)
			{
				String temp = i;
				temp = temp.Replace("[", "");
				temp = temp.Replace("]", "");
				sublista= new List<string>(temp.Split(','));
				List<int> subListOfPoint = new List<int>();
				foreach (String j in sublista)
                {
					int n = Convert.ToInt32(j);
					subListOfPoint.Add(n);
					Console.WriteLine(j);
                }
				Console.WriteLine("========");
				listOfPoint.Add(subListOfPoint);
			}
			
		}


		private  List<List<int>> foundGroup(String X, String Y)
		{
			
			
			PlQuery consulta = new PlQuery("grupo([" + X+","+ Y+"],L).");

			String stringConsult = " ";
			foreach (PlQueryVariables query in consulta.SolutionVariables)
			{
				Console.WriteLine("X = " + query["L"].ToString());
				stringConsult = query["L"].ToString();
				break;
			}

			Console.WriteLine(consulta.NextSolution());
			consulta.Dispose();

			stringConsult = stringConsult.Replace("],[", "].[");
			stringConsult = stringConsult.Replace("[[", "[");
			stringConsult = stringConsult.Replace("]]", "");
			List<string> listConsult = new List<string>(stringConsult.Split('.'));
			List<string> sublista;
			List<List<int>> listOfPoint = new List<List<int>>();
			foreach (String i in listConsult)
			{
				String temp = i;
				temp = temp.Replace("[", "");
				temp = temp.Replace("]", "");
				sublista = new List<string>(temp.Split(','));
				List<int> subListOfPoint = new List<int>();
				foreach (String j in sublista)
				{
					int n = Convert.ToInt32(j);
					subListOfPoint.Add(n);
					Console.WriteLine(j);
				}
				Console.WriteLine("========");
				listOfPoint.Add(subListOfPoint);
			}
			return listOfPoint;
		}
	}
}

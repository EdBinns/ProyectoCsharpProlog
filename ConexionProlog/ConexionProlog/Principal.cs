using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using SbsSW.SwiPlCs;

namespace ConexionProlog
{
    public partial class Principal : Form
    {
        Boolean flag = false;
        DataTable dataTable = new DataTable();
        int tamannoMatriz = 0;
        List<List<List<int>>> listGlobal = new List<List<List<int>>>();
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
            foreach (DataGridViewColumn column in tabla.Columns)
            {
                column.Width = 35;
            }

            foreach (DataGridViewRow row in tabla.Rows)
            {
                row.Height = 35;
            }

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
                    int n = seeLenghtGroup(x, y);
                    String consult = consultGroup(x, y);
                    List<List<int>> list = makeAList(consult);
                    MessageBox.Show("Usted esta en la posición x =" + x + " y = " + y + ", y el tamaño de ese grupo es de " + n.ToString());
                    Random random = new Random();
                    int r = random.Next(0, 256);
                    int g = random.Next(0, 256);
                    int b = random.Next(0, 256);
                    foreach (List<int> subList in list)
                    {
                        DataGridViewCell color = (DataGridViewCell)tabla.Rows[subList[1]].Cells[subList[0]];
                        color.Style.BackColor = System.Drawing.Color.FromArgb(r, g, b);
                    }
                    tabla.ClearSelection();

                }
                else
                {
                    tabla.ClearSelection();
                    MessageBox.Show("Usted esta en la posición x =" + e.ColumnIndex.ToString() + " y = " + e.RowIndex.ToString());
                }
            }
            else
            {
                if (cell.Value == "O")
                {
                    MessageBox.Show("Usted esta en la posición x =" + e.ColumnIndex.ToString() + " y = " + e.RowIndex.ToString());
                    tabla.ClearSelection();
                }
                else
                {
                    String x = e.ColumnIndex.ToString();
                    String y = e.RowIndex.ToString();
                    String z = "punto(" + x.ToString() + "," + y.ToString() + ").";
                    addPoint(z);
                    cell.Value = "O";
                    tabla.ClearSelection();
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            if (!flag)
            {
                Random rnd = new Random();
                int totalValues = rnd.Next(0, tamannoMatriz * tamannoMatriz);

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

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            flag = true;
            MessageBox.Show("La actividad ha iniciado");

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
            flag = false;
            cleanMatriz();
            deleteFile();
           
        }

        private void cleanMatriz()
        {
            deleteFile();
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
            foreach (DataGridViewColumn column in tabla.Columns)
            {
                column.Width = 35;
            }

            foreach (DataGridViewRow row in tabla.Rows)
            {
                row.Height = 35;
            }
        }

        private void btnConsult_Click(object sender, EventArgs e)
        {
            if (flag)
            {
                List<List<List<int>>> listOfGroup = new List<List<List<int>>>();

                List<String> listStrings = new List<String>();
                for (int i = 0; i < tamannoMatriz; i++)
                {
                    for (int j = 0; j < tamannoMatriz; j++)
                    {
                        if (tabla.Rows[j].Cells[i].Value == "O")
                        {
                            String group = consultGroup(i.ToString(), j.ToString());
                            if ((group != null))
                            {
                                if (!listStrings.Contains(group))
                                {
                                    listStrings.Add(group);
                                    List<List<int>> newgroup = makeAList(group);
                                    listOfGroup.Add(newgroup);
                                }
                            }
                        }
                    }
                }
                listGlobal.Clear();
                listGlobal = listOfGroup;
                Random random = new Random();
                foreach (List<List<int>> list in listOfGroup)
                {
                    int r = random.Next(0, 256);
                    int g = random.Next(0, 256);
                    int b = random.Next(0, 256);
                    foreach (List<int> subList in list)
                    {
                        DataGridViewCell color = (DataGridViewCell)tabla.Rows[subList[1]].Cells[subList[0]];
                        color.Style.BackColor = System.Drawing.Color.FromArgb(r, g, b);
                    }
                }

                String strSize = "" ;
                foreach(List<List<int>> list in listOfGroup)
                {
                    strSize = strSize + list.Count + ",";
                }

                MessageBox.Show("El total de grupos en la matriz es de " + listOfGroup.Count.ToString() + ", y los grupos tienen tamaños de " + strSize);
                tabla.ClearSelection();
            }

        }


        private String consultGroup(String X, String Y)
        {
            PlQuery consulta = new PlQuery("grupo([" + X + "," + Y + "],L).");

            String stringConsult = " ";
            foreach (PlQueryVariables query in consulta.SolutionVariables)
            {
                stringConsult = query["L"].ToString();
            }


            consulta.Dispose();
            if (consulta.NextSolution())
            {
                return stringConsult;

            }
            return null;
        }
        private List<List<int>> makeAList(String stringConsult)
        {
            List<List<int>> listOfPoint = new List<List<int>>();
            if (stringConsult != null)
            {
                stringConsult = stringConsult.Replace("],[", "].[");
                stringConsult = stringConsult.Replace("[[", "");
                stringConsult = stringConsult.Replace("]]", "");
                List<string> listConsult = new List<string>(stringConsult.Split('.'));
                List<string> sublista;

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
                    }
                    listOfPoint.Add(subListOfPoint);
                }
                return listOfPoint;
            }
            return null;
        }



        private int seeLenghtGroup(String X, String Y)
        {

            PlQuery consulta = new PlQuery("tamanogrupo([" + X + "," + Y + "],N).");

            String stringConsult = " ";
            foreach (PlQueryVariables query in consulta.SolutionVariables)
            {
                stringConsult = query["N"].ToString();
            }
            consulta.Dispose();
            return Convert.ToInt32(stringConsult); ;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int n = int.Parse(tbSearch.Text.ToString());
            Random random = new Random();
            foreach (List<List<int>> list in listGlobal)
            {
                if (list.Count == n)
                {
                    foreach (List<int> subList in list)
                    {
                        DataGridViewCell color = (DataGridViewCell)tabla.Rows[subList[1]].Cells[subList[0]];
                        color.Style.BackColor = System.Drawing.Color.BlueViolet;
                    }
                }
                else
                {
                    int r = random.Next(0, 256);
                    int g = random.Next(0, 256);
                    int b = random.Next(0, 256);
                    foreach (List<int> subList in list)
                    {
                        DataGridViewCell color = (DataGridViewCell)tabla.Rows[subList[1]].Cells[subList[0]];
                        color.Style.BackColor = System.Drawing.Color.FromArgb(r, g, b);
                    }

                }
            }

        }
    }
}

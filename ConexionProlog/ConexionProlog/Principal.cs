using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SbsSW.SwiPlCs;

namespace ConexionProlog
{
    public partial class Principal : Form
    {
        Boolean flag = false;
        DataTable dataTable = new DataTable();
        DataTable table = new DataTable();
        int secondGrid = 0;
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
            griedLists.CellClick += listenerrDataGridVie2OnClick;
            griedLists.ColumnHeadersVisible = false;
            griedLists.RowHeadersVisible = false;
            griedLists.AllowUserToAddRows = false;
            griedLists.AllowUserToResizeRows = false;
            griedLists.AllowUserToResizeColumns = false;
            griedLists.AllowDrop = false;


        }

        ///<summary>
        ///Inicializa la matriz según el número ingresado por el usuario
        ///</summary>
        ///<param name="e">
        ///Detecta los eventos del boton
        ///</param>
        ///  ///<param name="sender">
        ///Detecta los eventos del boton
        ///</param>
        private void button1_Click(object sender, EventArgs e)
        {
            deleteFile();
            dataTable.Rows.Clear();
            dataTable.Columns.Clear();

            table.Rows.Clear();
            table.Columns.Clear();

            flag = false;
            btnRandom.Enabled = true;
            //Se rellena las columnas de la matriz
            tamannoMatriz = int.Parse(tbMatriz.Text.ToString());
            for (int i = 0; i < tamannoMatriz; i++)
            {
                dataTable.Columns.Add();
            }

            //Se rellena las filas de la matriz
            for (int i = 0; i < tamannoMatriz; i++)
            {
                dataTable.Rows.Add();
            }
            //Se define un tamaño a las celdas de la matriz
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

        ///<summary>
        ///Detecta el click en una celda de la matriz
        ///</summary>
        ///<param name="sender">
        ///Detecta el evento del click
        ///</param>
        ///  ///<param name="e">
        ///Detecta el evento del click
        ///</param>
        private void listenerrDataGridViewOnClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = (DataGridViewCell)tabla.Rows[e.RowIndex].Cells[e.ColumnIndex];

            
            if (flag)
            {
                if (cell.Value == "O")
                {
                    ///Se detecta el click en el simbolo y se procede a pintar su grupo
                    String x = e.ColumnIndex.ToString();
                    String y = e.RowIndex.ToString();
                    int n = seeLenghtGroup(x, y);
                    String consult = consultGroup(x, y);
                    List<List<int>> list = makeAList(consult);
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
                    MessageBox.Show("Usted esta en la posición x =" + x + " y = " + y + "\n El tamaño del grupo a donde se encuentra es de: " + n.ToString());

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

                    //Se procede a pintar el O en la celda seleccionada
                    String x = e.ColumnIndex.ToString();
                    String y = e.RowIndex.ToString();
                    String z = "punto(" + x.ToString() + "," + y.ToString() + ").";
                    addPoint(z);
                    MessageBox.Show("Añadio un punto en la posición x =" + e.ColumnIndex.ToString() + " y = " + e.RowIndex.ToString());
                    cell.Value = "O";
                    tabla.ClearSelection();
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        ///<summary>
        ///Rellena la matriz de forma automatica
        ///</summary>
        ///<param name="e">
        ///Detecta los eventos del boton
        ///</param>
        ///  ///<param name="sender">
        ///Detecta los eventos del boton
        ///</param>
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
        ///<summary>
        ///Detecta cuando el usuario ha iniciado la actividad
        ///</summary>
        ///<param name="e">
        ///Detecta los eventos del boton
        ///</param>
        ///  ///<param name="sender">
        ///Detecta los eventos del boton
        ///</param>
        private void button2_Click_1(object sender, EventArgs e)
        {
            flag = true;
            btnRandom.Enabled = false;
            MessageBox.Show("La actividad ha iniciado");

        }


        ///<summary>
        ///Agrega el punto seleccionado por el usuario para proceder agregarlo a la base de datos
        ///</summary>
        ///<param name="consult">
        ///String que contiene el punto por agregar a la base de datos
        ///</param>
        private void addPoint(String consult)
        {
            using (StreamWriter outputFile = new StreamWriter("C:\\Users\\edubi\\OneDrive - Estudiantes ITCR\\Cuarto Semestre\\Lenguajes de programación\\Proyectos\\Proyecto 3\\ProyectoCsharpProlog\\ConexionProlog\\ConexionProlog\\bin\\Debug\\bdp.pl", true))
            {
                outputFile.WriteLine(consult);
            }
        }

        ///<summary>
        ///Procede a eliminar de memoria todos los puntos que hayan estado guardados en la base de datos
        ///</summary>
        private void deleteFile()
        {
            File.Delete("C:\\Users\\edubi\\OneDrive - Estudiantes ITCR\\Cuarto Semestre\\Lenguajes de programación\\Proyectos\\Proyecto 3\\ProyectoCsharpProlog\\ConexionProlog\\ConexionProlog\\bin\\Debug\\bdp.pl");
            addPoint(":- dynamic punto/2.");
        }


        ///<summary>
        ///Detecta cuando un usuario quiere limpiar la matriz
        ///</summary>
        ///<param name="e">
        ///Detecta los eventos del boton
        ///</param>
        ///  ///<param name="sender">
        ///Detecta los eventos del boton
        ///</param>
        private void clean_Click(object sender, EventArgs e)
        {
            flag = false;
            cleanMatriz();    
        }


        ///<summary>
        ///Se encarga de ir limpiando la matriz celda por celda
        ///</summary>
        private void cleanMatriz()
        {
            btnRandom.Enabled = true;
            deleteFile();
            for (int i = 0; i < tamannoMatriz; i++)
            {
                for (int j = 0; j < tamannoMatriz; j++)
                {
                    dataTable.Rows[i][j] = " ";
                }
            }
            deleteFile();
            table.Clear();
            tabla.DataSource = null;
            tabla.Rows.Clear();
            tabla.DataSource = dataTable;

            griedLists.DataSource = null;
            griedLists.Rows.Clear();
            griedLists.DataSource = table;
            foreach (DataGridViewColumn column in tabla.Columns)
            {
                column.Width = 35;
            }

            foreach (DataGridViewRow row in tabla.Rows)
            {
                row.Height = 35;
            }
        }

        ///<summary>
        ///Detecta cuando un usuario quiere limpiar la matriz desea ver todos los grupos que se encuentran \
        ///dentro de la matriz, estos se colorean y se manda un mensaje indicando sus tamaños
        ///</summary>
        ///<param name="e">
        ///Detecta los eventos del boton
        ///</param>
        ///  ///<param name="sender">
        ///Detecta los eventos del boton
        ///</param>
        private void btnConsult_Click(object sender, EventArgs e)
        {
            if (flag)
            {
                
                ///listOfGroup contiene todos los grupos encontrados
                List<List<List<int>>> listOfGroup = new List<List<List<int>>>();

                ///listString va guardando los grupos en forma de strings para saber/
                ///si ya se encuentran dentro de listOfGroup
                String point = "";
              
                String totalPoinst = "";
                List<String> listStrings = new List<String>();
                for (int i = 0; i < tamannoMatriz; i++)
                {
                    for (int j = 0; j < tamannoMatriz; j++)
                    {
                        if (tabla.Rows[j].Cells[i].Value == "O")
                        {
                            point = "[" + i + "," + j + "]";
                            if (!totalPoinst.Contains(point))
                            {
                          
                                String group = consultGroup(i.ToString(), j.ToString());
                                totalPoinst = totalPoinst + " " + group;
                                if ((group != null) && (!listStrings.Contains(group)))
                                {
                                    listStrings.Add(group);
                                    List<List<int>> newgroup = makeAList(group);
                                    listOfGroup.Add(newgroup);
                                }
                            }
                        }
                    }
                }
                ///Se procede a pintar todos los grupos que se encontraron
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

   
                ///Se prepara el mensaje para detectar cuantos grupos hay y sus tamaños
                List<int> listSize = new List<int>();
                addInGried(listOfGroup);
                foreach (List<List<int>> list in listOfGroup)
                {
                    listSize.Add(list.Count);
                }

                var agrupar = listSize.GroupBy(i => i);

                String message = "";
                foreach (var grp in agrupar)
                {
                    message = message + "Hay " + grp.Count() + " grupos de " + grp.Key + " \n";
                }

                MessageBox.Show("El total de grupos en la matriz es de " + listOfGroup.Count.ToString() + "\nLos grupos estan distribuidos de la siguiente manera \n" + message);
                tabla.ClearSelection();
            }

        }

        ///<summary>
        ///Realiza la conexión con prolog para poder ir a buscar el grupo que contiene 
        ///el punto que llego por parametro
        ///</summary>
        ///<return>
        ///Devuelve un string a donde se encuentra todo el grupo que se logro encontrar. en caso de que no /
        ///encontrara nada, se devuelve un valor nulo
        ///</return>
        /// ///<param name="X">
        ///Eje X del punto que llega
        ///</param>
        ///<param name="Y">
        ///Eje Y del punto que llega
        ///</param>

        private String consultGroup(String X, String Y)
        {

            ///Se procede a realizar la conexión con prologón con prolog y se reliza la consulta
            PlQuery consulta = new PlQuery("grupo([" + X + "," + Y + "],L).");


            ///Se recupera lo que devolvio la consulta en la incognita
            String stringConsult = " ";
            foreach (PlQueryVariables query in consulta.SolutionVariables)
            {
                stringConsult = query["L"].ToString();
            }

            ///Se verifica que haya sido exitosa la consulta y se devuelve su valor, en caso
            ///contrario se devuelve null
            consulta.Dispose();
            if (consulta.NextSolution())
            {
                return stringConsult;
            }
            return null;
        }

        ///<summary>
        /// Convierte el string de la consulta que se encontro en la conexión con prologón con prolog a una lista
        /// De esta forma se vuelve más factible trabajar con los resultados
        ///</summary>
        ///<return>
        ///Devuelve una lista que contiene un grupo de puntos, si el string no es nulo, en caso contrario
        ///se procede a devolver null
        ///</return>
        /// ///<param name="stringConsult">
        ///String que contiene el grupo de puntos
        ///</param>
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


        ///<summary>
        ///Realiza la conexión con prologón con prolog para poder encontrar el tamaño de un grupo
        ///</summary>
        ///<return>
        ///Devuelve el tamaño de un grupo
        ///</return>
        /// ///<param name="X">
        ///Eje X del punto que llega
        ///</param>
        ///<param name="Y">
        ///Eje Y del punto que llega
        ///</param>
        private int seeLenghtGroup(String X, String Y)
        {
            ///Se procede a realizar la conexión con prolog
            PlQuery consulta = new PlQuery("tamanogrupo([" + X + "," + Y + "],N).");


            ///Se recupere el valor que devuelve la consulta
            String stringConsult = " ";
            foreach (PlQueryVariables query in consulta.SolutionVariables)
            {
                stringConsult = query["N"].ToString();
            }
            consulta.Dispose();

            ///Se devuelve en int el valor del string
            return Convert.ToInt32(stringConsult); ;
        }


        private void listenerrDataGridVie2OnClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = (DataGridViewCell)griedLists.Rows[e.RowIndex].Cells[e.ColumnIndex];

            String value = cell.Value.ToString();


            if (!value.Contains("Grupos de"))
            {
                List<List<int>> list = makeAList(value);

                Random random = new Random();

                int r = random.Next(0, 256);
                int g = random.Next(0, 256);
                int b = random.Next(0, 256);
                foreach (List<int> subList in list)
                {
                    DataGridViewCell color = (DataGridViewCell)tabla.Rows[subList[1]].Cells[subList[0]];
                    color.Style.BackColor = System.Drawing.Color.FromArgb(r, g, b);
                    cell.Style.BackColor = System.Drawing.Color.FromArgb(r, g, b);
                }
                griedLists.ClearSelection();
            }
        }

        private void addInGried(List<List<List<int>>> listOfGroup)
        {

        
            List<int> listSize = new List<int>();
            foreach (List<List<int>> list in listOfGroup)
            {
                listSize.Add(list.Count);
            }

            var agrupar = listSize.GroupBy(i => i);

            int posX = 0;
            int posY = 0;
         
            table.Columns.Add();
           
            int subTotal = 0;
            foreach (var grp in agrupar)
            {
                subTotal += 1;
            }
            int totalGroups = listOfGroup.Count;
            secondGrid = (totalGroups + subTotal);
            for (int i = 0; i < secondGrid; i++)
            {
                table.Rows.Add();
            }
            foreach (var grp in agrupar)
            {
                table.Rows[posX][posY] = "Grupos de " + grp.Key;
                for(int i = 0; i < totalGroups; i++)
                {       
                    List<List<int>> list = listOfGroup[i];
                    if (posX < secondGrid && list.Count == grp.Key)
                    { 
                        posX += 1;
                        String print = "[";
                        foreach (List<int> point in list)
                        {
                            print = print +",[" + point[0] + "," + point[1] + "]";
                        }
                        String newPrint = print.Replace("[,[","[[");
                        print = newPrint + "]";
                        table.Rows[posX][posY] = print; 
                    }     
                }
                posX += 1;
            }
            griedLists.DataSource = null;
            griedLists.Rows.Clear();
            griedLists.DataSource = table;
       
            foreach (DataGridViewColumn column in griedLists.Columns)
            {
                column.Width = 305;
                break;
            }

            

        }

    }
}

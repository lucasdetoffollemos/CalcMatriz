using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Xml.XPath;

namespace CalcMatriz
{
    public partial class Form1 : Form
    {
        private double [,] matrixResult;
        private double[,] matrixResult2;
        private object matrixLock = new object();
        private Stopwatch watch = new Stopwatch();
        private static List<string> matrixes = new List<string>();
        public Form1()
        {
            
            InitializeComponent();
        }

        

        private void btCarregaMatriz_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult result = openFileDialog1.ShowDialog();
            // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                /*if (!file.Contains(".txt"))
                {
                    MessageBox.Show("Necessita ser um txt");
                    return;
                }*/
                try
                {
                    if (matrixes.Count < 2)
                        matrixes.Add(file);

                    if (matrixes.Count == 2)
                    {
                        btCalculaMatriz.Enabled = true;
                    }

                }
                catch (IOException)
                {
                }
            }
        }

        private void btCalculaMatriz_Click(object sender, EventArgs e)
        {
            watch.Start();
            var fileMatrix1 = matrixes[0];

            var fileMatrix2 = matrixes[1];


            var fileRead1 = ReadFile(fileMatrix1);

            var fileRead2 = ReadFile(fileMatrix2);

            var matrixA = ConvertToDouble(fileRead1);

            var matrixB = ConvertToDouble(fileRead2);


            //split matrix A in 2 matrixA1 and matrixA2

            var matrixesA = SplitMatrixByRows(matrixA);
            var matrixA1 = matrixesA.Item1;
            var matrixA2 = matrixesA.Item2;

            int rows = matrixA.GetLength(0);
            int cols = matrixA.GetLength(1);

            matrixResult = new double[rows, cols];

            //MULT THREAD
            //Create a thread 1 to multiply matrixA1 and matrixB1
            Task task1 = Task.Run(() => MultiplyMatrixes(matrixA1, matrixB, 0, matrixA1.Length));
            ////Create a thread 2 to multiply matrixA2 and matrixB2
            Task task2 = Task.Run(() => MultiplyMatrixes(matrixA2, matrixB, matrixA1.Length, matrixA2.Length));


            Task.WhenAll(task1, task2).Wait();

            /*thread1.Start();
            Thread.Sleep(1000);
            thread2.Start();


            thread1.Join();
            thread2.Join();*/

            //SINGLE THREAD
           //MultiplyMatrixes(matrixA, matrixB);

            matrixes.Clear();

            btCalculaMatriz.Enabled=false;

            GenerateFile(matrixResult);

            watch.Stop();

            MessageBox.Show($"Tempo para realizar a execu��o em minutos: {watch.Elapsed.TotalMinutes.ToString()}");
            MessageBox.Show($"Tempo para realizar a execu��o em segundos: {watch.Elapsed.TotalSeconds.ToString()}");
            MessageBox.Show($"Tempo para realizar a execu��o em milisegundos: {watch.Elapsed.TotalMilliseconds.ToString()}");
        }

        private (double[,], double[,]) SplitMatrixByRows(double[,] matrix)
        {
            int totalRows = matrix.GetLength(0);
            int totalCols = matrix.GetLength(1);

            // Determine the split point (e.g., half of the rows)
            int splitIndex = totalRows / 2;

            // Create the first half of the matrix (top half)
            double[,] topHalf = new double[splitIndex, totalCols];
            for (int i = 0; i < splitIndex; i++)
            {
                for (int j = 0; j < totalCols; j++)
                {
                    topHalf[i, j] = matrix[i, j];
                }
            }

            // Create the second half of the matrix (bottom half)
            double[,] bottomHalf = new double[totalRows - splitIndex, totalCols];
            for (int i = splitIndex; i < totalRows; i++)
            {
                for (int j = 0; j < totalCols; j++)
                {
                    bottomHalf[i - splitIndex, j] = matrix[i, j];
                }
            }

            return (topHalf, bottomHalf);
        }

        private void GenerateFile(double[,] result)
        {
            var rows = result.GetLength(0);
            var cols = result.GetLength(1);

            var path = $"C:/Users/lucas/Downloads/matA_B/matResult-{DateTime.Now.ToString("HH-mm-ss")}.txt";
            using (var sw = new System.IO.StreamWriter(path))
            {

                for (int i = 0; i < rows; i++) {
                    for (int j = 0; j < cols; j++) {
                        sw.Write(result[i, j].ToString() + " ");
                    }
                    sw.WriteLine();
                }
            }

            Array.Clear(matrixResult);
            MessageBox.Show($"Arquivo criado {path}");
        }

        private void MultiplyMatrixes(double[,] matrixA, double [,] matrixB, int index, int len)
        {
            int rows = matrixA.GetLength(0);
            int cols = matrixA.GetLength(1);
            
            

            // Create the result matrix with appropriate dimensions (rowsA x colsB)
            double[,] resultMatrix = new double[rows, cols];

            // Perform matrix multiplication
            for (int i = 0; i < rows; i++)      // Loop through rows of matrix A
            {
                for (int j = 0; j < cols; j++)   // Loop through columns of matrix B
                {
                    double result = 0;
                    for (int k = 0; k < cols; k++)   // Loop through columns of matrix A / rows of matrix B
                    {
                        result += matrixA[i, k] * matrixB[k, j];
                    }
                    resultMatrix[i, j] = Math.Round(result, 4);
                }
            }

            Array.Copy(resultMatrix, 0, matrixResult, index, len );
        }

        private double[,] ConvertToDouble(string[] fileRead)
        {
            int rows = fileRead.Length; ;
            int collumns = fileRead[0].Split(' ').Length;


            double[,] matrix = new double[rows, collumns];

            for (int i = 0; i < rows; i++)
            {

                var lineReplaced = fileRead[i].Replace('.', ',');
                var lineDouble = lineReplaced.Split(' ');

                // Convert each item in the line to a double and store it in the array
                for (int j = 0; j < collumns; j++)
                {
                    matrix[i, j] = Convert.ToDouble(lineDouble[j]);
                }

            }

            return matrix;
        }

        private string[] ReadFile(string fileMatrix)
        {
            return File.ReadAllLines(fileMatrix);
        }
    }
}

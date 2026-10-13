using System.Diagnostics;

namespace Busqueda_Binaria
{
    public partial class Form1 : Form
    {
        private TextBox txtNumbers = null!;
        private TextBox txtTarget = null!;
        private Button btnSearch = null!;
        private Button btnGenerateSmall = null!;
        private Button btnGenerateMassive = null!;
        private Label lblResult = null!;
        private Label lblTime = null!;
        private Label lblBigO = null!;
        private TextBox txtExplanation = null!;

        private int[] currentArray = Array.Empty<int>();
        private Random random = new Random();

        public Form1()
        {
            InitializeComponentCustom();
            GenerateSmallArray();
        }

        private void InitializeComponentCustom()
        {
            this.Text = "Busqueda Binaria con Arreglos Aleatorios en C#";
            this.Width = 650;
            this.Height = 620;
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lbl1 = new Label() { Text = "Elementos del Arreglo (Generados Aleatoriamente y Ordenados):", Top = 20, Left = 20, Width = 400 };
            txtNumbers = new TextBox() { Text = "", Top = 45, Left = 20, Width = 590, Multiline = true, Height = 40 };

            btnGenerateSmall = new Button() { Text = "Generar Pequeno Aleatorio (15)", Top = 95, Left = 20, Width = 210, Height = 28 };
            btnGenerateSmall.Click += BtnGenerateSmall_Click;

            btnGenerateMassive = new Button() { Text = "Generar Masivo Aleatorio (1 Millon)", Top = 95, Left = 240, Width = 220, Height = 28 };
            btnGenerateMassive.Click += BtnGenerateMassive_Click;

            Label lbl2 = new Label() { Text = "Numero a buscar (Target):", Top = 135, Left = 20, Width = 200 };
            txtTarget = new TextBox() { Text = "", Top = 160, Left = 20, Width = 150 };

            txtTarget.KeyPress += TxtTarget_KeyPress;

            btnSearch = new Button() { Text = "Ejecutar Busqueda Binaria", Top = 160, Left = 190, Width = 230, Height = 25 };
            btnSearch.Click += BtnSearch_Click;

            lblResult = new Label() { Text = "Resultado: -", Top = 200, Left = 20, Width = 590, Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold) };
            lblTime = new Label() { Text = "Tiempo de ejecucion real (T): -", Top = 225, Left = 20, Width = 590 };
            lblBigO = new Label() { Text = "Complejidad Asintotica (Big-O): -", Top = 250, Left = 20, Width = 590, Font = new System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Bold) };

            Label lblExpTitle = new Label() { Text = "Explicacion Detallada de T y Big-O:", Top = 285, Left = 20, Width = 400, Font = new System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Bold) };
            txtExplanation = new TextBox()
            {
                Top = 310,
                Left = 20,
                Width = 590,
                Height = 250,
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Text =
@"=== QUE ES T (TIEMPO DE EJECUCION REAL)? ===
• T representa el tiempo fisico exacto que le toma al procesador (CPU) de tu computadora llevar a cabo la operacion de busqueda en milisegundos o microsegundos.
• Se mide utilizando la clase de alta precision 'Stopwatch' de C#.
• A diferencia de Big-O, T cambia dependiendo del hardware de tu equipo, la carga del sistema operativo, el lenguaje de programacion y el tamano exacto de 'n'.

=== QUE ES BIG-O O(log n) (COMPLEJIDAD TEORICA)? ===
• Big-O describe el comportamiento de crecimiento del algoritmo conforme el tamano del conjunto de datos ('n') tiende al infinito, ignorando factores externos como la velocidad del procesador.
• La Busqueda Binaria tiene una complejidad de O(log n) (Logaritmo en base 2).
• Por que O(log n)? Porque en cada iteracion del algoritmo, el espacio de busqueda se divide exactamente a la mitad:
  - Si n = 15 elementos, toma como maximo ~4 pasos.
  - Si n = 1,000,000 de elementos, toma como maximo solo ~20 pasos.
• Esto demuestra que es altamente escalable y eficiente para colecciones masivas de datos en comparacion con una busqueda lineal O(n) que requeriria hasta 1 millon de pasos."
            };

            this.Controls.Add(lbl1);
            this.Controls.Add(txtNumbers);
            this.Controls.Add(btnGenerateSmall);
            this.Controls.Add(btnGenerateMassive);
            this.Controls.Add(lbl2);
            this.Controls.Add(txtTarget);
            this.Controls.Add(btnSearch);
            this.Controls.Add(lblResult);
            this.Controls.Add(lblTime);
            this.Controls.Add(lblBigO);
            this.Controls.Add(lblExpTitle);
            this.Controls.Add(txtExplanation);
        }

        private void TxtTarget_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; 
            }
        }

        private void GenerateSmallArray()
        {
            currentArray = Enumerable.Range(0, 15)
                                     .Select(_ => random.Next(1, 101))
                                     .ToArray();

            Array.Sort(currentArray);
            txtNumbers.Text = string.Join(", ", currentArray);

            int targetIndex = random.Next(0, currentArray.Length);
            txtTarget.Text = currentArray[targetIndex].ToString();

            lblResult.Text = "Resultado: Arreglo pequeno aleatorio generado y ordenado.";
            lblTime.Text = "Tiempo de ejecucion real (T): -";
            lblBigO.Text = "Complejidad Asintotica (Big-O): O(log 15) =~ 4 operaciones maximas.";
        }

        private void BtnGenerateSmall_Click(object? sender, EventArgs e)
        {
            GenerateSmallArray();
        }

        private void BtnGenerateMassive_Click(object? sender, EventArgs e)
        {
            int n = 1000000;
            currentArray = new int[n];
            int currentVal = 0;
            for (int i = 0; i < n; i++)
            {
                currentVal += random.Next(1, 4);
                currentArray[i] = currentVal;
            }

            txtNumbers.Text = "[Arreglo masivo de 1,000,000 de elementos aleatorios cargado en memoria]";

            int randomTargetIndex = random.Next(0, n);
            int target = currentArray[randomTargetIndex];
            txtTarget.Text = target.ToString();

            Stopwatch sw = Stopwatch.StartNew();
            int index = BinarySearch(currentArray, target);
            sw.Stop();

            lblResult.Text = $"Encontrado en arreglo masivo! El numero {target} esta en el indice: {index}";
            lblResult.ForeColor = System.Drawing.Color.DarkGreen;

            double microseconds = (double)sw.ElapsedTicks / (Stopwatch.Frequency / 1000000.0);
            lblTime.Text = $"Tiempo de ejecucion real (T): {sw.ElapsedMilliseconds} ms ({microseconds:F2} us / {sw.ElapsedTicks} ticks)";
            lblBigO.Text = $"Complejidad Asintotica (Big-O): O(log2 1,000,000) =~ 20 iteraciones maximas.";
        }

        private void BtnSearch_Click(object? sender, EventArgs e)
        {
            try
            {
                if (currentArray == null || !txtNumbers.Text.Contains("["))
                {
                    currentArray = txtNumbers.Text.Split(',')
                                                  .Select(s => int.Parse(s.Trim()))
                                                  .ToArray();
                    Array.Sort(currentArray);
                    txtNumbers.Text = string.Join(", ", currentArray);
                }

                int target = int.Parse(txtTarget.Text.Trim());

                Stopwatch stopwatch = Stopwatch.StartNew();
                int index = BinarySearch(currentArray, target);
                stopwatch.Stop();

                int n = currentArray.Length;
                double maxSteps = Math.Ceiling(Math.Log(n, 2));

                if (index != -1)
                {
                    lblResult.Text = $"Encontrado! El numero {target} esta en el indice: {index}";
                    lblResult.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblResult.Text = $"El numero {target} no se encuentra en el arreglo.";
                    lblResult.ForeColor = System.Drawing.Color.Red;
                }

                long ticks = stopwatch.ElapsedTicks;
                double microseconds = (double)stopwatch.ElapsedTicks / (Stopwatch.Frequency / 1000000.0);

                lblTime.Text = $"Tiempo de ejecucion real (T): {stopwatch.ElapsedMilliseconds} ms ({microseconds:F2} microsegundos)";
                lblBigO.Text = $"Complejidad Asintotica (Big-O): O(log2 {n}) -> Tomo maximo {maxSteps} divisiones logicas.";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en los datos de entrada. Asegurate de generar un arreglo o ingresar numeros validos.\nDetalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int BinarySearch(int[] arr, int target)
        {
            int left = 0;
            int right = arr.Length - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;

                if (arr[mid] == target)
                    return mid;

                if (arr[mid] < target)
                    left = mid + 1;
                else
                    right = mid - 1;
            }

            return -1;
        }
    }
}
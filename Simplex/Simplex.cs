using Simplex.SMath;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simplex
{
    public partial class Simplex : Form
    {
        private const double M = 100000000000.0;

        private Vec C;
        private List<string> varNames;
        private List<string> exprSigns;

        private Mat A;

       
        private List<int> basicVariablesRows;

        private string target;

        private Vec Z;
        private Vec Q;
        private Vec result;

        double basSum = 0;

        int resolvingColumn = 0;
        int resolvingRow = 0;

        double resolvingElemValue = 0;

        List<bool> isNonNegative;
         int[] wVariables;  //

        int wNum;
        public Simplex(int variablesCount, int limitsCount)
        {
            InitializeComponent();
            VariablesCount = variablesCount;
            LimitsCount = limitsCount;
            IsLastTable = false;
            HasSolutions = false;
            DrawTables();

        }
        #region Properties

        public int VariablesCount { get; }
        public int LimitsCount { get; }
        public bool IsLastTable { get; private set; }
        public bool HasSolutions { get; private set; }

        #endregion
        private void ClearTables()
        {
            dataGridView1
                .Rows
                .Clear();

            dataGridView1
                .Columns
                .Clear();


        }

        private void ReDrawTables()
        {
            ClearTables();

            for (int i = 0; i < VariablesCount + 1; i++)
            {
                dataGridView1
                    .Columns
                    .Add($"{i}", "");
            }

            dataGridView1
                .Rows
                .Add(LimitsCount + 2);

            dataGridView1[VariablesCount, LimitsCount + 1].Value = "Δj";

            for (int i = 0; i < VariablesCount; i++)
            {
                dataGridView1[i, 0].Value = $"x{i + 1}";
            }

            dataGridView1[VariablesCount, 0].Value = "Qi";

            dataGridView1
                .Columns[VariablesCount]
                .HeaderText = $"Cj";
            SetColors(Color.LightGray);
        }

        public void DrawTables()
        {
            for (int i = 0; i < VariablesCount + 1; i++)
            {
                dataGridView1
                    .Columns
                    .Add($"{i}", "");
            }

            dataGridView1
                .Rows
                .Add(LimitsCount + 2);

            dataGridView1[VariablesCount, LimitsCount + 1].Value = "Δj";

            for (int i = 0; i < VariablesCount; i++)
            {
                dataGridView1[i, 0].Value = $"x{i + 1}";
            }

            dataGridView1[VariablesCount, 0].Value = "Qi";

            dataGridView1
                .Columns[VariablesCount]
                .HeaderText = $"Cj";


            for (int i = 0; i < VariablesCount + 2; i++)
            {
                dataGridView3.Columns.Add("", "");

                if (i < VariablesCount)
                    dataGridView3.Columns[i].HeaderText = $"x{i + 1}";

                if (i == VariablesCount)
                    dataGridView3.Columns[i].HeaderText = $"Знак";

                if (i == VariablesCount + 1)
                    dataGridView3.Columns[i].HeaderText = $"БР";
            }

            dataGridView3
                .Rows
                .Add(LimitsCount + 2);

            for (int i = 0; i < LimitsCount; i++)
            {
                var cell = new DataGridViewComboBoxCell();
                cell.Items.AddRange("≥", "=", "≤");
                dataGridView3[VariablesCount, i] = cell;
            }

            for (int i = 0; i < VariablesCount; i++)
            {
                var checkBoxCell = new DataGridViewCheckBoxCell();
                checkBoxCell.FalseValue = "false";
                checkBoxCell.TrueValue = "true";
                checkBoxCell.Value = "true";

                dataGridView3[i, LimitsCount].Value = $"x{i + 1} ≥ 0";
                dataGridView3[i, LimitsCount + 1] = checkBoxCell;

            }

            for (int i = 0; i < VariablesCount + 2; i++)
            {
                dataGridView4.Columns.Add("", "");

                if (i < VariablesCount)
                    dataGridView4.Columns[i].HeaderText = $"x{i + 1}";

                if (i == VariablesCount)
                    dataGridView4.Columns[i].HeaderText = $"C";

                if (i == VariablesCount + 1)
                    dataGridView4.Columns[i].HeaderText = $"Цель";
            }

            dataGridView4
                .Rows
                .Add();

            var targetCell = new DataGridViewComboBoxCell();
            targetCell.Items.AddRange("max", "min");
            dataGridView4[VariablesCount + 1, 0] = targetCell;


            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            for (int i = 0; i < dataGridView3.Columns.Count; i++)
            {
                dataGridView3.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            for (int i = 0; i < dataGridView4.Columns.Count; i++)
            {
                dataGridView4.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            SetColors(Color.LightGray);
        }

        private void SetColors(Color color)
        {
            dataGridView1[VariablesCount, LimitsCount + 1]
                .Style
                .BackColor = color;

            for (int i = 0; i < VariablesCount; i++)
            {
                dataGridView1[i, 0]
                    .Style
                    .BackColor = color;
            }

            dataGridView1[VariablesCount, 0]
                .Style
                .BackColor = color;

            for (int row = LimitsCount; row < LimitsCount + 2; row++)
            {
                for (int column = 0; column < VariablesCount; column++)
                {
                    dataGridView3[column, row]
                        .Style
                        .BackColor = color;
                }
            }
        }

        private void SetText()
        {
            for (int ci = 1; ci <= C.VariablesCount; ci++)
            {
                dataGridView1
                    .Columns[ci - 1]
                    .HeaderText = C[ci].ToString();
            }

            for (int i = 0; i < LimitsCount; i++)
            {
                for (int j = 0; j < A.VariablesCount; j++)
                {
                    dataGridView1[j, i + 1].Value = A[i, j + 1].ToString();
                }
            }

        }

        private List<bool> IsNonNegative()
        {
            var isNonNegativeArr = new bool[A.VariablesCount + 1];

            for (int i = 1; i <= A.VariablesCount; i++)
            {
                if (dataGridView3[i - 1, LimitsCount + 1].Value.ToString() == "true")
                    isNonNegativeArr[i] = true;
            }

            isNonNegative = new List<bool>(isNonNegativeArr);

            return isNonNegative;
        }

        private bool IsRightPartsNonNegative(int row)
        {
            if (-A[row][0] >= 0) return true;
            else return false;
        }

        private void NegateExpression(int row)
        {
            var oldExpr = A[row].ExprView(varNames);

            A[row] = A[row].Mul(-1);

            textBox1.Text += $"Правая часть выражения {oldExpr} отрицательная. Умножим обе части на -1:" +
                    Environment.NewLine +
                    $"\t{A[row].ExprView(varNames)}" +
                    Environment.NewLine +
                    Environment.NewLine;
        }

        private void ResearchExpressions()
        {
            for (int row = 0; row < A.RowsCount; row++)
            {
                if (A[row][basicVariablesRows[row]] < 0)
                {
                    A.AddVariable(row, 1);
                }
            }
        }

        private void UpdateTable()
        {
            for (int i = 1; i <= A.VariablesCount; i++)
            {
                dataGridView1[i - 1, A.RowsCount + 1].Value = Z[i].ToString();
            }
            for (int row = 0; row < A.RowsCount; row++)
            {
                for (int column = 1; column <= A.VariablesCount; column++)
                {
                    dataGridView1[column - 1, row + 1].Value = A[row, column].ToString();
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Font = SystemFonts.MessageBoxFont;
            MinimumSize = Size;
        }

        private void PrintTargetFunction()
        {
            textBox1.Text += $"Целевая функция: {C.FuncView("f(x)", varNames)}"
                + Environment.NewLine
                + Environment.NewLine;
        }

        private void PrintLimits()
        {
            var limits = "";
            for (int row = 0; row < A.RowsCount; row++)
            {
                limits += $"{A.VariablesMatrix[row].ExprView(varNames)}" +
                    Environment.NewLine;
            }

            textBox1.Text += $"Ограничения: " +
                Environment.NewLine +
                limits;

            PrintVariables();
        }

        private void PrintVariables()
        {
            var variables = "";
            for (int variable = 1; variable <= A.VariablesCount; variable++)
            {
                if (isNonNegative[variable])
                {
                    variables += $"{varNames[variable - 1]} ≥ 0, ";
                }
            }

            if (variables != string.Empty)
            {
                textBox1.Text += variables.Substring(0, variables.Length - 2)
                    + Environment.NewLine;
            }

            textBox1.Text += Environment.NewLine;
        }

        public void Initialize()
        {
            ReDrawTables();

            IsLastTable = false;

            exprSigns = new List<string>();
         
            textBox1.Clear();

            var arr = new double[VariablesCount + 1];
            for (int i = 1; i < arr.Length; i++)
            {
                arr[i] = double.Parse(dataGridView4[i - 1, 0].Value.ToString());
            }

            arr[0] = double.Parse(dataGridView4[arr.Length - 1, 0].Value.ToString());

            C = new Vec(arr);

            varNames = new List<string>();
            for (int i = 0; i < C.VariablesCount; i++)
            {
                varNames.Add($"x{i + 1}");
            }

            var vecList = new List<Vec>();

            for (int i = 0; i < LimitsCount; i++)
            {
                var limitArr = new double[VariablesCount + 1];

                for (int j = 1; j <= VariablesCount; j++)
                {
                    limitArr[j] = double.Parse(dataGridView3[j - 1, i].Value.ToString());
                }
                limitArr[0] = -double.Parse(dataGridView3[VariablesCount + 1, i].Value.ToString());
                var vec = new Vec(limitArr);
                vec.Sign = dataGridView3[VariablesCount, i].Value.ToString();
                vecList.Add(vec);
                exprSigns.Add(vec.Sign);
            }
            A = new Mat(vecList);
            isNonNegative = IsNonNegative();
            PrintTargetFunction();
            PrintLimits();
            wNum = 0;
            CanonicalAnalys();

            textBox1.Text += $"Базисные переменные:" + Environment.NewLine;
            for (int i = 0; i < basicVariablesRows.Count; i++)
            {
                var limitVec = vecList[i];
                var basicVarNum = basicVariablesRows[i];

                textBox1.Text += limitVec
                    .Solve(basicVarNum)
                    .FuncView($"{varNames[basicVariablesRows[i] - 1]}", varNames) + Environment.NewLine;
            }

            C = C.GetTagretFunc(vecList, basicVariablesRows);//
            PrintTargetFunction();
            SetText();
        }

        private void MakeBasicSum()
        {
            double sum = 0;

            for (int i = 0; i < A.RowsCount; i++)
            {
                sum += C[basicVariablesRows[i]] * (-A[i, 0]);
            }

            basSum = sum;
        }

        private void SetBasicVariables()
        {
            basicVariablesRows = new List<int>();

            for (int i = 0; i < A.RowsCount; i++)
            {
                basicVariablesRows.Add(-1);
            }

            for (int row = 0; row < A.RowsCount; row++)
            {
                try
                {
                    int basicVar = A.GetBasisVariable(row);

                    if (basicVar != -1)
                    {
                        basicVariablesRows[row] = basicVar;
                    }
                    else
                    {
                        if (!IsRightPartsNonNegative(row))
                        {
                            NegateExpression(row);
                        }

                        MakePrefferedExpressions(row);
                        basicVariablesRows[row] = A.VariablesCount;

                        PrintTargetFunction();
                        PrintLimits();
                    }
                }
                catch (IndexOutOfRangeException)
                {
                }
            }
        }

        private void ChangeBasicVariables()
        {
            basicVariablesRows[resolvingRow] = resolvingColumn;
        }

        private void MakeSumVec()
        {
            Z = new Vec(A.VariablesCount);

            for (int i = 1; i <= Z.VariablesCount; i++)
            {
                double sum = 0;

                for (int j = 0; j < A.RowsCount; j++)
                {
                    sum += C[basicVariablesRows[j]] * A[j, i];
                }

                Z[i] = C[i] - sum;
            }
        }

        private void ResearchingSumVec()
        {
            IList<double> neg = null;
            IList<double> pos = null;

            if (target == "max")
            {
                pos = Z.VariablesList.Where((elem, index) => elem > 0 && index != 0).ToList();

                if (pos.Count == 0)
                {
                    // задача решена

                    IsLastTable = true;
                    HasSolutions = true;
                }
                else
                {
                    double max = pos.Max();
                    resolvingColumn = Z.VariablesList.IndexOf(max);
                }

            }
            else
            {
                neg = Z.VariablesList.Where((elem, index) => elem < 0 && index != 0).ToList();

                if (neg.Count == 0)
                {
                    // задача решена

                    IsLastTable = true;
                    HasSolutions = true;
                }
                else
                {
                    double min = neg.Min();
                    resolvingColumn = Z.VariablesList.IndexOf(min);
                }
            }
        }

        private void ResearchingColumn(int column)
        {
            var vecList = A.VariablesMatrix.ToList();

            var positive = new List<double>();

            foreach (var vec in vecList)
            {
                positive.AddRange(vec.VariablesList.Where((e, i) => e > 0 && i == column));
            }

            if (positive.Count == 0)
            {
                // Это последняя симплексная таблица. Если basictSum содержит M, то не имеет оптимальных решений
                IsLastTable = true;
            }
            else if (positive.Count == 1)
            {
                resolvingElemValue = positive[0];

                int row = 0;
                foreach (var vec in vecList)
                {
                    if (vec.VariablesList.Contains(resolvingElemValue))
                    {
                        if (vec[column] == resolvingElemValue)
                        {
                            resolvingRow = row;
                            break;
                        }
                    }
                    row++;
                }
            }
            else
            {
                FillQColumn();
            }
        }

        private void FillQColumn()
        {
            Q = new Vec(A.RowsCount - 1);

            for (int row = 0; row <= Q.VariablesCount; row++)
            {
                Q[row] = -A[row, 0] / A[row, resolvingColumn];
            }

            for (int row = 0; row <= Q.VariablesCount; row++)
            {
                dataGridView1[VariablesCount, row + 1].Value = Q[row].ToString();
            }
            double min = Q
                .VariablesList
                .Where(e => e > 0)
                .Min();

            resolvingRow = Q
                .VariablesList
                .IndexOf(min);

            resolvingElemValue = A[resolvingRow, resolvingColumn];
        }

        private void Fill()
        {
            var prevMat = A.Clone();
            var prevSumVec = Z.Clone();

            for (int column = 0; column <= A.VariablesCount; column++)
            {
                A[resolvingRow, column] /= resolvingElemValue;
            }

            for (int row = 0; row <= A.RowsCount; row++)
            {
                for (int column = 0; column <= A.VariablesCount; column++)
                {
                    if (row == A.RowsCount)
                    {
                        if (column != 0)
                        {
                            double a = resolvingElemValue * prevSumVec[column];
                            double b = (column != 0 ? prevMat[resolvingRow, column] : -prevMat[resolvingRow, column]) * prevSumVec[resolvingColumn];

                            double sub = a - b;
                            Z[column] = sub / resolvingElemValue;
                        }
                    }
                    else if (row != resolvingRow)
                    {
                        double a = resolvingElemValue * (column != 0 ? prevMat[row, column] : -prevMat[row, column]);
                        double b = (column != 0 ? prevMat[resolvingRow, column] : -prevMat[resolvingRow, column]) * prevMat[row, resolvingColumn];

                        double sub = a - b;
                        A[row, column] = (column != 0 ? sub / resolvingElemValue : -sub / resolvingElemValue);
                    }
                }
            }

            basSum = (basSum * resolvingElemValue - prevSumVec[resolvingColumn]) / resolvingElemValue;
        }

        private bool HasSolution(Vec vec, out Vec result)
        {
            result = null;

            var w = varNames
                .Where((e, i) => e.StartsWith("w"))
                .ToList();

            if (w != null)
            {
                foreach (var wi in w)
                {
                    var value = varNames.IndexOf(wi);
                    if (vec[value + 1] != 0)
                    {
                        return false;
                    }
                }
            }

            var resultArr = new double[VariablesCount + 1];

            Array.Copy(vec.VariablesList.ToArray(), resultArr, VariablesCount + 1);

            result = new Vec(resultArr);
            return true;
        }

        private void Finish()
        {
            result = new Vec(A.VariablesCount);

            for (int row = 0; row < A.RowsCount; row++)
            {
                result[basicVariablesRows[row]] = -A[row, 0];
            }

            if (HasSolution(result, out result))
            {
                textBox1.Text += "Достигнуто оптимальное решение:" +
                    Environment.NewLine +
                    Environment.NewLine +
                $"Оптимальное решение: X = {result}" +
                Environment.NewLine +
                $"Оптимальное значение функции: f(x) = {basSum + C[0]}";
            }
            else
            {
                textBox1.Text += "Задача не имеет оптимальных решений." + Environment.NewLine;
            }
        }

       
        private void ReplaceNegativeVariables()
        {
            int j = 1;
            bool isChanged = false;

            for (int i = 1; i < isNonNegative.Count; i++)
            {
                if (!isNonNegative[i])
                {
                    isChanged = true;

                    var oldName = varNames[i - 1];

                    A.ReplaceVariableWithSub(i);

                    varNames[i - 1] = $"x'{j}";
                    ReplaceDataGridColumn(dataGridView1, $"x'{j}", i - 1, Color.LightGray);

                    varNames.Insert(i, $"x''{j}");
                    InsertDataGridColumn(dataGridView1, $"x''{j}", i, Color.LightGray);



                    C.InsertAndReplace(i);

                    textBox1.Text += $"Переменная {oldName} < 0. Выполним замену:"
                        + Environment.NewLine
                        + $"\t{oldName} = {varNames[i - 1]} - {varNames[i]}"
                        + Environment.NewLine
                        + Environment.NewLine;

                    isNonNegative[i] = true;
                    isNonNegative.Insert(i++, true);
                }

                j++;
            }

            if (isChanged)
            {
                PrintTargetFunction();
                PrintLimits();
            }
        }

        private void MakeExpressions()
        {
            int uNum = 1;
            var noPrefferedRows = new List<int>();
            bool isChanged = false;

            for (int row = 0; row < LimitsCount; row++)
            {
                if (exprSigns[row] != "=")
                {
                    isChanged = true;

                    var oldExpr = A[row].ExprView(varNames);

                    if (exprSigns[row] == "≥")
                    {
                        A.AddVariable(row, -1);
                    }
                    else if (exprSigns[row] == "≤")
                    {
                        A.AddVariable(row, 1);
                    }

                    C.AddVariable(0);

                    var varName = $"u{uNum++}";

                    varNames.Add(varName);
                    InsertDataGridColumn(dataGridView1, varName, A.VariablesCount - 1, Color.LightGray);

                    isNonNegative.Add(true);

                    if (!IsRightPartsNonNegative(row))
                    {
                        NegateExpression(row);
                    }

                    if (!IsExpressionPreffered(row, A.VariablesCount))
                    {
                        noPrefferedRows.Add(row);
                    }

                    var sign = exprSigns[row] == "≥" ? "-" : "+";

                    textBox1.Text += $"Выражение {oldExpr} является неравенством. "
                        + Environment.NewLine
                        + $"Так как знак {exprSigns[row]}, то введём новую переменную {varName} со знаком {sign}:"
                        + Environment.NewLine
                        + Environment.NewLine;

                    A[row].Sign = "=";
                    exprSigns[row] = "=";
                }
                else if (!IsRightPartsNonNegative(row))
                {
                    NegateExpression(row);
                    isChanged = true;
                }
            }

            if (isChanged)
            {
                PrintLimits();
                isChanged = false;
            }

            for (int row = 0; row < noPrefferedRows.Count; row++)
            {
                MakePrefferedExpressions(noPrefferedRows[row]);
                isChanged = true;
            }

            if (isChanged)
            {
                PrintTargetFunction();
                PrintLimits();
            }
        }

        private void MakePrefferedExpressions(int noPrefferedRow)
        {
            var oldExpr = A[noPrefferedRow].ExprView(varNames);

            A.AddVariable(noPrefferedRow, 1);
            C.AddVariable(-M);

            isNonNegative.Add(true);

            var wName = $"w{++wNum}";

            varNames.Add(wName);
            InsertDataGridColumn(dataGridView1, wName, A.VariablesCount - 1, Color.LightGray);

            textBox1.Text += $"В выражении {oldExpr} отсутствуют базисные переменные. "
                + Environment.NewLine
                + $"Введём новую переменную {wName} со коэффициентом -{M}:"
                + Environment.NewLine
                + Environment.NewLine;
        }

        private bool IsExpressionPreffered(int row, int var)
        {
            if (A[row, var] < 0) return false;
            else return true;
        }

        private void CanonicalAnalys()
        {
            ReplaceNegativeVariables();
            MakeExpressions();
            SetBasicVariables();
        }

        private void AddDataGridColumn(DataGridView dataGridView, string columnName, Color color)
        {
            var column = new DataGridViewColumn(new DataGridViewTextBoxCell());

            dataGridView
                .Columns
                .Add(column);

            dataGridView[dataGridView.ColumnCount - 1, 0].Value = columnName;
            dataGridView[dataGridView.ColumnCount - 1, 0]
                .Style
                .BackColor = color;
        }

        private void InsertDataGridColumn(DataGridView dataGridView, string columnName, int index, Color color)
        {
            var column = new DataGridViewColumn(new DataGridViewTextBoxCell());

            dataGridView
                .Columns
                .Insert(index, column);

            dataGridView[index, 0].Value = columnName;
            dataGridView[index, 0]
                .Style
                .BackColor = color;
        }

        private void ReplaceDataGridColumn(DataGridView dataGridView, string columnName, int index, Color color)
        {
            dataGridView[index, 0].Value = columnName;
            dataGridView[index, 0]
                .Style
                .BackColor = color;
        }

        public void Calculate()
        {
            if (dataGridView4[VariablesCount + 1, 0].Value != null)
            {
                target = dataGridView4[VariablesCount + 1, 0].Value.ToString();
            }
            else return;

            if (dataGridView4[VariablesCount + 1, 0].Value.ToString() == "")
            {
                return;
            }
            MakeBasicSum();
            MakeSumVec();
            ResearchingSumVec();
            if (!IsLastTable)
            {
                ResearchingColumn(resolvingColumn);
            }
            UpdateTable();
            while (!IsLastTable)
            {
                ChangeBasicVariables();
                Fill();
                ResearchingSumVec();

                if (!IsLastTable)
                {
                    ResearchingColumn(resolvingColumn);
                }
            }

            // Завершающий этап
            UpdateTable();
            Finish();
        }
       


    }
}

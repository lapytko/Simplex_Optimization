using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplex.SMath
{
    public class Mat
    {
        #region Data

        public IList<Vec> VariablesMatrix;
        #endregion

        #region .ctor

        public Mat(double[,] varArray)
        {
            VariablesMatrix = new List<Vec>();

            for (int i = 0; i < varArray.GetLength(0); i++)
            {
                var vector = new List<double>();

                for (int j = 0; j < varArray.GetLength(1); j++)
                {
                    vector.Add(varArray[i, j]);
                }

                VariablesMatrix.Add(new Vec(vector));
            }
        }

        public Mat(IList<Vec> list)
        {
            VariablesMatrix = list;
        }
        #endregion

        #region Properties

        public int RowsCount
        {
            get { return VariablesMatrix.Count; }
        }

        public int VariablesCount
        {
            get { return VariablesMatrix[0].VariablesCount; }
        }

        public double this[int i, int j]
        {
            get { return VariablesMatrix[i][j]; }
            set { VariablesMatrix[i][j] = value; }
        }

        public Vec this[int i]
        {
            get { return VariablesMatrix[i]; }
            set { VariablesMatrix[i] = value; }
        }
        #endregion

        public int GetBasisVariable(int row)
        {
            for (int column = 1; column <= VariablesCount; column++)
            {
                bool isBasis = true;

                if (this[row, column] > 0)
                {
                    for (int j = 0; j < RowsCount; j++)
                    {
                        if (j != row)
                        {
                            if (this[j, column] != 0)
                            {
                                isBasis = false;
                                break;
                            }
                        }
                    }

                    if (!isBasis) continue;
                    else return column;
                }
            }

            return -1;
        }

        public void AddVariable(int row, double value)
        {
            for (int rowIndex = 0; rowIndex < RowsCount; rowIndex++)
            {
                if (rowIndex == row)
                {
                    this[rowIndex].AddVariable(value);
                }
                else
                {
                    this[rowIndex].AddVariable(0);
                }
            }

            for (int column = 1; column <= VariablesCount; column++)
            {
                bool isBasis = true;

                if (this[row, column] > 0)
                {
                    for (int j = 0; j < RowsCount; j++)
                    {
                        if (j != row)
                        {
                            if (this[j, column] != 0)
                            {
                                isBasis = false;
                                break;
                            }
                        }
                    }

                    if (!isBasis) continue;
                    else return;
                }
            }
        }

        public void ReplaceVariableWithSub(int varNum)
        {
            for (int row = 0; row < RowsCount; row++)
            {
                this[row].InsertAndReplace(varNum);
            }
        }

        public Mat Clone()
        {
            var newVarMat = new List<Vec>();

            foreach (var row in VariablesMatrix)
            {
                newVarMat.Add(row.Clone());
            }

            return new Mat(newVarMat);
        }
    }
}

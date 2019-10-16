using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplex.SMath
{
    public class Vec
    {
        #region .ctor

        public Vec(double[] varArray)
        {
            VariablesList = new List<double>(varArray);
            Sign = "=";
        }

        public Vec(IList<double> list)
        {
            VariablesList = list;
            Sign = "=";
        }

        public Vec(int count)
        {
            VariablesList = new List<double>();

            for (int i = 0; i < count + 1; i++)
            {
                VariablesList.Add(0);
            }

            Sign = "=";
        }
        #endregion

        #region Properties

        /// <summary> Список коэффициентов переменных</summary>
        public IList<double> VariablesList { get; private set; }

        /// <summary> Количество переменных </summary>
        public int VariablesCount
        {
            get { return VariablesList.Count - 1; }
        }

        public double this[int index]
        {
            get { return VariablesList[index]; }
            set { VariablesList[index] = value; }
        }

        public string Sign { get; set; }
        #endregion

        public void AddVariable(double value)
        {
            VariablesList.Add(value);
        }

        public void InsertAndReplace(int varNum)
        {
            VariablesList.Insert(varNum + 1, -this[varNum]);
        }

        public Vec Clone()
        {
            var array = VariablesList.ToArray();
            var result = new Vec(array);
            result.Sign = Sign;

            return result;
        }

        public string ExprView(IList<string> varNames)
        {
            int usedVariables = 0;

            var sb = new StringBuilder();

            for (int i = 1; i <= VariablesCount; i++)
            {
                if (this[i] != 0 && usedVariables != 0)
                {
                    sb.Append($"{CheckSign(this[i], i)}{varNames[i - 1]} ");
                    usedVariables++;
                }
                else if (this[i] != 0)
                {
                    if (this[i] > 0)
                    {
                        if (this[i] != 1)
                            sb.Append($"{this[i]}{varNames[i - 1]} ");
                        else sb.Append($"{varNames[i - 1]} ");
                    }
                    else if (this[0] < 0)
                    {
                        if (this[i] != -1)
                            sb.Append($"-{System.Math.Abs(this[i])}{varNames[i - 1]} ");
                        else sb.Append($"-{varNames[i - 1]} ");
                    }

                    usedVariables++;
                }
            }

            if (this[0] > 0)
            {
                sb.Append($"{Sign} - {this[0]}");
            }
            else if (this[0] < 0)
            {
                sb.Append($"{Sign} {System.Math.Abs(this[0])}");
            }

            return sb.ToString();
        }

        public string FuncView(string funcName, IList<string> varName)
        {
            int usedVariables = 0;

            var sb = new StringBuilder();
            sb.Append($"{funcName} = ");

            for (int i = 1; i <= VariablesCount; i++)
            {
                if (this[i] != 0 && usedVariables != 0)
                {
                    sb.Append($"{CheckSign(this[i], i)}{varName[i - 1]} ");
                    usedVariables++;
                }
                else if (this[i] != 0)
                {
                    if (this[i] > 0)
                    {
                        if (this[i] != 1)
                            sb.Append($"{this[i]}{varName[i - 1]} ");
                        else sb.Append($"{varName[i - 1]} ");
                    }
                    else if (this[i] < 0)
                    {
                        if (this[i] != -1)
                            sb.Append($"-{System.Math.Abs(this[i])}{varName[i - 1]} ");
                        else sb.Append($"-{varName[i - 1]} ");
                    }

                    usedVariables++;
                }
            }

            if (this[0] > 0)
            {
                if (usedVariables >= 1)
                    sb.Append($"+ {this[0]}");
                else
                    sb.Append($" {this[0]}");
            }
            else if (this[0] < 0)
            {
                sb.Append($"- {System.Math.Abs(this[0])}");
            }

            return sb.ToString();
        }

        #region Math Methods

        public double Solve(Vec solution)
        {
            if (VariablesCount != solution.VariablesCount) return double.NaN;

            double result = 0;
            for (int i = 1; i <= VariablesCount; i++)
            {
                result += this[i] * solution[i];
            }

            return result + this[0];
        }

        public Vec Solve(int varNum)
        {
            double ratio = 1 / this[varNum];

            Vec result = new Vec(VariablesList.ToArray());
            result.Sign = Sign;

            for (int i = 0; i <= VariablesCount; i++)
            {
                if (i == varNum)
                {
                    result[i] = 0;
                    continue;
                }

                result[i] *= -ratio;
            }

            return result;
        }

        public Vec Mul(double ratio)
        {
            Vec result = new Vec(VariablesList.ToArray());
            result.Sign = Sign;

            for (int i = 0; i <= VariablesCount; i++)
            {
                result[i] *= ratio;
            }

            return result;
        }

        public Vec AddFunc(Vec vec)
        {
            Vec result = new Vec(VariablesList.ToArray());
            result.Sign = Sign;

            for (int i = 0; i <= VariablesCount; i++)
            {
                result[i] += vec[i];
            }

            return result;
        }

        public Vec ReplaceVarWithVec(Vec vec, int basicVarNumber)
        {
            Vec result = new Vec(VariablesList.ToArray());
            result.Sign = Sign;

            result = result.AddFunc(vec.Mul(result[basicVarNumber]));
            result[basicVarNumber] = 0;

            return result;
        }

        public Vec GetTagretFunc(IList<Vec> vecList, IList<int> basicVarNumbers)
        {
            if (basicVarNumbers == null) return null;

            Vec target = new Vec(VariablesList.ToArray());
            target.Sign = Sign;

            for (int i = 0; i < vecList.Count; i++)
            {
                var limit = vecList[i].Solve(basicVarNumbers[i]);
                target = target.ReplaceVarWithVec(limit, basicVarNumbers[i]);
            }

            return target;
        }

        #endregion

        public override string ToString()
        {
            var sb = new StringBuilder();

            for (int i = 1; i <= VariablesCount; i++)
            {
                sb.Append(this[i] + ", ");
            }

            return "{ " + sb.ToString().Substring(0, sb.Length - 2) + " }";
        }
        #region Private Methods

        private string CheckSign(double val, int i)
        {
            var c = string.Empty;

            if (val > 0)
            {
                if (val != 1)
                {
                    if (i > 1)
                    {
                        c = "+ " + val.ToString();
                    }
                    else
                    {
                        c = val.ToString();
                    }
                }
                else
                {
                    if (i > 1)
                    {
                        c = "+ ";
                    }
                }

            }
            else if (val < 0)
            {
                if (val != -1)
                {
                    c = "- " + System.Math.Abs(this[i]).ToString();
                }
                else
                {
                    c = "- ";
                }
            }

            return c;
        }
        #endregion
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplex.SMath
{
    class Expr
    {
        public Expr(double[] leftExpr, double[] rightExpr, string sign)
        {
            LeftExpr = new Vec(leftExpr);
            RightExpr = new Vec(rightExpr);
            Sign = sign;
        }

        public Expr(Vec leftExpr, Vec rightExpr, string sign)
        {
            LeftExpr = leftExpr;
            RightExpr = rightExpr;
            Sign = sign;
        }

        public Vec LeftExpr { get; set; }
        public Vec RightExpr { get; set; }
        public string Sign { get; }
        public int VariablesCount { get { return LeftExpr.VariablesCount; } }


        public Vec Solve(int varIndex)
        {
            var result = GetSimilar();

            return Divide(result, varIndex);
        }

        private Vec GetSimilar()
        {
            var result = new Vec(VariablesCount);

            for (int i = 0; i < VariablesCount + 1; i++)
            {
                result[i] += LeftExpr[i];
                result[i] -= RightExpr[i];
            }

            return result;
        }

        private Vec Divide(Vec vec, int varIndex)
        {
            for (int i = 0; i < VariablesCount; i++)
            {
                vec[i] /= RightExpr[varIndex] - LeftExpr[varIndex];
            }

            return vec;
        }

        public Vec ToVec()
        {
            return GetSimilar();
        }
    }
}

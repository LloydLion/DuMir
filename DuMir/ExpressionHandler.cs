using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuMir
{
	class ExpressionHandler
	{
		private readonly static List<Operator> operators = new List<Operator>
		{
			new Operator("+", (a, b) => a + b),
			new Operator("-", (a, b) => a - b),
			new Operator("/", (a, b) => a / b),
			new Operator("*", (a, b) => a * b),
			new Operator("==", (a, b) => a == b),
			new Operator("!=", (a, b) => a != b),
			new Operator("&", (a, b) => a && b),
			new Operator("|", (a, b) => a || b),
			new Operator(">", (a, b) => a > b),
			new Operator(">=", (a, b) => a >= b),
			new Operator("<", (a, b) => a < b),
			new Operator("<=", (a, b) => a <= b),
			new Operator("recast", (a, b) => Static.ShortTypeParsers[b as string].Invoke((a as object).ToString())),
			new Operator("is", (a, b) => (b as object).GetType().IsInstanceOfType(a)),
		};

		private readonly Operator selectedOperator;
		private readonly string operand1, operand2;
		private readonly bool isPrimitive = false;


		public ExpressionHandler(string expression)
		{
			var list = SmartSplit(expression);

			if (list.Count == 1)
			{
				isPrimitive = true;
				operand1 = list[0];
			}
			else
			{
				operand1 = list[0];
				operand2 = list[2];

				selectedOperator = operators.Find((s) => s.Define == list[1]);
			}
		}


		private class Operator
		{
			public string Define { get; }
			public Func<object, object, object> OpFunc { get; }


			public Operator(string define, Func<dynamic, dynamic, dynamic> opFunc)
			{
				Define = define;
				OpFunc = opFunc;
			}


			public object Apply(object a, object b) => OpFunc.Invoke(a, b);
		}


		private static object GetValueFromOperand(string operand, InterpretatorContext ctx)
		{
			if(operand.StartsWith("$"))
			{
				return ctx.Variables.Single(s => s.Name == operand[1..]).CurrentValue;
			}
			else if(operand.StartsWith("@"))
			{
				throw new NotImplementedException();
			}
			else if(operand.StartsWith("|"))
			{
				var parts = operand[1..].Split("|");
				var type = parts[0];
				var value = parts[1];

				return Static.ShortTypeParsers[type].Invoke(value);
			}
			else if(operand.StartsWith("[") && operand.EndsWith("]"))
			{
				return new ExpressionHandler(operand[1..^1]).Run(ctx);
			}
			else
			{
				throw new ArgumentException("Invalid operand string", nameof(operand));
			}
		}

		private static IList<string> SmartSplit(string expression)
		{
			var ss = expression.Split(" ");

			int isInnerOpCounter = 0;
			List<StringBuilder> chunks = new List<StringBuilder>();

			foreach(var ssitem in ss)
			{
				if(isInnerOpCounter == 0) chunks.Add(new StringBuilder());


				var prefix = chunks[^1].Length == 0 ? "" : " ";
				chunks[^1].Append(prefix + ssitem);

				isInnerOpCounter += ssitem.Where(s => s == '[').Count();
				isInnerOpCounter -= ssitem.Where(s => s == ']').Count();
			}

			return chunks.Select(s => s.ToString()).ToList();
		}

		public object Run(InterpretatorContext ctx)
		{
			if (isPrimitive == true)
			{
				return GetValueFromOperand(operand1, ctx);
			}
			else
			{
				var value1 = GetValueFromOperand(operand1, ctx);
				var value2 = GetValueFromOperand(operand2, ctx);

				return selectedOperator.Apply(value1, value2);
			}
		}
	}
}

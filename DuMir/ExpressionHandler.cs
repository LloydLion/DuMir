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
			new Operator("cast", (a, b) => Static.ShortTypeParsers[b as string].Invoke((a as object).ToString())),
		};

		private readonly Operator selectedOperator;


		public ExpressionHandler(string targetOperator)
		{
			selectedOperator = operators.Find((s) => s.Define == targetOperator);
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
				var parts = operand[1..^2].Split(" ");

				return new ExpressionHandler(parts[1]).Run(parts[0], parts[2], ctx);
			}
			else
			{
				throw new ArgumentException("Invalid operand string", nameof(operand));
			}
		}

		public object Run(string operand1, string operand2, InterpretatorContext ctx)
		{
			var value1 = GetValueFromOperand(operand1, ctx);
			var value2 = GetValueFromOperand(operand2, ctx);

			return selectedOperator.Apply(value1, value2);
		}
	}
}

using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace frontendlib.Base
{
    public abstract class FieldInputBase<T> : InputBase<T>
    {
        [Parameter] public string Id { get; set; }
        [Parameter] public string Label { get; set; }
        [Parameter] public Expression<Func<T>> Validation { get; set; }

        protected override bool TryParseValueFromString(string value, out T result, out string validationErrorMessage)
        {
            Type paramType = typeof(T);
            switch (paramType.FullName)
            {
                case "System.String":
                    result = (T)(object)value; break;
                case "System.Int32":
                    result = (T)(object)int.Parse(value); break;
                default:
                    throw new NotSupportedException($"FieldInputBase does not support the type {paramType}");
            }
            validationErrorMessage = null;
            return true;
        }
    }
}

/*
Type paramType = typeof(T);
            switch (paramType.Name)
            {
                case "System.String":
                    result = (T)(object)value; break;
                case "System.Int":
                    int.Parse(value);
                default:
                    throw new NotSupportedException($"FieldInputBase does not support the type {paramType}");
            }
            */
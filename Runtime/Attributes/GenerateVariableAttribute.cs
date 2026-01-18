using System;

namespace YanickSenn.CodeGen.Attributes {

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum, Inherited = false)]
    public class GenerateVariableAttribute : Attribute {
        public bool Generate { get; set; } = true;
    }
}

using System;

namespace YanickSenn.CodeGen.Attributes {

    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class GenerateInjectionRegistryAttribute : Attribute {
        public bool Generate { get; set; } = true;
    }
}

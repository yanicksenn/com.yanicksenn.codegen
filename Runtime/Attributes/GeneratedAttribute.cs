using System;

namespace YanickSenn.CodeGen.Attributes {

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface, Inherited = false)]
    public class GeneratedAttribute : Attribute { }
}

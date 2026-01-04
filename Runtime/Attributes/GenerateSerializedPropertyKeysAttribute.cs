using System;

namespace YanickSenn.CodeGen.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
    public class GenerateSerializedPropertyKeysAttribute : Attribute
    {
    }
}
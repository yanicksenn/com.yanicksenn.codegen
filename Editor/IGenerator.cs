namespace YanickSenn.CodeGen.Editor
{
    public interface IGenerator {
        void Generate();
        void Clear();

        bool ShouldRegenerateForAsset(string assetPath) {
            return false;
        }

        bool ShouldRegenerateForScriptCompilation() {
            return false;
        }
    }
}

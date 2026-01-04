namespace YanickSenn.CodeGen.Editor
{
    public interface IGenerator {
        void Generate();
        void Clear();

        bool ShouldRetriggerGenerationForAsset(string assetPath) {
            return false;
        }
    }
}
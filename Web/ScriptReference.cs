namespace Web
{
    public class ScriptReference
    {
        public string ScriptPath { get; private set; }
        public int IncludeOrderPriorty { get; private set; }

        public ScriptReference(string scriptPath, int includeOrderPriorty = 0)
        {
            ScriptPath = scriptPath;
            IncludeOrderPriorty = includeOrderPriorty;
        }
    }
}
using System.Collections.Generic;

namespace Web
{
    public interface IScriptManager
    {
        List<ScriptReference> Scripts { get; }
        List<string> ScriptTexts { get; }
        void AddScriptText(string scriptTextExecute);
        void AddScript(ScriptReference script);
    }
}

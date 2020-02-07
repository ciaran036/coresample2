using System.Collections.Generic;
using System.Linq;

namespace Web
{
    /// <summary>
    /// See Pluralsight training here: https://app.pluralsight.com/course-player?clipId=1f5eadc9-ee68-43e5-8889-83b86df919bf
    /// See example of usage here: https://github.com/pkellner/pluralsight-course-aspnet-taghelpers-viewcomponents 
    /// </summary>
    public class ScriptManager : IScriptManager
    {
        // getter only prop retrieves scripts
        // this is the filenames (or URL's) of the script tags
        private readonly List<ScriptReference> _scripts = new List<ScriptReference>();

        public List<ScriptReference> Scripts => _scripts;
        public List<string> ScriptTexts { get; } = new List<string>();

        public void AddScript(ScriptReference scriptReference)
        {
            if (Scripts.All(x => x.ScriptPath != scriptReference.ScriptPath))
                _scripts.Add(scriptReference);
        }

        public void AddScriptText(string scriptTextExecute)
        {
            if (!ScriptTexts.Contains(scriptTextExecute))
                ScriptTexts.Add(scriptTextExecute);
        }
    }
}
// @GHInput: Num (int) [Access=Item]
// @GHInput: Str (string) [Access=Item]
// @GHOutput: Result

using System.Linq;
using System.Drawing;

public class Script_Instance : GH_ScriptInstance
{
    #region Notes
    /* 
      Members:
        RhinoDoc RhinoDocument
        GH_Document GrasshopperDocument
        IGH_Component Component
        int Iteration

      Methods (Virtual & overridable):
        Print(string text)
        Print(string format, params object[] args)
        Reflect(object obj)
        Reflect(object obj, string method_name)
    */
    #endregion

    private void RunScript(int Num, string Str, ref object Result)
    {
        // Write your logic here
        Result = string.Join(", ", Enumerable.Repeat(Str, Num));
    }
}


// @GHInput: A (int) [Access=List]
// @GHInput: B (double) [Access=Item]
// @GHOutput: R1 (No Type Hint)
// @GHOutput: R2 (No Type Hint)

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

    private void RunScript(List<int> A, double B, ref object R1, ref object R2)
    {
        // Write your logic here
        R1 = (A == null) ? "null" : string.Join(" & ", A);
        R2 = (B == null) ? "null" : B.ToString();
    }
}


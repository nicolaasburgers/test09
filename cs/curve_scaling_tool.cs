// @GHInput: Crv (Rhino.Geometry.Curve) [Access=Item]
// @GHInput: Scale (double) [Access=Item]
// @GHInput: Comment (string) [Access=Item]
// @GHOutput: Geo

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

    private void RunScript(Curve Crv, double Scale, string Comment, ref object Geo)
    {
        // Write your logic here
        if (Crv == null) Geo = null;
        else
        {
            Crv.Scale(Scale);
            Geo = Crv;
        }
        Print(Comment);
    }
}


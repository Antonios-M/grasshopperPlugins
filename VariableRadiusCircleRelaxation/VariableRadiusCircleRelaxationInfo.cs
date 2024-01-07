using Grasshopper;
using Grasshopper.Kernel;
using System;
using System.Drawing;

namespace VariableRadiusCircleRelaxation
{
  public class VariableRadiusCircleRelaxationInfo : GH_AssemblyInfo
  {
    public override string Name => "VariableRadiusCircleRelaxation";

    //Return a 24x24 pixel bitmap to represent this GHA library.
    public override Bitmap Icon => null;

    //Return a short string describing the purpose of this GHA library.
    public override string Description => "";

    public override Guid Id => new Guid("f1766cec-4947-4f77-8368-60100cf58ebf");

    //Return a string identifying you or your company.
    public override string AuthorName => "";

    //Return a string representing your preferred contact details.
    public override string AuthorContact => "";
  }
}
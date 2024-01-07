using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using MainFunc;
using System.Security.Cryptography.X509Certificates;

namespace VariableRadiusCircleRelaxation
{
  public class VariableRadiusCircleRelaxationComponent : GH_Component
  {
    private Cloud myCloud;
    public VariableRadiusCircleRelaxationComponent()
      : base("VariableRadiusCircleRelaxation", "CirRlx",
        "'relax' a cloud of circles relative to the collision of each circle to neighbouring circles.",
        "Paws", "Shapes")
    {
    }
    protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
    {
      pManager.AddBooleanParameter("button", "B", "press to reset", GH_ParamAccess.item);
      pManager.AddPointParameter("centers", "c", "list of centers of circlea", GH_ParamAccess.list);
      pManager.AddNumberParameter("radii", "r", "list of each specified radius", GH_ParamAccess.list);
    }
    protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
    {
      pManager.AddPointParameter("newCenters", "c", "moved centers", GH_ParamAccess.list);
    }
    protected override void SolveInstance(IGH_DataAccess DA)
    {
      Cloud returnCloud;
      bool reset = false;
      List<Point3d> inodes = new List<Point3d>();
      List<double> iradii = new List<double>();

      DA.GetData(0, ref reset);
      DA.GetDataList(1, inodes);
      DA.GetDataList(2, iradii);

      if (reset)
      {
          myCloud = new Cloud(inodes, iradii);
      }
    myCloud.Update();
    returnCloud = myCloud;
    DA.SetDataList(0, returnCloud.nodes);
    }
    protected override System.Drawing.Bitmap Icon
    {
      get
      {
        return Properties.Resources.logo;
      }
    }
    public override Guid ComponentGuid => new Guid("53db2635-9de4-47db-bdea-98e8227b9612");
  }
}
using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace NFP
{
    public class NFPComponent : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public NFPComponent()
          : base("NextFurtherPoints", 
                "NFP",
                "recursively finds the next n furthest points",
                "Paws", 
                "points")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPointParameter("candidates", "c", "possible positions of new points", GH_ParamAccess.list);
            pManager.AddPointParameter("obstacles", "o", "obstacle points", GH_ParamAccess.list);
            pManager.AddIntegerParameter("steps", "n", "number of steps", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddPointParameter("centres", "c", "new centres", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<Point3d> c = new List<Point3d>();
            List<Point3d> o = new List<Point3d>();
            int s = new int();
            DA.GetDataList(0, c);
            DA.GetDataList(1, o);
            DA.GetData(2, ref s);

            List<Point3d> supps(int steps, List<Point3d> candidates, List<Point3d> obstacles)
            {
                if (obstacles.Count == steps)
                {
                    return obstacles;
                }

                List<Point3d> newObstacles = new List<Point3d>();
                foreach (Point3d point in obstacles)
                {
                    newObstacles.Add(point);
                }

                int newIdx = 0;

                double maxGap = 0.0;

                for (int i = 0; i < candidates.Count; i++)
                {
                    double gap = 1.0;
                    for (int j = 0; j < obstacles.Count; j++)
                    {
                        gap *= candidates[i].DistanceTo(obstacles[j]);
                    }
                    if (gap > maxGap)
                    {
                        maxGap = gap;
                        newIdx = i;
                    }
                }

                newObstacles.Add(candidates[newIdx]);

                return supps(steps, candidates, newObstacles);
            }

            List<Point3d> newSupps = supps(s + o.Count, c, o);

            DA.SetDataList(0, newSupps);
        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// You can add image files to your project resources and access them like this:
        /// return Resources.IconForThisComponent;
        /// </summary>
        protected override System.Drawing.Bitmap Icon => null;

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid => new Guid("529d36ee-adfe-47a4-9dc5-d18f8f9b48e2");
    }
}
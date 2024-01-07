using Grasshopper;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Data;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace ClosestPointsWithinRange
{
    public class ClosestPointsWithinRangeComponent : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public ClosestPointsWithinRangeComponent()
          : base("ClosestPointsWithinRangeComponent", "CPR",
            "R-Tree algorithm to find clusters of points within foci",
            "Paws", "Shapes")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPointParameter("points", "p", "search points", GH_ParamAccess.list);
            pManager.AddPointParameter("foci", "c", "focus points", GH_ParamAccess.list);
            pManager.AddNumberParameter("distance", "d", "distance from focus", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddNumberParameter("cluster points", "CP", "clustered points in range of focus points", GH_ParamAccess.tree);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<Point3d> points = new List<Point3d>();
            List<Point3d> focus = new List<Point3d>();
            double distance = new double();
            DA.GetDataList(0, points);
            DA.GetDataList(1, focus);
            DA.GetData(2, ref distance);

            RTree rtree = new RTree();
            for (int i = 0; i < points.Count; i++)
            {
                rtree.Insert(points[i], i);
            }

            DataTree<int> neighbourTree = new DataTree<int>();

            for (int i = 0; i < focus.Count; i++)
            {
                List<int> neighbours = new List<int>();

                Sphere searchSphere = new Sphere(focus[i], distance);
                rtree.Search(searchSphere,
                  (sender, args) => { neighbours.Add(args.Id); });

                GH_Path path = new GH_Path(i);
                if (neighbours.Count == 0) neighbourTree.EnsurePath(path);
                else
                {
                    //Add non-empty bus stops to data tree
                    foreach (int neighbour in neighbours)
                    {
                        neighbourTree.Add(neighbour, path);
                    }
                }
            }
            DA.SetDataTree(0, neighbourTree);
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
        public override Guid ComponentGuid => new Guid("882d8125-50db-4c94-845b-1a0c8d22e9b6");
    }
}
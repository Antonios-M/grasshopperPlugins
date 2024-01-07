using Grasshopper;
using Grasshopper.Kernel;
using System;
using System.Drawing;

namespace NCP
{
    public class NCPInfo : GH_AssemblyInfo
    {
        public override string Name => "NCP";

        //Return a 24x24 pixel bitmap to represent this GHA library.
        public override Bitmap Icon => null;

        //Return a short string describing the purpose of this GHA library.
        public override string Description => "";

        public override Guid Id => new Guid("541e641e-4d4b-428f-b447-7e8a0e82684b");

        //Return a string identifying you or your company.
        public override string AuthorName => "";

        //Return a string representing your preferred contact details.
        public override string AuthorContact => "";
    }
}
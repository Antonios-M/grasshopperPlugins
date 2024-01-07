using Grasshopper;
using Grasshopper.Kernel;
using System;
using System.Drawing;

namespace NFP
{
    public class NFPInfo : GH_AssemblyInfo
    {
        public override string Name => "NFP";

        //Return a 24x24 pixel bitmap to represent this GHA library.
        public override Bitmap Icon => null;

        //Return a short string describing the purpose of this GHA library.
        public override string Description => "";

        public override Guid Id => new Guid("12f45658-9d46-4a4c-be7c-3907bced4ce9");

        //Return a string identifying you or your company.
        public override string AuthorName => "";

        //Return a string representing your preferred contact details.
        public override string AuthorContact => "";
    }
}
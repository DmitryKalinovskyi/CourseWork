﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphApplication.Model
{
    public class VertexModel: GraphObjectModel
    {
        public string Caption { get; set; }

        public double Left { get; set; }

        public double Top { get; set; }

        public VertexModel()
        {
            Caption = "";
        }

        public VertexModel(string caption)
        {
            Caption = caption;
        }

        public VertexModel(double left, double top, string caption) 
        {
            Caption = caption;
            Left = left;
            Top = top;
        }

    }
}

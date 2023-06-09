﻿using GraphApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphApplication.ModelView
{
    public class EdgeModelView: NotifyModelView
    {
        public readonly EdgeModel Model;

        private bool _isMarked;

        public bool IsMarked
        {
            get
            {
                return _isMarked;
            }

            set
            {
                _isMarked = value;
                OnPropertyChanged(nameof(IsMarked));
            }
        }

        public VertexModelView Start { get; private set; }
        public VertexModelView End { get; private set; }

        public EdgeModelView(VertexModelView start, VertexModelView end)
        { 
            Model = new EdgeModel(start.Model, end.Model);
            Start = start;
            End = end;
        }

        public EdgeModelView(EdgeModel model, VertexModelView start, VertexModelView end)
        {
            Model = model;
            Start = start;
            End = end;
        }
    }
}

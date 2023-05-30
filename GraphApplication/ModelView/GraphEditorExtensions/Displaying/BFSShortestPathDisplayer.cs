﻿using GraphApplication.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GraphApplication.ModelView.GraphEditorExtensions.Displaying
{
    public class BFSShortestPathDisplayer: IDisplayAnimation
    {
        private List<VertexModelView> _path;
        private List<EdgeModelView> _edges;

        private GraphModelView _graphModelView;

        private Thread animationThread;
        public int moveDelay = 200;


        public BFSShortestPathDisplayer(GraphModelView graphModelView, List<VertexModelView> path)
        {
            _path = path;
            _edges = new();
            _graphModelView = graphModelView;
            animationThread = new(Animation);
        }

        private bool isCanceled = false;

        public void StartAnimation()
        {
            animationThread.Start();
        }

        private void Animation()
        {
            _path[0].IsMarked = true;

            for (int i = 1; i < _path.Count(); i++)
            {
                if (isCanceled) return;

                try
                {
                    _path[i].IsMarked = true;
                    VertexModelView prev = _path[i - 1];
                    VertexModelView next = _path[i];

                    EdgeModel edge = _graphModelView.Model.EdgeDictionary[(prev.Model, next.Model)];
                    EdgeModelView edgeModelView = _graphModelView.EdgeModelViewAssociation[edge];

                    _edges.Add(edgeModelView);

                    edgeModelView.IsMarked = true;
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e);
                }

                Thread.Sleep(moveDelay);
            }
        }

        public void StopAnimation()
        {
            isCanceled = true;
        }

        public void RestoreAnimation()
        {
            foreach (var v in _path)
            {
                v.IsMarked = false;
            }

            if (_edges.Count > 0)
                foreach (var e in _edges)
                {
                    e.IsMarked = false;
                }
        }
    }
}
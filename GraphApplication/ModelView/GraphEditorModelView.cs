﻿using GraphApplication.Model;
using GraphApplication.ModelView.GraphEditorExtensions;
using GraphApplication.Services.Commands;
using GraphApplication.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace GraphApplication.ModelView
{
    public partial class GraphEditorModelView: NotifyModelView
    {
        #region Commands
        private RelayCommand _createVertexCommand;

        public RelayCommand CreateVertexCommand
        {
            get
            {
                return _createVertexCommand ??
                     (_createVertexCommand = new RelayCommand(obj =>
                     {
                         try
                         {
                             //define args for our model
                             VertexModel vertex = new VertexModel();

                             VertexModelViews.Add(new VertexModelView(vertex, this));

                         }
                         catch (Exception ex)
                         {
                             MessageBox.Show("Відбулася помилка виконання команди: " + ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                         }
                     }));
            }
        }
        private RelayCommand _createEdgeCommand;

        public RelayCommand CreateEdgeCommand
        {
            get
            {
                return _createEdgeCommand ??
                     (_createEdgeCommand = new RelayCommand(obj =>
                     {
                         try
                         {
                             //define args for our model



                         }
                         catch (Exception ex)
                         {
                             MessageBox.Show("Відбулася помилка виконання команди: " + ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                         }
                     }));
            }
        }
        #endregion

        public GraphEditorModel Model { get; private set; }

        public List<VertexModel> VertexObjects
        {
            get { return Model.VertexObjects; }
            set
            {
                Model.VertexObjects = value;
                OnPropertyChanged(nameof(Model.VertexObjects));
            }
        }

        public List<EdgeModel> EdgeObjects
        {
            get { return Model.EdgeObjects; }
            set
            {
                Model.EdgeObjects = value;
                OnPropertyChanged(nameof(Model.EdgeObjects));
            }
        }

        private ObservableCollection<VertexModelView> _vertexModelViews;
        public ObservableCollection<VertexModelView> VertexModelViews
        {
            get { return _vertexModelViews; }
            set
            {
                _vertexModelViews = value;
                OnPropertyChanged(nameof(_vertexModelViews));
            }
        }
        private ObservableCollection<EdgeModelView> _edgeModelViews;
        public ObservableCollection<EdgeModelView> EdgeModelViews
        {
            get { return _edgeModelViews; }
            set
            {
                _edgeModelViews = value;
                OnPropertyChanged(nameof(EdgeModelViews));
            }
        }


        private string _name;
        private bool _isSaved;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(GraphNameFormat));
            }
        }
        public bool IsSaved
        {
            get { return _isSaved; }
            set
            {
                _isSaved = value;
                OnPropertyChanged(nameof(IsSaved));
                OnPropertyChanged(nameof(GraphNameFormat));

            }
        }

        public string GraphNameFormat
        {
            get{
                return Name + (IsSaved ? "*" : "");
            }
        }


        private double _scaleValue = 1;
        public double ScaleValue 
        {
            get { return _scaleValue; }
            set
            {
                _scaleValue = value;
                OnPropertyChanged(nameof(ScaleValue));
            }
        }

        private GraphEditorMode _currentEditorMode;

        public GraphEditorMode CurrentEditorMode
        {
            get { return _currentEditorMode; }
            set
            {
                _currentEditorMode = value;
                OnPropertyChanged(nameof(CurrentEditorMode));
            }
        }


        public GraphEditorModelView(GraphEditorModel model, string name, bool isSaved = false)
        {
            Model = model;
            Name = name;
            IsSaved = isSaved;

            CurrentEditorMode = new GraphEditorVertexCreationMode(this);

            //create all modelViews for elements
            VertexModelViews = new ObservableCollection<VertexModelView>(
                Model.VertexObjects.Select(vertexModel => new VertexModelView(vertexModel, this)));


            EdgeModelViews = new ObservableCollection<EdgeModelView>();

            VertexModelViews.CollectionChanged += (sender, e) =>
            {
                if (e.NewItems == null)
                    return;

                foreach (VertexModelView vertexModelView in e.NewItems)
                    VertexObjects.Add(vertexModelView.Model);

            };
            EdgeModelViews.CollectionChanged += (sender, e) =>
            {
                if (e.NewItems == null)
                    return;

                foreach (EdgeModelView edgeModelView in e.NewItems)
                    EdgeObjects.Add(edgeModelView.EdgeModel);

            };
            //EdgeModelViews = new ObservableCollection<EdgeModelView>(
            //    _model.EdgeObjects.Select(edgeModel => new EdgeModelView(edgeModel)));
            InitializeEvents();
        }
    }
}

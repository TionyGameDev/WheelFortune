﻿using Assets.Scripts.MVC;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MVC
{
    public interface IController 
    {
        void SetViewer(IViewer viewer);
    }
    interface IController <T> : IController
    {
        IViewer viewer { get;set;} }
    }
public abstract class BaseController<T> : IController<T> where T : IViewer
{
    public IViewer viewer { get  ; set ; }

    public void SetViewer(IViewer viewer)
    {
        if (viewer != null)
            Debug.LogError(viewer.ToString());
        this.viewer = viewer;
    }
}

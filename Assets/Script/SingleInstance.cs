using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleInstance<T> where T: class,new()
{
    private static T _instance;
    public static T instance { 
        get {
            if (null == _instance) {
                _instance = new T();
                SingleInstance<T> t = _instance as SingleInstance<T>;
                t.Init();
            }
            return _instance; 
        }  
    }

    public virtual void Init() { 
    
    }

    public virtual void Dispose() {
        _instance = null;
    }
}

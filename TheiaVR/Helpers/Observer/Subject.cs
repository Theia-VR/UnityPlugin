using System;
using System.Collections.Generic;

namespace TheiaVR.Helpers.Observer
{
    public abstract class Subject
    {
        private List<Observer> observers = new List<Observer>();

        public void Attach(Observer aObserver)
        {
            observers.Add(aObserver);
        }

        public void Detach(Observer aObserver)
        {
            observers.Remove(aObserver);
        }

        public void Notify(object aObject)
        {
            foreach (Observer vObserver in observers)
            {
                vObserver.Update(aObject);
            }
        }   
    }
}

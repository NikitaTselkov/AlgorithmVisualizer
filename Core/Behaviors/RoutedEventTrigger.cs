﻿using Microsoft.Xaml.Behaviors;
using System;
using System.Windows;

namespace Core.Behaviors
{
    public class RoutedEventTrigger : EventTriggerBase<DependencyObject>
    {
        private RoutedEvent _routedEvent;

        public RoutedEventTrigger() { }

        public RoutedEvent RoutedEvent
        {
            get { return this._routedEvent; }
            set { this._routedEvent = value; }
        }

        protected override string GetEventName()
        {
            return RoutedEvent.Name;
        }

        protected override void OnAttached()
        {
            Behavior behavior = this.AssociatedObject as Behavior;
            FrameworkElement associatedElement = this.AssociatedObject as FrameworkElement;
            if (behavior != null)
            {
                associatedElement = ((IAttachedObject)behavior).AssociatedObject as FrameworkElement;
            }

            if (associatedElement == null)
            {
                throw new ArgumentException("Routed Event trigger can only be associated to framework elements");
            }

            if (RoutedEvent != null)
            {
                associatedElement.AddHandler(RoutedEvent, new RoutedEventHandler(this.OnRoutedEvent));
            }
        }

        private void OnRoutedEvent(object sender, RoutedEventArgs args)
        {
            this.OnEvent(args);
        }
    }
}

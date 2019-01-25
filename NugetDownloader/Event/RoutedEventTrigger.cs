using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace NugetDownloader.Event
{
	public class RoutedEventTrigger : EventTriggerBase<DependencyObject>
	{
		public RoutedEvent RoutedEvent { get; set; }
		
		public RoutedEventTrigger()
		{
		}

		protected override void OnAttached()
		{
			Behavior behavior = base.AssociatedObject as Behavior;
			FrameworkElement associatedElement = base.AssociatedObject as FrameworkElement;

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

		protected override string GetEventName()
		{
			return RoutedEvent.Name;
		}

		void OnRoutedEvent(object sender, RoutedEventArgs args)
		{
			base.OnEvent(args);
		}


	}
}

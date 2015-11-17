using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Shapes;

namespace KLM
{
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public class _ColorSwatchControl : UserControl, IComponentConnector
	{
		internal _ColorSwatchControl UserControl;

		internal Grid LayoutRoot;

		internal Path Color;

		internal Path Color_shadow;

		private bool _contentLoaded;

		public _ColorSwatchControl()
		{
			this.InitializeComponent();
		}

		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/KLM;component/8colorswatchcontrol.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		[EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.UserControl = (_ColorSwatchControl)target;
				return;
			case 2:
				this.LayoutRoot = (Grid)target;
				return;
			case 3:
				this.Color = (Path)target;
				return;
			case 4:
				this.Color_shadow = (Path)target;
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}
	}
}

using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace KLM.Properties
{
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0"), CompilerGenerated]
	internal sealed class Settings : ApplicationSettingsBase
	{
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());

		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		[DefaultSettingValue("1"), UserScopedSetting, DebuggerNonUserCode]
		public int Current_Mode
		{
			get
			{
				return (int)this["Current_Mode"];
			}
			set
			{
				this["Current_Mode"] = value;
			}
		}

		[DefaultSettingValue("-1"), UserScopedSetting, DebuggerNonUserCode]
		public int KBLockState
		{
			get
			{
				return (int)this["KBLockState"];
			}
			set
			{
				this["KBLockState"] = value;
			}
		}

		[DefaultSettingValue("1"), UserScopedSetting, DebuggerNonUserCode]
		public int LeftArea_Color
		{
			get
			{
				return (int)this["LeftArea_Color"];
			}
			set
			{
				this["LeftArea_Color"] = value;
			}
		}

		[DefaultSettingValue("0"), UserScopedSetting, DebuggerNonUserCode]
		public int LeftArea_Level
		{
			get
			{
				return (int)this["LeftArea_Level"];
			}
			set
			{
				this["LeftArea_Level"] = value;
			}
		}

		[DefaultSettingValue("4"), UserScopedSetting, DebuggerNonUserCode]
		public int MiddleArea_Color
		{
			get
			{
				return (int)this["MiddleArea_Color"];
			}
			set
			{
				this["MiddleArea_Color"] = value;
			}
		}

		[DefaultSettingValue("0"), UserScopedSetting, DebuggerNonUserCode]
		public int MiddleArea_Level
		{
			get
			{
				return (int)this["MiddleArea_Level"];
			}
			set
			{
				this["MiddleArea_Level"] = value;
			}
		}

		[DefaultSettingValue("6"), UserScopedSetting, DebuggerNonUserCode]
		public int RightArea_Color
		{
			get
			{
				return (int)this["RightArea_Color"];
			}
			set
			{
				this["RightArea_Color"] = value;
			}
		}

		[DefaultSettingValue("0"), UserScopedSetting, DebuggerNonUserCode]
		public int RightArea_Level
		{
			get
			{
				return (int)this["RightArea_Level"];
			}
			set
			{
				this["RightArea_Level"] = value;
			}
		}

		[DefaultSettingValue("1"), UserScopedSetting, DebuggerNonUserCode]
		public int DualColorLeft_Color
		{
			get
			{
				return (int)this["DualColorLeft_Color"];
			}
			set
			{
				this["DualColorLeft_Color"] = value;
			}
		}

		[DefaultSettingValue("1"), UserScopedSetting, DebuggerNonUserCode]
		public int DualColorLeft_Level
		{
			get
			{
				return (int)this["DualColorLeft_Level"];
			}
			set
			{
				this["DualColorLeft_Level"] = value;
			}
		}

		[DefaultSettingValue("4"), UserScopedSetting, DebuggerNonUserCode]
		public int DualColorRight_Color
		{
			get
			{
				return (int)this["DualColorRight_Color"];
			}
			set
			{
				this["DualColorRight_Color"] = value;
			}
		}

		[DefaultSettingValue("1"), UserScopedSetting, DebuggerNonUserCode]
		public int DualColorRight_Level
		{
			get
			{
				return (int)this["DualColorRight_Level"];
			}
			set
			{
				this["DualColorRight_Level"] = value;
			}
		}

		[DefaultSettingValue("6"), UserScopedSetting, DebuggerNonUserCode]
		public int WholeArea_Color
		{
			get
			{
				return (int)this["WholeArea_Color"];
			}
			set
			{
				this["WholeArea_Color"] = value;
			}
		}

		[DefaultSettingValue("0"), UserScopedSetting, DebuggerNonUserCode]
		public int WholeArea_Level
		{
			get
			{
				return (int)this["WholeArea_Level"];
			}
			set
			{
				this["WholeArea_Level"] = value;
			}
		}

		[DefaultSettingValue("7"), UserScopedSetting, DebuggerNonUserCode]
		public int IDLE_Mode_Setting
		{
			get
			{
				return (int)this["IDLE_Mode_Setting"];
			}
			set
			{
				this["IDLE_Mode_Setting"] = value;
			}
		}
	}
}

using KLM.Properties;
using Microsoft.Win32;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Management;
using System.Reflection;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using USB2SMBUS;

namespace KLM
{
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public class MainWindow : Window, IComponentConnector
	{
		private enum WMIEventCode
		{
			KLM = 82
		}

		public delegate void SetStatusDelegate(string message, bool addInListBox);

		public delegate void EnableCtlsDelegate(bool bEnable);

		private const byte __BUFFER_DEFAULT_VALUE__ = 170;

		private const bool __ENABLE_CTRLS__ = true;

		private const bool __DISABLE_CTRLS__ = false;

		private const int __DELAY__ = 5;

		private const int __LONG_DELAY__ = 10;

		private const int __MAX_LIST_ITEM__ = 1000;

		private const int __MAX_CMD_FRAME__ = 6;

		private const int USB_RETRY_COUNT = 30;

		private const int PBT_APMRESUMESUSPEND = 7;

		private const int PBT_APMSUSPEND = 4;

		private const int WM_KLMDUPLICATE = 2785;

		private const int _RED = 1;

		private const int _GREEN = 2;

		private const int _BLUE = 3;

		private const int _cBlack = 0;

		private const int _cRed = 1;

		private const int _cOrange = 2;

		private const int _cYellow = 3;

		private const int _cGreen = 4;

		private const int _cSky = 5;

		private const int _cBlue = 6;

		private const int _cPurple = 7;

		private const int _cWhite = 8;

		private const int _Level1 = 0;

		private const int _Level2 = 1;

		private const int _Level3 = 2;

		private const int _Level4 = 3;

		private const int _TurnOffWholeArea = 0;

		private const int _LeftArea = 1;

		private const int _MiddleArea = 2;

		private const int _RightArea = 3;

		private const int _DualColorLeftBall = 4;

		private const int _DualColorRightBall = 5;

		private const int _WholeArea = 6;

		private const int DISABLE_MODE = 0;

		private const int STD_NORMAL_MODE = 1;

		private const int STD_GAMING_MODE = 2;

		private const int STD_BREATHING_MODE = 3;

		private const int STD_AUDIO_MODE = 4;

		private const int STD_WAVE_MODE = 5;

		private const int STD_DUAL_COLOR_MODE = 6;

		private const int IDLE_OFF_MODE = 7;

		private const int IDLE_BREATHING_MODE = 8;

		private const int IDLE_WAVE_MODE = 9;

		private const double ColorChangePeroidOfSTDWave = 1.5;

		private const double ColorChangePeroidOfSTDBreathing = 1.0;

		private const double ColorChangePeroidOfSTDDualColor = 2.0;

		private const double ColorChangePeroidOfIDLEWave = 6.0;

		private const double ColorChangePeroidOfIDLEBreathing = 5.5;

		private string[] m_strUSBSymbolicLinks = new string[0];

		public string m_strAddress = "";

		private CUSB m_usb;

		private int[] CMD2send;

		private int[] readDataBuf;

		private NotifyIcon notifyIcon;

		private static Mutex mutex;

		private bool bIsAlreadyRun;

		private bool bLEDHotkeyMask;

		private bool Minimized;

		private int[,] rgb;

		private int[] RED;

		private int[] GREEN;

		private int[] BLUE;

		private int[,] ColorConfig;

		private int[] area_switch;

		private string userConfigPath;

		private int[,] GlowFlagOf29ColorSwatch;

		private int[] GlowFlagOf8ColorSwatch;

		private SolidColorBrush myBrush;

		private int[,,] ColorIndexTable;

		private int[,,] ColorIndexTable4OnScreenDisplay;

		private int currentArea;

		private int previous_area;

		private int beforeLocked_area;

		private DispatcherTimer timer;

		private bool sendCMDFlag;

		private int timerInterval;

		private int pause;

		private int timeCounter;

		private bool stateChangeFlag;

		private int UsbCmdDelay;

		private int currentMode;

		private int previousMode;

		private int Switch2Mode;

		private int lastSTDmode;

		private int lastIDLEmode;

		private bool enteredIDLEmode;

		private bool resumeFromS3;

		private int keyboardLockedFlag;

		private bool switchFromKBLocked;

		private bool firstTime2RunWaveMode;

		private bool firstTime2RunBreathingMode;

		private bool firstTime2RunKLM;

		private DoubleAnimation deselection;

		private DoubleAnimation selection;

		private int modeB4S3;

		private double[,] ColorRampSpeed;

		private int ColorChangeStep;

		private double[] ColorChangeStep4WaveMode;

		private double ColorChangePeroid;

		private double ColorChangeTimesPerSec;

		private int reverseFlag;

		private int[] reverseFlage4WaveMode;

		private ManagementEventWatcher watcher;

		private bool existBacklightKeyboardDevice;

		internal MainWindow Window;

		internal VisualStateGroup CloseStatusGroup;

		internal VisualState Enter;

		internal VisualState Leave;

		internal VisualStateGroup KeyboardLockStateGroup;

		internal VisualState EnterState;

		internal VisualState LeaveState;

		internal VisualStateGroup LeftballStateGroup;

		internal VisualState EnterState1;

		internal VisualState LeaveState1;

		internal VisualStateGroup RightballStateGroup;

		internal VisualState EnterState2;

		internal VisualState LeaveState2;

		internal VisualStateGroup MiddleballStateGroup;

		internal VisualState EnterState3;

		internal VisualState LeaveState3;

		internal VisualStateGroup StdStateGroup;

		internal VisualState EnterState4;

		internal VisualState LeaveState4;

		internal VisualStateGroup IdleStateGroup;

		internal VisualState EnterState5;

		internal VisualState LeaveState5;

		internal VisualStateGroup StdOption_Normal;

		internal VisualState EnterState6;

		internal VisualState LeaveState6;

		internal VisualStateGroup StdOptionGaming;

		internal VisualState EnterState8;

		internal VisualState LeaveState8;

		internal VisualStateGroup StdOption_Breathing;

		internal VisualState EnterState9;

		internal VisualState LeaveState9;

		internal VisualStateGroup StdOption_Wave;

		internal VisualState EnterState10;

		internal VisualState LeaveState10;

		internal VisualStateGroup StdOption_Dual;

		internal VisualState EnterState11;

		internal VisualState LeaveState11;

		internal VisualStateGroup IdleOption_Off;

		internal VisualState EnterState12;

		internal VisualState LeaveState12;

		internal VisualStateGroup IdleOption_Breathing;

		internal VisualState EnterState13;

		internal VisualState LeaveState13;

		internal VisualStateGroup IdleOption_Wave;

		internal VisualState EnterState14;

		internal VisualState LeaveState14;

		internal VisualStateGroup Led_left;

		internal VisualState led_enter;

		internal VisualState led_leave;

		internal VisualStateGroup Led_middle;

		internal VisualState led_enter1;

		internal VisualState led_leave1;

		internal VisualStateGroup Led_right;

		internal VisualState led_enter2;

		internal VisualState led_leave2;

		internal VisualStateGroup KeyboardUnockStateGroup;

		internal VisualState EnterState7;

		internal VisualState LeaveState7;

		internal VisualStateGroup Std_normal_selected_glow;

		internal VisualState selected;

		internal VisualState deselected;

		internal VisualStateGroup Std_dual_color_selected_glow;

		internal VisualState selected1;

		internal VisualState deselected1;

		internal VisualStateGroup Std_wave_selected_glow;

		internal VisualState selected2;

		internal VisualState deselected2;

		internal VisualStateGroup Std_breathing_selected_glow;

		internal VisualState selected3;

		internal VisualState deselected3;

		internal VisualStateGroup Std_gaming_selected_glow;

		internal VisualState selected4;

		internal VisualState deselected4;

		internal VisualStateGroup Idle_off;

		internal VisualState selected5;

		internal VisualState deselected5;

		internal VisualStateGroup Idel_wave;

		internal VisualState selected6;

		internal VisualState deselected6;

		internal VisualStateGroup Idle_breathing;

		internal VisualState selected7;

		internal VisualState deselected7;

		internal VisualStateGroup Single_color_glow;

		internal VisualState Enter1;

		internal VisualState Leave1;

		internal VisualStateGroup Color_outter_cycle_purple;

		internal VisualState enter3;

		internal VisualState leave;

		internal VisualState select;

		internal VisualState deselect;

		internal VisualStateGroup Color_outter_middle_purple;

		internal VisualState enter4;

		internal VisualState leave1;

		internal VisualState select1;

		internal VisualState deselect1;

		internal VisualStateGroup Color_inner_middle_purple;

		internal VisualState enter5;

		internal VisualState leave2;

		internal VisualState select2;

		internal VisualState deselect2;

		internal VisualStateGroup Color_inner_cycle_purple;

		internal VisualState enter6;

		internal VisualState leave3;

		internal VisualState select3;

		internal VisualState deselect3;

		internal VisualStateGroup Color_inner_middle_red;

		internal VisualState enter7;

		internal VisualState leave9;

		internal VisualStateGroup Color_inner_middle_orange;

		internal VisualState enter8;

		internal VisualState leave4;

		internal VisualStateGroup Color_inner_middle_yellow;

		internal VisualState enter9;

		internal VisualState leave5;

		internal VisualStateGroup Color_inner_middle_green;

		internal VisualState enter10;

		internal VisualState leave6;

		internal VisualStateGroup Color_inner_middle_sky;

		internal VisualState enter11;

		internal VisualState leave7;

		internal VisualStateGroup Color_inner_middle_blue;

		internal VisualState enter12;

		internal VisualState leave8;

		internal VisualStateGroup Color_inner_cycle_red;

		internal VisualState enter13;

		internal VisualState leave10;

		internal VisualStateGroup Color_inner_cycle_orange;

		internal VisualState enter14;

		internal VisualState leave11;

		internal VisualStateGroup Color_inner_cycle_yellow;

		internal VisualState enter15;

		internal VisualState leave12;

		internal VisualStateGroup Color_inner_cycle_green;

		internal VisualState enter16;

		internal VisualState leave13;

		internal VisualStateGroup Color_inner_cycle_sky;

		internal VisualState enter17;

		internal VisualState leave14;

		internal VisualStateGroup Color_inner_cycle_blue;

		internal VisualState enter18;

		internal VisualState leave15;

		internal VisualStateGroup Color_outter_middle_red;

		internal VisualState enter19;

		internal VisualState leave16;

		internal VisualStateGroup Color_outter_middle_orange;

		internal VisualState enter20;

		internal VisualState leave17;

		internal VisualStateGroup Color_outter_middle_yellow;

		internal VisualState enter21;

		internal VisualState leave18;

		internal VisualStateGroup Color_outter_middle_green;

		internal VisualState enter22;

		internal VisualState leave19;

		internal VisualStateGroup Color_outter_middle_sky;

		internal VisualState enter23;

		internal VisualState leave20;

		internal VisualStateGroup Color_outter_middle_blue;

		internal VisualState enter24;

		internal VisualState leave21;

		internal VisualStateGroup Color_outter_cycle_sky;

		internal VisualState enter25;

		internal VisualState leave22;

		internal VisualStateGroup Color_outter_cycle_green;

		internal VisualState enter26;

		internal VisualState leave23;

		internal VisualStateGroup Color_outter_cycle_yellow;

		internal VisualState enter27;

		internal VisualState leave24;

		internal VisualStateGroup Color_outter_cycle_orange;

		internal VisualState enter28;

		internal VisualState leave25;

		internal VisualStateGroup Color_outter_cycle_red;

		internal VisualState enter29;

		internal VisualState leave26;

		internal VisualStateGroup Color_outter_cycle_blue;

		internal VisualState enter30;

		internal VisualState leave27;

		internal Canvas led_manager_all_ui;

		internal Canvas option_buttons_idle;

		internal Canvas 圖層_11;

		internal System.Windows.Shapes.Path idle_off;

		internal System.Windows.Shapes.Path idle_breathing;

		internal System.Windows.Shapes.Path idle_wave;

		internal Canvas TextBlock5;

		internal System.Windows.Shapes.Path idle_off_sensor;

		internal System.Windows.Shapes.Path idle_breathing_sensor;

		internal System.Windows.Shapes.Path idle_wave_sensor;

		internal System.Windows.Shapes.Path idle_off_glow;

		internal System.Windows.Shapes.Path idle_breathing_glow;

		internal System.Windows.Shapes.Path idle_wave_glow;

		internal Canvas option_buttons_std;

		internal Canvas 圖層_12;

		internal System.Windows.Shapes.Path gaming;

		internal System.Windows.Shapes.Path audio;

		internal System.Windows.Shapes.Path breathing;

		internal System.Windows.Shapes.Path normal;

		internal System.Windows.Shapes.Path wave;

		internal System.Windows.Shapes.Path dual_color;

		internal System.Windows.Shapes.Path normal_glow;

		internal System.Windows.Shapes.Path gaming_glow;

		internal System.Windows.Shapes.Path breathing_glow;

		internal System.Windows.Shapes.Path wave_glow;

		internal System.Windows.Shapes.Path dual_color_glow;

		internal Canvas TextBlock6;

		internal System.Windows.Shapes.Path dual_color1;

		internal System.Windows.Shapes.Path normal1;

		internal System.Windows.Shapes.Path wave1;

		internal System.Windows.Shapes.Path breathing1;

		internal System.Windows.Shapes.Path audio1;

		internal System.Windows.Shapes.Path gaming1;

		internal System.Windows.Shapes.Path audio_sensor;

		internal System.Windows.Shapes.Path gaming_sensor;

		internal System.Windows.Shapes.Path breathing_sensor;

		internal System.Windows.Shapes.Path normal_sensor;

		internal System.Windows.Shapes.Path wave_sensor;

		internal System.Windows.Shapes.Path dual_color_sensor;

		internal Canvas bg;

		internal Canvas 圖層_1;

		internal Canvas Main_Body;

		internal Canvas mode;

		internal System.Windows.Shapes.Path std_mode_bg;

		internal System.Windows.Shapes.Path idle_mode_bg;

		internal System.Windows.Shapes.Path std_mode_bg_Copy;

		internal System.Windows.Shapes.Path idle_mode_bg_Copy;

		internal System.Windows.Shapes.Path std_mode_bg_glow;

		internal System.Windows.Shapes.Path idle_mode_bg_glow;

		internal System.Windows.Shapes.Path std_mode_frame;

		internal System.Windows.Shapes.Path idle_mode_frame;

		internal Canvas TextBlock_Standard;

		internal Canvas TextBlock2;

		internal Canvas TextBlock3;

		internal Canvas TextBlock4;

		internal Canvas TextBlock_Idle;

		internal System.Windows.Shapes.Path std_mode_sensor;

		internal System.Windows.Shapes.Path idle_mode_sensor;

		internal Canvas led_on_off_left;

		internal Canvas led_on_off_bg_left;

		internal Canvas 圖層_5;

		internal System.Windows.Shapes.Path enter;

		internal Canvas led_on_off_switch_left;

		internal Canvas 圖層_8;

		internal System.Windows.Shapes.Path left_switch;

		internal System.Windows.Shapes.Path left_switch_on;

		internal Canvas led_on_off_middle;

		internal Canvas led_on_off_bg_middle;

		internal Canvas 圖層_6;

		internal System.Windows.Shapes.Path enter1;

		internal Canvas led_on_off_switch_middle;

		internal Canvas 圖層_7;

		internal System.Windows.Shapes.Path middle_switch;

		internal System.Windows.Shapes.Path middle_switch_on;

		internal Canvas led_on_off_right;

		internal Canvas led_on_off_bg_right;

		internal Canvas 圖層_9;

		internal System.Windows.Shapes.Path enter2;

		internal Canvas led_on_off_switch_right;

		internal Canvas 圖層_10;

		internal System.Windows.Shapes.Path right_switch;

		internal System.Windows.Shapes.Path right_switch_on;

		internal Viewbox keyboard;

		internal System.Windows.Shapes.Rectangle color_area;

		internal GradientStop left_color;

		internal GradientStop left_middle_color;

		internal GradientStop middle_left_color;

		internal GradientStop middle_right_color;

		internal GradientStop right_middle_color;

		internal GradientStop right_color;

		internal System.Windows.Shapes.Path kb_right_color;

		internal System.Windows.Shapes.Path kb_middle_color;

		internal System.Windows.Shapes.Path kb_left_color;

		internal Canvas kb_left_outer_frame_blur;

		internal System.Windows.Shapes.Path kb_left_outer_frame_solid;

		internal Canvas kb_middle_btm_outer_frame_blur;

		internal System.Windows.Shapes.Path kb_middle_btm_outer_frame_solid;

		internal Canvas kb_middle_top_outer_frame_blur;

		internal System.Windows.Shapes.Path kb_middle_top_outer_frame_solid;

		internal Canvas kb_right_outer_frame_blur;

		internal System.Windows.Shapes.Path kb_right_outer_frame_solid;

		internal System.Windows.Shapes.Path kb_black_bg;

		internal Canvas kb_buttons;

		internal Canvas kb_right_frame_blur;

		internal System.Windows.Shapes.Path kb_right_frame_white;

		internal Canvas kb_middle_frame_blur;

		internal System.Windows.Shapes.Path kb_middle_frame_white;

		internal Canvas kb_left_frame_blur;

		internal System.Windows.Shapes.Path kb_left_frame_white;

		internal System.Windows.Shapes.Path kb_all_blur;

		internal System.Windows.Shapes.Path kb_all_frame;

		internal System.Windows.Shapes.Path kb_left_frame_sensor;

		internal System.Windows.Shapes.Path kb_middle_frame_sensor;

		internal System.Windows.Shapes.Path kb_right_frame_sensor;

		internal Canvas color_ball_right;

		internal Ellipse ellipse1_Copy;

		internal System.Windows.Shapes.Path colorball_right_1;

		internal Ellipse colorball_right_sensor;

		internal Canvas color_ball_middle_dual;

		internal System.Windows.Shapes.Path colorball_middle_1;

		internal GradientStop dual_colorball_gradient_left_left;

		internal GradientStop dual_colorball_gradient_left_right;

		internal GradientStop dual_colorball_gradient_left_middle_left;

		internal GradientStop dual_colorball_gradient_left_middle_right;

		internal System.Windows.Shapes.Path colorball_middle_2;

		internal GradientStop dual_colorball_right_right;

		internal Ellipse colorball_middle_left_sensor;

		internal Ellipse colorball_middle_right_sensor;

		internal Canvas color_ball_middle;

		internal Ellipse ellipse1_Copy1;

		internal System.Windows.Shapes.Path colorball_middle;

		internal Ellipse colorball_middle_sensor;

		internal Canvas color_ball_left;

		internal Ellipse ellipse1;

		internal System.Windows.Shapes.Path colorball_left_1;

		internal Ellipse colorball_left_sensor;

		internal Canvas bk_white;

		internal Canvas 圖層_13;

		internal Viewbox color_board;

		internal Canvas outter_cycle_frame1;

		internal Canvas outter_middle_cycle_frame1;

		internal Canvas inner_middle_cycle_frame1;

		internal Canvas inner_cycle_frame1;

		internal Canvas outter_cycle_color1;

		internal System.Windows.Shapes.Path red4;

		internal System.Windows.Shapes.Path orange4;

		internal System.Windows.Shapes.Path yellow4;

		internal System.Windows.Shapes.Path green4;

		internal System.Windows.Shapes.Path sky4;

		internal System.Windows.Shapes.Path blue4;

		internal System.Windows.Shapes.Path purple4;

		internal Canvas outter_cycle_color1_glow;

		internal System.Windows.Shapes.Path red4_glow;

		internal System.Windows.Shapes.Path orange4_glow;

		internal System.Windows.Shapes.Path yellow4_glow;

		internal System.Windows.Shapes.Path green4_glow;

		internal System.Windows.Shapes.Path sky4_glow;

		internal System.Windows.Shapes.Path blue4_glow;

		internal System.Windows.Shapes.Path purple4_glow;

		internal Canvas outter_middle_cycle_color1;

		internal System.Windows.Shapes.Path red3;

		internal System.Windows.Shapes.Path orange3;

		internal System.Windows.Shapes.Path yellow3;

		internal System.Windows.Shapes.Path green3;

		internal System.Windows.Shapes.Path sky3;

		internal System.Windows.Shapes.Path blue3;

		internal System.Windows.Shapes.Path purple3;

		internal Canvas outter_middle_cycle_color1_glow;

		internal System.Windows.Shapes.Path red3_glow;

		internal System.Windows.Shapes.Path orange3_glow;

		internal System.Windows.Shapes.Path yellow3_glow;

		internal System.Windows.Shapes.Path green3_glow;

		internal System.Windows.Shapes.Path sky3_glow;

		internal System.Windows.Shapes.Path blue3_glow;

		internal System.Windows.Shapes.Path purple3_glow;

		internal Canvas inner_middle_cycle_color1;

		internal System.Windows.Shapes.Path red2;

		internal System.Windows.Shapes.Path orange2;

		internal System.Windows.Shapes.Path yellow2;

		internal System.Windows.Shapes.Path green2;

		internal System.Windows.Shapes.Path sky2;

		internal System.Windows.Shapes.Path blue2;

		internal System.Windows.Shapes.Path purple2;

		internal Canvas inner_middle_cycle_color1_glow;

		internal System.Windows.Shapes.Path red2_glow;

		internal System.Windows.Shapes.Path orange2_glow;

		internal System.Windows.Shapes.Path yellow2_glow;

		internal System.Windows.Shapes.Path green2_glow;

		internal System.Windows.Shapes.Path sky2_glow;

		internal System.Windows.Shapes.Path blue2_glow;

		internal System.Windows.Shapes.Path purple2_glow;

		internal Canvas inner_cycle_color1;

		internal System.Windows.Shapes.Path red1;

		internal System.Windows.Shapes.Path orange1;

		internal System.Windows.Shapes.Path yellow1;

		internal System.Windows.Shapes.Path green1;

		internal System.Windows.Shapes.Path sky1;

		internal System.Windows.Shapes.Path blue1;

		internal System.Windows.Shapes.Path purple1;

		internal Canvas inner_cycle_color1_glow;

		internal System.Windows.Shapes.Path red1_glow;

		internal System.Windows.Shapes.Path orange1_glow;

		internal System.Windows.Shapes.Path yellow1_glow;

		internal System.Windows.Shapes.Path green1_glow;

		internal System.Windows.Shapes.Path sky1_glow;

		internal System.Windows.Shapes.Path blue1_glow;

		internal System.Windows.Shapes.Path purple1_glow;

		internal Canvas outter_cycle_sensor;

		internal Canvas red5;

		internal System.Windows.Shapes.Path red_5;

		internal Canvas orange5;

		internal System.Windows.Shapes.Path orange_5;

		internal Canvas yellow5;

		internal System.Windows.Shapes.Path yellow_5;

		internal Canvas green5;

		internal System.Windows.Shapes.Path green_5;

		internal Canvas sky5;

		internal System.Windows.Shapes.Path sky_5;

		internal Canvas blue5;

		internal System.Windows.Shapes.Path blue_5;

		internal Canvas purple5;

		internal System.Windows.Shapes.Path purple_5;

		internal Canvas outter_middle_cycle_sensor;

		internal Canvas red6;

		internal System.Windows.Shapes.Path red_6;

		internal Canvas orange6;

		internal System.Windows.Shapes.Path orange_6;

		internal Canvas yellow6;

		internal System.Windows.Shapes.Path yellow_6;

		internal Canvas green6;

		internal System.Windows.Shapes.Path green_6;

		internal Canvas sky6;

		internal System.Windows.Shapes.Path sky_6;

		internal Canvas blue6;

		internal System.Windows.Shapes.Path blue_6;

		internal Canvas purple6;

		internal System.Windows.Shapes.Path purple_6;

		internal Canvas inner_middle_cycle_sensor;

		internal Canvas red7;

		internal System.Windows.Shapes.Path red_7;

		internal Canvas orange7;

		internal System.Windows.Shapes.Path orange_7;

		internal Canvas yellow7;

		internal System.Windows.Shapes.Path yellow_7;

		internal Canvas green7;

		internal System.Windows.Shapes.Path green_7;

		internal Canvas sky7;

		internal System.Windows.Shapes.Path sky_7;

		internal Canvas blue7;

		internal System.Windows.Shapes.Path blue_7;

		internal Canvas purple7;

		internal System.Windows.Shapes.Path purple_7;

		internal Canvas inner_cycle_sensor;

		internal Canvas red8;

		internal System.Windows.Shapes.Path red_8;

		internal Canvas orange8;

		internal System.Windows.Shapes.Path orange_8;

		internal Canvas yellow8;

		internal System.Windows.Shapes.Path yellow_8;

		internal Canvas green8;

		internal System.Windows.Shapes.Path green_8;

		internal Canvas sky8;

		internal System.Windows.Shapes.Path sky_8;

		internal Canvas blue8;

		internal System.Windows.Shapes.Path blue_8;

		internal Canvas purple8;

		internal System.Windows.Shapes.Path purple_8;

		internal Canvas white;

		internal System.Windows.Shapes.Path center_ball1;

		internal Canvas color_swatch_shadow;

		internal Canvas color_swatch_single;

		internal Canvas 圖層_18;

		internal Canvas white_Copy;

		internal System.Windows.Shapes.Path center_ball2;

		internal _ColorSwatchControl _8ColorSwatchControl_;

		internal System.Windows.Shapes.Path red;

		internal System.Windows.Shapes.Path orange;

		internal System.Windows.Shapes.Path yellow;

		internal System.Windows.Shapes.Path green;

		internal System.Windows.Shapes.Path sky;

		internal System.Windows.Shapes.Path blue;

		internal System.Windows.Shapes.Path purple;

		internal System.Windows.Shapes.Path red_glow;

		internal System.Windows.Shapes.Path orange_glow;

		internal System.Windows.Shapes.Path yellow_glow;

		internal System.Windows.Shapes.Path green_glow;

		internal System.Windows.Shapes.Path sky_glow;

		internal System.Windows.Shapes.Path blue_glow;

		internal System.Windows.Shapes.Path purple_glow;

		internal System.Windows.Shapes.Path red_shadow;

		internal System.Windows.Shapes.Path orange_shadow;

		internal System.Windows.Shapes.Path yellow_shadow;

		internal System.Windows.Shapes.Path green_shadow;

		internal System.Windows.Shapes.Path sky_shadow;

		internal System.Windows.Shapes.Path blue_shadow;

		internal System.Windows.Shapes.Path purple_shadow;

		internal Canvas close_btn;

		internal Canvas 圖層_14;

		internal Ellipse ellipse;

		internal Canvas keyboard_lock;

		internal Canvas 圖層_15;

		internal Ellipse ellipse_Copy1;

		internal Ellipse ellipse3;

		internal Canvas keyboard_unlock;

		internal Canvas 圖層_16;

		internal Ellipse ellipse_Copy;

		private bool _contentLoaded;

		[DllImport("User32.dll")]
		private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		[DllImport("User32.dll")]
		private static extern IntPtr FindWindowEx(IntPtr hwndParent, int hwndChildAfter, string lpszClass, string lpszWindow);

		[DllImport("user32.dll ", CharSet = CharSet.Unicode)]
		public static extern IntPtr PostMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

		public MainWindow()
		{
			int[] cMD2send = new int[5];
			this.CMD2send = cMD2send;
			int[] array = new int[5];
			this.readDataBuf = array;
			this.notifyIcon = new NotifyIcon();
			this.rgb = new int[7, 4];
			this.RED = new int[7];
			this.GREEN = new int[7];
			this.BLUE = new int[7];
			this.ColorConfig = new int[7, 2];
			this.area_switch = new int[]
			{
				0,
				1,
				1,
				1,
				1,
				1,
				1
			};
			this.GlowFlagOf29ColorSwatch = new int[8, 4];
			this.GlowFlagOf8ColorSwatch = new int[8];
			this.myBrush = new SolidColorBrush();
			this.ColorIndexTable = new int[,,]
			{
				{
					{
						0,
						0,
						0,
						0
					},
					{
						0,
						0,
						0,
						0
					},
					{
						0,
						0,
						0,
						0
					},
					{
						0,
						0,
						0,
						0
					}
				},
				{
					{
						0,
						170,
						0,
						0
					},
					{
						0,
						255,
						0,
						0
					},
					{
						0,
						255,
						32,
						16
					},
					{
						0,
						238,
						80,
						48
					}
				},
				{
					{
						0,
						170,
						64,
						0
					},
					{
						0,
						187,
						112,
						0
					},
					{
						0,
						204,
						170,
						16
					},
					{
						0,
						238,
						204,
						48
					}
				},
				{
					{
						0,
						128,
						128,
						0
					},
					{
						0,
						238,
						238,
						0
					},
					{
						0,
						255,
						255,
						32
					},
					{
						0,
						238,
						238,
						48
					}
				},
				{
					{
						0,
						16,
						255,
						0
					},
					{
						0,
						176,
						255,
						0
					},
					{
						0,
						32,
						221,
						32
					},
					{
						0,
						170,
						238,
						48
					}
				},
				{
					{
						0,
						0,
						170,
						255
					},
					{
						0,
						0,
						255,
						255
					},
					{
						0,
						80,
						255,
						238
					},
					{
						0,
						144,
						238,
						255
					}
				},
				{
					{
						0,
						0,
						0,
						170
					},
					{
						0,
						0,
						0,
						255
					},
					{
						0,
						0,
						255,
						255
					},
					{
						0,
						80,
						204,
						238
					}
				},
				{
					{
						0,
						21,
						0,
						160
					},
					{
						0,
						48,
						0,
						255
					},
					{
						0,
						187,
						170,
						255
					},
					{
						0,
						255,
						255,
						255
					}
				},
				{
					{
						0,
						176,
						255,
						176
					},
					{
						0,
						176,
						255,
						176
					},
					{
						0,
						176,
						255,
						176
					},
					{
						0,
						176,
						255,
						176
					}
				}
			};
			this.ColorIndexTable4OnScreenDisplay = new int[,,]
			{
				{
					{
						0,
						35,
						25,
						22
					},
					{
						0,
						35,
						25,
						22
					},
					{
						0,
						35,
						25,
						22
					},
					{
						0,
						35,
						25,
						22
					}
				},
				{
					{
						0,
						191,
						0,
						8
					},
					{
						0,
						230,
						0,
						18
					},
					{
						0,
						234,
						85,
						50
					},
					{
						0,
						239,
						132,
						93
					}
				},
				{
					{
						0,
						196,
						85,
						0
					},
					{
						0,
						242,
						147,
						0
					},
					{
						0,
						246,
						170,
						59
					},
					{
						0,
						249,
						192,
						111
					}
				},
				{
					{
						0,
						214,
						199,
						0
					},
					{
						0,
						255,
						241,
						0
					},
					{
						0,
						255,
						246,
						127
					},
					{
						0,
						255,
						249,
						177
					}
				},
				{
					{
						0,
						107,
						160,
						27
					},
					{
						0,
						133,
						192,
						35
					},
					{
						0,
						163,
						205,
						83
					},
					{
						0,
						189,
						217,
						129
					}
				},
				{
					{
						0,
						0,
						149,
						198
					},
					{
						0,
						47,
						253,
						240
					},
					{
						0,
						115,
						203,
						243
					},
					{
						0,
						160,
						217,
						247
					}
				},
				{
					{
						0,
						0,
						56,
						132
					},
					{
						0,
						0,
						88,
						169
					},
					{
						0,
						57,
						113,
						184
					},
					{
						0,
						116,
						144,
						202
					}
				},
				{
					{
						0,
						62,
						16,
						114
					},
					{
						0,
						103,
						23,
						133
					},
					{
						0,
						130,
						71,
						152
					},
					{
						0,
						159,
						115,
						176
					}
				},
				{
					{
						0,
						255,
						255,
						255
					},
					{
						0,
						255,
						255,
						255
					},
					{
						0,
						255,
						255,
						255
					},
					{
						0,
						255,
						255,
						255
					}
				}
			};
			this.currentArea = 1;
			this.previous_area = 1;
			this.beforeLocked_area = -1;
			this.timerInterval = 20;
			this.pause = 500;
			this.UsbCmdDelay = 190;
			this.currentMode = 1;
			this.previousMode = 1;
			this.Switch2Mode = 1;
			this.lastSTDmode = 1;
			this.lastIDLEmode = 7;
			this.keyboardLockedFlag = -1;
			this.firstTime2RunWaveMode = true;
			this.firstTime2RunBreathingMode = true;
			this.firstTime2RunKLM = true;
			this.deselection = new DoubleAnimation(1.0, 0.0, new Duration(TimeSpan.Parse("0:0:0.5")), FillBehavior.HoldEnd);
			this.selection = new DoubleAnimation(0.0, 1.0, new Duration(TimeSpan.Parse("0:0:0.5")), FillBehavior.HoldEnd);
			this.ColorRampSpeed = new double[7, 4];
			double[] colorChangeStep4WaveMode = new double[3];
			this.ColorChangeStep4WaveMode = colorChangeStep4WaveMode;
			this.ColorChangePeroid = 2.0;
			int[] array2 = new int[3];
			this.reverseFlage4WaveMode = array2;
			this.existBacklightKeyboardDevice = true;
			//base..ctor();
			this.InitializeComponent();
			try
			{
				ServiceController serviceController = new ServiceController("Micro Star SCM");
				string a = serviceController.Status.ToString();
				if (!(a == "Running") && a == "Stopped")
				{
					System.Windows.MessageBox.Show("Micro Star SCM service is stopped!! \r\nStarting the service now.");
					Process process = new Process();
					if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.SystemX86) + "\\MSIService.exe"))
					{
						process.StartInfo.FileName = Environment.GetFolderPath(Environment.SpecialFolder.SystemX86) + "\\MSIService.exe";
					}
					else if (File.Exists(Environment.SystemDirectory + "\\MSIService.exe"))
					{
						process.StartInfo.FileName = Environment.SystemDirectory + "\\MSIService.exe";
					}
					else
					{
						process.StartInfo.FileName = System.Windows.Forms.Application.StartupPath + "\\MSIService.exe";
					}
					process.StartInfo.Arguments = " -start";
					process.StartInfo.Verb = "runas";
					process.Start();
					process.WaitForExit();
					process.Close();
				}
			}
			catch (Exception ex)
			{
				if (ex.Source == "System.ServiceProcess")
				{
					System.Windows.MessageBox.Show("Micro Star SCM service is not installed!! \r\nInstalling the service now.");
					Process process2 = new Process();
					if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.SystemX86) + "\\MSIService.exe"))
					{
						process2.StartInfo.FileName = Environment.GetFolderPath(Environment.SpecialFolder.SystemX86) + "\\MSIService.exe";
					}
					else if (File.Exists(Environment.SystemDirectory + "\\MSIService.exe"))
					{
						process2.StartInfo.FileName = Environment.SystemDirectory + "\\MSIService.exe";
					}
					else
					{
						process2.StartInfo.FileName = System.Windows.Forms.Application.StartupPath + "\\MSIService.exe";
					}
					process2.StartInfo.Arguments = " -install";
					process2.StartInfo.Verb = "runas";
					process2.Start();
					process2.WaitForExit();
					process2.Close();
				}
			}
			ThreadStart start = delegate
			{
				WqlEventQuery query = new WqlEventQuery("SELECT * FROM MSI_Event");
				ManagementScope managementScope = new ManagementScope("\\\\.\\root\\WMI");
				managementScope.Connect();
				this.watcher = new ManagementEventWatcher(managementScope, query);
				this.watcher.EventArrived += new EventArrivedEventHandler(this.WMIEvent_EventArrived);
				this.watcher.Start();
			};
			new Thread(start).Start();
			Process[] processesByName = Process.GetProcessesByName("S-Bar");
			if (processesByName.Length <= 0)
			{
				this.EnableEvent(true);
			}
			this.m_strUSBSymbolicLinks = CUSB._EnumPorts_();
			if (this.m_strUSBSymbolicLinks.Length == 0)
			{
				this.OnUSBChanged(false);
			}
			else
			{
				this.OnUSBChanged(true);
			}
			SystemEvents.DisplaySettingsChanged += new EventHandler(this.SystemEvents_DisplaySettingsChanged);
			this.bIsAlreadyRun = MainWindow.IsAlreadyRunning();
			this.build_and_send_CMD(49, 1, 0, 0, 0);
			if (this.bIsAlreadyRun)
			{
				IntPtr hwnd = MainWindow.FindWindow(null, "MSI Keyboard LED Manager");
				MainWindow.PostMessage(hwnd, 2785, IntPtr.Zero, IntPtr.Zero);
				this.notifyIcon.Dispose();
				base.Close();
				return;
			}
			this.initParam();
		}

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool CallNamedPipe(string lpNamedPipeName, IntPtr lpInBuffer, uint nInBufferSize, IntPtr lpOutBuffer, uint nOutBufferSize, out uint lpBytesRead, uint nTimeOut);

		private void EnableEvent(bool Active)
		{
			int num = Active ? 1 : 0;
			try
			{
				new ASCIIEncoding();
				string text = "root\\WMI:S:MSI_Software:Software:0:" + num.ToString();
				IntPtr intPtr = Marshal.StringToHGlobalAnsi(text);
				IntPtr intPtr2 = Marshal.AllocHGlobal(360);
				uint num2;
				MainWindow.CallNamedPipe("\\\\.\\pipe\\simple", intPtr, (uint)(text.Length + 1), intPtr2, 360u, out num2, 0u);
				Marshal.PtrToStringAnsi(intPtr2);
				Marshal.FreeHGlobal(intPtr);
				Marshal.FreeHGlobal(intPtr2);
				if (Active)
				{
					text = "root\\WMI:S:MSI_Software:Software:3:1";
					intPtr = Marshal.StringToHGlobalAnsi(text);
					intPtr2 = Marshal.AllocHGlobal(360);
					MainWindow.CallNamedPipe("\\\\.\\pipe\\simple", intPtr, (uint)(text.Length + 1), intPtr2, 360u, out num2, 0u);
					Marshal.PtrToStringAnsi(intPtr2);
					Marshal.FreeHGlobal(intPtr);
					Marshal.FreeHGlobal(intPtr2);
				}
			}
			catch
			{
			}
		}

		private void WMIEvent_EventArrived(object sender, EventArrivedEventArgs e)
		{
			ManagementBaseObject newEvent = e.NewEvent;
			int num = int.Parse(newEvent["MSIEvt"].ToString());
			base.Dispatcher.Invoke(new Action<int>(this.WMIEventMath), new object[]
			{
				num
			});
		}

		private void WMIEventMath(int WMIEvent)
		{
			MainWindow.WMIEventCode wMIEventCode = (MainWindow.WMIEventCode)(WMIEvent & 255);
			MainWindow.WMIEventCode wMIEventCode2 = wMIEventCode;
			if (wMIEventCode2 != MainWindow.WMIEventCode.KLM)
			{
				return;
			}
			if (base.Visibility.ToString() == "Visible")
			{
				this.close_MainWindow();
			}
		}

		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);
			HwndSource hwndSource = PresentationSource.FromVisual(this) as HwndSource;
			hwndSource.AddHook(new HwndSourceHook(this.WndProc));
			if (this.firstTime2RunKLM)
			{
				this.close_MainWindow();
				base.ShowInTaskbar = false;
				this.firstTime2RunKLM = false;
			}
		}

		private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			if (msg != 536)
			{
				if (msg == 2785)
				{
					this.show_MainWindow();
				}
			}
			else
			{
				if (wParam.ToInt32() == 7)
				{
					Process[] processesByName = Process.GetProcessesByName("S-Bar");
					if (processesByName.Length <= 0)
					{
						this.EnableEvent(true);
					}
					this.resumeFromS3 = true;
					this.check_if_Settings_exists();
					if (this.currentMode >= 7)
					{
						this.change_to_selected_mode(this.currentMode);
					}
					else
					{
						this.idle_mode();
						this.std_mode();
					}
					this.enteredIDLEmode = false;
				}
				if (wParam.ToInt32() == 4)
				{
					this.modeB4S3 = this.currentMode;
				}
			}
			if (msg == 537 && (wParam.ToInt32() == 32768 || wParam.ToInt32() == 32772))
			{
				DM.DEV_BROADCAST_HDR dEV_BROADCAST_HDR = (DM.DEV_BROADCAST_HDR)Marshal.PtrToStructure(lParam, typeof(DM.DEV_BROADCAST_HDR));
				if (5 == dEV_BROADCAST_HDR.dbch_devicetype)
				{
					DM.DEV_BROADCAST_DEVICEINTERFACE_1 arg_E5_0 = (DM.DEV_BROADCAST_DEVICEINTERFACE_1)Marshal.PtrToStructure(lParam, typeof(DM.DEV_BROADCAST_DEVICEINTERFACE_1));
					this.m_strUSBSymbolicLinks = CUSB._EnumPorts_();
					if (this.m_strUSBSymbolicLinks.Length == 0)
					{
						this.OnUSBChanged(false);
					}
					else
					{
						this.OnUSBChanged(true);
					}
				}
			}
			return IntPtr.Zero;
		}

		private void OnDeviceChanged(ref Message wndMessage)
		{
			if (wndMessage.WParam.ToInt32() == 32768 || wndMessage.WParam.ToInt32() == 32772)
			{
				DM.DEV_BROADCAST_HDR dEV_BROADCAST_HDR = (DM.DEV_BROADCAST_HDR)Marshal.PtrToStructure(wndMessage.LParam, typeof(DM.DEV_BROADCAST_HDR));
				if (5 == dEV_BROADCAST_HDR.dbch_devicetype)
				{
					DM.DEV_BROADCAST_DEVICEINTERFACE_1 arg_68_0 = (DM.DEV_BROADCAST_DEVICEINTERFACE_1)Marshal.PtrToStructure(wndMessage.LParam, typeof(DM.DEV_BROADCAST_DEVICEINTERFACE_1));
					this.m_strUSBSymbolicLinks = CUSB._EnumPorts_();
					if (this.m_strUSBSymbolicLinks.Length == 0)
					{
						this.OnUSBChanged(false);
						return;
					}
					this.OnUSBChanged(true);
				}
			}
		}

		private void OnUSBChanged(bool bEnable)
		{
			this.SetStatus(bEnable ? CMessages.Get(7) : CMessages.Get(8), false);
			this.existBacklightKeyboardDevice = bEnable;
		}

		public void SetStatus(string message, bool addInListBox)
		{
		}

		private void EnableCtls(bool bEnable)
		{
		}

		private void SendCMD()
		{
			this.usbDelay();
			try
			{
				byte b = 0;
				byte[] array = new byte[6];
				array[0] = (byte)this.CMD2send[0];
				array[1] = (byte)this.CMD2send[1];
				array[2] = (byte)this.CMD2send[2];
				array[3] = (byte)this.CMD2send[3];
				array[4] = (byte)this.CMD2send[4];
				this.m_usb = new CUSB();
				this.m_usb.Open(this.m_strUSBSymbolicLinks[0]);
				this.m_usb.CmdWrite(array, ref b);
				this.m_usb.Close();
			}
			catch (Exception ex)
			{
				this.HandleException(ex.Message);
			}
		}

		private void ReadDataBufFromHID(int readCmd, int data1)
		{
			try
			{
				byte[] array = new byte[6];
				array[0] = (byte)readCmd;
				array[1] = (byte)data1;
				this.m_usb = new CUSB();
				this.m_usb.Open(this.m_strUSBSymbolicLinks[0]);
				this.m_usb.CmdRead(ref array);
				this.m_usb.Close();
				this.readDataBuf[0] = (int)array[0];
				this.readDataBuf[1] = (int)array[1];
				this.readDataBuf[2] = (int)array[2];
				this.readDataBuf[3] = (int)array[3];
				this.readDataBuf[4] = (int)array[4];
			}
			catch (Exception ex)
			{
				this.HandleException(ex.Message);
			}
		}

		private void HandleException(string strMsgException)
		{
		}

		private void reliability_test_Click(object sender, EventArgs e)
		{
		}

		private void initParam()
		{
			this.notifyIcon.BalloonTipTitle = "MSI Keyboard LED Manager (Ver:" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + ")";
			this.notifyIcon.BalloonTipIcon = ToolTipIcon.None;
			this.notifyIcon.Text = "MSI Keyboard  LED Manager (Ver:" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + ")";
			Stream stream = System.Windows.Application.GetResourceStream(new Uri("KLM_icon.ico", UriKind.RelativeOrAbsolute)).Stream;
			this.notifyIcon.Icon = new Icon(stream);
			this.notifyIcon.DoubleClick += new EventHandler(this.notifyIcon_DoubleClick);
			this.notifyIcon.Visible = true;
			System.Windows.Forms.ContextMenu contextMenu = new System.Windows.Forms.ContextMenu();
			System.Windows.Forms.MenuItem menuItem = new System.Windows.Forms.MenuItem();
			menuItem.Index = 1;
			menuItem.Text = "Setting";
			menuItem.Click += new EventHandler(this.miTrayIconItem_Setting_Click);
			contextMenu.MenuItems.Add(menuItem);
			System.Windows.Forms.MenuItem menuItem2 = new System.Windows.Forms.MenuItem();
			menuItem2.Index = 0;
			menuItem2.Text = "Exit";
			menuItem2.Click += new EventHandler(this.miTrayIconItem_Exit_Click);
			contextMenu.MenuItems.Add(menuItem2);
			this.notifyIcon.ContextMenu = contextMenu;
			CubicEase easingFunction = new CubicEase();
			this.deselection.EasingFunction = easingFunction;
			this.selection.EasingFunction = easingFunction;
			for (int i = 1; i < 8; i++)
			{
				this.GlowFlagOf8ColorSwatch[i] = 0;
				for (int j = 0; j < 4; j++)
				{
					this.GlowFlagOf29ColorSwatch[i, j] = 0;
				}
			}
			this.check_if_Settings_exists();
			this.timer = new DispatcherTimer();
			this.timer.Interval = TimeSpan.FromMilliseconds((double)this.timerInterval);
			this.timer.Tick += new EventHandler(this.timer_Tick);
			this.timer.Start();
			this.read_and_apply_config_from_settings();
		}

		private void check_if_Settings_exists()
		{
			try
			{
				Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
				this.userConfigPath = configuration.FilePath;
			}
			catch (ConfigurationErrorsException ex)
			{
				string path = "";
				if (!string.IsNullOrEmpty(ex.Filename))
				{
					path = ex.Filename;
				}
				else
				{
					ConfigurationErrorsException ex2 = ex.InnerException as ConfigurationErrorsException;
					if (ex2 != null && !string.IsNullOrEmpty(ex2.Filename))
					{
						path = ex2.Filename;
					}
				}
				if (File.Exists(path))
				{
					File.Delete(path);
				}
				this.restartKLM();
			}
		}

		private void restartKLM()
		{
			System.Windows.Forms.Application.Restart();
		}

		private void write_color_config(int select_area)
		{
			switch (select_area)
			{
			case 1:
				Settings.Default.LeftArea_Color = this.ColorConfig[1, 0];
				Settings.Default.LeftArea_Level = this.ColorConfig[1, 1];
				break;
			case 2:
				Settings.Default.MiddleArea_Color = this.ColorConfig[2, 0];
				Settings.Default.MiddleArea_Level = this.ColorConfig[2, 1];
				break;
			case 3:
				Settings.Default.RightArea_Color = this.ColorConfig[3, 0];
				Settings.Default.RightArea_Level = this.ColorConfig[3, 1];
				break;
			case 4:
				Settings.Default.DualColorLeft_Color = this.ColorConfig[4, 0];
				Settings.Default.DualColorLeft_Level = this.ColorConfig[4, 1];
				break;
			case 5:
				Settings.Default.DualColorRight_Color = this.ColorConfig[5, 0];
				Settings.Default.DualColorRight_Level = this.ColorConfig[5, 1];
				break;
			case 6:
				Settings.Default.WholeArea_Color = this.ColorConfig[6, 0];
				Settings.Default.WholeArea_Level = this.ColorConfig[6, 1];
				break;
			}
			Settings.Default.Save();
		}

		private void read_and_apply_config_from_settings()
		{
			this.choose_color_from_index_table(Settings.Default.LeftArea_Color, Settings.Default.LeftArea_Level, 1);
			this.saveSetting(1);
			this.choose_color_from_index_table(Settings.Default.MiddleArea_Color, Settings.Default.MiddleArea_Level, 2);
			this.saveSetting(2);
			this.choose_color_from_index_table(Settings.Default.RightArea_Color, Settings.Default.RightArea_Level, 3);
			this.saveSetting(3);
			this.choose_color_from_index_table(Settings.Default.DualColorLeft_Color, Settings.Default.DualColorLeft_Level, 4);
			this.saveSetting(4);
			this.choose_color_from_index_table(Settings.Default.DualColorRight_Color, Settings.Default.DualColorRight_Level, 5);
			this.saveSetting(5);
			this.choose_color_from_index_table(Settings.Default.WholeArea_Color, Settings.Default.WholeArea_Level, 6);
			this.saveSetting(6);
			if (this.keyboardLockedFlag != Settings.Default.KBLockState)
			{
				switch (this.keyboardLockedFlag)
				{
				case -1:
					this.keyboard_switch_to_lock();
					break;
				case 1:
					this.keyboard_switch_to_unlock();
					break;
				}
			}
			if (this.currentMode != Settings.Default.Current_Mode)
			{
				this.change_to_selected_mode(Settings.Default.Current_Mode);
			}
			else
			{
				this.change_to_selected_mode(this.currentMode);
			}
			if (this.currentMode >= 7)
			{
				this.option_buttons_idle.Visibility = Visibility.Visible;
				this.idle_mode_bg.Visibility = Visibility.Visible;
				this.option_buttons_std.Visibility = Visibility.Hidden;
				this.std_mode_bg.Visibility = Visibility.Hidden;
			}
			else
			{
				this.option_buttons_std.Visibility = Visibility.Visible;
				this.std_mode_bg.Visibility = Visibility.Visible;
				this.option_buttons_idle.Visibility = Visibility.Hidden;
				this.idle_mode_bg.Visibility = Visibility.Hidden;
			}
			this.lastIDLEmode = Settings.Default.IDLE_Mode_Setting;
		}

		private void change_to_selected_mode(int select_mode)
		{
			switch (select_mode)
			{
			case 1:
				this.std_normal_mode();
				return;
			case 2:
				this.std_gaming_mode();
				return;
			case 3:
				this.std_breathing_mode();
				return;
			case 4:
				this.std_audio_mode();
				return;
			case 5:
				this.std_wave_mode();
				return;
			case 6:
				this.dual_colorball_gradient_left_left.Color = System.Windows.Media.Color.FromArgb(255, (byte)this.RED[4], (byte)this.GREEN[4], (byte)this.BLUE[4]);
				this.dual_colorball_gradient_left_middle_left.Color = System.Windows.Media.Color.FromArgb(255, (byte)this.RED[4], (byte)this.GREEN[4], (byte)this.BLUE[4]);
				this.dual_colorball_gradient_left_middle_right.Color = System.Windows.Media.Color.FromArgb(255, (byte)this.RED[5], (byte)this.GREEN[5], (byte)this.BLUE[5]);
				this.dual_colorball_gradient_left_right.Color = System.Windows.Media.Color.FromArgb(255, (byte)this.RED[5], (byte)this.GREEN[5], (byte)this.BLUE[5]);
				this.std_dual_color_mode();
				return;
			case 7:
				this.idle_off_mode();
				return;
			case 8:
				this.idle_breathing_mode();
				return;
			case 9:
				this.idle_wave_mode();
				return;
			default:
				return;
			}
		}

		private void change_current_mode(int mode)
		{
			this.previousMode = this.currentMode;
			this.currentMode = mode;
			if (this.currentMode >= 7)
			{
				Settings.Default.IDLE_Mode_Setting = this.currentMode;
			}
			Settings.Default.Current_Mode = this.currentMode;
			Settings.Default.Save();
		}

		private void build_and_send_CMD(int cmd, int data1, int data2, int data3, int data4)
		{
			this.CMD2send[0] = cmd;
			this.CMD2send[1] = data1;
			this.CMD2send[2] = data2;
			this.CMD2send[3] = data3;
			this.CMD2send[4] = data4;
			this.SendCMD();
		}

		private void apply_color_change(int select_area)
		{
			this.myBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, (byte)this.RED[select_area], (byte)this.GREEN[select_area], (byte)this.BLUE[select_area]));
			switch (select_area)
			{
			case 1:
				this.change_left_area_color(this.RED[select_area], this.GREEN[select_area], this.BLUE[select_area]);
				if (this.currentMode == 1 || this.currentMode == 2)
				{
					this.build_and_send_CMD(66, select_area, this.ColorConfig[select_area, 0], this.ColorConfig[select_area, 1], 0);
				}
				break;
			case 2:
				this.change_middle_area_color(this.RED[select_area], this.GREEN[select_area], this.BLUE[select_area]);
				if (this.currentMode == 1 || this.currentMode == 2)
				{
					this.build_and_send_CMD(66, select_area, this.ColorConfig[select_area, 0], this.ColorConfig[select_area, 1], 0);
				}
				break;
			case 3:
				this.change_right_area_color(this.RED[select_area], this.GREEN[select_area], this.BLUE[select_area]);
				if (this.currentMode == 1 || this.currentMode == 2)
				{
					this.build_and_send_CMD(66, select_area, this.ColorConfig[select_area, 0], this.ColorConfig[select_area, 1], 0);
				}
				break;
			case 4:
				this.dual_colorball_gradient_left_left.Color = System.Windows.Media.Color.FromArgb(255, (byte)this.RED[select_area], (byte)this.GREEN[select_area], (byte)this.BLUE[select_area]);
				this.dual_colorball_gradient_left_middle_left.Color = System.Windows.Media.Color.FromArgb(255, (byte)this.RED[select_area], (byte)this.GREEN[select_area], (byte)this.BLUE[select_area]);
				this.dualColorModeInit();
				break;
			case 5:
				this.dual_colorball_gradient_left_middle_right.Color = System.Windows.Media.Color.FromArgb(255, (byte)this.RED[select_area], (byte)this.GREEN[select_area], (byte)this.BLUE[select_area]);
				this.dual_colorball_gradient_left_right.Color = System.Windows.Media.Color.FromArgb(255, (byte)this.RED[select_area], (byte)this.GREEN[select_area], (byte)this.BLUE[select_area]);
				this.dualColorModeInit();
				break;
			case 6:
				this.change_left_area_color(this.RED[select_area], this.GREEN[select_area], this.BLUE[select_area]);
				this.change_middle_area_color(this.RED[select_area], this.GREEN[select_area], this.BLUE[select_area]);
				this.change_right_area_color(this.RED[select_area], this.GREEN[select_area], this.BLUE[select_area]);
				if (this.currentMode == 1 || this.currentMode == 2)
				{
					this.build_and_send_CMD(66, 1, this.ColorConfig[select_area, 0], this.ColorConfig[select_area, 1], 0);
					this.build_and_send_CMD(66, 2, this.ColorConfig[select_area, 0], this.ColorConfig[select_area, 1], 0);
					this.build_and_send_CMD(66, 3, this.ColorConfig[select_area, 0], this.ColorConfig[select_area, 1], 0);
				}
				break;
			}
			if (select_area != 4 && select_area != 5)
			{
				if (this.currentMode == 1)
				{
					this.build_and_send_CMD(65, 1, 0, 0, 0);
				}
				else if (this.currentMode == 2)
				{
					this.build_and_send_CMD(65, 2, 0, 0, 0);
				}
				else if (this.currentMode == 3 || this.currentMode == 8)
				{
					this.breathingModeInit();
				}
				else if (this.currentMode == 5 || this.currentMode == 9)
				{
					this.waveModeInit();
				}
			}
			if (!this.sendCMDFlag)
			{
				this.sendCMDFlag = true;
			}
			if (this.currentMode != 7 && this.currentArea == select_area)
			{
				if (this.currentMode != 1 && this.currentMode != 2)
				{
					this.selected_8color(this.ColorConfig[select_area, 0]);
					return;
				}
				this.selected_29color(this.ColorConfig[select_area, 0], this.ColorConfig[select_area, 1]);
			}
		}

		private void choose_color_from_index_table(int color, int level, int select_area)
		{
			if (this.currentMode != 7)
			{
				if (this.currentMode != 1 && this.currentMode != 2)
				{
					this.deselected_8color(this.ColorConfig[select_area, 0]);
				}
				else
				{
					this.deselected_29color(this.ColorConfig[select_area, 0], this.ColorConfig[select_area, 1]);
				}
			}
			this.RED[select_area] = this.ColorIndexTable4OnScreenDisplay[color, level, 1];
			this.GREEN[select_area] = this.ColorIndexTable4OnScreenDisplay[color, level, 2];
			this.BLUE[select_area] = this.ColorIndexTable4OnScreenDisplay[color, level, 3];
			this.ColorConfig[select_area, 0] = color;
			this.ColorConfig[select_area, 1] = level;
			this.write_color_config(select_area);
		}

		private void selectArea(int select_area)
		{
			this.kb_left_frame_blur.Visibility = Visibility.Hidden;
			this.kb_left_frame_white.Visibility = Visibility.Hidden;
			this.kb_middle_frame_blur.Visibility = Visibility.Hidden;
			this.kb_middle_frame_white.Visibility = Visibility.Hidden;
			this.kb_right_frame_blur.Visibility = Visibility.Hidden;
			this.kb_right_frame_white.Visibility = Visibility.Hidden;
			this.kb_all_blur.Visibility = Visibility.Hidden;
			this.kb_all_frame.Visibility = Visibility.Hidden;
			this.color_ball_right.Visibility = Visibility.Hidden;
			this.color_ball_left.Visibility = Visibility.Hidden;
			this.color_ball_middle_dual.Visibility = Visibility.Hidden;
			this.color_ball_middle.Visibility = Visibility.Hidden;
			for (int i = 1; i <= 6; i++)
			{
				if (i != select_area)
				{
					if (this.currentMode != 1 && this.currentMode != 2)
					{
						this.deselected_8color(this.ColorConfig[i, 0]);
					}
					else
					{
						this.deselected_29color(this.ColorConfig[i, 0], this.ColorConfig[i, 1]);
					}
				}
			}
			if (this.currentMode != 1 && this.currentMode != 2)
			{
				this.selected_8color(this.ColorConfig[select_area, 0]);
			}
			else
			{
				this.selected_29color(this.ColorConfig[select_area, 0], this.ColorConfig[select_area, 1]);
			}
			switch (select_area)
			{
			case 0:
				if (this.keyboardLockedFlag == -1)
				{
					this.color_ball_left.Visibility = Visibility.Visible;
					this.color_ball_right.Visibility = Visibility.Visible;
					this.color_ball_middle.Visibility = Visibility.Visible;
				}
				break;
			case 1:
				this.kb_left_frame_blur.Visibility = Visibility.Visible;
				this.kb_left_frame_white.Visibility = Visibility.Visible;
				this.color_ball_left.Visibility = Visibility.Visible;
				this.color_ball_right.Visibility = Visibility.Visible;
				this.color_ball_middle.Visibility = Visibility.Visible;
				break;
			case 2:
				this.kb_middle_frame_blur.Visibility = Visibility.Visible;
				this.kb_middle_frame_white.Visibility = Visibility.Visible;
				this.color_ball_left.Visibility = Visibility.Visible;
				this.color_ball_right.Visibility = Visibility.Visible;
				this.color_ball_middle.Visibility = Visibility.Visible;
				break;
			case 3:
				this.kb_right_frame_blur.Visibility = Visibility.Visible;
				this.kb_right_frame_white.Visibility = Visibility.Visible;
				this.color_ball_left.Visibility = Visibility.Visible;
				this.color_ball_right.Visibility = Visibility.Visible;
				this.color_ball_middle.Visibility = Visibility.Visible;
				break;
			case 4:
			case 5:
				this.kb_all_blur.Visibility = Visibility.Visible;
				this.kb_all_frame.Visibility = Visibility.Visible;
				this.color_ball_middle_dual.Visibility = Visibility.Visible;
				break;
			case 6:
				if (this.currentMode != 7)
				{
					this.kb_all_blur.Visibility = Visibility.Visible;
					this.kb_all_frame.Visibility = Visibility.Visible;
					this.color_ball_middle.Visibility = Visibility.Visible;
				}
				break;
			}
			this.currentArea = select_area;
		}

		private void turnOffColor(int select_area)
		{
			this.RED[select_area] = 35;
			this.GREEN[select_area] = 25;
			this.BLUE[select_area] = 22;
			this.myBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, (byte)this.RED[select_area], (byte)this.GREEN[select_area], (byte)this.BLUE[select_area]));
			switch (select_area)
			{
			case 1:
				this.left_color.Color = System.Windows.Media.Color.FromArgb(255, (byte)this.RED[select_area], (byte)this.GREEN[select_area], (byte)this.BLUE[select_area]);
				this.left_middle_color.Color = System.Windows.Media.Color.FromArgb(255, (byte)this.RED[select_area], (byte)this.GREEN[select_area], (byte)this.BLUE[select_area]);
				this.colorball_left_1.Fill = this.myBrush;
				break;
			case 2:
				this.middle_left_color.Color = System.Windows.Media.Color.FromArgb(255, (byte)this.RED[select_area], (byte)this.GREEN[select_area], (byte)this.BLUE[select_area]);
				this.middle_right_color.Color = System.Windows.Media.Color.FromArgb(255, (byte)this.RED[select_area], (byte)this.GREEN[select_area], (byte)this.BLUE[select_area]);
				this.colorball_middle.Fill = this.myBrush;
				break;
			case 3:
				this.right_middle_color.Color = System.Windows.Media.Color.FromArgb(255, (byte)this.RED[select_area], (byte)this.GREEN[select_area], (byte)this.BLUE[select_area]);
				this.right_color.Color = System.Windows.Media.Color.FromArgb(255, (byte)this.RED[select_area], (byte)this.GREEN[select_area], (byte)this.BLUE[select_area]);
				this.colorball_right_1.Fill = this.myBrush;
				break;
			}
			if (this.currentMode != 2 && this.currentMode != 7)
			{
				this.build_and_send_CMD(66, select_area, 0, 1, 0);
				if (this.keyboardLockedFlag == -1)
				{
					this.build_and_send_CMD(65, this.currentMode, 0, 0, 0);
				}
			}
		}

		private void sendCMD_to_restore_3area_color()
		{
			this.build_and_send_CMD(66, 1, this.ColorConfig[1, 0], this.ColorConfig[1, 1], 0);
			this.build_and_send_CMD(66, 2, this.ColorConfig[2, 0], this.ColorConfig[2, 1], 0);
			this.build_and_send_CMD(66, 3, this.ColorConfig[3, 0], this.ColorConfig[3, 1], 0);
		}

		private void change_left_area_color(int red, int green, int blue)
		{
			if (this.area_switch[1] == 1)
			{
				this.left_color.Color = System.Windows.Media.Color.FromArgb(255, (byte)red, (byte)green, (byte)blue);
				this.left_middle_color.Color = System.Windows.Media.Color.FromArgb(255, (byte)red, (byte)green, (byte)blue);
				if (this.currentArea == 1 || this.keyboardLockedFlag < 0)
				{
					this.myBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, (byte)red, (byte)green, (byte)blue));
					this.colorball_left_1.Fill = this.myBrush;
				}
			}
		}

		private void change_middle_area_color(int red, int green, int blue)
		{
			if (this.area_switch[2] == 1 || this.area_switch[6] == 1)
			{
				this.middle_left_color.Color = System.Windows.Media.Color.FromArgb(255, (byte)red, (byte)green, (byte)blue);
				this.middle_right_color.Color = System.Windows.Media.Color.FromArgb(255, (byte)red, (byte)green, (byte)blue);
				if (this.currentArea == 2 || this.currentArea == 6 || this.keyboardLockedFlag == -1)
				{
					this.myBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, (byte)red, (byte)green, (byte)blue));
					this.colorball_middle.Fill = this.myBrush;
				}
			}
		}

		private void change_right_area_color(int red, int green, int blue)
		{
			if (this.area_switch[3] == 1)
			{
				this.right_middle_color.Color = System.Windows.Media.Color.FromArgb(255, (byte)red, (byte)green, (byte)blue);
				this.right_color.Color = System.Windows.Media.Color.FromArgb(255, (byte)red, (byte)green, (byte)blue);
				if (this.currentArea == 3 || this.keyboardLockedFlag < 0)
				{
					this.myBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, (byte)red, (byte)green, (byte)blue));
					this.colorball_right_1.Fill = this.myBrush;
				}
			}
		}

		private void change_current_color_to_level2()
		{
			this.choose_color_from_index_table(this.ColorConfig[1, 0], 1, 1);
			this.saveSetting(1);
			this.choose_color_from_index_table(this.ColorConfig[2, 0], 1, 2);
			this.saveSetting(2);
			this.choose_color_from_index_table(this.ColorConfig[3, 0], 1, 3);
			this.saveSetting(3);
			this.choose_color_from_index_table(this.ColorConfig[6, 0], 1, 6);
			this.saveSetting(6);
		}

		private void search_and_save_color_index_from_RGB(int select_area, int red, int green, int blue)
		{
			for (int i = 0; i < 8; i++)
			{
				for (int j = 1; j < 5; j++)
				{
					if (this.RED[select_area] == this.ColorIndexTable4OnScreenDisplay[i, j, 1] && this.GREEN[select_area] == this.ColorIndexTable4OnScreenDisplay[i, j, 2] && this.BLUE[select_area] == this.ColorIndexTable4OnScreenDisplay[i, j, 3])
					{
						this.ColorConfig[select_area, 0] = i;
						this.ColorConfig[select_area, 1] = j;
					}
				}
			}
		}

		private void saveSetting(int select_area)
		{
			if (this.area_switch[select_area] > 0)
			{
				this.rgb[select_area, 1] = this.RED[select_area];
				this.rgb[select_area, 2] = this.GREEN[select_area];
				this.rgb[select_area, 3] = this.BLUE[select_area];
			}
		}

		private void readSetting(int select_area)
		{
			this.RED[select_area] = this.rgb[select_area, 1];
			this.GREEN[select_area] = this.rgb[select_area, 2];
			this.BLUE[select_area] = this.rgb[select_area, 3];
			this.apply_color_change(select_area);
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			if (!this.Minimized)
			{
				switch (this.currentMode)
				{
				case 3:
				case 8:
					if (!this.stateChangeFlag)
					{
						this.breathingMode(this.ColorChangeStep);
					}
					else
					{
						this.timeCounter += this.timerInterval;
						if (this.timeCounter >= this.pause)
						{
							this.stateChangeFlag = false;
							this.timeCounter = 0;
						}
					}
					break;
				case 4:
					this.audioMode();
					if (this.sendCMDFlag)
					{
						this.SendCMD();
						this.sendCMDFlag = false;
					}
					break;
				case 5:
				case 9:
					this.waveMode(this.ColorChangeStep4WaveMode);
					for (int i = 0; i < 3; i++)
					{
						if (this.reverseFlage4WaveMode[i] == 0)
						{
							if (this.ColorChangeStep4WaveMode[i] < this.ColorChangeTimesPerSec * this.ColorChangePeroid)
							{
								this.ColorChangeStep4WaveMode[i] += 1.0;
							}
							else if (this.ColorChangeStep4WaveMode[i] >= this.ColorChangeTimesPerSec * this.ColorChangePeroid)
							{
								this.ColorChangeStep4WaveMode[i] -= 1.0;
								this.reverseFlage4WaveMode[i] = 1;
							}
						}
						else if (this.reverseFlage4WaveMode[i] == 1)
						{
							if (this.ColorChangeStep4WaveMode[i] > 0.0)
							{
								this.ColorChangeStep4WaveMode[i] -= 1.0;
							}
							else if (this.ColorChangeStep4WaveMode[i] <= 0.0)
							{
								this.ColorChangeStep4WaveMode[i] += 1.0;
								this.reverseFlage4WaveMode[i] = 0;
							}
						}
					}
					break;
				case 6:
					if (!this.stateChangeFlag)
					{
						this.dualColorMode(this.ColorChangeStep);
					}
					else
					{
						this.timeCounter += this.timerInterval;
						if (this.timeCounter >= this.pause)
						{
							this.stateChangeFlag = false;
							this.timeCounter = 0;
						}
					}
					break;
				}
				if (this.currentMode == 6 || this.currentMode == 3 || this.currentMode == 8)
				{
					if (this.reverseFlag == 0 && !this.stateChangeFlag)
					{
						if ((double)this.ColorChangeStep < this.ColorChangeTimesPerSec * this.ColorChangePeroid)
						{
							this.ColorChangeStep++;
							return;
						}
						if ((double)this.ColorChangeStep >= this.ColorChangeTimesPerSec * this.ColorChangePeroid)
						{
							this.ColorChangeStep--;
							this.reverseFlag = 1;
							this.stateChangeFlag = true;
							return;
						}
					}
					else if (this.reverseFlag == 1 && !this.stateChangeFlag)
					{
						if (this.ColorChangeStep > 0)
						{
							this.ColorChangeStep--;
							return;
						}
						if (this.ColorChangeStep <= 0)
						{
							this.ColorChangeStep++;
							this.reverseFlag = 0;
							this.stateChangeFlag = true;
						}
					}
				}
			}
		}

		private int ComputeRampSpeed(double StartColor, double EndColor, double ChangePeroid)
		{
			if (StartColor - EndColor == 0.0)
			{
				return 0;
			}
			return (int)Math.Ceiling(1.0 / (Math.Abs(StartColor - EndColor) / ChangePeroid) * 250.0);
		}

		private void usbDelay()
		{
			Thread.Sleep(this.UsbCmdDelay);
		}

		private void normalModeInit(int select_area)
		{
			if (select_area == 1 || select_area == 2 || select_area == 3)
			{
				this.build_and_send_CMD(66, select_area, this.ColorConfig[select_area, 0], this.ColorConfig[select_area, 1], 0);
			}
			this.build_and_send_CMD(65, 1, 0, 0, 0);
		}

		private void audioMode()
		{
		}

		private void dualColorModeInit()
		{
			this.ColorChangeStep = 0;
			this.reverseFlag = 0;
			this.ColorChangeTimesPerSec = 1000.0 / (double)this.timerInterval;
			this.ColorRampSpeed[0, 1] = (double)(this.RED[5] - this.RED[4]) / this.ColorChangePeroid / this.ColorChangeTimesPerSec;
			this.ColorRampSpeed[0, 2] = (double)(this.GREEN[5] - this.GREEN[4]) / this.ColorChangePeroid / this.ColorChangeTimesPerSec;
			this.ColorRampSpeed[0, 3] = (double)(this.BLUE[5] - this.BLUE[4]) / this.ColorChangePeroid / this.ColorChangeTimesPerSec;
			int data = this.ComputeRampSpeed((double)this.ColorIndexTable[this.ColorConfig[4, 0], this.ColorConfig[4, 1], 1], (double)this.ColorIndexTable[this.ColorConfig[5, 0], this.ColorConfig[5, 1], 1], this.ColorChangePeroid);
			int data2 = this.ComputeRampSpeed((double)this.ColorIndexTable[this.ColorConfig[4, 0], this.ColorConfig[4, 1], 2], (double)this.ColorIndexTable[this.ColorConfig[5, 0], this.ColorConfig[5, 1], 2], this.ColorChangePeroid);
			int data3 = this.ComputeRampSpeed((double)this.ColorIndexTable[this.ColorConfig[4, 0], this.ColorConfig[4, 1], 3], (double)this.ColorIndexTable[this.ColorConfig[5, 0], this.ColorConfig[5, 1], 3], this.ColorChangePeroid);
			this.build_and_send_CMD(67, 1, this.ColorConfig[4, 0], this.ColorConfig[4, 1], 0);
			this.build_and_send_CMD(67, 2, this.ColorConfig[5, 0], this.ColorConfig[5, 1], 0);
			this.build_and_send_CMD(67, 3, data, data2, data3);
			this.build_and_send_CMD(67, 4, this.ColorConfig[4, 0], this.ColorConfig[4, 1], 0);
			this.build_and_send_CMD(67, 5, this.ColorConfig[5, 0], this.ColorConfig[5, 1], 0);
			this.build_and_send_CMD(67, 6, data, data2, data3);
			this.build_and_send_CMD(67, 7, this.ColorConfig[4, 0], this.ColorConfig[4, 1], 0);
			this.build_and_send_CMD(67, 8, this.ColorConfig[5, 0], this.ColorConfig[5, 1], 0);
			this.build_and_send_CMD(67, 9, data, data2, data3);
			this.build_and_send_CMD(65, 6, 0, 0, 0);
		}

		private void dualColorMode(int colorChangeTimeStep)
		{
			int num = this.RED[4] + (int)(this.ColorRampSpeed[0, 1] * (double)colorChangeTimeStep);
			int num2 = this.GREEN[4] + (int)(this.ColorRampSpeed[0, 2] * (double)colorChangeTimeStep);
			int num3 = this.BLUE[4] + (int)(this.ColorRampSpeed[0, 3] * (double)colorChangeTimeStep);
			this.change_left_area_color(num, num2, num3);
			this.change_middle_area_color(num, num2, num3);
			this.change_right_area_color(num, num2, num3);
		}

		private void breathingModeInit()
		{
			this.ColorChangeStep = 0;
			this.reverseFlag = 0;
			this.ColorChangeTimesPerSec = 1000.0 / (double)this.timerInterval;
			if (this.currentArea == 1 || (this.firstTime2RunBreathingMode && this.currentArea != 6) || this.switchFromKBLocked || this.previousMode == 7 || this.previousMode == 2)
			{
				this.ColorRampSpeed[1, 1] = (double)(-(double)this.RED[1]) / this.ColorChangePeroid / this.ColorChangeTimesPerSec;
				this.ColorRampSpeed[1, 2] = (double)(-(double)this.GREEN[1]) / this.ColorChangePeroid / this.ColorChangeTimesPerSec;
				this.ColorRampSpeed[1, 3] = (double)(-(double)this.BLUE[1]) / this.ColorChangePeroid / this.ColorChangeTimesPerSec;
				int data = this.ComputeRampSpeed((double)this.ColorIndexTable[this.ColorConfig[1, 0], this.ColorConfig[1, 1], 1], 0.0, this.ColorChangePeroid);
				int data2 = this.ComputeRampSpeed((double)this.ColorIndexTable[this.ColorConfig[1, 0], this.ColorConfig[1, 1], 2], 0.0, this.ColorChangePeroid);
				int data3 = this.ComputeRampSpeed((double)this.ColorIndexTable[this.ColorConfig[1, 0], this.ColorConfig[1, 1], 3], 0.0, this.ColorChangePeroid);
				this.build_and_send_CMD(67, 1, this.ColorConfig[1, 0], this.ColorConfig[1, 1], 0);
				this.build_and_send_CMD(67, 2, 0, 1, 0);
				this.build_and_send_CMD(67, 3, data, data2, data3);
			}
			if (this.currentArea == 2 || (this.firstTime2RunBreathingMode && this.currentArea != 6) || this.switchFromKBLocked || this.previousMode == 7 || this.previousMode == 2)
			{
				this.ColorRampSpeed[2, 1] = (double)(-(double)this.RED[2]) / this.ColorChangePeroid / this.ColorChangeTimesPerSec;
				this.ColorRampSpeed[2, 2] = (double)(-(double)this.GREEN[2]) / this.ColorChangePeroid / this.ColorChangeTimesPerSec;
				this.ColorRampSpeed[2, 3] = (double)(-(double)this.BLUE[2]) / this.ColorChangePeroid / this.ColorChangeTimesPerSec;
				int data = this.ComputeRampSpeed((double)this.ColorIndexTable[this.ColorConfig[2, 0], this.ColorConfig[2, 1], 1], 0.0, this.ColorChangePeroid);
				int data2 = this.ComputeRampSpeed((double)this.ColorIndexTable[this.ColorConfig[2, 0], this.ColorConfig[2, 1], 2], 0.0, this.ColorChangePeroid);
				int data3 = this.ComputeRampSpeed((double)this.ColorIndexTable[this.ColorConfig[2, 0], this.ColorConfig[2, 1], 3], 0.0, this.ColorChangePeroid);
				this.build_and_send_CMD(67, 4, this.ColorConfig[2, 0], this.ColorConfig[2, 1], 0);
				this.build_and_send_CMD(67, 5, 0, 1, 0);
				this.build_and_send_CMD(67, 6, data, data2, data3);
			}
			if (this.currentArea == 3 || (this.firstTime2RunBreathingMode && this.currentArea != 6) || this.switchFromKBLocked || this.previousMode == 7 || this.previousMode == 2)
			{
				this.ColorRampSpeed[3, 1] = (double)(-(double)this.RED[3]) / this.ColorChangePeroid / this.ColorChangeTimesPerSec;
				this.ColorRampSpeed[3, 2] = (double)(-(double)this.GREEN[3]) / this.ColorChangePeroid / this.ColorChangeTimesPerSec;
				this.ColorRampSpeed[3, 3] = (double)(-(double)this.BLUE[3]) / this.ColorChangePeroid / this.ColorChangeTimesPerSec;
				int data = this.ComputeRampSpeed((double)this.ColorIndexTable[this.ColorConfig[3, 0], this.ColorConfig[3, 1], 1], 0.0, this.ColorChangePeroid);
				int data2 = this.ComputeRampSpeed((double)this.ColorIndexTable[this.ColorConfig[3, 0], this.ColorConfig[3, 1], 2], 0.0, this.ColorChangePeroid);
				int data3 = this.ComputeRampSpeed((double)this.ColorIndexTable[this.ColorConfig[3, 0], this.ColorConfig[3, 1], 3], 0.0, this.ColorChangePeroid);
				this.build_and_send_CMD(67, 7, this.ColorConfig[3, 0], this.ColorConfig[3, 1], 0);
				this.build_and_send_CMD(67, 8, 0, 1, 0);
				this.build_and_send_CMD(67, 9, data, data2, data3);
			}
			if (this.currentArea == 6)
			{
				this.ColorRampSpeed[6, 1] = (double)(-(double)this.RED[6]) / this.ColorChangePeroid / this.ColorChangeTimesPerSec;
				this.ColorRampSpeed[6, 2] = (double)(-(double)this.GREEN[6]) / this.ColorChangePeroid / this.ColorChangeTimesPerSec;
				this.ColorRampSpeed[6, 3] = (double)(-(double)this.BLUE[6]) / this.ColorChangePeroid / this.ColorChangeTimesPerSec;
				int data = this.ComputeRampSpeed((double)this.ColorIndexTable[this.ColorConfig[6, 0], this.ColorConfig[6, 1], 1], 0.0, this.ColorChangePeroid);
				int data2 = this.ComputeRampSpeed((double)this.ColorIndexTable[this.ColorConfig[6, 0], this.ColorConfig[6, 1], 2], 0.0, this.ColorChangePeroid);
				int data3 = this.ComputeRampSpeed((double)this.ColorIndexTable[this.ColorConfig[6, 0], this.ColorConfig[6, 1], 3], 0.0, this.ColorChangePeroid);
				this.build_and_send_CMD(67, 1, this.ColorConfig[6, 0], this.ColorConfig[6, 1], 0);
				this.build_and_send_CMD(67, 2, 0, 1, 0);
				this.build_and_send_CMD(67, 3, data, data2, data3);
				this.build_and_send_CMD(67, 4, this.ColorConfig[6, 0], this.ColorConfig[6, 1], 0);
				this.build_and_send_CMD(67, 5, 0, 1, 0);
				this.build_and_send_CMD(67, 6, data, data2, data3);
				this.build_and_send_CMD(67, 7, this.ColorConfig[6, 0], this.ColorConfig[6, 1], 0);
				this.build_and_send_CMD(67, 8, 0, 1, 0);
				this.build_and_send_CMD(67, 9, data, data2, data3);
			}
			this.firstTime2RunBreathingMode = false;
			this.switchFromKBLocked = false;
			if (this.currentMode == 3)
			{
				this.build_and_send_CMD(65, 3, 0, 0, 0);
				return;
			}
			if (this.currentMode == 8)
			{
				this.build_and_send_CMD(65, 8, 0, 0, 0);
			}
		}

		private void breathingMode(int colorChangeTimeStep)
		{
			int[] array = new int[4];
			int[] array2 = new int[4];
			int[] array3 = new int[4];
			if (this.keyboardLockedFlag == -1)
			{
				for (int i = 1; i < 4; i++)
				{
					array[i] = this.RED[i] + (int)(this.ColorRampSpeed[i, 1] * (double)colorChangeTimeStep);
					array2[i] = this.GREEN[i] + (int)(this.ColorRampSpeed[i, 2] * (double)colorChangeTimeStep);
					array3[i] = this.BLUE[i] + (int)(this.ColorRampSpeed[i, 3] * (double)colorChangeTimeStep);
				}
			}
			else
			{
				for (int j = 1; j < 4; j++)
				{
					array[j] = this.RED[6] + (int)(this.ColorRampSpeed[6, 1] * (double)colorChangeTimeStep);
					array2[j] = this.GREEN[6] + (int)(this.ColorRampSpeed[6, 2] * (double)colorChangeTimeStep);
					array3[j] = this.BLUE[6] + (int)(this.ColorRampSpeed[6, 3] * (double)colorChangeTimeStep);
				}
			}
			this.change_left_area_color(array[1], array2[1], array3[1]);
			this.change_middle_area_color(array[2], array2[2], array3[2]);
			this.change_right_area_color(array[3], array2[3], array3[3]);
		}

		private void waveModeInit()
		{
			this.reverseFlage4WaveMode[0] = 0;
			this.reverseFlage4WaveMode[1] = 0;
			this.reverseFlage4WaveMode[2] = 0;
			this.ColorChangeTimesPerSec = 1000.0 / (double)this.timerInterval;
			this.ColorChangeStep4WaveMode[0] = 0.0;
			this.ColorChangeStep4WaveMode[1] = this.ColorChangeTimesPerSec * this.ColorChangePeroid / 3.0;
			this.ColorChangeStep4WaveMode[2] = this.ColorChangeTimesPerSec * this.ColorChangePeroid * 2.0 / 3.0;
			if (this.currentArea == 1 || (this.firstTime2RunWaveMode && this.currentArea != 6) || this.switchFromKBLocked || this.previousMode == 7 || this.previousMode == 2)
			{
				this.ColorRampSpeed[1, 1] = (double)(-(double)this.RED[1]) / this.ColorChangePeroid / this.ColorChangeTimesPerSec;
				this.ColorRampSpeed[1, 2] = (double)(-(double)this.GREEN[1]) / this.ColorChangePeroid / this.ColorChangeTimesPerSec;
				this.ColorRampSpeed[1, 3] = (double)(-(double)this.BLUE[1]) / this.ColorChangePeroid / this.ColorChangeTimesPerSec;
				int data = this.ComputeRampSpeed((double)this.ColorIndexTable[this.ColorConfig[1, 0], this.ColorConfig[1, 1], 1], 0.0, this.ColorChangePeroid);
				int data2 = this.ComputeRampSpeed((double)this.ColorIndexTable[this.ColorConfig[1, 0], this.ColorConfig[1, 1], 2], 0.0, this.ColorChangePeroid);
				int data3 = this.ComputeRampSpeed((double)this.ColorIndexTable[this.ColorConfig[1, 0], this.ColorConfig[1, 1], 3], 0.0, this.ColorChangePeroid);
				this.build_and_send_CMD(67, 1, this.ColorConfig[1, 0], this.ColorConfig[1, 1], 0);
				this.build_and_send_CMD(67, 2, 0, 1, 0);
				this.build_and_send_CMD(67, 3, data, data2, data3);
			}
			if (this.currentArea == 2 || (this.firstTime2RunWaveMode && this.currentArea != 6) || this.switchFromKBLocked || this.previousMode == 7 || this.previousMode == 2)
			{
				this.ColorRampSpeed[2, 1] = (double)(-(double)this.RED[2]) / this.ColorChangePeroid / this.ColorChangeTimesPerSec;
				this.ColorRampSpeed[2, 2] = (double)(-(double)this.GREEN[2]) / this.ColorChangePeroid / this.ColorChangeTimesPerSec;
				this.ColorRampSpeed[2, 3] = (double)(-(double)this.BLUE[2]) / this.ColorChangePeroid / this.ColorChangeTimesPerSec;
				int data = this.ComputeRampSpeed((double)this.ColorIndexTable[this.ColorConfig[2, 0], this.ColorConfig[2, 1], 1], 0.0, this.ColorChangePeroid);
				int data2 = this.ComputeRampSpeed((double)this.ColorIndexTable[this.ColorConfig[2, 0], this.ColorConfig[2, 1], 2], 0.0, this.ColorChangePeroid);
				int data3 = this.ComputeRampSpeed((double)this.ColorIndexTable[this.ColorConfig[2, 0], this.ColorConfig[2, 1], 3], 0.0, this.ColorChangePeroid);
				this.build_and_send_CMD(67, 4, this.ColorConfig[2, 0], this.ColorConfig[2, 1], 0);
				this.build_and_send_CMD(67, 5, 0, 1, 0);
				this.build_and_send_CMD(67, 6, data, data2, data3);
			}
			if (this.currentArea == 3 || (this.firstTime2RunWaveMode && this.currentArea != 6) || this.switchFromKBLocked || this.previousMode == 7 || this.previousMode == 2)
			{
				this.ColorRampSpeed[3, 1] = (double)(-(double)this.RED[3]) / this.ColorChangePeroid / this.ColorChangeTimesPerSec;
				this.ColorRampSpeed[3, 2] = (double)(-(double)this.GREEN[3]) / this.ColorChangePeroid / this.ColorChangeTimesPerSec;
				this.ColorRampSpeed[3, 3] = (double)(-(double)this.BLUE[3]) / this.ColorChangePeroid / this.ColorChangeTimesPerSec;
				int data = this.ComputeRampSpeed((double)this.ColorIndexTable[this.ColorConfig[3, 0], this.ColorConfig[3, 1], 1], 0.0, this.ColorChangePeroid);
				int data2 = this.ComputeRampSpeed((double)this.ColorIndexTable[this.ColorConfig[3, 0], this.ColorConfig[3, 1], 2], 0.0, this.ColorChangePeroid);
				int data3 = this.ComputeRampSpeed((double)this.ColorIndexTable[this.ColorConfig[3, 0], this.ColorConfig[3, 1], 3], 0.0, this.ColorChangePeroid);
				this.build_and_send_CMD(67, 7, this.ColorConfig[3, 0], this.ColorConfig[3, 1], 0);
				this.build_and_send_CMD(67, 8, 0, 1, 0);
				this.build_and_send_CMD(67, 9, data, data2, data3);
			}
			if (this.currentArea == 6)
			{
				this.ColorRampSpeed[6, 1] = (double)(-(double)this.RED[6]) / this.ColorChangePeroid / this.ColorChangeTimesPerSec;
				this.ColorRampSpeed[6, 2] = (double)(-(double)this.GREEN[6]) / this.ColorChangePeroid / this.ColorChangeTimesPerSec;
				this.ColorRampSpeed[6, 3] = (double)(-(double)this.BLUE[6]) / this.ColorChangePeroid / this.ColorChangeTimesPerSec;
				int data = this.ComputeRampSpeed((double)this.ColorIndexTable[this.ColorConfig[6, 0], this.ColorConfig[6, 1], 1], 0.0, this.ColorChangePeroid);
				int data2 = this.ComputeRampSpeed((double)this.ColorIndexTable[this.ColorConfig[6, 0], this.ColorConfig[6, 1], 2], 0.0, this.ColorChangePeroid);
				int data3 = this.ComputeRampSpeed((double)this.ColorIndexTable[this.ColorConfig[6, 0], this.ColorConfig[6, 1], 3], 0.0, this.ColorChangePeroid);
				this.build_and_send_CMD(67, 1, this.ColorConfig[6, 0], this.ColorConfig[6, 1], 0);
				this.build_and_send_CMD(67, 2, 0, 1, 0);
				this.build_and_send_CMD(67, 3, data, data2, data3);
				this.build_and_send_CMD(67, 4, this.ColorConfig[6, 0], this.ColorConfig[6, 1], 0);
				this.build_and_send_CMD(67, 5, 0, 1, 0);
				this.build_and_send_CMD(67, 6, data, data2, data3);
				this.build_and_send_CMD(67, 7, this.ColorConfig[6, 0], this.ColorConfig[6, 1], 0);
				this.build_and_send_CMD(67, 8, 0, 1, 0);
				this.build_and_send_CMD(67, 9, data, data2, data3);
			}
			this.firstTime2RunWaveMode = false;
			this.switchFromKBLocked = false;
			if (this.currentMode == 5)
			{
				this.build_and_send_CMD(65, 5, 0, 0, 0);
				return;
			}
			if (this.currentMode == 9)
			{
				this.build_and_send_CMD(65, 9, 0, 0, 0);
			}
		}

		private void waveMode(double[] colorChangeTimeStep)
		{
			int[] array = new int[4];
			int[] array2 = new int[4];
			int[] array3 = new int[4];
			if (this.keyboardLockedFlag == -1)
			{
				for (int i = 1; i < 4; i++)
				{
					array[i] = this.RED[i] + (int)(this.ColorRampSpeed[i, 1] * this.ColorChangeStep4WaveMode[i - 1]);
					array2[i] = this.GREEN[i] + (int)(this.ColorRampSpeed[i, 2] * this.ColorChangeStep4WaveMode[i - 1]);
					array3[i] = this.BLUE[i] + (int)(this.ColorRampSpeed[i, 3] * this.ColorChangeStep4WaveMode[i - 1]);
				}
			}
			else
			{
				for (int j = 1; j < 4; j++)
				{
					array[j] = this.RED[6] + (int)(this.ColorRampSpeed[6, 1] * this.ColorChangeStep4WaveMode[j - 1]);
					array2[j] = this.GREEN[6] + (int)(this.ColorRampSpeed[6, 2] * this.ColorChangeStep4WaveMode[j - 1]);
					array3[j] = this.BLUE[6] + (int)(this.ColorRampSpeed[6, 3] * this.ColorChangeStep4WaveMode[j - 1]);
				}
			}
			this.change_left_area_color(array[1], array2[1], array3[1]);
			this.change_middle_area_color(array[2], array2[2], array3[2]);
			this.change_right_area_color(array[3], array2[3], array3[3]);
		}

		private void build_and_send_CMD_from_Color_Index(int color, int level)
		{
			if (this.currentMode == 1 || this.currentMode == 2 || this.currentMode == 4)
			{
				if (this.currentArea == 6)
				{
					this.CMD2send[0] = 66;
					this.CMD2send[1] = 1;
					this.CMD2send[2] = color;
					this.CMD2send[3] = level;
					this.CMD2send[4] = 0;
					this.SendCMD();
					this.CMD2send[0] = 66;
					this.CMD2send[1] = 2;
					this.CMD2send[2] = color;
					this.CMD2send[3] = level;
					this.CMD2send[4] = 0;
					this.SendCMD();
					this.CMD2send[0] = 66;
					this.CMD2send[1] = 3;
					this.CMD2send[2] = color;
					this.CMD2send[3] = level;
					this.CMD2send[4] = 0;
					this.SendCMD();
					return;
				}
				if (this.currentArea == 1 || this.currentArea == 2 || this.currentArea == 3)
				{
					this.CMD2send[0] = 66;
					this.CMD2send[1] = this.currentArea;
					this.CMD2send[2] = color;
					this.CMD2send[3] = level;
					this.CMD2send[4] = 0;
					this.SendCMD();
				}
			}
		}

		private void deselected_29color(int color, int level)
		{
			switch (color)
			{
			case 1:
				switch (level)
				{
				case 0:
					if (this.GlowFlagOf29ColorSwatch[1, 0] > 0)
					{
						this.red1_glow.BeginAnimation(UIElement.OpacityProperty, this.deselection);
						this.GlowFlagOf29ColorSwatch[1, 0] = 0;
						return;
					}
					break;
				case 1:
					if (this.GlowFlagOf29ColorSwatch[1, 1] > 0)
					{
						this.red2_glow.BeginAnimation(UIElement.OpacityProperty, this.deselection);
						this.GlowFlagOf29ColorSwatch[1, 1] = 0;
						return;
					}
					break;
				case 2:
					if (this.GlowFlagOf29ColorSwatch[1, 2] > 0)
					{
						this.red3_glow.BeginAnimation(UIElement.OpacityProperty, this.deselection);
						this.GlowFlagOf29ColorSwatch[1, 2] = 0;
						return;
					}
					break;
				case 3:
					if (this.GlowFlagOf29ColorSwatch[1, 3] > 0)
					{
						this.red4_glow.BeginAnimation(UIElement.OpacityProperty, this.deselection);
						this.GlowFlagOf29ColorSwatch[1, 3] = 0;
						return;
					}
					break;
				default:
					return;
				}
				break;
			case 2:
				switch (level)
				{
				case 0:
					if (this.GlowFlagOf29ColorSwatch[2, 0] > 0)
					{
						this.orange1_glow.BeginAnimation(UIElement.OpacityProperty, this.deselection);
						this.GlowFlagOf29ColorSwatch[2, 0] = 0;
						return;
					}
					break;
				case 1:
					if (this.GlowFlagOf29ColorSwatch[2, 1] > 0)
					{
						this.orange2_glow.BeginAnimation(UIElement.OpacityProperty, this.deselection);
						this.GlowFlagOf29ColorSwatch[2, 1] = 0;
						return;
					}
					break;
				case 2:
					if (this.GlowFlagOf29ColorSwatch[2, 2] > 0)
					{
						this.orange3_glow.BeginAnimation(UIElement.OpacityProperty, this.deselection);
						this.GlowFlagOf29ColorSwatch[2, 2] = 0;
						return;
					}
					break;
				case 3:
					if (this.GlowFlagOf29ColorSwatch[2, 3] > 0)
					{
						this.orange4_glow.BeginAnimation(UIElement.OpacityProperty, this.deselection);
						this.GlowFlagOf29ColorSwatch[2, 3] = 0;
						return;
					}
					break;
				default:
					return;
				}
				break;
			case 3:
				switch (level)
				{
				case 0:
					if (this.GlowFlagOf29ColorSwatch[3, 0] > 0)
					{
						this.yellow1_glow.BeginAnimation(UIElement.OpacityProperty, this.deselection);
						this.GlowFlagOf29ColorSwatch[3, 0] = 0;
						return;
					}
					break;
				case 1:
					if (this.GlowFlagOf29ColorSwatch[3, 1] > 0)
					{
						this.yellow2_glow.BeginAnimation(UIElement.OpacityProperty, this.deselection);
						this.GlowFlagOf29ColorSwatch[3, 1] = 0;
						return;
					}
					break;
				case 2:
					if (this.GlowFlagOf29ColorSwatch[3, 2] > 0)
					{
						this.yellow3_glow.BeginAnimation(UIElement.OpacityProperty, this.deselection);
						this.GlowFlagOf29ColorSwatch[3, 2] = 0;
						return;
					}
					break;
				case 3:
					if (this.GlowFlagOf29ColorSwatch[3, 3] > 0)
					{
						this.yellow4_glow.BeginAnimation(UIElement.OpacityProperty, this.deselection);
						this.GlowFlagOf29ColorSwatch[3, 3] = 0;
						return;
					}
					break;
				default:
					return;
				}
				break;
			case 4:
				switch (level)
				{
				case 0:
					if (this.GlowFlagOf29ColorSwatch[4, 0] > 0)
					{
						this.green1_glow.BeginAnimation(UIElement.OpacityProperty, this.deselection);
						this.GlowFlagOf29ColorSwatch[4, 0] = 0;
						return;
					}
					break;
				case 1:
					if (this.GlowFlagOf29ColorSwatch[4, 1] > 0)
					{
						this.green2_glow.BeginAnimation(UIElement.OpacityProperty, this.deselection);
						this.GlowFlagOf29ColorSwatch[4, 1] = 0;
						return;
					}
					break;
				case 2:
					if (this.GlowFlagOf29ColorSwatch[4, 2] > 0)
					{
						this.green3_glow.BeginAnimation(UIElement.OpacityProperty, this.deselection);
						this.GlowFlagOf29ColorSwatch[4, 2] = 0;
						return;
					}
					break;
				case 3:
					if (this.GlowFlagOf29ColorSwatch[4, 3] > 0)
					{
						this.green4_glow.BeginAnimation(UIElement.OpacityProperty, this.deselection);
						this.GlowFlagOf29ColorSwatch[4, 3] = 0;
						return;
					}
					break;
				default:
					return;
				}
				break;
			case 5:
				switch (level)
				{
				case 0:
					if (this.GlowFlagOf29ColorSwatch[5, 0] > 0)
					{
						this.sky1_glow.BeginAnimation(UIElement.OpacityProperty, this.deselection);
						this.GlowFlagOf29ColorSwatch[5, 0] = 0;
						return;
					}
					break;
				case 1:
					if (this.GlowFlagOf29ColorSwatch[5, 1] > 0)
					{
						this.sky2_glow.BeginAnimation(UIElement.OpacityProperty, this.deselection);
						this.GlowFlagOf29ColorSwatch[5, 1] = 0;
						return;
					}
					break;
				case 2:
					if (this.GlowFlagOf29ColorSwatch[5, 2] > 0)
					{
						this.sky3_glow.BeginAnimation(UIElement.OpacityProperty, this.deselection);
						this.GlowFlagOf29ColorSwatch[5, 2] = 0;
						return;
					}
					break;
				case 3:
					if (this.GlowFlagOf29ColorSwatch[5, 3] > 0)
					{
						this.sky4_glow.BeginAnimation(UIElement.OpacityProperty, this.deselection);
						this.GlowFlagOf29ColorSwatch[5, 3] = 0;
						return;
					}
					break;
				default:
					return;
				}
				break;
			case 6:
				switch (level)
				{
				case 0:
					if (this.GlowFlagOf29ColorSwatch[6, 0] > 0)
					{
						this.blue1_glow.BeginAnimation(UIElement.OpacityProperty, this.deselection);
						this.GlowFlagOf29ColorSwatch[6, 0] = 0;
						return;
					}
					break;
				case 1:
					if (this.GlowFlagOf29ColorSwatch[6, 1] > 0)
					{
						this.blue2_glow.BeginAnimation(UIElement.OpacityProperty, this.deselection);
						this.GlowFlagOf29ColorSwatch[6, 1] = 0;
						return;
					}
					break;
				case 2:
					if (this.GlowFlagOf29ColorSwatch[6, 2] > 0)
					{
						this.blue3_glow.BeginAnimation(UIElement.OpacityProperty, this.deselection);
						this.GlowFlagOf29ColorSwatch[6, 2] = 0;
						return;
					}
					break;
				case 3:
					if (this.GlowFlagOf29ColorSwatch[6, 3] > 0)
					{
						this.blue4_glow.BeginAnimation(UIElement.OpacityProperty, this.deselection);
						this.GlowFlagOf29ColorSwatch[6, 3] = 0;
						return;
					}
					break;
				default:
					return;
				}
				break;
			case 7:
				switch (level)
				{
				case 0:
					if (this.GlowFlagOf29ColorSwatch[7, 0] > 0)
					{
						this.purple1_glow.BeginAnimation(UIElement.OpacityProperty, this.deselection);
						this.GlowFlagOf29ColorSwatch[7, 0] = 0;
						return;
					}
					break;
				case 1:
					if (this.GlowFlagOf29ColorSwatch[7, 1] > 0)
					{
						this.purple2_glow.BeginAnimation(UIElement.OpacityProperty, this.deselection);
						this.GlowFlagOf29ColorSwatch[7, 1] = 0;
						return;
					}
					break;
				case 2:
					if (this.GlowFlagOf29ColorSwatch[7, 2] > 0)
					{
						this.purple3_glow.BeginAnimation(UIElement.OpacityProperty, this.deselection);
						this.GlowFlagOf29ColorSwatch[7, 2] = 0;
						return;
					}
					break;
				case 3:
					if (this.GlowFlagOf29ColorSwatch[7, 3] > 0)
					{
						this.purple4_glow.BeginAnimation(UIElement.OpacityProperty, this.deselection);
						this.GlowFlagOf29ColorSwatch[7, 3] = 0;
					}
					break;
				default:
					return;
				}
				break;
			default:
				return;
			}
		}

		private void selected_29color(int color, int level)
		{
			switch (color)
			{
			case 1:
				switch (level)
				{
				case 0:
					this.red1_glow.BeginAnimation(UIElement.OpacityProperty, this.selection);
					this.GlowFlagOf29ColorSwatch[1, 0] = 1;
					return;
				case 1:
					this.red2_glow.BeginAnimation(UIElement.OpacityProperty, this.selection);
					this.GlowFlagOf29ColorSwatch[1, 1] = 1;
					return;
				case 2:
					this.red3_glow.BeginAnimation(UIElement.OpacityProperty, this.selection);
					this.GlowFlagOf29ColorSwatch[1, 2] = 1;
					return;
				case 3:
					this.red4_glow.BeginAnimation(UIElement.OpacityProperty, this.selection);
					this.GlowFlagOf29ColorSwatch[1, 3] = 1;
					return;
				default:
					return;
				}
				break;
			case 2:
				switch (level)
				{
				case 0:
					this.orange1_glow.BeginAnimation(UIElement.OpacityProperty, this.selection);
					this.GlowFlagOf29ColorSwatch[2, 0] = 1;
					return;
				case 1:
					this.orange2_glow.BeginAnimation(UIElement.OpacityProperty, this.selection);
					this.GlowFlagOf29ColorSwatch[2, 1] = 1;
					return;
				case 2:
					this.orange3_glow.BeginAnimation(UIElement.OpacityProperty, this.selection);
					this.GlowFlagOf29ColorSwatch[2, 2] = 1;
					return;
				case 3:
					this.orange4_glow.BeginAnimation(UIElement.OpacityProperty, this.selection);
					this.GlowFlagOf29ColorSwatch[2, 3] = 1;
					return;
				default:
					return;
				}
				break;
			case 3:
				switch (level)
				{
				case 0:
					this.yellow1_glow.BeginAnimation(UIElement.OpacityProperty, this.selection);
					this.GlowFlagOf29ColorSwatch[3, 0] = 1;
					return;
				case 1:
					this.yellow2_glow.BeginAnimation(UIElement.OpacityProperty, this.selection);
					this.GlowFlagOf29ColorSwatch[3, 1] = 1;
					return;
				case 2:
					this.yellow3_glow.BeginAnimation(UIElement.OpacityProperty, this.selection);
					this.GlowFlagOf29ColorSwatch[3, 2] = 1;
					return;
				case 3:
					this.yellow4_glow.BeginAnimation(UIElement.OpacityProperty, this.selection);
					this.GlowFlagOf29ColorSwatch[3, 3] = 1;
					return;
				default:
					return;
				}
				break;
			case 4:
				switch (level)
				{
				case 0:
					this.green1_glow.BeginAnimation(UIElement.OpacityProperty, this.selection);
					this.GlowFlagOf29ColorSwatch[4, 0] = 1;
					return;
				case 1:
					this.green2_glow.BeginAnimation(UIElement.OpacityProperty, this.selection);
					this.GlowFlagOf29ColorSwatch[4, 1] = 1;
					return;
				case 2:
					this.green3_glow.BeginAnimation(UIElement.OpacityProperty, this.selection);
					this.GlowFlagOf29ColorSwatch[4, 2] = 1;
					return;
				case 3:
					this.green4_glow.BeginAnimation(UIElement.OpacityProperty, this.selection);
					this.GlowFlagOf29ColorSwatch[4, 3] = 1;
					return;
				default:
					return;
				}
				break;
			case 5:
				switch (level)
				{
				case 0:
					this.sky1_glow.BeginAnimation(UIElement.OpacityProperty, this.selection);
					this.GlowFlagOf29ColorSwatch[5, 0] = 1;
					return;
				case 1:
					this.sky2_glow.BeginAnimation(UIElement.OpacityProperty, this.selection);
					this.GlowFlagOf29ColorSwatch[5, 1] = 1;
					return;
				case 2:
					this.sky3_glow.BeginAnimation(UIElement.OpacityProperty, this.selection);
					this.GlowFlagOf29ColorSwatch[5, 2] = 1;
					return;
				case 3:
					this.sky4_glow.BeginAnimation(UIElement.OpacityProperty, this.selection);
					this.GlowFlagOf29ColorSwatch[5, 3] = 1;
					return;
				default:
					return;
				}
				break;
			case 6:
				switch (level)
				{
				case 0:
					this.blue1_glow.BeginAnimation(UIElement.OpacityProperty, this.selection);
					this.GlowFlagOf29ColorSwatch[6, 0] = 1;
					return;
				case 1:
					this.blue2_glow.BeginAnimation(UIElement.OpacityProperty, this.selection);
					this.GlowFlagOf29ColorSwatch[6, 1] = 1;
					return;
				case 2:
					this.blue3_glow.BeginAnimation(UIElement.OpacityProperty, this.selection);
					this.GlowFlagOf29ColorSwatch[6, 2] = 1;
					return;
				case 3:
					this.blue4_glow.BeginAnimation(UIElement.OpacityProperty, this.selection);
					this.GlowFlagOf29ColorSwatch[6, 3] = 1;
					return;
				default:
					return;
				}
				break;
			case 7:
				switch (level)
				{
				case 0:
					this.purple1_glow.BeginAnimation(UIElement.OpacityProperty, this.selection);
					this.GlowFlagOf29ColorSwatch[7, 0] = 1;
					return;
				case 1:
					this.purple2_glow.BeginAnimation(UIElement.OpacityProperty, this.selection);
					this.GlowFlagOf29ColorSwatch[7, 1] = 1;
					return;
				case 2:
					this.purple3_glow.BeginAnimation(UIElement.OpacityProperty, this.selection);
					this.GlowFlagOf29ColorSwatch[7, 2] = 1;
					return;
				case 3:
					this.purple4_glow.BeginAnimation(UIElement.OpacityProperty, this.selection);
					this.GlowFlagOf29ColorSwatch[7, 3] = 1;
					return;
				default:
					return;
				}
				break;
			default:
				return;
			}
		}

		private void deselected_8color(int color)
		{
			switch (color)
			{
			case 1:
				if (this.GlowFlagOf8ColorSwatch[1] > 0)
				{
					this.red_glow.BeginAnimation(UIElement.OpacityProperty, this.deselection);
					this.GlowFlagOf8ColorSwatch[1] = 0;
					return;
				}
				break;
			case 2:
				if (this.GlowFlagOf8ColorSwatch[2] > 0)
				{
					this.orange_glow.BeginAnimation(UIElement.OpacityProperty, this.deselection);
					this.GlowFlagOf8ColorSwatch[2] = 0;
					return;
				}
				break;
			case 3:
				if (this.GlowFlagOf8ColorSwatch[3] > 0)
				{
					this.yellow_glow.BeginAnimation(UIElement.OpacityProperty, this.deselection);
					this.GlowFlagOf8ColorSwatch[3] = 0;
					return;
				}
				break;
			case 4:
				if (this.GlowFlagOf8ColorSwatch[4] > 0)
				{
					this.green_glow.BeginAnimation(UIElement.OpacityProperty, this.deselection);
					this.GlowFlagOf8ColorSwatch[4] = 0;
					return;
				}
				break;
			case 5:
				if (this.GlowFlagOf8ColorSwatch[5] > 0)
				{
					this.sky_glow.BeginAnimation(UIElement.OpacityProperty, this.deselection);
					this.GlowFlagOf8ColorSwatch[5] = 0;
					return;
				}
				break;
			case 6:
				if (this.GlowFlagOf8ColorSwatch[6] > 0)
				{
					this.blue_glow.BeginAnimation(UIElement.OpacityProperty, this.deselection);
					this.GlowFlagOf8ColorSwatch[6] = 0;
					return;
				}
				break;
			case 7:
				if (this.GlowFlagOf8ColorSwatch[7] > 0)
				{
					this.purple_glow.BeginAnimation(UIElement.OpacityProperty, this.deselection);
					this.GlowFlagOf8ColorSwatch[7] = 0;
				}
				break;
			default:
				return;
			}
		}

		private void selected_8color(int color)
		{
			switch (color)
			{
			case 1:
				this.red_glow.BeginAnimation(UIElement.OpacityProperty, this.selection);
				this.GlowFlagOf8ColorSwatch[1] = 1;
				return;
			case 2:
				this.orange_glow.BeginAnimation(UIElement.OpacityProperty, this.selection);
				this.GlowFlagOf8ColorSwatch[2] = 1;
				return;
			case 3:
				this.yellow_glow.BeginAnimation(UIElement.OpacityProperty, this.selection);
				this.GlowFlagOf8ColorSwatch[3] = 1;
				return;
			case 4:
				this.green_glow.BeginAnimation(UIElement.OpacityProperty, this.selection);
				this.GlowFlagOf8ColorSwatch[4] = 1;
				return;
			case 5:
				this.sky_glow.BeginAnimation(UIElement.OpacityProperty, this.selection);
				this.GlowFlagOf8ColorSwatch[5] = 1;
				return;
			case 6:
				this.blue_glow.BeginAnimation(UIElement.OpacityProperty, this.selection);
				this.GlowFlagOf8ColorSwatch[6] = 1;
				return;
			case 7:
				this.purple_glow.BeginAnimation(UIElement.OpacityProperty, this.selection);
				this.GlowFlagOf8ColorSwatch[7] = 1;
				return;
			default:
				return;
			}
		}

		private void red_1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if ((this.area_switch[this.currentArea] > 0 && this.keyboardLockedFlag == -1) || (this.area_switch[6] > 0 && this.keyboardLockedFlag == 1))
			{
				this.choose_color_from_index_table(1, 0, this.currentArea);
				this.apply_color_change(this.currentArea);
				this.saveSetting(this.currentArea);
			}
		}

		private void orange_1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if ((this.area_switch[this.currentArea] > 0 && this.keyboardLockedFlag == -1) || (this.area_switch[6] > 0 && this.keyboardLockedFlag == 1))
			{
				this.choose_color_from_index_table(2, 0, this.currentArea);
				this.apply_color_change(this.currentArea);
				this.saveSetting(this.currentArea);
			}
		}

		private void yellow_1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if ((this.area_switch[this.currentArea] > 0 && this.keyboardLockedFlag == -1) || (this.area_switch[6] > 0 && this.keyboardLockedFlag == 1))
			{
				this.choose_color_from_index_table(3, 0, this.currentArea);
				this.apply_color_change(this.currentArea);
				this.saveSetting(this.currentArea);
			}
		}

		private void green_1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if ((this.area_switch[this.currentArea] > 0 && this.keyboardLockedFlag == -1) || (this.area_switch[6] > 0 && this.keyboardLockedFlag == 1))
			{
				this.choose_color_from_index_table(4, 0, this.currentArea);
				this.apply_color_change(this.currentArea);
				this.saveSetting(this.currentArea);
			}
		}

		private void sky_1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if ((this.area_switch[this.currentArea] > 0 && this.keyboardLockedFlag == -1) || (this.area_switch[6] > 0 && this.keyboardLockedFlag == 1))
			{
				this.choose_color_from_index_table(5, 0, this.currentArea);
				this.apply_color_change(this.currentArea);
				this.saveSetting(this.currentArea);
			}
		}

		private void blue_1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if ((this.area_switch[this.currentArea] > 0 && this.keyboardLockedFlag == -1) || (this.area_switch[6] > 0 && this.keyboardLockedFlag == 1))
			{
				this.choose_color_from_index_table(6, 0, this.currentArea);
				this.apply_color_change(this.currentArea);
				this.saveSetting(this.currentArea);
			}
		}

		private void purple_1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if ((this.area_switch[this.currentArea] > 0 && this.keyboardLockedFlag == -1) || (this.area_switch[6] > 0 && this.keyboardLockedFlag == 1))
			{
				this.choose_color_from_index_table(7, 0, this.currentArea);
				this.apply_color_change(this.currentArea);
				this.saveSetting(this.currentArea);
			}
		}

		private void red_2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if ((this.area_switch[this.currentArea] > 0 && this.keyboardLockedFlag == -1) || (this.area_switch[6] > 0 && this.keyboardLockedFlag == 1))
			{
				this.choose_color_from_index_table(1, 1, this.currentArea);
				this.apply_color_change(this.currentArea);
				this.saveSetting(this.currentArea);
			}
		}

		private void orange_2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if ((this.area_switch[this.currentArea] > 0 && this.keyboardLockedFlag == -1) || (this.area_switch[6] > 0 && this.keyboardLockedFlag == 1))
			{
				this.choose_color_from_index_table(2, 1, this.currentArea);
				this.apply_color_change(this.currentArea);
				this.saveSetting(this.currentArea);
			}
		}

		private void yellow_2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if ((this.area_switch[this.currentArea] > 0 && this.keyboardLockedFlag == -1) || (this.area_switch[6] > 0 && this.keyboardLockedFlag == 1))
			{
				this.choose_color_from_index_table(3, 1, this.currentArea);
				this.apply_color_change(this.currentArea);
				this.saveSetting(this.currentArea);
			}
		}

		private void green_2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if ((this.area_switch[this.currentArea] > 0 && this.keyboardLockedFlag == -1) || (this.area_switch[6] > 0 && this.keyboardLockedFlag == 1))
			{
				this.choose_color_from_index_table(4, 1, this.currentArea);
				this.apply_color_change(this.currentArea);
				this.saveSetting(this.currentArea);
			}
		}

		private void sky_2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if ((this.area_switch[this.currentArea] > 0 && this.keyboardLockedFlag == -1) || (this.area_switch[6] > 0 && this.keyboardLockedFlag == 1))
			{
				this.choose_color_from_index_table(5, 1, this.currentArea);
				this.apply_color_change(this.currentArea);
				this.saveSetting(this.currentArea);
			}
		}

		private void blue_2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if ((this.area_switch[this.currentArea] > 0 && this.keyboardLockedFlag == -1) || (this.area_switch[6] > 0 && this.keyboardLockedFlag == 1))
			{
				this.choose_color_from_index_table(6, 1, this.currentArea);
				this.apply_color_change(this.currentArea);
				this.saveSetting(this.currentArea);
			}
		}

		private void purple_2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if ((this.area_switch[this.currentArea] > 0 && this.keyboardLockedFlag == -1) || (this.area_switch[6] > 0 && this.keyboardLockedFlag == 1))
			{
				this.choose_color_from_index_table(7, 1, this.currentArea);
				this.apply_color_change(this.currentArea);
				this.saveSetting(this.currentArea);
			}
		}

		private void red_3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if ((this.area_switch[this.currentArea] > 0 && this.keyboardLockedFlag == -1) || (this.area_switch[6] > 0 && this.keyboardLockedFlag == 1))
			{
				this.choose_color_from_index_table(1, 2, this.currentArea);
				this.apply_color_change(this.currentArea);
				this.saveSetting(this.currentArea);
			}
		}

		private void orange_3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if ((this.area_switch[this.currentArea] > 0 && this.keyboardLockedFlag == -1) || (this.area_switch[6] > 0 && this.keyboardLockedFlag == 1))
			{
				this.choose_color_from_index_table(2, 2, this.currentArea);
				this.apply_color_change(this.currentArea);
				this.saveSetting(this.currentArea);
			}
		}

		private void yellow_3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if ((this.area_switch[this.currentArea] > 0 && this.keyboardLockedFlag == -1) || (this.area_switch[6] > 0 && this.keyboardLockedFlag == 1))
			{
				this.choose_color_from_index_table(3, 2, this.currentArea);
				this.apply_color_change(this.currentArea);
				this.saveSetting(this.currentArea);
			}
		}

		private void green_3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if ((this.area_switch[this.currentArea] > 0 && this.keyboardLockedFlag == -1) || (this.area_switch[6] > 0 && this.keyboardLockedFlag == 1))
			{
				this.choose_color_from_index_table(4, 2, this.currentArea);
				this.apply_color_change(this.currentArea);
				this.saveSetting(this.currentArea);
			}
		}

		private void sky_3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if ((this.area_switch[this.currentArea] > 0 && this.keyboardLockedFlag == -1) || (this.area_switch[6] > 0 && this.keyboardLockedFlag == 1))
			{
				this.choose_color_from_index_table(5, 2, this.currentArea);
				this.apply_color_change(this.currentArea);
				this.saveSetting(this.currentArea);
			}
		}

		private void blue_3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if ((this.area_switch[this.currentArea] > 0 && this.keyboardLockedFlag == -1) || (this.area_switch[6] > 0 && this.keyboardLockedFlag == 1))
			{
				this.choose_color_from_index_table(6, 2, this.currentArea);
				this.apply_color_change(this.currentArea);
				this.saveSetting(this.currentArea);
			}
		}

		private void purple_3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if ((this.area_switch[this.currentArea] > 0 && this.keyboardLockedFlag == -1) || (this.area_switch[6] > 0 && this.keyboardLockedFlag == 1))
			{
				this.choose_color_from_index_table(7, 2, this.currentArea);
				this.apply_color_change(this.currentArea);
				this.saveSetting(this.currentArea);
			}
		}

		private void red_4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if ((this.area_switch[this.currentArea] > 0 && this.keyboardLockedFlag == -1) || (this.area_switch[6] > 0 && this.keyboardLockedFlag == 1))
			{
				this.choose_color_from_index_table(1, 3, this.currentArea);
				this.apply_color_change(this.currentArea);
				this.saveSetting(this.currentArea);
			}
		}

		private void orange_4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if ((this.area_switch[this.currentArea] > 0 && this.keyboardLockedFlag == -1) || (this.area_switch[6] > 0 && this.keyboardLockedFlag == 1))
			{
				this.choose_color_from_index_table(2, 3, this.currentArea);
				this.apply_color_change(this.currentArea);
				this.saveSetting(this.currentArea);
			}
		}

		private void yellow_4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if ((this.area_switch[this.currentArea] > 0 && this.keyboardLockedFlag == -1) || (this.area_switch[6] > 0 && this.keyboardLockedFlag == 1))
			{
				this.choose_color_from_index_table(3, 3, this.currentArea);
				this.apply_color_change(this.currentArea);
				this.saveSetting(this.currentArea);
			}
		}

		private void green_4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if ((this.area_switch[this.currentArea] > 0 && this.keyboardLockedFlag == -1) || (this.area_switch[6] > 0 && this.keyboardLockedFlag == 1))
			{
				this.choose_color_from_index_table(4, 3, this.currentArea);
				this.apply_color_change(this.currentArea);
				this.saveSetting(this.currentArea);
			}
		}

		private void sky_4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if ((this.area_switch[this.currentArea] > 0 && this.keyboardLockedFlag == -1) || (this.area_switch[6] > 0 && this.keyboardLockedFlag == 1))
			{
				this.choose_color_from_index_table(5, 3, this.currentArea);
				this.apply_color_change(this.currentArea);
				this.saveSetting(this.currentArea);
			}
		}

		private void blue_4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if ((this.area_switch[this.currentArea] > 0 && this.keyboardLockedFlag == -1) || (this.area_switch[6] > 0 && this.keyboardLockedFlag == 1))
			{
				this.choose_color_from_index_table(6, 3, this.currentArea);
				this.apply_color_change(this.currentArea);
				this.saveSetting(this.currentArea);
			}
		}

		private void purple_4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if ((this.area_switch[this.currentArea] > 0 && this.keyboardLockedFlag == -1) || (this.area_switch[6] > 0 && this.keyboardLockedFlag == 1))
			{
				this.choose_color_from_index_table(7, 3, this.currentArea);
				this.apply_color_change(this.currentArea);
				this.saveSetting(this.currentArea);
			}
		}

		private void white_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if ((this.area_switch[this.currentArea] > 0 && this.keyboardLockedFlag == -1) || (this.area_switch[6] > 0 && this.keyboardLockedFlag == 1))
			{
				this.choose_color_from_index_table(8, 0, this.currentArea);
				this.apply_color_change(this.currentArea);
				this.saveSetting(this.currentArea);
			}
		}

		private void black_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if ((this.area_switch[this.currentArea] > 0 && this.keyboardLockedFlag == -1) || (this.area_switch[6] > 0 && this.keyboardLockedFlag == 1))
			{
				this.RED[this.currentArea] = 35;
				this.GREEN[this.currentArea] = 25;
				this.BLUE[this.currentArea] = 22;
				this.build_and_send_CMD(64, this.currentArea, 0, 0, 0);
				this.apply_color_change(this.currentArea);
				this.saveSetting(this.currentArea);
			}
		}

		private void red_shadow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.red_2_MouseLeftButtonDown(sender, e);
		}

		private void orange_shadow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.orange_2_MouseLeftButtonDown(sender, e);
		}

		private void yellow_shadow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.yellow_2_MouseLeftButtonDown(sender, e);
		}

		private void green_shadow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.green_2_MouseLeftButtonDown(sender, e);
		}

		private void sky_shadow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.sky_2_MouseLeftButtonDown(sender, e);
		}

		private void blue_shadow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.blue_2_MouseLeftButtonDown(sender, e);
		}

		private void purple_shadow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.purple_2_MouseLeftButtonDown(sender, e);
		}

		private void change_to_29_color_swatch()
		{
			this.color_board.Visibility = Visibility.Visible;
			this.color_swatch_single.Visibility = Visibility.Hidden;
		}

		private void change_to_8_color_swatch()
		{
			this.color_board.Visibility = Visibility.Hidden;
			this.color_swatch_single.Visibility = Visibility.Visible;
		}

		private void kb_left_frame_sensor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (this.currentMode != 6 && this.currentMode != 7 && this.keyboardLockedFlag == -1)
			{
				this.selectArea(1);
			}
		}

		private void kb_middle_frame_sensor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (this.currentMode != 2 && this.currentMode != 6 && this.currentMode != 7 && this.keyboardLockedFlag == -1)
			{
				this.selectArea(2);
			}
		}

		private void kb_right_frame_sensor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (this.currentMode != 2 && this.currentMode != 6 && this.currentMode != 7 && this.keyboardLockedFlag == -1)
			{
				this.selectArea(3);
			}
		}

		private void colorball_middle_left_sensor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.selectArea(4);
		}

		private void colorball_middle_right_sensor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.selectArea(5);
		}

		private void close_btn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.close_MainWindow();
		}

		private void close_MainWindow()
		{
			this.bLEDHotkeyMask = false;
			this.Minimized = true;
			this.build_and_send_CMD(49, 0, 0, 0, 0);
			base.Hide();
		}

		private void close_btn_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
		}

		private void close_btn_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
		{
		}

		private void deselected_mode(int mode)
		{
			switch (mode)
			{
			case 1:
				this.normal_glow.BeginAnimation(UIElement.OpacityProperty, this.deselection);
				return;
			case 2:
				this.gaming_glow.BeginAnimation(UIElement.OpacityProperty, this.deselection);
				return;
			case 3:
				this.breathing_glow.BeginAnimation(UIElement.OpacityProperty, this.deselection);
				return;
			case 4:
				break;
			case 5:
				this.wave_glow.BeginAnimation(UIElement.OpacityProperty, this.deselection);
				return;
			case 6:
				this.dual_color_glow.BeginAnimation(UIElement.OpacityProperty, this.deselection);
				return;
			case 7:
				this.idle_off_glow.BeginAnimation(UIElement.OpacityProperty, this.deselection);
				return;
			case 8:
				this.idle_breathing_glow.BeginAnimation(UIElement.OpacityProperty, this.deselection);
				return;
			case 9:
				this.idle_wave_glow.BeginAnimation(UIElement.OpacityProperty, this.deselection);
				break;
			default:
				return;
			}
		}

		private void selected_mode(int mode)
		{
			switch (mode)
			{
			case 1:
				this.normal_glow.BeginAnimation(UIElement.OpacityProperty, this.selection);
				return;
			case 2:
				this.gaming_glow.BeginAnimation(UIElement.OpacityProperty, this.selection);
				return;
			case 3:
				this.breathing_glow.BeginAnimation(UIElement.OpacityProperty, this.selection);
				return;
			case 4:
				break;
			case 5:
				this.wave_glow.BeginAnimation(UIElement.OpacityProperty, this.selection);
				return;
			case 6:
				this.dual_color_glow.BeginAnimation(UIElement.OpacityProperty, this.selection);
				return;
			case 7:
				this.idle_off_glow.BeginAnimation(UIElement.OpacityProperty, this.selection);
				return;
			case 8:
				this.idle_breathing_glow.BeginAnimation(UIElement.OpacityProperty, this.selection);
				return;
			case 9:
				this.idle_wave_glow.BeginAnimation(UIElement.OpacityProperty, this.selection);
				break;
			default:
				return;
			}
		}

		private void std_normal_mode()
		{
			this.change_current_mode(1);
			this.turn_on_all_area();
			this.change_to_29_color_swatch();
			this.firstTime2RunWaveMode = true;
			this.firstTime2RunBreathingMode = true;
			if (this.keyboardLockedFlag == -1)
			{
				for (int i = 1; i <= 3; i++)
				{
					this.RED[i] = this.rgb[i, 1];
					this.GREEN[i] = this.rgb[i, 2];
					this.BLUE[i] = this.rgb[i, 3];
				}
				this.change_left_area_color(this.rgb[1, 1], this.rgb[1, 2], this.rgb[1, 3]);
				this.change_middle_area_color(this.rgb[2, 1], this.rgb[2, 2], this.rgb[2, 3]);
				this.change_right_area_color(this.rgb[3, 1], this.rgb[3, 2], this.rgb[3, 3]);
				this.selectArea(this.previous_area);
				if (this.Switch2Mode == 1)
				{
					this.deselected_mode(this.previousMode);
					this.selected_mode(this.currentMode);
					this.sendCMD_to_restore_3area_color();
					this.apply_color_change(this.currentArea);
					this.turn_on_switch();
					return;
				}
			}
			else
			{
				if (this.Switch2Mode == 1)
				{
					this.deselected_mode(this.previousMode);
					this.selected_mode(this.currentMode);
					this.selectArea(6);
					this.apply_color_change(this.currentArea);
				}
				this.led_on_off_middle.Visibility = Visibility.Visible;
			}
		}

		private void normal_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.Switch2Mode = 1;
			this.std_normal_mode();
		}

		private void std_audio_mode()
		{
			int num = 0;
			if (num == 1)
			{
				if (this.currentMode == 4)
				{
					this.Switch2Mode = 1;
					if (!this.resumeFromS3)
					{
						this.std_normal_mode();
						return;
					}
					this.resumeFromS3 = false;
					return;
				}
				else
				{
					if (this.currentArea != 4 && this.currentArea != 5 && this.currentArea != 6 && this.currentArea != 0)
					{
						this.previous_area = this.currentArea;
					}
					this.Switch2Mode = 4;
					this.std_normal_mode();
					this.turn_off_switch();
					this.saveSetting(this.currentArea);
					this.change_current_mode(4);
				}
			}
		}

		private void audio_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.std_audio_mode();
		}

		private void std_gaming_mode()
		{
			if (this.currentMode != 2)
			{
				this.Switch2Mode = 2;
				this.deselected_mode(this.currentMode);
				this.selected_mode(this.Switch2Mode);
				if (this.keyboardLockedFlag == 1)
				{
					this.keyboard_switch_to_unlock();
				}
				if (this.currentArea != 4 && this.currentArea != 5 && this.currentArea != 6 && this.currentArea != 0)
				{
					this.previous_area = this.currentArea;
				}
				this.std_normal_mode();
				this.build_and_send_CMD(66, 1, this.ColorConfig[1, 0], this.ColorConfig[1, 1], 0);
				this.turn_off_switch();
				this.saveSetting(this.currentArea);
				this.selectArea(1);
				this.change_current_mode(2);
				this.turnOffColor(2);
				this.turnOffColor(3);
				this.build_and_send_CMD(65, 2, 0, 0, 0);
				return;
			}
			this.Switch2Mode = 1;
			if (!this.resumeFromS3)
			{
				this.std_normal_mode();
				return;
			}
			this.resumeFromS3 = false;
		}

		private void gaming_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.std_gaming_mode();
		}

		private void std_dual_color_mode()
		{
			if (this.currentMode != 6)
			{
				if (this.currentArea != 4 && this.currentArea != 5 && this.currentArea != 6 && this.currentArea != 0)
				{
					this.previous_area = this.currentArea;
				}
				this.Switch2Mode = 6;
				this.deselected_mode(this.currentMode);
				this.selected_mode(this.Switch2Mode);
				this.std_normal_mode();
				this.turn_off_switch();
				this.change_to_8_color_swatch();
				this.change_current_color_to_level2();
				this.ColorChangePeroid = 2.0;
				this.change_current_mode(6);
				this.dual_colorball_gradient_left_left.Color = System.Windows.Media.Color.FromArgb(255, (byte)this.RED[4], (byte)this.GREEN[4], (byte)this.BLUE[4]);
				this.dual_colorball_gradient_left_middle_left.Color = System.Windows.Media.Color.FromArgb(255, (byte)this.RED[4], (byte)this.GREEN[4], (byte)this.BLUE[4]);
				this.dual_colorball_gradient_left_middle_right.Color = System.Windows.Media.Color.FromArgb(255, (byte)this.RED[5], (byte)this.GREEN[5], (byte)this.BLUE[5]);
				this.dual_colorball_gradient_left_right.Color = System.Windows.Media.Color.FromArgb(255, (byte)this.RED[5], (byte)this.GREEN[5], (byte)this.BLUE[5]);
				this.selectArea(4);
				this.apply_color_change(this.currentArea);
				return;
			}
			this.Switch2Mode = 1;
			if (!this.resumeFromS3)
			{
				this.std_normal_mode();
				return;
			}
			this.resumeFromS3 = false;
		}

		private void dual_color_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.std_dual_color_mode();
		}

		private void std_wave_mode()
		{
			if (this.currentMode != 5)
			{
				if (this.currentArea != 4 && this.currentArea != 5 && this.currentArea != 6 && this.currentArea != 0)
				{
					this.previous_area = this.currentArea;
				}
				this.Switch2Mode = 5;
				this.deselected_mode(this.currentMode);
				this.selected_mode(this.Switch2Mode);
				this.std_normal_mode();
				this.turn_off_switch();
				this.change_to_8_color_swatch();
				this.change_current_color_to_level2();
				this.ColorChangePeroid = 1.5;
				this.change_current_mode(5);
				if (this.keyboardLockedFlag == -1)
				{
					this.selectArea(this.previous_area);
				}
				else
				{
					this.selectArea(6);
				}
				this.apply_color_change(this.currentArea);
				return;
			}
			this.Switch2Mode = 1;
			if (!this.resumeFromS3)
			{
				this.std_normal_mode();
				return;
			}
			this.resumeFromS3 = false;
		}

		private void wave_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.std_wave_mode();
		}

		private void std_breathing_mode()
		{
			if (this.currentMode != 3)
			{
				if (this.currentArea != 4 && this.currentArea != 5 && this.currentArea != 6 && this.currentArea != 0)
				{
					this.previous_area = this.currentArea;
				}
				this.Switch2Mode = 3;
				this.deselected_mode(this.currentMode);
				this.selected_mode(this.Switch2Mode);
				this.std_normal_mode();
				this.turn_off_switch();
				this.change_to_8_color_swatch();
				this.change_current_color_to_level2();
				this.ColorChangePeroid = 1.0;
				this.change_current_mode(3);
				if (this.keyboardLockedFlag == -1)
				{
					this.selectArea(this.previous_area);
				}
				else
				{
					this.selectArea(6);
				}
				this.apply_color_change(this.currentArea);
				return;
			}
			this.Switch2Mode = 1;
			if (!this.resumeFromS3)
			{
				this.std_normal_mode();
				return;
			}
			this.resumeFromS3 = false;
		}

		private void breathing_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.std_breathing_mode();
		}

		private void std_mode_sensor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.std_mode();
		}

		private void std_mode()
		{
			if (this.currentMode >= 7)
			{
				this.option_buttons_std.Visibility = Visibility.Visible;
				this.option_buttons_idle.Visibility = Visibility.Hidden;
				this.std_mode_bg.Visibility = Visibility.Visible;
				this.idle_mode_bg.Visibility = Visibility.Hidden;
				this.lastIDLEmode = this.currentMode;
				this.Switch2Mode = this.lastSTDmode;
				switch (this.lastSTDmode)
				{
				case 1:
					this.Switch2Mode = 1;
					this.std_normal_mode();
					return;
				case 2:
					this.std_gaming_mode();
					return;
				case 3:
					this.std_breathing_mode();
					return;
				case 4:
					this.std_audio_mode();
					break;
				case 5:
					this.std_wave_mode();
					return;
				case 6:
					this.std_dual_color_mode();
					return;
				default:
					return;
				}
			}
		}

		private void idle_mode_sensor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.idle_mode();
		}

		private void idle_mode()
		{
			this.enteredIDLEmode = true;
			if (this.currentMode < 7)
			{
				this.option_buttons_std.Visibility = Visibility.Hidden;
				this.option_buttons_idle.Visibility = Visibility.Visible;
				this.std_mode_bg.Visibility = Visibility.Hidden;
				this.idle_mode_bg.Visibility = Visibility.Visible;
				this.lastSTDmode = this.currentMode;
				this.Switch2Mode = this.lastIDLEmode;
				switch (this.lastIDLEmode)
				{
				case 7:
					this.idle_off_mode();
					return;
				case 8:
					this.idle_breathing_mode();
					this.deselected_mode(this.previousMode);
					this.selected_mode(this.currentMode);
					return;
				case 9:
					this.idle_wave_mode();
					this.deselected_mode(this.previousMode);
					this.selected_mode(this.currentMode);
					break;
				default:
					return;
				}
			}
		}

		private void idle_off_mode()
		{
			if (this.currentMode != 7 || this.resumeFromS3)
			{
				this.deselected_mode(this.currentMode);
				this.selected_mode(7);
				this.change_current_mode(7);
				if (this.currentArea > 3)
				{
					this.previous_area = 1;
				}
				else
				{
					this.previous_area = this.currentArea;
				}
				this.selectArea(0);
				if (this.area_switch[1] > 0)
				{
					this.LED_switch_left();
				}
				if ((this.area_switch[2] > 0 && this.keyboardLockedFlag == -1) || (this.area_switch[6] > 0 && this.keyboardLockedFlag == 1))
				{
					this.LED_switch_middle();
				}
				if (this.area_switch[3] > 0)
				{
					this.LED_switch_right();
				}
				this.turn_off_switch();
				this.change_to_8_color_swatch();
				this.build_and_send_CMD(65, 7, 0, 0, 0);
			}
			if (this.resumeFromS3)
			{
				this.resumeFromS3 = false;
			}
		}

		private void idle_off_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.idle_off_mode();
		}

		private void idle_wave_mode()
		{
			if (this.currentMode == 9 && !this.resumeFromS3)
			{
				this.Switch2Mode = 7;
				if (!this.resumeFromS3)
				{
					this.idle_off_mode();
				}
				else
				{
					this.resumeFromS3 = false;
				}
			}
			else
			{
				this.Switch2Mode = 9;
				this.firstTime2RunWaveMode = true;
				if (this.currentArea != 4 && this.currentArea != 5 && this.currentArea != 6 && this.currentArea != 0)
				{
					this.previous_area = this.currentArea;
				}
				if (this.currentMode == 1 || this.currentMode == 2 || this.currentMode == 7)
				{
					this.turn_on_all_area();
					this.turn_off_switch();
					this.change_to_8_color_swatch();
				}
				this.change_current_color_to_level2();
				this.ColorChangePeroid = 6.0;
				this.change_current_mode(9);
				if (this.keyboardLockedFlag == -1)
				{
					this.selectArea(this.previous_area);
				}
				else
				{
					this.selectArea(6);
				}
				this.apply_color_change(this.currentArea);
			}
			this.deselected_mode(this.previousMode);
			this.selected_mode(this.currentMode);
			this.resumeFromS3 = false;
		}

		private void idle_wave_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.idle_wave_mode();
		}

		private void idle_breathing_mode()
		{
			if (this.currentMode == 8 && !this.resumeFromS3)
			{
				this.Switch2Mode = 7;
				if (!this.resumeFromS3)
				{
					this.idle_off_mode();
				}
				else
				{
					this.resumeFromS3 = false;
				}
			}
			else
			{
				this.Switch2Mode = 8;
				this.firstTime2RunBreathingMode = true;
				if (this.currentArea != 4 && this.currentArea != 5 && this.currentArea != 6 && this.currentArea != 0)
				{
					this.previous_area = this.currentArea;
				}
				if (this.currentMode == 1 || this.currentMode == 2 || this.currentMode == 7)
				{
					this.turn_on_all_area();
					this.turn_off_switch();
					this.change_to_8_color_swatch();
				}
				this.change_current_color_to_level2();
				this.ColorChangePeroid = 5.5;
				this.change_current_mode(8);
				if (this.keyboardLockedFlag == -1)
				{
					this.selectArea(this.previous_area);
				}
				else
				{
					this.selectArea(6);
				}
				this.apply_color_change(this.currentArea);
			}
			this.deselected_mode(this.previousMode);
			this.selected_mode(this.currentMode);
			this.resumeFromS3 = false;
		}

		private void idle_breathing_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.idle_breathing_mode();
		}

		private void bg_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
			{
				base.DragMove();
			}
		}

		private void LED_switch_left()
		{
			if (this.currentMode == 1 || this.currentMode == 7)
			{
				this.area_switch[1] *= -1;
				if (this.area_switch[1] > 0)
				{
					this.kb_left_frame_sensor.Visibility = Visibility.Visible;
					this.readSetting(1);
					this.selectArea(1);
					return;
				}
				this.kb_left_frame_sensor.Visibility = Visibility.Hidden;
				this.kb_left_frame_blur.Visibility = Visibility.Hidden;
				this.kb_left_frame_white.Visibility = Visibility.Hidden;
				this.switch2area(1);
				this.turnOffColor(1);
			}
		}

		private void led_on_off_left_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.LED_switch_left();
		}

		private void LED_switch_middle()
		{
			if (this.currentMode == 1 || this.currentMode == 7)
			{
				if (this.keyboardLockedFlag == -1)
				{
					this.area_switch[2] *= -1;
					if (this.area_switch[2] > 0)
					{
						this.kb_middle_frame_sensor.Visibility = Visibility.Visible;
						this.readSetting(2);
						this.selectArea(2);
						return;
					}
					this.kb_middle_frame_sensor.Visibility = Visibility.Hidden;
					this.kb_middle_frame_blur.Visibility = Visibility.Hidden;
					this.kb_middle_frame_white.Visibility = Visibility.Hidden;
					this.switch2area(2);
					this.turnOffColor(2);
					return;
				}
				else
				{
					this.area_switch[6] *= -1;
					if (this.area_switch[6] > 0)
					{
						this.selectArea(6);
						this.readSetting(6);
						return;
					}
					this.turnOffColor(1);
					this.turnOffColor(2);
					this.turnOffColor(3);
					if (this.currentMode != 7)
					{
						this.build_and_send_CMD(65, this.currentMode, 0, 0, 0);
					}
					this.selectArea(0);
				}
			}
		}

		private void led_on_off_middle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.LED_switch_middle();
		}

		private void LED_switch_right()
		{
			if (this.currentMode == 1 || this.currentMode == 7)
			{
				this.area_switch[3] *= -1;
				if (this.area_switch[3] > 0)
				{
					this.kb_right_frame_sensor.Visibility = Visibility.Visible;
					this.readSetting(3);
					this.selectArea(3);
					return;
				}
				this.kb_right_frame_sensor.Visibility = Visibility.Hidden;
				this.kb_right_frame_blur.Visibility = Visibility.Hidden;
				this.kb_right_frame_white.Visibility = Visibility.Hidden;
				this.switch2area(3);
				this.turnOffColor(3);
			}
		}

		private void led_on_off_right_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.LED_switch_right();
		}

		private void switch2area(int area_off)
		{
			if (this.currentArea == area_off)
			{
				if (this.area_switch[1] > 0)
				{
					this.selectArea(1);
					return;
				}
				if (this.area_switch[2] > 0)
				{
					this.selectArea(2);
					return;
				}
				if (this.area_switch[3] > 0)
				{
					this.selectArea(3);
					return;
				}
				this.selectArea(0);
			}
		}

		private void turn_on_all_area()
		{
			if (this.area_switch[1] < 0)
			{
				this.LED_switch_left();
			}
			if ((this.area_switch[2] < 0 && this.keyboardLockedFlag == -1) || (this.area_switch[6] < 0 && this.keyboardLockedFlag == 1))
			{
				this.LED_switch_middle();
			}
			if (this.area_switch[3] < 0)
			{
				this.LED_switch_right();
			}
		}

		private void turn_on_switch()
		{
			this.led_on_off_left.Visibility = Visibility.Visible;
			this.led_on_off_middle.Visibility = Visibility.Visible;
			this.led_on_off_right.Visibility = Visibility.Visible;
		}

		private void turn_off_switch()
		{
			this.led_on_off_left.Visibility = Visibility.Hidden;
			this.led_on_off_middle.Visibility = Visibility.Hidden;
			this.led_on_off_right.Visibility = Visibility.Hidden;
		}

		private void keyboard_switch_to_lock()
		{
			if (this.currentMode != 6 && this.currentMode != 2 && this.currentMode != 7)
			{
				if (this.currentMode == 1)
				{
					this.turn_off_switch();
					this.led_on_off_middle.Visibility = Visibility.Visible;
					this.led_on_off_middle.ToolTip = "Turn off whole keyboard";
				}
				this.turn_on_all_area();
				this.keyboardLockedFlag *= -1;
				Settings.Default.KBLockState = this.keyboardLockedFlag;
				Settings.Default.Save();
				this.beforeLocked_area = this.currentArea;
				this.selectArea(6);
				this.apply_color_change(this.currentArea);
				this.keyboard_unlock.Visibility = Visibility.Hidden;
			}
		}

		private void keyboard_unlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.keyboard_switch_to_lock();
		}

		private void keyboard_switch_to_unlock()
		{
			if (this.currentMode != 6 && this.currentMode != 2 && this.currentMode != 7)
			{
				if (this.currentMode == 1)
				{
					this.turn_on_switch();
					this.led_on_off_middle.ToolTip = "Turn off middle area";
				}
				this.keyboardLockedFlag *= -1;
				this.switchFromKBLocked = true;
				Settings.Default.KBLockState = this.keyboardLockedFlag;
				Settings.Default.Save();
				for (int i = 1; i <= 3; i++)
				{
					this.RED[i] = this.rgb[i, 1];
					this.GREEN[i] = this.rgb[i, 2];
					this.BLUE[i] = this.rgb[i, 3];
				}
				this.change_left_area_color(this.rgb[1, 1], this.rgb[1, 2], this.rgb[1, 3]);
				this.change_middle_area_color(this.rgb[2, 1], this.rgb[2, 2], this.rgb[2, 3]);
				this.change_right_area_color(this.rgb[3, 1], this.rgb[3, 2], this.rgb[3, 3]);
				this.sendCMD_to_restore_3area_color();
				this.selectArea(this.beforeLocked_area);
				this.apply_color_change(this.currentArea);
				this.keyboard_unlock.Visibility = Visibility.Visible;
			}
		}

		private void keyboard_lock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.keyboard_switch_to_unlock();
		}

		private void Window_Activated(object sender, EventArgs e)
		{
		}

		private void Window_Deactivated(object sender, EventArgs e)
		{
		}

		private void Window_StateChanged(object sender, EventArgs e)
		{
		}

		private void SyncWithLED()
		{
			this.ReadDataBufFromHID(65, 0);
			int num = this.readDataBuf[1];
			if (num == 1)
			{
				if (this.keyboardLockedFlag == -1)
				{
					for (int i = 1; i <= 3; i++)
					{
						if (this.area_switch[i] == 1)
						{
							this.ReadDataBufFromHID(66, i);
							if (this.readDataBuf[2] >= 0 && this.readDataBuf[2] <= 8 && this.readDataBuf[3] >= 0 && this.readDataBuf[3] <= 3)
							{
								this.choose_color_from_index_table(this.readDataBuf[2], this.readDataBuf[3], i);
								this.saveSetting(i);
							}
						}
					}
					this.change_left_area_color(this.rgb[1, 1], this.rgb[1, 2], this.rgb[1, 3]);
					this.change_middle_area_color(this.rgb[2, 1], this.rgb[2, 2], this.rgb[2, 3]);
					this.change_right_area_color(this.rgb[3, 1], this.rgb[3, 2], this.rgb[3, 3]);
					this.selectArea(1);
				}
				else if (this.keyboardLockedFlag == 1 && this.area_switch[6] == 1)
				{
					this.ReadDataBufFromHID(66, 1);
					if (this.readDataBuf[1] >= 0 && this.readDataBuf[1] <= 8 && this.readDataBuf[2] >= 0 && this.readDataBuf[2] <= 3)
					{
						this.choose_color_from_index_table(this.readDataBuf[1], this.readDataBuf[2], 6);
						this.saveSetting(6);
					}
					this.change_left_area_color(this.rgb[6, 1], this.rgb[6, 2], this.rgb[6, 3]);
					this.change_middle_area_color(this.rgb[6, 1], this.rgb[6, 2], this.rgb[6, 3]);
					this.change_right_area_color(this.rgb[6, 1], this.rgb[6, 2], this.rgb[6, 3]);
					this.selectArea(6);
				}
			}
			else if (num == 2)
			{
				this.ReadDataBufFromHID(66, 1);
				if (this.readDataBuf[1] >= 0 && this.readDataBuf[1] <= 8 && this.readDataBuf[2] >= 0 && this.readDataBuf[2] <= 3)
				{
					this.choose_color_from_index_table(this.readDataBuf[1], this.readDataBuf[2], 1);
					this.saveSetting(1);
				}
				this.change_left_area_color(this.rgb[1, 1], this.rgb[1, 2], this.rgb[1, 3]);
				this.selectArea(1);
			}
			if (this.currentMode != num && this.existBacklightKeyboardDevice)
			{
				if (this.currentMode >= 7 && num < 7 && num != 0)
				{
					this.std_mode();
				}
				else if (this.currentMode < 7 && (num >= 7 || num == 0))
				{
					this.idle_mode();
				}
				if (num == 2)
				{
					this.std_gaming_mode();
					return;
				}
				if (num == 0)
				{
					this.idle_off_mode();
					return;
				}
				this.Switch2Mode = 1;
				this.std_normal_mode();
			}
		}

		private static bool IsAlreadyRunning()
		{
			string location = Assembly.GetExecutingAssembly().Location;
			FileSystemInfo fileSystemInfo = new FileInfo(location);
			string name = fileSystemInfo.Name;
			bool flag;
			MainWindow.mutex = new Mutex(true, "Global\\" + name, out flag);
			if (flag)
			{
				MainWindow.mutex.ReleaseMutex();
			}
			return !flag;
		}

		private void miTrayIconItem_Exit_Click(object sender, EventArgs e)
		{
			this.notifyIcon.Dispose();
			base.Close();
		}

		private void miTrayIconItem_Setting_Click(object sender, EventArgs e)
		{
			this.show_MainWindow();
		}

		private void notifyIcon_DoubleClick(object sender, EventArgs e)
		{
			this.show_MainWindow();
		}

		private void show_MainWindow()
		{
			this.bLEDHotkeyMask = true;
			this.Minimized = false;
			this.build_and_send_CMD(49, 1, 0, 0, 0);
			this.check_if_Settings_exists();
			this.SyncWithLED();
			base.Show();
			base.ShowInTaskbar = true;
		}

		private void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
		{
			Screen primaryScreen = Screen.PrimaryScreen;
			int height = primaryScreen.Bounds.Height;
			int arg_25_0 = primaryScreen.Bounds.Width;
			Window mainWindow = System.Windows.Application.Current.MainWindow;
			if (height == 600)
			{
				mainWindow.Height = 600.0;
				mainWindow.Width = 800.0;
			}
			Rect workArea = SystemParameters.WorkArea;
			base.Left = (workArea.Width - base.ActualWidth) / 2.0;
			base.Top = (workArea.Height - base.ActualHeight) / 2.0;
		}

		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/KLM;component/mainwindow.xaml", UriKind.Relative);
			System.Windows.Application.LoadComponent(this, resourceLocator);
		}

		[DebuggerNonUserCode]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		[EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.Window = (MainWindow)target;
				this.Window.StateChanged += new EventHandler(this.Window_StateChanged);
				this.Window.Activated += new EventHandler(this.Window_Activated);
				this.Window.Deactivated += new EventHandler(this.Window_Deactivated);
				return;
			case 2:
				this.CloseStatusGroup = (VisualStateGroup)target;
				return;
			case 3:
				this.Enter = (VisualState)target;
				return;
			case 4:
				this.Leave = (VisualState)target;
				return;
			case 5:
				this.KeyboardLockStateGroup = (VisualStateGroup)target;
				return;
			case 6:
				this.EnterState = (VisualState)target;
				return;
			case 7:
				this.LeaveState = (VisualState)target;
				return;
			case 8:
				this.LeftballStateGroup = (VisualStateGroup)target;
				return;
			case 9:
				this.EnterState1 = (VisualState)target;
				return;
			case 10:
				this.LeaveState1 = (VisualState)target;
				return;
			case 11:
				this.RightballStateGroup = (VisualStateGroup)target;
				return;
			case 12:
				this.EnterState2 = (VisualState)target;
				return;
			case 13:
				this.LeaveState2 = (VisualState)target;
				return;
			case 14:
				this.MiddleballStateGroup = (VisualStateGroup)target;
				return;
			case 15:
				this.EnterState3 = (VisualState)target;
				return;
			case 16:
				this.LeaveState3 = (VisualState)target;
				return;
			case 17:
				this.StdStateGroup = (VisualStateGroup)target;
				return;
			case 18:
				this.EnterState4 = (VisualState)target;
				return;
			case 19:
				this.LeaveState4 = (VisualState)target;
				return;
			case 20:
				this.IdleStateGroup = (VisualStateGroup)target;
				return;
			case 21:
				this.EnterState5 = (VisualState)target;
				return;
			case 22:
				this.LeaveState5 = (VisualState)target;
				return;
			case 23:
				this.StdOption_Normal = (VisualStateGroup)target;
				return;
			case 24:
				this.EnterState6 = (VisualState)target;
				return;
			case 25:
				this.LeaveState6 = (VisualState)target;
				return;
			case 26:
				this.StdOptionGaming = (VisualStateGroup)target;
				return;
			case 27:
				this.EnterState8 = (VisualState)target;
				return;
			case 28:
				this.LeaveState8 = (VisualState)target;
				return;
			case 29:
				this.StdOption_Breathing = (VisualStateGroup)target;
				return;
			case 30:
				this.EnterState9 = (VisualState)target;
				return;
			case 31:
				this.LeaveState9 = (VisualState)target;
				return;
			case 32:
				this.StdOption_Wave = (VisualStateGroup)target;
				return;
			case 33:
				this.EnterState10 = (VisualState)target;
				return;
			case 34:
				this.LeaveState10 = (VisualState)target;
				return;
			case 35:
				this.StdOption_Dual = (VisualStateGroup)target;
				return;
			case 36:
				this.EnterState11 = (VisualState)target;
				return;
			case 37:
				this.LeaveState11 = (VisualState)target;
				return;
			case 38:
				this.IdleOption_Off = (VisualStateGroup)target;
				return;
			case 39:
				this.EnterState12 = (VisualState)target;
				return;
			case 40:
				this.LeaveState12 = (VisualState)target;
				return;
			case 41:
				this.IdleOption_Breathing = (VisualStateGroup)target;
				return;
			case 42:
				this.EnterState13 = (VisualState)target;
				return;
			case 43:
				this.LeaveState13 = (VisualState)target;
				return;
			case 44:
				this.IdleOption_Wave = (VisualStateGroup)target;
				return;
			case 45:
				this.EnterState14 = (VisualState)target;
				return;
			case 46:
				this.LeaveState14 = (VisualState)target;
				return;
			case 47:
				this.Led_left = (VisualStateGroup)target;
				return;
			case 48:
				this.led_enter = (VisualState)target;
				return;
			case 49:
				this.led_leave = (VisualState)target;
				return;
			case 50:
				this.Led_middle = (VisualStateGroup)target;
				return;
			case 51:
				this.led_enter1 = (VisualState)target;
				return;
			case 52:
				this.led_leave1 = (VisualState)target;
				return;
			case 53:
				this.Led_right = (VisualStateGroup)target;
				return;
			case 54:
				this.led_enter2 = (VisualState)target;
				return;
			case 55:
				this.led_leave2 = (VisualState)target;
				return;
			case 56:
				this.KeyboardUnockStateGroup = (VisualStateGroup)target;
				return;
			case 57:
				this.EnterState7 = (VisualState)target;
				return;
			case 58:
				this.LeaveState7 = (VisualState)target;
				return;
			case 59:
				this.Std_normal_selected_glow = (VisualStateGroup)target;
				return;
			case 60:
				this.selected = (VisualState)target;
				return;
			case 61:
				this.deselected = (VisualState)target;
				return;
			case 62:
				this.Std_dual_color_selected_glow = (VisualStateGroup)target;
				return;
			case 63:
				this.selected1 = (VisualState)target;
				return;
			case 64:
				this.deselected1 = (VisualState)target;
				return;
			case 65:
				this.Std_wave_selected_glow = (VisualStateGroup)target;
				return;
			case 66:
				this.selected2 = (VisualState)target;
				return;
			case 67:
				this.deselected2 = (VisualState)target;
				return;
			case 68:
				this.Std_breathing_selected_glow = (VisualStateGroup)target;
				return;
			case 69:
				this.selected3 = (VisualState)target;
				return;
			case 70:
				this.deselected3 = (VisualState)target;
				return;
			case 71:
				this.Std_gaming_selected_glow = (VisualStateGroup)target;
				return;
			case 72:
				this.selected4 = (VisualState)target;
				return;
			case 73:
				this.deselected4 = (VisualState)target;
				return;
			case 74:
				this.Idle_off = (VisualStateGroup)target;
				return;
			case 75:
				this.selected5 = (VisualState)target;
				return;
			case 76:
				this.deselected5 = (VisualState)target;
				return;
			case 77:
				this.Idel_wave = (VisualStateGroup)target;
				return;
			case 78:
				this.selected6 = (VisualState)target;
				return;
			case 79:
				this.deselected6 = (VisualState)target;
				return;
			case 80:
				this.Idle_breathing = (VisualStateGroup)target;
				return;
			case 81:
				this.selected7 = (VisualState)target;
				return;
			case 82:
				this.deselected7 = (VisualState)target;
				return;
			case 83:
				this.Single_color_glow = (VisualStateGroup)target;
				return;
			case 84:
				this.Enter1 = (VisualState)target;
				return;
			case 85:
				this.Leave1 = (VisualState)target;
				return;
			case 86:
				this.Color_outter_cycle_purple = (VisualStateGroup)target;
				return;
			case 87:
				this.enter3 = (VisualState)target;
				return;
			case 88:
				this.leave = (VisualState)target;
				return;
			case 89:
				this.select = (VisualState)target;
				return;
			case 90:
				this.deselect = (VisualState)target;
				return;
			case 91:
				this.Color_outter_middle_purple = (VisualStateGroup)target;
				return;
			case 92:
				this.enter4 = (VisualState)target;
				return;
			case 93:
				this.leave1 = (VisualState)target;
				return;
			case 94:
				this.select1 = (VisualState)target;
				return;
			case 95:
				this.deselect1 = (VisualState)target;
				return;
			case 96:
				this.Color_inner_middle_purple = (VisualStateGroup)target;
				return;
			case 97:
				this.enter5 = (VisualState)target;
				return;
			case 98:
				this.leave2 = (VisualState)target;
				return;
			case 99:
				this.select2 = (VisualState)target;
				return;
			case 100:
				this.deselect2 = (VisualState)target;
				return;
			case 101:
				this.Color_inner_cycle_purple = (VisualStateGroup)target;
				return;
			case 102:
				this.enter6 = (VisualState)target;
				return;
			case 103:
				this.leave3 = (VisualState)target;
				return;
			case 104:
				this.select3 = (VisualState)target;
				return;
			case 105:
				this.deselect3 = (VisualState)target;
				return;
			case 106:
				this.Color_inner_middle_red = (VisualStateGroup)target;
				return;
			case 107:
				this.enter7 = (VisualState)target;
				return;
			case 108:
				this.leave9 = (VisualState)target;
				return;
			case 109:
				this.Color_inner_middle_orange = (VisualStateGroup)target;
				return;
			case 110:
				this.enter8 = (VisualState)target;
				return;
			case 111:
				this.leave4 = (VisualState)target;
				return;
			case 112:
				this.Color_inner_middle_yellow = (VisualStateGroup)target;
				return;
			case 113:
				this.enter9 = (VisualState)target;
				return;
			case 114:
				this.leave5 = (VisualState)target;
				return;
			case 115:
				this.Color_inner_middle_green = (VisualStateGroup)target;
				return;
			case 116:
				this.enter10 = (VisualState)target;
				return;
			case 117:
				this.leave6 = (VisualState)target;
				return;
			case 118:
				this.Color_inner_middle_sky = (VisualStateGroup)target;
				return;
			case 119:
				this.enter11 = (VisualState)target;
				return;
			case 120:
				this.leave7 = (VisualState)target;
				return;
			case 121:
				this.Color_inner_middle_blue = (VisualStateGroup)target;
				return;
			case 122:
				this.enter12 = (VisualState)target;
				return;
			case 123:
				this.leave8 = (VisualState)target;
				return;
			case 124:
				this.Color_inner_cycle_red = (VisualStateGroup)target;
				return;
			case 125:
				this.enter13 = (VisualState)target;
				return;
			case 126:
				this.leave10 = (VisualState)target;
				return;
			case 127:
				this.Color_inner_cycle_orange = (VisualStateGroup)target;
				return;
			case 128:
				this.enter14 = (VisualState)target;
				return;
			case 129:
				this.leave11 = (VisualState)target;
				return;
			case 130:
				this.Color_inner_cycle_yellow = (VisualStateGroup)target;
				return;
			case 131:
				this.enter15 = (VisualState)target;
				return;
			case 132:
				this.leave12 = (VisualState)target;
				return;
			case 133:
				this.Color_inner_cycle_green = (VisualStateGroup)target;
				return;
			case 134:
				this.enter16 = (VisualState)target;
				return;
			case 135:
				this.leave13 = (VisualState)target;
				return;
			case 136:
				this.Color_inner_cycle_sky = (VisualStateGroup)target;
				return;
			case 137:
				this.enter17 = (VisualState)target;
				return;
			case 138:
				this.leave14 = (VisualState)target;
				return;
			case 139:
				this.Color_inner_cycle_blue = (VisualStateGroup)target;
				return;
			case 140:
				this.enter18 = (VisualState)target;
				return;
			case 141:
				this.leave15 = (VisualState)target;
				return;
			case 142:
				this.Color_outter_middle_red = (VisualStateGroup)target;
				return;
			case 143:
				this.enter19 = (VisualState)target;
				return;
			case 144:
				this.leave16 = (VisualState)target;
				return;
			case 145:
				this.Color_outter_middle_orange = (VisualStateGroup)target;
				return;
			case 146:
				this.enter20 = (VisualState)target;
				return;
			case 147:
				this.leave17 = (VisualState)target;
				return;
			case 148:
				this.Color_outter_middle_yellow = (VisualStateGroup)target;
				return;
			case 149:
				this.enter21 = (VisualState)target;
				return;
			case 150:
				this.leave18 = (VisualState)target;
				return;
			case 151:
				this.Color_outter_middle_green = (VisualStateGroup)target;
				return;
			case 152:
				this.enter22 = (VisualState)target;
				return;
			case 153:
				this.leave19 = (VisualState)target;
				return;
			case 154:
				this.Color_outter_middle_sky = (VisualStateGroup)target;
				return;
			case 155:
				this.enter23 = (VisualState)target;
				return;
			case 156:
				this.leave20 = (VisualState)target;
				return;
			case 157:
				this.Color_outter_middle_blue = (VisualStateGroup)target;
				return;
			case 158:
				this.enter24 = (VisualState)target;
				return;
			case 159:
				this.leave21 = (VisualState)target;
				return;
			case 160:
				this.Color_outter_cycle_sky = (VisualStateGroup)target;
				return;
			case 161:
				this.enter25 = (VisualState)target;
				return;
			case 162:
				this.leave22 = (VisualState)target;
				return;
			case 163:
				this.Color_outter_cycle_green = (VisualStateGroup)target;
				return;
			case 164:
				this.enter26 = (VisualState)target;
				return;
			case 165:
				this.leave23 = (VisualState)target;
				return;
			case 166:
				this.Color_outter_cycle_yellow = (VisualStateGroup)target;
				return;
			case 167:
				this.enter27 = (VisualState)target;
				return;
			case 168:
				this.leave24 = (VisualState)target;
				return;
			case 169:
				this.Color_outter_cycle_orange = (VisualStateGroup)target;
				return;
			case 170:
				this.enter28 = (VisualState)target;
				return;
			case 171:
				this.leave25 = (VisualState)target;
				return;
			case 172:
				this.Color_outter_cycle_red = (VisualStateGroup)target;
				return;
			case 173:
				this.enter29 = (VisualState)target;
				return;
			case 174:
				this.leave26 = (VisualState)target;
				return;
			case 175:
				this.Color_outter_cycle_blue = (VisualStateGroup)target;
				return;
			case 176:
				this.enter30 = (VisualState)target;
				return;
			case 177:
				this.leave27 = (VisualState)target;
				return;
			case 178:
				this.led_manager_all_ui = (Canvas)target;
				return;
			case 179:
				this.option_buttons_idle = (Canvas)target;
				return;
			case 180:
				this.圖層_11 = (Canvas)target;
				return;
			case 181:
				this.idle_off = (System.Windows.Shapes.Path)target;
				this.idle_off.MouseLeftButtonDown += new MouseButtonEventHandler(this.idle_off_MouseLeftButtonDown);
				return;
			case 182:
				this.idle_breathing = (System.Windows.Shapes.Path)target;
				this.idle_breathing.MouseLeftButtonDown += new MouseButtonEventHandler(this.idle_breathing_MouseLeftButtonDown);
				return;
			case 183:
				this.idle_wave = (System.Windows.Shapes.Path)target;
				this.idle_wave.MouseLeftButtonDown += new MouseButtonEventHandler(this.idle_wave_MouseLeftButtonDown);
				return;
			case 184:
				this.TextBlock5 = (Canvas)target;
				return;
			case 185:
				this.idle_off_sensor = (System.Windows.Shapes.Path)target;
				this.idle_off_sensor.MouseLeftButtonDown += new MouseButtonEventHandler(this.idle_off_MouseLeftButtonDown);
				return;
			case 186:
				this.idle_breathing_sensor = (System.Windows.Shapes.Path)target;
				this.idle_breathing_sensor.MouseLeftButtonDown += new MouseButtonEventHandler(this.idle_breathing_MouseLeftButtonDown);
				return;
			case 187:
				this.idle_wave_sensor = (System.Windows.Shapes.Path)target;
				this.idle_wave_sensor.MouseLeftButtonDown += new MouseButtonEventHandler(this.idle_wave_MouseLeftButtonDown);
				return;
			case 188:
				this.idle_off_glow = (System.Windows.Shapes.Path)target;
				this.idle_off_glow.MouseLeftButtonDown += new MouseButtonEventHandler(this.idle_off_MouseLeftButtonDown);
				return;
			case 189:
				this.idle_breathing_glow = (System.Windows.Shapes.Path)target;
				this.idle_breathing_glow.MouseLeftButtonDown += new MouseButtonEventHandler(this.idle_breathing_MouseLeftButtonDown);
				return;
			case 190:
				this.idle_wave_glow = (System.Windows.Shapes.Path)target;
				this.idle_wave_glow.MouseLeftButtonDown += new MouseButtonEventHandler(this.idle_wave_MouseLeftButtonDown);
				return;
			case 191:
				this.option_buttons_std = (Canvas)target;
				return;
			case 192:
				this.圖層_12 = (Canvas)target;
				return;
			case 193:
				this.gaming = (System.Windows.Shapes.Path)target;
				this.gaming.MouseLeftButtonDown += new MouseButtonEventHandler(this.gaming_MouseLeftButtonDown);
				return;
			case 194:
				this.audio = (System.Windows.Shapes.Path)target;
				this.audio.MouseLeftButtonDown += new MouseButtonEventHandler(this.audio_MouseLeftButtonDown);
				return;
			case 195:
				this.breathing = (System.Windows.Shapes.Path)target;
				this.breathing.MouseLeftButtonDown += new MouseButtonEventHandler(this.breathing_MouseLeftButtonDown);
				return;
			case 196:
				this.normal = (System.Windows.Shapes.Path)target;
				this.normal.MouseLeftButtonDown += new MouseButtonEventHandler(this.normal_MouseLeftButtonDown);
				return;
			case 197:
				this.wave = (System.Windows.Shapes.Path)target;
				this.wave.MouseLeftButtonDown += new MouseButtonEventHandler(this.wave_MouseLeftButtonDown);
				return;
			case 198:
				this.dual_color = (System.Windows.Shapes.Path)target;
				this.dual_color.MouseLeftButtonDown += new MouseButtonEventHandler(this.dual_color_MouseLeftButtonDown);
				return;
			case 199:
				this.normal_glow = (System.Windows.Shapes.Path)target;
				this.normal_glow.MouseLeftButtonDown += new MouseButtonEventHandler(this.normal_MouseLeftButtonDown);
				return;
			case 200:
				this.gaming_glow = (System.Windows.Shapes.Path)target;
				this.gaming_glow.MouseLeftButtonDown += new MouseButtonEventHandler(this.gaming_MouseLeftButtonDown);
				return;
			case 201:
				this.breathing_glow = (System.Windows.Shapes.Path)target;
				this.breathing_glow.MouseLeftButtonDown += new MouseButtonEventHandler(this.breathing_MouseLeftButtonDown);
				return;
			case 202:
				this.wave_glow = (System.Windows.Shapes.Path)target;
				this.wave_glow.MouseLeftButtonDown += new MouseButtonEventHandler(this.wave_MouseLeftButtonDown);
				return;
			case 203:
				this.dual_color_glow = (System.Windows.Shapes.Path)target;
				this.dual_color_glow.MouseLeftButtonDown += new MouseButtonEventHandler(this.dual_color_MouseLeftButtonDown);
				return;
			case 204:
				this.TextBlock6 = (Canvas)target;
				return;
			case 205:
				this.dual_color1 = (System.Windows.Shapes.Path)target;
				return;
			case 206:
				this.normal1 = (System.Windows.Shapes.Path)target;
				return;
			case 207:
				this.wave1 = (System.Windows.Shapes.Path)target;
				return;
			case 208:
				this.breathing1 = (System.Windows.Shapes.Path)target;
				return;
			case 209:
				this.audio1 = (System.Windows.Shapes.Path)target;
				return;
			case 210:
				this.gaming1 = (System.Windows.Shapes.Path)target;
				return;
			case 211:
				this.audio_sensor = (System.Windows.Shapes.Path)target;
				this.audio_sensor.MouseLeftButtonDown += new MouseButtonEventHandler(this.audio_MouseLeftButtonDown);
				return;
			case 212:
				this.gaming_sensor = (System.Windows.Shapes.Path)target;
				this.gaming_sensor.MouseLeftButtonDown += new MouseButtonEventHandler(this.gaming_MouseLeftButtonDown);
				return;
			case 213:
				this.breathing_sensor = (System.Windows.Shapes.Path)target;
				this.breathing_sensor.MouseLeftButtonDown += new MouseButtonEventHandler(this.breathing_MouseLeftButtonDown);
				return;
			case 214:
				this.normal_sensor = (System.Windows.Shapes.Path)target;
				this.normal_sensor.MouseLeftButtonDown += new MouseButtonEventHandler(this.normal_MouseLeftButtonDown);
				return;
			case 215:
				this.wave_sensor = (System.Windows.Shapes.Path)target;
				this.wave_sensor.MouseLeftButtonDown += new MouseButtonEventHandler(this.wave_MouseLeftButtonDown);
				return;
			case 216:
				this.dual_color_sensor = (System.Windows.Shapes.Path)target;
				this.dual_color_sensor.MouseLeftButtonDown += new MouseButtonEventHandler(this.dual_color_MouseLeftButtonDown);
				return;
			case 217:
				this.bg = (Canvas)target;
				this.bg.MouseDown += new MouseButtonEventHandler(this.bg_MouseDown);
				return;
			case 218:
				this.圖層_1 = (Canvas)target;
				return;
			case 219:
				this.Main_Body = (Canvas)target;
				return;
			case 220:
				this.mode = (Canvas)target;
				return;
			case 221:
				this.std_mode_bg = (System.Windows.Shapes.Path)target;
				return;
			case 222:
				this.idle_mode_bg = (System.Windows.Shapes.Path)target;
				return;
			case 223:
				this.std_mode_bg_Copy = (System.Windows.Shapes.Path)target;
				return;
			case 224:
				this.idle_mode_bg_Copy = (System.Windows.Shapes.Path)target;
				return;
			case 225:
				this.std_mode_bg_glow = (System.Windows.Shapes.Path)target;
				return;
			case 226:
				this.idle_mode_bg_glow = (System.Windows.Shapes.Path)target;
				return;
			case 227:
				this.std_mode_frame = (System.Windows.Shapes.Path)target;
				return;
			case 228:
				this.idle_mode_frame = (System.Windows.Shapes.Path)target;
				return;
			case 229:
				this.TextBlock_Standard = (Canvas)target;
				return;
			case 230:
				this.TextBlock2 = (Canvas)target;
				return;
			case 231:
				this.TextBlock3 = (Canvas)target;
				return;
			case 232:
				this.TextBlock4 = (Canvas)target;
				return;
			case 233:
				this.TextBlock_Idle = (Canvas)target;
				return;
			case 234:
				this.std_mode_sensor = (System.Windows.Shapes.Path)target;
				this.std_mode_sensor.MouseLeftButtonDown += new MouseButtonEventHandler(this.std_mode_sensor_MouseLeftButtonDown);
				return;
			case 235:
				this.idle_mode_sensor = (System.Windows.Shapes.Path)target;
				this.idle_mode_sensor.MouseLeftButtonDown += new MouseButtonEventHandler(this.idle_mode_sensor_MouseLeftButtonDown);
				return;
			case 236:
				this.led_on_off_left = (Canvas)target;
				this.led_on_off_left.MouseLeftButtonDown += new MouseButtonEventHandler(this.led_on_off_left_MouseLeftButtonDown);
				return;
			case 237:
				this.led_on_off_bg_left = (Canvas)target;
				return;
			case 238:
				this.圖層_5 = (Canvas)target;
				return;
			case 239:
				this.enter = (System.Windows.Shapes.Path)target;
				return;
			case 240:
				this.led_on_off_switch_left = (Canvas)target;
				return;
			case 241:
				this.圖層_8 = (Canvas)target;
				return;
			case 242:
				this.left_switch = (System.Windows.Shapes.Path)target;
				return;
			case 243:
				this.left_switch_on = (System.Windows.Shapes.Path)target;
				return;
			case 244:
				this.led_on_off_middle = (Canvas)target;
				this.led_on_off_middle.MouseLeftButtonDown += new MouseButtonEventHandler(this.led_on_off_middle_MouseLeftButtonDown);
				return;
			case 245:
				this.led_on_off_bg_middle = (Canvas)target;
				return;
			case 246:
				this.圖層_6 = (Canvas)target;
				return;
			case 247:
				this.enter1 = (System.Windows.Shapes.Path)target;
				return;
			case 248:
				this.led_on_off_switch_middle = (Canvas)target;
				return;
			case 249:
				this.圖層_7 = (Canvas)target;
				return;
			case 250:
				this.middle_switch = (System.Windows.Shapes.Path)target;
				return;
			case 251:
				this.middle_switch_on = (System.Windows.Shapes.Path)target;
				return;
			case 252:
				this.led_on_off_right = (Canvas)target;
				this.led_on_off_right.MouseLeftButtonDown += new MouseButtonEventHandler(this.led_on_off_right_MouseLeftButtonDown);
				return;
			case 253:
				this.led_on_off_bg_right = (Canvas)target;
				return;
			case 254:
				this.圖層_9 = (Canvas)target;
				return;
			case 255:
				this.enter2 = (System.Windows.Shapes.Path)target;
				return;
			case 256:
				this.led_on_off_switch_right = (Canvas)target;
				return;
			case 257:
				this.圖層_10 = (Canvas)target;
				return;
			case 258:
				this.right_switch = (System.Windows.Shapes.Path)target;
				return;
			case 259:
				this.right_switch_on = (System.Windows.Shapes.Path)target;
				return;
			case 260:
				this.keyboard = (Viewbox)target;
				return;
			case 261:
				this.color_area = (System.Windows.Shapes.Rectangle)target;
				return;
			case 262:
				this.left_color = (GradientStop)target;
				return;
			case 263:
				this.left_middle_color = (GradientStop)target;
				return;
			case 264:
				this.middle_left_color = (GradientStop)target;
				return;
			case 265:
				this.middle_right_color = (GradientStop)target;
				return;
			case 266:
				this.right_middle_color = (GradientStop)target;
				return;
			case 267:
				this.right_color = (GradientStop)target;
				return;
			case 268:
				this.kb_right_color = (System.Windows.Shapes.Path)target;
				return;
			case 269:
				this.kb_middle_color = (System.Windows.Shapes.Path)target;
				return;
			case 270:
				this.kb_left_color = (System.Windows.Shapes.Path)target;
				return;
			case 271:
				this.kb_left_outer_frame_blur = (Canvas)target;
				return;
			case 272:
				this.kb_left_outer_frame_solid = (System.Windows.Shapes.Path)target;
				return;
			case 273:
				this.kb_middle_btm_outer_frame_blur = (Canvas)target;
				return;
			case 274:
				this.kb_middle_btm_outer_frame_solid = (System.Windows.Shapes.Path)target;
				return;
			case 275:
				this.kb_middle_top_outer_frame_blur = (Canvas)target;
				return;
			case 276:
				this.kb_middle_top_outer_frame_solid = (System.Windows.Shapes.Path)target;
				return;
			case 277:
				this.kb_right_outer_frame_blur = (Canvas)target;
				return;
			case 278:
				this.kb_right_outer_frame_solid = (System.Windows.Shapes.Path)target;
				return;
			case 279:
				this.kb_black_bg = (System.Windows.Shapes.Path)target;
				return;
			case 280:
				this.kb_buttons = (Canvas)target;
				return;
			case 281:
				this.kb_right_frame_blur = (Canvas)target;
				return;
			case 282:
				this.kb_right_frame_white = (System.Windows.Shapes.Path)target;
				return;
			case 283:
				this.kb_middle_frame_blur = (Canvas)target;
				return;
			case 284:
				this.kb_middle_frame_white = (System.Windows.Shapes.Path)target;
				return;
			case 285:
				this.kb_left_frame_blur = (Canvas)target;
				return;
			case 286:
				this.kb_left_frame_white = (System.Windows.Shapes.Path)target;
				return;
			case 287:
				this.kb_all_blur = (System.Windows.Shapes.Path)target;
				return;
			case 288:
				this.kb_all_frame = (System.Windows.Shapes.Path)target;
				return;
			case 289:
				this.kb_left_frame_sensor = (System.Windows.Shapes.Path)target;
				this.kb_left_frame_sensor.MouseLeftButtonDown += new MouseButtonEventHandler(this.kb_left_frame_sensor_MouseLeftButtonDown);
				return;
			case 290:
				this.kb_middle_frame_sensor = (System.Windows.Shapes.Path)target;
				this.kb_middle_frame_sensor.MouseLeftButtonDown += new MouseButtonEventHandler(this.kb_middle_frame_sensor_MouseLeftButtonDown);
				return;
			case 291:
				this.kb_right_frame_sensor = (System.Windows.Shapes.Path)target;
				this.kb_right_frame_sensor.MouseLeftButtonDown += new MouseButtonEventHandler(this.kb_right_frame_sensor_MouseLeftButtonDown);
				return;
			case 292:
				this.color_ball_right = (Canvas)target;
				return;
			case 293:
				this.ellipse1_Copy = (Ellipse)target;
				return;
			case 294:
				this.colorball_right_1 = (System.Windows.Shapes.Path)target;
				return;
			case 295:
				this.colorball_right_sensor = (Ellipse)target;
				this.colorball_right_sensor.MouseLeftButtonDown += new MouseButtonEventHandler(this.kb_right_frame_sensor_MouseLeftButtonDown);
				return;
			case 296:
				this.color_ball_middle_dual = (Canvas)target;
				return;
			case 297:
				this.colorball_middle_1 = (System.Windows.Shapes.Path)target;
				return;
			case 298:
				this.dual_colorball_gradient_left_left = (GradientStop)target;
				return;
			case 299:
				this.dual_colorball_gradient_left_right = (GradientStop)target;
				return;
			case 300:
				this.dual_colorball_gradient_left_middle_left = (GradientStop)target;
				return;
			case 301:
				this.dual_colorball_gradient_left_middle_right = (GradientStop)target;
				return;
			case 302:
				this.colorball_middle_2 = (System.Windows.Shapes.Path)target;
				return;
			case 303:
				this.dual_colorball_right_right = (GradientStop)target;
				return;
			case 304:
				this.colorball_middle_left_sensor = (Ellipse)target;
				this.colorball_middle_left_sensor.MouseLeftButtonDown += new MouseButtonEventHandler(this.colorball_middle_left_sensor_MouseLeftButtonDown);
				return;
			case 305:
				this.colorball_middle_right_sensor = (Ellipse)target;
				this.colorball_middle_right_sensor.MouseLeftButtonDown += new MouseButtonEventHandler(this.colorball_middle_right_sensor_MouseLeftButtonDown);
				return;
			case 306:
				this.color_ball_middle = (Canvas)target;
				return;
			case 307:
				this.ellipse1_Copy1 = (Ellipse)target;
				return;
			case 308:
				this.colorball_middle = (System.Windows.Shapes.Path)target;
				return;
			case 309:
				this.colorball_middle_sensor = (Ellipse)target;
				this.colorball_middle_sensor.MouseLeftButtonDown += new MouseButtonEventHandler(this.kb_middle_frame_sensor_MouseLeftButtonDown);
				return;
			case 310:
				this.color_ball_left = (Canvas)target;
				return;
			case 311:
				this.ellipse1 = (Ellipse)target;
				return;
			case 312:
				this.colorball_left_1 = (System.Windows.Shapes.Path)target;
				return;
			case 313:
				this.colorball_left_sensor = (Ellipse)target;
				this.colorball_left_sensor.MouseLeftButtonDown += new MouseButtonEventHandler(this.kb_left_frame_sensor_MouseLeftButtonDown);
				return;
			case 314:
				this.bk_white = (Canvas)target;
				return;
			case 315:
				this.圖層_13 = (Canvas)target;
				return;
			case 316:
				this.color_board = (Viewbox)target;
				return;
			case 317:
				this.outter_cycle_frame1 = (Canvas)target;
				return;
			case 318:
				this.outter_middle_cycle_frame1 = (Canvas)target;
				return;
			case 319:
				this.inner_middle_cycle_frame1 = (Canvas)target;
				return;
			case 320:
				this.inner_cycle_frame1 = (Canvas)target;
				return;
			case 321:
				this.outter_cycle_color1 = (Canvas)target;
				return;
			case 322:
				this.red4 = (System.Windows.Shapes.Path)target;
				return;
			case 323:
				this.orange4 = (System.Windows.Shapes.Path)target;
				return;
			case 324:
				this.yellow4 = (System.Windows.Shapes.Path)target;
				return;
			case 325:
				this.green4 = (System.Windows.Shapes.Path)target;
				return;
			case 326:
				this.sky4 = (System.Windows.Shapes.Path)target;
				return;
			case 327:
				this.blue4 = (System.Windows.Shapes.Path)target;
				return;
			case 328:
				this.purple4 = (System.Windows.Shapes.Path)target;
				return;
			case 329:
				((System.Windows.Shapes.Path)target).MouseLeftButtonDown += new MouseButtonEventHandler(this.red_4_MouseLeftButtonDown);
				return;
			case 330:
				((System.Windows.Shapes.Path)target).MouseLeftButtonDown += new MouseButtonEventHandler(this.orange_4_MouseLeftButtonDown);
				return;
			case 331:
				((System.Windows.Shapes.Path)target).MouseLeftButtonDown += new MouseButtonEventHandler(this.yellow_4_MouseLeftButtonDown);
				return;
			case 332:
				((System.Windows.Shapes.Path)target).MouseLeftButtonDown += new MouseButtonEventHandler(this.green_4_MouseLeftButtonDown);
				return;
			case 333:
				((System.Windows.Shapes.Path)target).MouseLeftButtonDown += new MouseButtonEventHandler(this.sky_4_MouseLeftButtonDown);
				return;
			case 334:
				((System.Windows.Shapes.Path)target).MouseLeftButtonDown += new MouseButtonEventHandler(this.blue_4_MouseLeftButtonDown);
				return;
			case 335:
				((System.Windows.Shapes.Path)target).MouseLeftButtonDown += new MouseButtonEventHandler(this.purple_4_MouseLeftButtonDown);
				return;
			case 336:
				this.outter_cycle_color1_glow = (Canvas)target;
				return;
			case 337:
				this.red4_glow = (System.Windows.Shapes.Path)target;
				return;
			case 338:
				this.orange4_glow = (System.Windows.Shapes.Path)target;
				return;
			case 339:
				this.yellow4_glow = (System.Windows.Shapes.Path)target;
				return;
			case 340:
				this.green4_glow = (System.Windows.Shapes.Path)target;
				return;
			case 341:
				this.sky4_glow = (System.Windows.Shapes.Path)target;
				return;
			case 342:
				this.blue4_glow = (System.Windows.Shapes.Path)target;
				return;
			case 343:
				this.purple4_glow = (System.Windows.Shapes.Path)target;
				return;
			case 344:
				this.outter_middle_cycle_color1 = (Canvas)target;
				return;
			case 345:
				this.red3 = (System.Windows.Shapes.Path)target;
				return;
			case 346:
				this.orange3 = (System.Windows.Shapes.Path)target;
				return;
			case 347:
				this.yellow3 = (System.Windows.Shapes.Path)target;
				return;
			case 348:
				this.green3 = (System.Windows.Shapes.Path)target;
				return;
			case 349:
				this.sky3 = (System.Windows.Shapes.Path)target;
				return;
			case 350:
				this.blue3 = (System.Windows.Shapes.Path)target;
				return;
			case 351:
				this.purple3 = (System.Windows.Shapes.Path)target;
				return;
			case 352:
				((System.Windows.Shapes.Path)target).MouseLeftButtonDown += new MouseButtonEventHandler(this.red_3_MouseLeftButtonDown);
				return;
			case 353:
				((System.Windows.Shapes.Path)target).MouseLeftButtonDown += new MouseButtonEventHandler(this.orange_3_MouseLeftButtonDown);
				return;
			case 354:
				((System.Windows.Shapes.Path)target).MouseLeftButtonDown += new MouseButtonEventHandler(this.yellow_3_MouseLeftButtonDown);
				return;
			case 355:
				((System.Windows.Shapes.Path)target).MouseLeftButtonDown += new MouseButtonEventHandler(this.green_3_MouseLeftButtonDown);
				return;
			case 356:
				((System.Windows.Shapes.Path)target).MouseLeftButtonDown += new MouseButtonEventHandler(this.sky_3_MouseLeftButtonDown);
				return;
			case 357:
				((System.Windows.Shapes.Path)target).MouseLeftButtonDown += new MouseButtonEventHandler(this.blue_3_MouseLeftButtonDown);
				return;
			case 358:
				((System.Windows.Shapes.Path)target).MouseLeftButtonDown += new MouseButtonEventHandler(this.purple_3_MouseLeftButtonDown);
				return;
			case 359:
				this.outter_middle_cycle_color1_glow = (Canvas)target;
				return;
			case 360:
				this.red3_glow = (System.Windows.Shapes.Path)target;
				return;
			case 361:
				this.orange3_glow = (System.Windows.Shapes.Path)target;
				return;
			case 362:
				this.yellow3_glow = (System.Windows.Shapes.Path)target;
				return;
			case 363:
				this.green3_glow = (System.Windows.Shapes.Path)target;
				return;
			case 364:
				this.sky3_glow = (System.Windows.Shapes.Path)target;
				return;
			case 365:
				this.blue3_glow = (System.Windows.Shapes.Path)target;
				return;
			case 366:
				this.purple3_glow = (System.Windows.Shapes.Path)target;
				return;
			case 367:
				this.inner_middle_cycle_color1 = (Canvas)target;
				return;
			case 368:
				this.red2 = (System.Windows.Shapes.Path)target;
				return;
			case 369:
				this.orange2 = (System.Windows.Shapes.Path)target;
				return;
			case 370:
				this.yellow2 = (System.Windows.Shapes.Path)target;
				return;
			case 371:
				this.green2 = (System.Windows.Shapes.Path)target;
				return;
			case 372:
				this.sky2 = (System.Windows.Shapes.Path)target;
				return;
			case 373:
				this.blue2 = (System.Windows.Shapes.Path)target;
				return;
			case 374:
				this.purple2 = (System.Windows.Shapes.Path)target;
				return;
			case 375:
				((System.Windows.Shapes.Path)target).MouseLeftButtonDown += new MouseButtonEventHandler(this.red_2_MouseLeftButtonDown);
				return;
			case 376:
				((System.Windows.Shapes.Path)target).MouseLeftButtonDown += new MouseButtonEventHandler(this.orange_2_MouseLeftButtonDown);
				return;
			case 377:
				((System.Windows.Shapes.Path)target).MouseLeftButtonDown += new MouseButtonEventHandler(this.yellow_2_MouseLeftButtonDown);
				return;
			case 378:
				((System.Windows.Shapes.Path)target).MouseLeftButtonDown += new MouseButtonEventHandler(this.green_2_MouseLeftButtonDown);
				return;
			case 379:
				((System.Windows.Shapes.Path)target).MouseLeftButtonDown += new MouseButtonEventHandler(this.sky_2_MouseLeftButtonDown);
				return;
			case 380:
				((System.Windows.Shapes.Path)target).MouseLeftButtonDown += new MouseButtonEventHandler(this.blue_2_MouseLeftButtonDown);
				return;
			case 381:
				((System.Windows.Shapes.Path)target).MouseLeftButtonDown += new MouseButtonEventHandler(this.purple_2_MouseLeftButtonDown);
				return;
			case 382:
				this.inner_middle_cycle_color1_glow = (Canvas)target;
				return;
			case 383:
				this.red2_glow = (System.Windows.Shapes.Path)target;
				return;
			case 384:
				this.orange2_glow = (System.Windows.Shapes.Path)target;
				return;
			case 385:
				this.yellow2_glow = (System.Windows.Shapes.Path)target;
				return;
			case 386:
				this.green2_glow = (System.Windows.Shapes.Path)target;
				return;
			case 387:
				this.sky2_glow = (System.Windows.Shapes.Path)target;
				return;
			case 388:
				this.blue2_glow = (System.Windows.Shapes.Path)target;
				return;
			case 389:
				this.purple2_glow = (System.Windows.Shapes.Path)target;
				return;
			case 390:
				this.inner_cycle_color1 = (Canvas)target;
				return;
			case 391:
				this.red1 = (System.Windows.Shapes.Path)target;
				return;
			case 392:
				this.orange1 = (System.Windows.Shapes.Path)target;
				return;
			case 393:
				this.yellow1 = (System.Windows.Shapes.Path)target;
				return;
			case 394:
				this.green1 = (System.Windows.Shapes.Path)target;
				return;
			case 395:
				this.sky1 = (System.Windows.Shapes.Path)target;
				return;
			case 396:
				this.blue1 = (System.Windows.Shapes.Path)target;
				return;
			case 397:
				this.purple1 = (System.Windows.Shapes.Path)target;
				return;
			case 398:
				((System.Windows.Shapes.Path)target).MouseLeftButtonDown += new MouseButtonEventHandler(this.red_1_MouseLeftButtonDown);
				return;
			case 399:
				((System.Windows.Shapes.Path)target).MouseLeftButtonDown += new MouseButtonEventHandler(this.orange_1_MouseLeftButtonDown);
				return;
			case 400:
				((System.Windows.Shapes.Path)target).MouseLeftButtonDown += new MouseButtonEventHandler(this.yellow_1_MouseLeftButtonDown);
				return;
			case 401:
				((System.Windows.Shapes.Path)target).MouseLeftButtonDown += new MouseButtonEventHandler(this.green_1_MouseLeftButtonDown);
				return;
			case 402:
				((System.Windows.Shapes.Path)target).MouseLeftButtonDown += new MouseButtonEventHandler(this.sky_1_MouseLeftButtonDown);
				return;
			case 403:
				((System.Windows.Shapes.Path)target).MouseLeftButtonDown += new MouseButtonEventHandler(this.blue_2_MouseLeftButtonDown);
				return;
			case 404:
				((System.Windows.Shapes.Path)target).MouseLeftButtonDown += new MouseButtonEventHandler(this.purple_1_MouseLeftButtonDown);
				return;
			case 405:
				this.inner_cycle_color1_glow = (Canvas)target;
				return;
			case 406:
				this.red1_glow = (System.Windows.Shapes.Path)target;
				return;
			case 407:
				this.orange1_glow = (System.Windows.Shapes.Path)target;
				return;
			case 408:
				this.yellow1_glow = (System.Windows.Shapes.Path)target;
				return;
			case 409:
				this.green1_glow = (System.Windows.Shapes.Path)target;
				return;
			case 410:
				this.sky1_glow = (System.Windows.Shapes.Path)target;
				return;
			case 411:
				this.blue1_glow = (System.Windows.Shapes.Path)target;
				return;
			case 412:
				this.purple1_glow = (System.Windows.Shapes.Path)target;
				return;
			case 413:
				this.outter_cycle_sensor = (Canvas)target;
				return;
			case 414:
				this.red5 = (Canvas)target;
				return;
			case 415:
				this.red_5 = (System.Windows.Shapes.Path)target;
				this.red_5.MouseLeftButtonDown += new MouseButtonEventHandler(this.red_4_MouseLeftButtonDown);
				return;
			case 416:
				this.orange5 = (Canvas)target;
				return;
			case 417:
				this.orange_5 = (System.Windows.Shapes.Path)target;
				this.orange_5.MouseLeftButtonDown += new MouseButtonEventHandler(this.orange_4_MouseLeftButtonDown);
				return;
			case 418:
				this.yellow5 = (Canvas)target;
				return;
			case 419:
				this.yellow_5 = (System.Windows.Shapes.Path)target;
				this.yellow_5.MouseLeftButtonDown += new MouseButtonEventHandler(this.yellow_4_MouseLeftButtonDown);
				return;
			case 420:
				this.green5 = (Canvas)target;
				return;
			case 421:
				this.green_5 = (System.Windows.Shapes.Path)target;
				this.green_5.MouseLeftButtonDown += new MouseButtonEventHandler(this.green_4_MouseLeftButtonDown);
				return;
			case 422:
				this.sky5 = (Canvas)target;
				return;
			case 423:
				this.sky_5 = (System.Windows.Shapes.Path)target;
				this.sky_5.MouseLeftButtonDown += new MouseButtonEventHandler(this.sky_4_MouseLeftButtonDown);
				return;
			case 424:
				this.blue5 = (Canvas)target;
				return;
			case 425:
				this.blue_5 = (System.Windows.Shapes.Path)target;
				this.blue_5.MouseLeftButtonDown += new MouseButtonEventHandler(this.blue_4_MouseLeftButtonDown);
				return;
			case 426:
				this.purple5 = (Canvas)target;
				return;
			case 427:
				this.purple_5 = (System.Windows.Shapes.Path)target;
				this.purple_5.MouseLeftButtonDown += new MouseButtonEventHandler(this.purple_4_MouseLeftButtonDown);
				return;
			case 428:
				this.outter_middle_cycle_sensor = (Canvas)target;
				return;
			case 429:
				this.red6 = (Canvas)target;
				return;
			case 430:
				this.red_6 = (System.Windows.Shapes.Path)target;
				this.red_6.MouseLeftButtonDown += new MouseButtonEventHandler(this.red_3_MouseLeftButtonDown);
				return;
			case 431:
				this.orange6 = (Canvas)target;
				return;
			case 432:
				this.orange_6 = (System.Windows.Shapes.Path)target;
				this.orange_6.MouseLeftButtonDown += new MouseButtonEventHandler(this.orange_3_MouseLeftButtonDown);
				return;
			case 433:
				this.yellow6 = (Canvas)target;
				return;
			case 434:
				this.yellow_6 = (System.Windows.Shapes.Path)target;
				this.yellow_6.MouseLeftButtonDown += new MouseButtonEventHandler(this.yellow_3_MouseLeftButtonDown);
				return;
			case 435:
				this.green6 = (Canvas)target;
				return;
			case 436:
				this.green_6 = (System.Windows.Shapes.Path)target;
				this.green_6.MouseLeftButtonDown += new MouseButtonEventHandler(this.green_3_MouseLeftButtonDown);
				return;
			case 437:
				this.sky6 = (Canvas)target;
				return;
			case 438:
				this.sky_6 = (System.Windows.Shapes.Path)target;
				this.sky_6.MouseLeftButtonDown += new MouseButtonEventHandler(this.sky_3_MouseLeftButtonDown);
				return;
			case 439:
				this.blue6 = (Canvas)target;
				return;
			case 440:
				this.blue_6 = (System.Windows.Shapes.Path)target;
				this.blue_6.MouseLeftButtonDown += new MouseButtonEventHandler(this.blue_3_MouseLeftButtonDown);
				return;
			case 441:
				this.purple6 = (Canvas)target;
				return;
			case 442:
				this.purple_6 = (System.Windows.Shapes.Path)target;
				this.purple_6.MouseLeftButtonDown += new MouseButtonEventHandler(this.purple_3_MouseLeftButtonDown);
				return;
			case 443:
				this.inner_middle_cycle_sensor = (Canvas)target;
				return;
			case 444:
				this.red7 = (Canvas)target;
				return;
			case 445:
				this.red_7 = (System.Windows.Shapes.Path)target;
				this.red_7.MouseLeftButtonDown += new MouseButtonEventHandler(this.red_2_MouseLeftButtonDown);
				return;
			case 446:
				this.orange7 = (Canvas)target;
				return;
			case 447:
				this.orange_7 = (System.Windows.Shapes.Path)target;
				this.orange_7.MouseLeftButtonDown += new MouseButtonEventHandler(this.orange_2_MouseLeftButtonDown);
				return;
			case 448:
				this.yellow7 = (Canvas)target;
				return;
			case 449:
				this.yellow_7 = (System.Windows.Shapes.Path)target;
				this.yellow_7.MouseLeftButtonDown += new MouseButtonEventHandler(this.yellow_2_MouseLeftButtonDown);
				return;
			case 450:
				this.green7 = (Canvas)target;
				return;
			case 451:
				this.green_7 = (System.Windows.Shapes.Path)target;
				this.green_7.MouseLeftButtonDown += new MouseButtonEventHandler(this.green_2_MouseLeftButtonDown);
				return;
			case 452:
				this.sky7 = (Canvas)target;
				return;
			case 453:
				this.sky_7 = (System.Windows.Shapes.Path)target;
				this.sky_7.MouseLeftButtonDown += new MouseButtonEventHandler(this.sky_2_MouseLeftButtonDown);
				return;
			case 454:
				this.blue7 = (Canvas)target;
				return;
			case 455:
				this.blue_7 = (System.Windows.Shapes.Path)target;
				this.blue_7.MouseLeftButtonDown += new MouseButtonEventHandler(this.blue_2_MouseLeftButtonDown);
				return;
			case 456:
				this.purple7 = (Canvas)target;
				return;
			case 457:
				this.purple_7 = (System.Windows.Shapes.Path)target;
				this.purple_7.MouseLeftButtonDown += new MouseButtonEventHandler(this.purple_2_MouseLeftButtonDown);
				return;
			case 458:
				this.inner_cycle_sensor = (Canvas)target;
				return;
			case 459:
				this.red8 = (Canvas)target;
				return;
			case 460:
				this.red_8 = (System.Windows.Shapes.Path)target;
				this.red_8.MouseLeftButtonDown += new MouseButtonEventHandler(this.red_1_MouseLeftButtonDown);
				return;
			case 461:
				this.orange8 = (Canvas)target;
				return;
			case 462:
				this.orange_8 = (System.Windows.Shapes.Path)target;
				this.orange_8.MouseLeftButtonDown += new MouseButtonEventHandler(this.orange_1_MouseLeftButtonDown);
				return;
			case 463:
				this.yellow8 = (Canvas)target;
				return;
			case 464:
				this.yellow_8 = (System.Windows.Shapes.Path)target;
				this.yellow_8.MouseLeftButtonDown += new MouseButtonEventHandler(this.yellow_1_MouseLeftButtonDown);
				return;
			case 465:
				this.green8 = (Canvas)target;
				return;
			case 466:
				this.green_8 = (System.Windows.Shapes.Path)target;
				this.green_8.MouseLeftButtonDown += new MouseButtonEventHandler(this.green_1_MouseLeftButtonDown);
				return;
			case 467:
				this.sky8 = (Canvas)target;
				return;
			case 468:
				this.sky_8 = (System.Windows.Shapes.Path)target;
				this.sky_8.MouseLeftButtonDown += new MouseButtonEventHandler(this.sky_1_MouseLeftButtonDown);
				return;
			case 469:
				this.blue8 = (Canvas)target;
				return;
			case 470:
				this.blue_8 = (System.Windows.Shapes.Path)target;
				this.blue_8.MouseLeftButtonDown += new MouseButtonEventHandler(this.blue_1_MouseLeftButtonDown);
				return;
			case 471:
				this.purple8 = (Canvas)target;
				return;
			case 472:
				this.purple_8 = (System.Windows.Shapes.Path)target;
				this.purple_8.MouseLeftButtonDown += new MouseButtonEventHandler(this.purple_1_MouseLeftButtonDown);
				return;
			case 473:
				this.white = (Canvas)target;
				this.white.MouseLeftButtonDown += new MouseButtonEventHandler(this.white_MouseLeftButtonDown);
				return;
			case 474:
				this.center_ball1 = (System.Windows.Shapes.Path)target;
				return;
			case 475:
				this.color_swatch_shadow = (Canvas)target;
				return;
			case 476:
				this.color_swatch_single = (Canvas)target;
				return;
			case 477:
				this.圖層_18 = (Canvas)target;
				return;
			case 478:
				this.white_Copy = (Canvas)target;
				this.white_Copy.MouseLeftButtonDown += new MouseButtonEventHandler(this.white_MouseLeftButtonDown);
				return;
			case 479:
				this.center_ball2 = (System.Windows.Shapes.Path)target;
				return;
			case 480:
				this._8ColorSwatchControl_ = (_ColorSwatchControl)target;
				return;
			case 481:
				this.red = (System.Windows.Shapes.Path)target;
				return;
			case 482:
				this.orange = (System.Windows.Shapes.Path)target;
				return;
			case 483:
				this.yellow = (System.Windows.Shapes.Path)target;
				return;
			case 484:
				this.green = (System.Windows.Shapes.Path)target;
				return;
			case 485:
				this.sky = (System.Windows.Shapes.Path)target;
				return;
			case 486:
				this.blue = (System.Windows.Shapes.Path)target;
				return;
			case 487:
				this.purple = (System.Windows.Shapes.Path)target;
				return;
			case 488:
				this.red_glow = (System.Windows.Shapes.Path)target;
				return;
			case 489:
				this.orange_glow = (System.Windows.Shapes.Path)target;
				return;
			case 490:
				this.yellow_glow = (System.Windows.Shapes.Path)target;
				return;
			case 491:
				this.green_glow = (System.Windows.Shapes.Path)target;
				return;
			case 492:
				this.sky_glow = (System.Windows.Shapes.Path)target;
				return;
			case 493:
				this.blue_glow = (System.Windows.Shapes.Path)target;
				return;
			case 494:
				this.purple_glow = (System.Windows.Shapes.Path)target;
				return;
			case 495:
				this.red_shadow = (System.Windows.Shapes.Path)target;
				this.red_shadow.MouseLeftButtonDown += new MouseButtonEventHandler(this.red_shadow_MouseLeftButtonDown);
				return;
			case 496:
				this.orange_shadow = (System.Windows.Shapes.Path)target;
				this.orange_shadow.MouseLeftButtonDown += new MouseButtonEventHandler(this.orange_shadow_MouseLeftButtonDown);
				return;
			case 497:
				this.yellow_shadow = (System.Windows.Shapes.Path)target;
				this.yellow_shadow.MouseLeftButtonDown += new MouseButtonEventHandler(this.yellow_shadow_MouseLeftButtonDown);
				return;
			case 498:
				this.green_shadow = (System.Windows.Shapes.Path)target;
				this.green_shadow.MouseLeftButtonDown += new MouseButtonEventHandler(this.green_shadow_MouseLeftButtonDown);
				return;
			case 499:
				this.sky_shadow = (System.Windows.Shapes.Path)target;
				this.sky_shadow.MouseLeftButtonDown += new MouseButtonEventHandler(this.sky_shadow_MouseLeftButtonDown);
				return;
			case 500:
				this.blue_shadow = (System.Windows.Shapes.Path)target;
				this.blue_shadow.MouseLeftButtonDown += new MouseButtonEventHandler(this.blue_shadow_MouseLeftButtonDown);
				return;
			case 501:
				this.purple_shadow = (System.Windows.Shapes.Path)target;
				this.purple_shadow.MouseLeftButtonDown += new MouseButtonEventHandler(this.purple_shadow_MouseLeftButtonDown);
				return;
			case 502:
				this.close_btn = (Canvas)target;
				this.close_btn.MouseEnter += new System.Windows.Input.MouseEventHandler(this.close_btn_MouseEnter);
				this.close_btn.MouseLeave += new System.Windows.Input.MouseEventHandler(this.close_btn_MouseLeave);
				this.close_btn.MouseLeftButtonDown += new MouseButtonEventHandler(this.close_btn_MouseLeftButtonDown);
				return;
			case 503:
				this.圖層_14 = (Canvas)target;
				return;
			case 504:
				this.ellipse = (Ellipse)target;
				return;
			case 505:
				this.keyboard_lock = (Canvas)target;
				this.keyboard_lock.MouseLeftButtonDown += new MouseButtonEventHandler(this.keyboard_lock_MouseLeftButtonDown);
				return;
			case 506:
				this.圖層_15 = (Canvas)target;
				return;
			case 507:
				this.ellipse_Copy1 = (Ellipse)target;
				return;
			case 508:
				this.ellipse3 = (Ellipse)target;
				return;
			case 509:
				this.keyboard_unlock = (Canvas)target;
				this.keyboard_unlock.MouseLeftButtonDown += new MouseButtonEventHandler(this.keyboard_unlock_MouseLeftButtonDown);
				return;
			case 510:
				this.圖層_16 = (Canvas)target;
				return;
			case 511:
				this.ellipse_Copy = (Ellipse)target;
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}
	}
}

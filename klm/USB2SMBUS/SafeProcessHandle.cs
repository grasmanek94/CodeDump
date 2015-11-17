using System;
using System.Runtime.InteropServices;

namespace USB2SMBUS
{
	internal class SafeProcessHandle : SafeHandle
	{
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		public SafeProcessHandle() : base(IntPtr.Zero, true)
		{
		}

		public SafeProcessHandle(IntPtr handle, bool ownsHandle) : base(IntPtr.Zero, ownsHandle)
		{
			this.handle = handle;
		}

		protected override bool ReleaseHandle()
		{
			return Kernel32.CloseHandle(this.handle);
		}
	}
}

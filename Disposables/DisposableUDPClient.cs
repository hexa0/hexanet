using System;
using System.Net;
using System.Net.Sockets;

namespace HexaNet.Disposables
{
	class DisposableUDPClient : UdpClient
	{
		public bool IsDisposed { get; set; }
		protected override void Dispose(bool disposing)
		{
			IsDisposed = true;
			base.Dispose(disposing);
		}

		public void Shutdown()
		{
			if (IsDisposed) return;

			Console.WriteLine("Shutting down DisposableUDPClient.");

			Client.Shutdown(SocketShutdown.Both);
			Client.Close();
			Dispose(true);
		}

		public DisposableUDPClient() : base()
		{
#if NET9_0_OR_GREATER
			Client.DualMode = true;
#elif NET35
			// 3.5 doesn't need ipv6 as it is only communicating with the game client itself
#endif
		}
		public DisposableUDPClient(IPEndPoint endPoint) : base(endPoint) { }
	}
}
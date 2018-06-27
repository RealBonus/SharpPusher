using System;
using System.Threading.Tasks;

namespace SharpPusher
{
	/// <summary>
	/// Pusher.
	/// T for Notification type;
	/// U for Resut code type;
	/// </summary>
	public interface IPusher<T, U>
	{
		Task SendNotificationAsync(T notification, string deviceToken);

		event NotificationSuccessHandler<T> OnNotificationSuccess;
		event NotificationFailedHandler<T, U> OnNotificationFailed;
	}

	public delegate void NotificationSuccessHandler<T>(object sender, NotificationSuccessEventArgs<T> args);
	public delegate void NotificationFailedHandler<T, U>(object sender, NotificationFailedEventArgs<T, U> args);

	public class NotificationSuccessEventArgs<T>: EventArgs
	{
		public T Notification { get; }
		
		public NotificationSuccessEventArgs(T notification)
		{
			Notification = notification;
		}

	}

	public class NotificationFailedEventArgs<T, U> : EventArgs
	{
		public T Notification { get; }
		public U ResultCode { get; }
		public string Reason { get; }
		public Exception Exception { get; }
		
		public NotificationFailedEventArgs(T notification, U resultCode, string reason, Exception exception)
		{
			Notification = notification;
			ResultCode = resultCode;
			Reason = reason;
			Exception = exception;
		}
	}
}

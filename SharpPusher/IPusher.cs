﻿using System;
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
		public string Token { get; }
		public T Notification { get; }
		
		public NotificationSuccessEventArgs(string token, T notification)
		{
			Token = token;
			Notification = notification;
		}

	}

	public class NotificationFailedEventArgs<T, U> : EventArgs
	{
		public string Token { get; }
		public T Notification { get; }
		public U ResultCode { get; }
		public string Reason { get; }
		public Exception Exception { get; }
		
		public NotificationFailedEventArgs(string token, T notification, U resultCode, string reason, Exception exception)
		{
			Token = token;
			Notification = notification;
			ResultCode = resultCode;
			Reason = reason;
			Exception = exception;
		}
	}
}

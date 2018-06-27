using System;
using System.Threading.Tasks;
using SharpPusher;

namespace SharpPusherSample
{
	class Program
	{
		static async Task Main(string[] args)
		{
			var keyId = "";
			var teamId = "";
			var bundleAppId = "";
			var keyPath = "";
			var keyPassword = "";

			var deviceToken = "";

			var pusher = new ApnsPusher(keyId, teamId, bundleAppId, keyPath, keyPassword, ApnsEnvironment.Production);

			var notification = new ApnsNotification
			{
				Payload = new ApnsPayload
				{
					Alert = new ApnsNotificationAlert
					{
						TitleLocalizationKey = "NotificationsService.NewMessage.Title",
						BodyLocalizationKey = "NotificationsService.NewMessage.BodySingle"
					},
					Badge = 7
				}
			};

			pusher.OnNotificationSuccess += OnNotificationSuccess;
			pusher.OnNotificationFailed += OnNotificationFailed;

			Console.WriteLine("Sending notification...");
			await pusher.SendNotificationAsync(notification, deviceToken);
		}

		static void OnNotificationSuccess(object sender, NotificationSuccessEventArgs<ApnsNotification> args)
		{
			Console.WriteLine("Notification success");
		}

		static void OnNotificationFailed(object sender, NotificationFailedEventArgs<ApnsNotification, ApnsResult> args)
		{
			Console.WriteLine("Notification failed. Code: {0}, Reason: {1}", args.ResultCode, args.Reason);
		}

	}
}

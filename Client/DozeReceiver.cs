using Android.App;
using Android.Content;
using Android.OS;

namespace Task2
{
    [IntentFilter(new[] { PowerManager.ActionDeviceIdleModeChanged })] // WTF, this does not triger..
    [BroadcastReceiver(Enabled = true)]
    class DozeReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                PowerManager pm = (PowerManager)context.GetSystemService("power");          
                PowerManager.WakeLock wakeLock = pm.NewWakeLock(WakeLockFlags.Partial, "LocationManagerService");
                if (pm.IsDeviceIdleMode)
                {
                    // the device is now in doze mode
                    Vibrator vibrator = (Vibrator)context.GetSystemService("vibrator");
                    vibrator.Vibrate(100);
                    if (wakeLock.IsHeld == false)
                    {
                        wakeLock.Acquire();
                    }
                }
                else
                {
                    Vibrator vibrator = (Vibrator)context.GetSystemService("vibrator");
                    vibrator.Vibrate(1000);
                    if (wakeLock.IsHeld)
                    {
                        wakeLock.Release();
                    }
                    // the device just woke up from doze mode
                }
            }
        }
    }
}
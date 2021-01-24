using Android.Content;
using Android.OS;

namespace Task2
{
    [BroadcastReceiver(Exported = true)]
    class Alarm : BroadcastReceiver
    {

        public override void OnReceive(Context context, Intent intent)
        {
            PowerManager pm = (PowerManager)context.GetSystemService("power");
            PowerManager.WakeLock wk = pm.NewWakeLock(WakeLockFlags.Partial, "LocationManagerService");
            wk.Acquire();
            ((MainActivity)MainActivity.global_activity).Baglanti_Kur();
            if (wk.IsHeld) { wk.Release(); }
        }
    }
}
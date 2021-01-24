using Android.Content;

namespace Task2
{
    [BroadcastReceiver(Exported = true)]
    class Alarm : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            ((MainActivity)MainActivity.global_activity).cancelAlarm(context);
            if (MainActivity.wk.IsHeld == true)
            {
                MainActivity.wk.Release();
            }
           ((MainActivity)MainActivity.global_activity).Baglanti_Kur();
        }
    }
}
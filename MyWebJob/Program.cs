using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyWebJob
{
	class Program
	{
		static void Main(string[] args)
		{
			while (true)
			{
				Console.WriteLine("Checking for new e-mails to be sent...");

				using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MainDatabase"].ConnectionString))
				{
					conn.Open();

					var cmd = new SqlCommand("SELECT * FROM EmailQueue WHERE Sent IS NULL", conn);

					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							Console.WriteLine($"Email ID:{reader["ID"]} found...");

							using (var smtpClient = new SmtpClient())
							{
								smtpClient.Host = ConfigurationManager.AppSettings["SmtpHost"];
								smtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SmtpUsername"], ConfigurationManager.AppSettings["SmtpPassword"]);

								smtpClient.Send(
									from: ConfigurationManager.AppSettings["SmtpFrom"],
									recipients: reader["Recipient"].ToString(),
									subject: reader["Subject"].ToString(),
									body: reader["Body"].ToString());

								// TODO: Write EmqilQueue.Sent to DB

								Console.WriteLine($"Email ID:{reader["ID"]} sent...");
							}
						}
					}
				}

				Thread.Sleep(30_000); // 30sec
			}
		}
	}
}

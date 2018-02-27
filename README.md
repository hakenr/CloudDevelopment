# NSWI152 - Cloud Development

## LAB 1 - Create & Deploy simple Web Site to Azure App Service
1. Create new Visual Studio project using *ASP.NET Empty Web Site* template.
1. Add new item to the root folder of the project - *Web Form (place code in separate file)* - Deafult.aspx
1. In Default.aspx - Add `<asp:Label ID="MyLabel" runat="server" />` inside the `<form>` element.
	``` html
	<body>
		<form id="form1" runat="server">
			<div>
				<asp:Label ID="MyLabel" runat="server" />
			</div>
		</form>
	</body>
	```
1. In Default.aspx.cs `Page_Load` method - Set `Text` property of the `MyLabel` control to whatever string:
	``` csharp
	protected void Page_Load(object sender, EventArgs e)
	{
		MyLabel.Text = "Hello World from Azure";
	}
	```
1. (Start the WebSite locally to verify the result - `Ctrl`+`F5`)
1. Publish the WebSite to Azure AppService

   1. RClick project name in *Solution Explorer* window - Select *Publish Web Site*.
   1. Select *Microsoft Azure App Service* as the publish target.
   1. Create new AppService (or choose existing one if you already created one).
		![Create App Service](images/CreateAppService.png)
   1. Publish the website to Azure and check the result.
		![Publish](images/Publish.png)
1. Change anything and re-publish the result to Azure.
### [OPTIONAL] Automatic Deployment from GitHub
8. Push the solution source code to your GitHub account.

    You might want to simplify your website by removing unnecessary configuration which can cause issues when building the app. See my [GitHub commit](https://github.com/hakenr/CloudDevelopment/commit/74f3b4f3f11e1ac05f2385b37b1aa47fb30f92e1).

9. Setup an automatic deployment from GitHub to Azure AppService:
   
   1. Create new Azure AppService using Azure Portal.
   1. Go to *Deployment Options* blade and set up your GitHub repository as deployment source (note other deployment sources available - VSTS, OneDrive, Dropbox, git, ...)
		![Git Hub Deployment](images/GitHubDeployment.png)
   1. Check the result - the application should be deployed within few seconds.
   1. Change anything in your website and push the commit to GitHub - the application should be redeployed within few seconds.


## LAB 2 - Web Job with Azure SQL connectivity
In this lab we will create and deploy a WebJob (background task) which connects to Azure SQL database. The job will run in scheduled time intervals, read data from a EmailQueue table and send e-mail via [SendGrid](https://docs.microsoft.com/en-us/azure/sendgrid-dotnet-how-to-send-email) mail service (in future lab we might move the queue from Azure SQL to Azure Storage Queues).
1. Create new Azure SQL server and database:
   1. Go to [Azure Portal](https://portal.azure.com) and use *Create Resource* button in left panel to create a new *SQL Database*.
   2. Fill in the name and other properties of the DB + create a new SQL server to host the DB ()

      ![Create Sql Database](images/CreateSqlDatabase.png)

1. In Visual Studio open new SQL Query window for the new DB

	Note: In practice you usualy use [Microsoft SQL Server Management Studio](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms) or [Microsoft SQL Server Operations Studio](https://docs.microsoft.com/en-us/sql/sql-operations-studio/download) to perform DB-related development/management tasks. In this lab we will use plain Visual Studio to demonstrate an alternate option.
   1. In Visual Studio open *Cloud Explorer* panel (you can use *Quick Launch*) and navigate to your database - *SQL Databases* / *Open SQL Server Object Explorer* (r-click)

      ![Cloud Explorer Sql](images/CloudExplorerSql.png)

	  2. Connect to your Azure SQL server

      ![Connect Azure Sql](images/ConnectAzureSql.png)

	  3. Confirm adding of your current IP address to Azure SQL Server firewall (You can check firewall rules using Azure Portal - Go to you SQL Server and see *Firewall / Network Settings* section)

      ![Visual Studio Azure Sql Server Firewall](images/VisualStudioAzureSqlServerFirewall.png)

   4. In *SQL Server Object Explorer* navigate to your DB and open *New query...* (r-click)

      ![New Query](images/NewQuery.png)

1. Create the *EmailQueue* table in your DB:
	``` sql
	CREATE TABLE dbo.EmailQueue	(
		ID int PRIMARY KEY NOT NULL IDENTITY (1, 1),
		Recipient nvarchar(MAX) NOT NULL,
		Subject nvarchar(400) NOT NULL,
		Body nvarchar(MAX) NOT NULL,
		Created datetime NOT NULL,
		Sent datetime NULL
	)
	```
1. [OPTIONAL] Create new SQL login account for the application not to use system administrator account

	Note: In real scenario you don't want your applications to use the system administrator account to access the DB. Dedicated login account for each client should be created (with restricted access rights).

   1. In *master* DB run the following SQL query
   ```sql
	CREATE LOGIN CloudDevLogin WITH PASSWORD = '***new password***'
   ```
   2. In your application DB run following SQL query
   ```sql
	CREATE USER CloudDevUser FOR LOGIN CloudDevLogin WITH DEFAULT_SCHEMA = dbo
	GO

	EXEC sp_addrolemember N'db_owner', N'CloudDevUser'
	GO
   ```
1. Add new *Console Application (.NET Framework)* project to the solution.
1. Add following code to *Program.cs* `Main` method (application entry-point):
	```csharp
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
	```
1. Add corresponding `ConnectionStrings` and `AppSettings` sections to *App.Config* file.

	Note: In real scenario you would set the values to development settings. These will be overriden by Azure Portal Application Settings (see bellow).

	```xml
	<connectionStrings>
		<!-- Will be replaced with Azure Portal Application Settings -->
		<add name="MainDatabase" connectionString="Data Source=sql.development.local;Initial Catalog=CloudDev;User Id=development;Password=development;Application Name=CloudDevWebJob"/>
	</connectionStrings>
	<appSettings>
		<!-- Will be replaced with Azure Portal Application Settings -->
		<add key="SmtpHost" value="mail.development.local" />
		<add key="SmtpUsername" value="" />
		<add key="SmtpPassword" value="" />
		<add key="SmtpFrom" value="haken@devmail.havit.cz" />
	</appSettings>
	```
1. Publish the result as Azure Web Job...
 
	![Publish Azure Web Job](images/PublishAzureWebJob.png)

	Note: The job will be failing now. We have to set the production Application Settings.

1. [OPTIONAL] Create a new SendGrid account in Azure and use it's credentials to send mails.
1. Go to Azure Portal, locate the App Service hosting the WebJob and set appropriate Application Settings (ConnectionStrings and AppSettings):

	![App Settings](images/AppSettings.png)

1. Try the job by adding a row to EmailQueue table.
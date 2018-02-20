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
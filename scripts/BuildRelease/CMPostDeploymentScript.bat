	DEL "App_Config\ConnectionStrings.config"
	COPY "App_Config\ConnectionStrings\ConnectionStrings.CM.config.disabled" "App_Config\ConnectionStrings.config"
	RMDIR "App_Config\ConnectionStrings" /s /q

	DEL "Web.config"
	COPY "WebConfigs\Sample.All.Website\WebConfigs\Web.CM.config.disabled" "Web.config"
	RMDIR "WebConfigs" /s /q

	DEL "App_Config\Include\Project\All\z.Project.All.config"
	COPY "App_Config\Include\Project\All\z.Project.All.CI.config.disabled" "App_Config\Include\Project\All\z.Project.All.CI.config"

	DEL "App_Config\Include\Project\Campaign\Project.Campaign.config"
	COPY "App_Config\Include\Project\Campaign\Project.Campaign.CI.config.disabled" "App_Config\Include\Project\Campaign\Project.Campaign.config"

	DEL "App_Config\Include\Project\Greenwood\Project.Brand.config"
	COPY "App_Config\Include\Project\Greenwood\Project.Brand.CI.config.disabled" "App_Config\Include\Project\Brand\Project.Greenwood.config"
--CREATE SCHEMA processor

GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='IntegratorHistory' AND xtype='U')
BEGIN	
	CREATE TABLE processor.IntegratorHistory (
		id INT IDENTITY(1,1) PRIMARY KEY,
		date_of_process DATETIME
	);
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='AggregationCriteria' AND xtype='U')
BEGIN
    CREATE TABLE AggregationCriteria (
        criteria_id INT IDENTITY(1,1) PRIMARY KEY,
        criteria_name NVARCHAR(50) UNIQUE NOT NULL
    );
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ContributorType' AND xtype='U')
BEGIN
    CREATE TABLE ContributorType (
        contributor_type_id INT IDENTITY(1,1) PRIMARY KEY,
        name NVARCHAR(50) UNIQUE NOT NULL
    );
END

--IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='TimeUnit' AND xtype='U')
--BEGIN
--    CREATE TABLE TimeUnit (
--        time_unit_id INT IDENTITY(1,1) PRIMARY KEY,
--        name NVARCHAR(50) UNIQUE NOT NULL
--    );
--END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ContentSource' AND xtype='U')
BEGIN
    CREATE TABLE ContentSource (
        source_id INT IDENTITY(1,1) PRIMARY KEY,
        source_name NVARCHAR(50) UNIQUE NOT NULL
    );
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='AssetType' AND xtype='U')
BEGIN
    CREATE TABLE AssetType (
        asset_type_id INT IDENTITY(1,1) PRIMARY KEY,
        asset_type_name NVARCHAR(50) UNIQUE NOT NULL,
		active BIT NOT NULL
    );
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='EngagementMetricType' AND xtype='U')
BEGIN
    CREATE TABLE EngagementMetricType (
        metric_type_id INT IDENTITY(1,1) PRIMARY KEY,
        metric_type_name NVARCHAR(50) UNIQUE NOT NULL
    );
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='EngagementMetricQualifier' AND xtype='U')
BEGIN
    CREATE TABLE EngagementMetricQualifier (
        metric_qualifier_id INT IDENTITY(1,1) PRIMARY KEY,
        metric_qualifier_name NVARCHAR(50) UNIQUE NOT NULL
    );
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='LearnerDetail' AND xtype='U')
BEGIN
    CREATE TABLE LearnerDetail (
        learner_id INT IDENTITY(1,1) PRIMARY KEY,
        name NVARCHAR(100) NULL,
        email NVARCHAR(250) NOT NULL UNIQUE,
        unique_user_id NVARCHAR(250) NULL,
        profile_urn NVARCHAR(255) NULL
    );
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='EnterpriseGroup' AND xtype='U')
BEGIN
    CREATE TABLE EnterpriseGroup (
        group_id INT IDENTITY(1,1) PRIMARY KEY,
        learner_id INT NOT NULL,
        group_name NVARCHAR(250) NOT NULL,
        FOREIGN KEY (learner_id) REFERENCES LearnerDetail(learner_id)
    );
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ContentDetail' AND xtype='U')
BEGIN
	CREATE TABLE ContentDetail (
		content_id INT IDENTITY(1,1) PRIMARY KEY,
		content_provider_name NVARCHAR(100) NOT NULL,
		name NVARCHAR(255) NOT NULL,
		content_urn NVARCHAR(255) NOT NULL,
		country CHAR(2) NULL,
		language CHAR(2) NULL
	);
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='EngagementActivity' AND xtype='U')
BEGIN
	CREATE TABLE EngagementActivity (
		engagement_activity_id INT IDENTITY(1,1) PRIMARY KEY,
		learner_id INT NOT NULL,
		content_id INT NOT NULL,
		asset_type_id INT NULL,
		metric_type_id INT NOT NULL,
		metric_qualifier_id INT NOT NULL,
		engagement_value INT NOT NULL,
		first_engaged_at DATETIME,
		last_engaged_at DATETIME,
		FOREIGN KEY (learner_id) REFERENCES LearnerDetail(learner_id),
		FOREIGN KEY (content_id) REFERENCES ContentDetail(content_id),
		FOREIGN KEY (asset_type_id) REFERENCES AssetType(asset_type_id),
		FOREIGN KEY (metric_type_id) REFERENCES EngagementMetricType(metric_type_id),
		FOREIGN KEY (metric_qualifier_id) REFERENCES EngagementMetricQualifier(metric_qualifier_id)
	);
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='QueryParameter' AND xtype='U')
BEGIN
	CREATE TABLE QueryParameter (
		param_id INT IDENTITY(1,1) PRIMARY KEY,
		name VARCHAR(255) NOT NULL,
		value VARCHAR(255) NOT NULL,
		active bit NOT NULL
	);
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ClassificationType' AND xtype='U')
BEGIN
	CREATE TABLE ClassificationType (
		classification_type_id INT IDENTITY(1,1) PRIMARY KEY,
		name NVARCHAR(50) NOT NULL UNIQUE
	);
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='OwnerDetail' AND xtype='U')
BEGIN
	CREATE TABLE OwnerDetail (
		owner_id INT IDENTITY(1,1) PRIMARY KEY,
		name NVARCHAR(255) NOT NULL,
		urn NVARCHAR(255) NOT NULL UNIQUE ,
		country CHAR(2) NULL,
		language CHAR(2) NULL
	);
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ClassificationDetail' AND xtype='U')
BEGIN
	CREATE TABLE ClassificationDetail (
		classification_id INT IDENTITY(1,1) PRIMARY KEY,
		classification_type_id INT NULL FOREIGN KEY REFERENCES ClassificationType(classification_type_id),
		owner_id INT NULL FOREIGN KEY REFERENCES OwnerDetail(owner_id),
		country CHAR(2) NULL,
		language CHAR(2) NULL,
		name_value NVARCHAR(255) NOT NULL,
		urn NVARCHAR(255) NOT NULL UNIQUE
	);
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Asset' AND xtype='U')
BEGIN
    CREATE TABLE Asset (
        asset_id INT IDENTITY(1,1) PRIMARY KEY,
        urn NVARCHAR(255) NOT NULL UNIQUE,
        title NVARCHAR(255) NOT NULL,
		title_country CHAR(2) NULL,	
		title_language CHAR(2) NULL,
        asset_type_id INT FOREIGN KEY REFERENCES AssetType(asset_type_id) 
    );
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='AssetDetail' AND xtype='U')
BEGIN
    CREATE TABLE AssetDetail (
        asset_id INT PRIMARY KEY FOREIGN KEY REFERENCES Asset(asset_id),        
        description NVARCHAR(MAX),
        short_description NVARCHAR(MAX),
		accessor_name NVARCHAR(255),
		accessor_urn NVARCHAR(255),
		availability NVARCHAR(50),
		level VARCHAR(25) NULL,
		time_to_complete_unit VARCHAR(25) NULL,
		time_to_complete_duration INT NULL,
        last_updated_at DATETIME,
        published_at DATETIME,
    );
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='AssetContent' AND xtype='U')
BEGIN
    CREATE TABLE AssetContent (
        parent_asset_id INT FOREIGN KEY REFERENCES Asset(asset_id),
        child_asset_id INT FOREIGN KEY REFERENCES Asset(asset_id),
		PRIMARY KEY (parent_asset_id, child_asset_id)
    );
END

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'AssetAvailableLocale')
BEGIN
    CREATE TABLE AssetAvailableLocale (
        asset_available_locale_id INT IDENTITY(1,1) PRIMARY KEY,
		asset_id INT  NOT NULL FOREIGN KEY REFERENCES AssetDetail(asset_id),
        country CHAR(2) NULL,	
		language CHAR(2) NULL,	
    );
END;

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'AssetUrl')
BEGIN
    CREATE TABLE AssetUrl (
        asset_url_id INT IDENTITY(1,1) PRIMARY KEY,
		asset_id INT  NOT NULL FOREIGN KEY REFERENCES AssetDetail(asset_id),
        aicc_launch NVARCHAR(MAX),
		sso_launch NVARCHAR(MAX),
		web_launch NVARCHAR(MAX)
    );
END;

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='AssetClassification' AND xtype='U')
BEGIN
    CREATE TABLE AssetClassification (
		asset_classification_id INT IDENTITY(1,1) PRIMARY KEY,
        classification_id INT FOREIGN KEY REFERENCES ClassificationDetail(classification_id),
		asset_id INT  FOREIGN KEY REFERENCES AssetDetail(asset_id),
        assigner_urn NVARCHAR(255),
        assigner_name NVARCHAR(255),
		--classification_path_id INT NULL FOREIGN KEY REFERENCES ClassificationPathDetail(classification_path_id)
    );
END

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'AssetClassificationPath')
BEGIN
    CREATE TABLE AssetClassificationPath (
        asset_classification_path_id INT IDENTITY(1,1) PRIMARY KEY,
        asset_classification_id INT NOT NULL FOREIGN KEY REFERENCES AssetClassification(asset_classification_id),
        classification_id INT NOT NULL FOREIGN KEY REFERENCES ClassificationDetail(classification_id)
    );
END;

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ContributorDetail' AND xtype='U')
BEGIN
    CREATE TABLE ContributorDetail (
        contributor_id INT IDENTITY(1,1) PRIMARY KEY,
        urn NVARCHAR(255) NOT NULL UNIQUE,
        name NVARCHAR(255),
		author_first_name NVARCHAR(100),
		author_last_name NVARCHAR(100),
        contributor_type_id INT FOREIGN KEY REFERENCES ContributorType(contributor_type_id)
    );
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='AssetContributor' AND xtype='U')
BEGIN
    CREATE TABLE AssetContributor (
        asset_contributor_id INT IDENTITY(1,1) PRIMARY KEY,
		contributor_id INT FOREIGN KEY REFERENCES ContributorDetail(contributor_id),
        asset_id INT FOREIGN KEY REFERENCES AssetDetail(asset_id)
    );
END


MERGE AggregationCriteria AS target
USING (VALUES 
    ('ACCOUNT'),
    ('INDIVIDUAL'),
    ('CONTENT')
) AS source (criteria_name)
ON target.criteria_name = source.criteria_name
WHEN NOT MATCHED BY TARGET THEN
    INSERT (criteria_name) VALUES (source.criteria_name);

-- ContentSource
MERGE ContentSource AS target
USING (VALUES 
    ('ALL_SOURCES'),
    ('LINKEDIN_LEARNING'),
    ('ORGANIZATION'),
    ('THIRD_PARTY')
) AS source (source_name)
ON target.source_name = source.source_name
WHEN NOT MATCHED BY TARGET THEN
    INSERT (source_name) VALUES (source.source_name);

-- AssetType
INSERT INTO AssetType (asset_type_name, active) VALUES ('ARTICLE', 1);
INSERT INTO AssetType (asset_type_name, active) VALUES ('ASSESSMENT', 0);
INSERT INTO AssetType (asset_type_name, active) VALUES ('AUDIO', 1);
INSERT INTO AssetType (asset_type_name, active) VALUES ('BOOK', 1);
INSERT INTO AssetType (asset_type_name, active) VALUES ('CHAPTER', 0);
INSERT INTO AssetType (asset_type_name, active) VALUES ('COURSE', 1);
INSERT INTO AssetType (asset_type_name, active) VALUES ('DOCUMENT', 0);
INSERT INTO AssetType (asset_type_name, active) VALUES ('EVENT', 1);
INSERT INTO AssetType (asset_type_name, active) VALUES ('LEARNING_COLLECTION', 1);
INSERT INTO AssetType (asset_type_name, active) VALUES ('LEARNING_PATH', 1);
INSERT INTO AssetType (asset_type_name, active) VALUES ('LEARNING_PATH_SECTION', 0);  
INSERT INTO AssetType (asset_type_name, active) VALUES ('VIDEO', 1);


-- EngagementMetricType
MERGE EngagementMetricType AS target
USING (VALUES 
    ('SECONDS_VIEWED'),
    ('COMPLETIONS'),
    ('DAYS_ACTIVE'),
    ('MARKED_AS_DONE'),
    ('PROGRESS_PERCENTAGE'),
    ('VIEWS'),
    ('ENGAGED_LEARNERS')
) AS source (metric_type_name)
ON target.metric_type_name = source.metric_type_name
WHEN NOT MATCHED BY TARGET THEN
    INSERT (metric_type_name) VALUES (source.metric_type_name);


-- EngagementMetricQualifier
MERGE EngagementMetricQualifier AS target
USING (VALUES 
    ('UNIQUE'),
    ('TOTAL')
) AS source (metric_qualifier_name)
ON target.metric_qualifier_name = source.metric_qualifier_name
WHEN NOT MATCHED BY TARGET THEN
    INSERT (metric_qualifier_name) VALUES (source.metric_qualifier_name);

GO

INSERT INTO ClassificationType (name)
VALUES
    ('SKILL'),
    ('LIBRARY'),
    ('SUBJECT'),
    ('TOPIC'),
    ('CONTINUING_EDUCATION_UNIT');


INSERT INTO [dbo].[QueryParameter]([name],[value],[active])
VALUES ('q', 'criteria', 1)
INSERT INTO [dbo].[QueryParameter]([name],[value],[active])
VALUES ('start', '1', 1)
INSERT INTO [dbo].[QueryParameter]([name],[value],[active])
VALUES ('count', '1000', 1)
INSERT INTO [dbo].[QueryParameter]([name],[value],[active])
VALUES ('startedAt', '1704067200000', 0)	-- Monday, 1 January 2024
INSERT INTO [dbo].[QueryParameter]([name],[value],[active])
VALUES ('timeOffset.duration', '1', 1)
INSERT INTO [dbo].[QueryParameter]([name],[value],[active])
VALUES ('aggregationCriteria.primary', 'INDIVIDUAL', 1)
INSERT INTO [dbo].[QueryParameter]([name],[value],[active])
VALUES ('timeOffset.unit', 'DAY', 1)
INSERT INTO [dbo].[QueryParameter]([name],[value],[active])
VALUES ('aggregationCriteria.secondary', 'CONTENT', 1)
INSERT INTO [dbo].[QueryParameter]([name],[value],[active])
VALUES ('assetType', 'COURSE', 0)

INSERT INTO ContributorType
VALUES ('AUTHOR')
INSERT INTO ContributorType
VALUES ('PUBLISHER')

--INSERT INTO TimeUnit
--VALUES ('SECOND')
--INSERT INTO TimeUnit
--VALUES ('MINUTE')
--INSERT INTO TimeUnit
--VALUES ('HOUR')

using MongoDB.Driver;
using TagIt;
using TagIt.Store;
using TagIt.Store.Mongo;

public class DataSeeder
{
    private readonly ITagIdDbContext _tagIdDbContext;

    public DataSeeder(ITagIdDbContext tagIdDbContext)
    {
        _tagIdDbContext = tagIdDbContext;
    }

    public async Task SeedAsync(CancellationToken cancellationToken)
    {
        //await SeedConnectorsAsync(cancellationToken);
        //await SeedJobsAsync(cancellationToken);
        //await SeedPropertyDefinitionsAsync(cancellationToken);
        //await SeedThingTypesAsync(cancellationToken);
        //await SeedThingClassesAsync(cancellationToken);

        await SeedReceiversAsync(cancellationToken);
    }

    public async Task SeedConnectorsAsync(CancellationToken cancellationToken)
    {
        IMongoCollection<ConnectorDefintion> collection = _tagIdDbContext.GetCollection<ConnectorDefintion>();

        await collection.DeleteManyAsync(Builders<ConnectorDefintion>.Filter.Empty);
        await collection.InsertManyAsync(
            Connectors,
            new InsertManyOptions(),
            cancellationToken);
    }

    public async Task SeedJobsAsync(CancellationToken cancellationToken)
    {
        IMongoCollection<JobDefintion> collection = _tagIdDbContext
            .GetCollection<JobDefintion>();

        await collection.DeleteManyAsync(Builders<JobDefintion>.Filter.Empty);
        await collection.InsertManyAsync(
            JobDefintions,
            new InsertManyOptions(),
            cancellationToken);
    }

    public async Task SeedPropertyDefinitionsAsync(CancellationToken cancellationToken)
    {
        IMongoCollection<PropertyDefinition> collection = _tagIdDbContext
            .GetCollection<PropertyDefinition>();

        await collection.DeleteManyAsync(Builders<PropertyDefinition>.Filter.Empty);
        await collection.InsertManyAsync(
            PropertyDefinitions,
            new InsertManyOptions(),
            cancellationToken);
    }

    public async Task SeedThingTypesAsync(CancellationToken cancellationToken)
    {
        IMongoCollection<ThingType> collection = _tagIdDbContext
            .GetCollection<ThingType>();

        await collection.DeleteManyAsync(Builders<ThingType>.Filter.Empty);
        await collection.InsertManyAsync(
            ThingTypes,
            new InsertManyOptions(),
            cancellationToken);
    }

    public async Task SeedReceiversAsync(CancellationToken cancellationToken)
    {
        IMongoCollection<Receiver> collection = _tagIdDbContext
            .GetCollection<Receiver>();

        await collection.DeleteManyAsync(Builders<Receiver>.Filter.Empty);
        await collection.InsertManyAsync(
            Receivers,
            new InsertManyOptions(),
            cancellationToken);
    }

    public async Task SeedThingClassesAsync(CancellationToken cancellationToken)
    {
        IMongoCollection<ThingClass> collection = _tagIdDbContext
            .GetCollection<ThingClass>();

        await collection.DeleteManyAsync(Builders<ThingClass>.Filter.Empty);
        await collection.InsertManyAsync(
            ThingClasses,
            new InsertManyOptions(),
            cancellationToken);
    }

    public IEnumerable<ThingClass> ThingClasses =>
        new List<ThingClass>()
        {
                new ThingClass
                {
                    Id = Guid.Parse("768956f0-4be2-463f-877c-164974a46a3a"),
                    Name = "Contract",
                    Version = NewVersion
                },
                new ThingClass
                {
                    Id = Guid.Parse("5cb97142-c291-429c-a070-5da01d7e3991"),
                    Name = "Bill",
                    Properties = new List<PropertyDefinitionLink>
                    {
                        new PropertyDefinitionLink{ DefinitionId = Guid.Parse("efaf1a07-6f3f-4169-9bab-1190e20e805d")},
                        new PropertyDefinitionLink{ DefinitionId = Guid.Parse("e16c1632-9272-4288-9533-112c85515598")},
                    },
                    Version = NewVersion
                },
                new ThingClass
                {
                    Id = Guid.Parse("4962ae06-d6c2-4ee1-9478-7f6ac1943901"),
                    Name = "Receipt",
                    Version = NewVersion
                }
        };

    public IEnumerable<PropertyDefinition> PropertyDefinitions =>
    new List<PropertyDefinition>()
    {
            new()
            {
                Id = Guid.Parse("efaf1a07-6f3f-4169-9bab-1190e20e805d"),
                Name = "Amount",
                DataType = PropertyDataType.Number
            },
            new()
            {
                Id = Guid.Parse("25b99fe4-21a0-4801-a010-1ff15f7d90aa"),
                Name = "WarantyUntil",
                DataType = PropertyDataType.DateTime
            },
            new()
            {
                Id = Guid.Parse("e16c1632-9272-4288-9533-112c85515598"),
                Name = "DueDate",
                DataType = PropertyDataType.DateTime
            },
    };

    public IEnumerable<ThingType> ThingTypes =>
        new List<ThingType>()
        {
            new ThingType
            {
                Id = Guid.NewGuid(),
                Name = "Document",
                ContentTypeMap = new List<string>{
                    "pdf",
                    "docx" },
                ValidClasses = ThingClasses.Select(x => x.Id).ToArray(),
                Version = NewVersion,
            },
            new ThingType
            {
                Id = Guid.NewGuid(),
                Name = "Email",
                ContentTypeMap = new List<string>{ "eml", "email" },
                Version = NewVersion
            },
            new ThingType
            {
                Id = Guid.NewGuid(),
                Name = "Web Address",
                Version = NewVersion
            },
            new ThingType
            {
                Id = Guid.NewGuid(),
                Name = "Geo Location",
                Version = NewVersion
            },
            new ThingType
            {
                Id = Guid.NewGuid(),
                Name = "Note",
                Version = NewVersion
            }
        };


    public IEnumerable<JobDefintion> JobDefintions =>
        new List<JobDefintion>()
        {
            new JobDefintion
            {
                Id = Guid.NewGuid(),
                Name = "Harvest_01",
                Enabled= false,
                RunMode = JobRunMode.Harvest,
                SourceConnectorId= Connectors.Skip(1).First().Id,
                Schedule = new JobSchedule
                {
                    Type = JobScheduleType.Interval,
                    Intervall = 60
                },
                Action = new JobAction
                {
                    Mode = JobActionMode.Import,
                    DestinationConnectorId = Connectors.First().Id,
                    Source = new SourceAction
                    {
                        Mode = SourceActionMode.Move,
                        NewConnectorId= Connectors.Skip(2).First().Id,
                        NewLocation = "Imported"
                    }
                }
            },
            new JobDefintion
            {
                Id = Guid.NewGuid(),
                Name = "OneDrive_Scan",
                Enabled= true,
                RunMode = JobRunMode.Harvest,
                SourceConnectorId = Connectors.Skip(3).First().Id,
                Schedule = new JobSchedule
                {
                    Type = JobScheduleType.Interval,
                    Intervall = 60
                },
                Action = new JobAction
                {
                    Mode = JobActionMode.Import,
                    DestinationConnectorId = Connectors.First().Id,
                    Source = new SourceAction
                    {
                        Mode = SourceActionMode.Delete,
                    }
                }
            },
            new JobDefintion
            {
                Id = Guid.NewGuid(),
                Name = "Outlook Listener",
                Enabled = true,
                RunMode = JobRunMode.Watch,
                SourceConnectorId = Connectors.First(x => x.Name == "Outlook Mail Philippe").Id,
                Action = new JobAction
                {
                    Mode = JobActionMode.Import,
                    DestinationConnectorId = Connectors.First().Id,
                    Source = new SourceAction
                    {
                        Mode = SourceActionMode.Delete,
                    }
                }
            }
        };

    public IEnumerable<ConnectorDefintion> Connectors =>
        new List<ConnectorDefintion>()
        {
            new ConnectorDefintion
            {
                Id = Guid.Parse("170761b0-2850-4cec-9eea-86f33d735284"),
                Name = "Internal DB",
                Type = "GridFS",
                Root = "default",
                Properties = new Dictionary<string, string>()
            },
            new ConnectorDefintion
            {
                Id = Guid.Parse("9c92f0e2-621b-4e0d-83ed-413f3dd1e1eb"),
                Name = "File Inbox",
                Type = "LFS",
                Root = @"C:\Temp\TagIt\0",
                Properties = new Dictionary<string, string>()
            },
            new ConnectorDefintion
            {
                Id = Guid.Parse("38e9deb9-088a-49f8-ad3e-9713da36b552"),
                Name = "File Others",
                Type = "LFS",
                Root = @"C:\Temp\TagIt\1",
                Properties = new Dictionary<string, string>()
            },
            new ConnectorDefintion
            {
                Id = Guid.Parse("b1342953-875e-42b6-9950-b4604429ee1b"),
                Name = "OneDrive Philippe",
                Type = "OneDrive",
                Root = "F7B0535AD9AB181E!1149045",
                CredentialId = Guid.Parse("23d87ac1-91d6-47d3-9cb7-abfde663eeeb"),
                Properties = new Dictionary<string, string>()
            },
            new ConnectorDefintion
            {
                Id = Guid.Parse("f0e62aed-9204-442e-aaf6-803d50e7f964"),
                Name = "OneDrive Storage",
                Type = "OneDrive",
                Root = "F7B0535AD9AB181E!1149048",
                CredentialId = Guid.Parse("23d87ac1-91d6-47d3-9cb7-abfde663eeeb"),
                Properties = new Dictionary<string, string>()
            },
            new ConnectorDefintion
            {
                Id = Guid.Parse("ddbe2c21-ed88-4949-a0c4-f05eb192e908"),
                Name = "Outlook Mail Philippe",
                Type = "OutlookMail",
                Root = "inbox",
                CredentialId = Guid.Parse("23d87ac1-91d6-47d3-9cb7-abfde663eeeb"),
                Properties = new Dictionary<string, string>()
            }
        };

    public IEnumerable<Receiver> Receivers =>
        new List<Receiver>()
        {
            new() { Id = Guid.NewGuid(), Name = "John"},
            new() { Id = Guid.NewGuid(), Name = "Annie"},
            new() { Id = Guid.NewGuid(), Name = "Yael"},
            new() { Id = Guid.NewGuid(), Name = "Jana"},
        };

    private EntityVersion NewVersion =>
        new EntityVersion
        {
            CreatedAt = DateTime.Now,
            CreatedBy = Guid.Parse("8d1c35e9-a4ed-4724-af0f-e96ad97a247b"),
            Version = 1
        };
}

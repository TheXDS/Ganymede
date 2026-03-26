using Moq;
using System.Text;
using System.Text.Json;

namespace TheXDS.Ganymede.Configuration;

internal class JsonConfigurationRepositoryTests
{
    private class TestConfig
    {
        public string Name { get; set; } = string.Empty;
        public int Value { get; set; }
    }

    private Mock<IConfigurationStore> storeMock;
    private IConfigurationRepository<TestConfig> repository;

    [SetUp]
    public void Setup()
    {
        storeMock = new();
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(@"{""Name"":""Test"",""Value"":1}"));
        storeMock.Setup(p => p.CanOpenStream()).Returns(true);
        storeMock.Setup(p => p.GetReadStream()).Returns(stream);
        repository = new JsonConfigurationRepository<TestConfig>(storeMock.Object);
    }

    [Test]
    public async Task Load_ShouldReturnDeserializedConfig_WhenStoreHasData()
    {
        var result = await repository.LoadAsync();

        Assert.That(result, Is.Not.Null);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result!.Name, Is.EqualTo("Test"));
            Assert.That(result.Value, Is.EqualTo(1));
        }
    }

    [TestCase(true, 1)]
    [TestCase(false, 0)]
    public async Task Load_ShouldCheckCanOpenStreamBeforeReading(bool canOpenStreamCase, int openReadStreamCalls)
    {
        storeMock.Setup(p => p.CanOpenStream()).Returns(canOpenStreamCase);

        _ = await repository.LoadAsync();

        storeMock.Verify(p => p.CanOpenStream(), Times.Once);
        storeMock.Verify(p => p.GetReadStream(), Times.Exactly(openReadStreamCalls));
    }

    [Test]
    public async Task Load_ShouldreturnDefault_WhenStoreCannotOpenStream()
    {
        storeMock.Setup(p => p.CanOpenStream()).Returns(false);
        var result = await repository.LoadAsync();
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task Save_ShouldSerializeConfigAndWriteToStore()
    {
        var config = new TestConfig { Name = "Test", Value = 1 };
        var writeStreamMock = new Mock<Stream>();
        storeMock.Setup(p => p.GetWriteStream()).Returns(writeStreamMock.Object);
        await repository.SaveAsync(config);
        storeMock.Verify(p => p.GetWriteStream(), Times.Once);
        writeStreamMock.Verify(s => s.WriteAsync(It.IsAny<ReadOnlyMemory<byte>>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Load_ShouldNotHandleExceptionsOpeningStreams()
    {
        storeMock.Setup(p => p.GetReadStream()).Throws(new IOException("Test exception"));
        await Assert.ThatAsync(() => repository.LoadAsync(), Throws.InstanceOf<IOException>());
    }

    [Test]
    public async Task Save_ShouldNotHandleExceptionsOpeningStreams()
    {
        storeMock.Setup(p => p.GetWriteStream()).Throws(new IOException("Test exception"));
        var config = new TestConfig { Name = "Test", Value = 1 };
        await Assert.ThatAsync(() => repository.SaveAsync(config), Throws.InstanceOf<IOException>());
    }

    [Test]
    public async Task Load_ShouldNotHandleExceptionsDeserializingInvalidJson()
    {
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(@"Invalid JSON"));
        storeMock.Setup(p => p.GetReadStream()).Returns(stream);
        await Assert.ThatAsync(() => repository.LoadAsync(), Throws.InstanceOf<JsonException>());
    }
}

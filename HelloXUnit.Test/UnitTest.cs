namespace HelloXUnit.Tests
{
    public class CalculatorTests
    {

        [Fact]
        public void AddTest()
        {
            Assert.Equal(4, Calculator.Add(2, 2));
        }

        [Fact]
        public void SubtractTest()
        {
            Assert.Equal(1, Calculator.Subtract(3, 2));
        }

        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        public void IsOddWhenTrue(int value)
        {
            Assert.True(Calculator.IsOdd(value));
        }

        [Theory]
        [InlineData(2)]
        [InlineData(4)]
        public void IsOddWhenFalse(int value)
        {
            Assert.False(Calculator.IsOdd(value));
        }

        public static IEnumerable<object[]> GetValues() =>
            new List<object[]>
            {
                new object[]{1, 2, 3},
                new object[]{-1, -2, -3},
            };

        [Theory]
        [MemberData(nameof(GetValues))]
        public void MemberDataTestByMethod(int x, int y, int result)
        {
            Assert.Equal(result, Calculator.Add(x, y));
        }

        public static IEnumerable<object[]> ValuesProperty { get; } =
            new List<object[]>
            {
                new object[]{1, 2, 3},
                new object[]{-1, -2, -3},
            };

        [Theory]
        [MemberData(nameof(ValuesProperty))]
        public void MemberDataTestByProperty(int x, int y, int result)
        {
            Assert.Equal(result, Calculator.Add(x, y));
        }

        public static readonly IEnumerable<object[]> ValuesField =
            new List<object[]>
            {
                new object[]{1, 2, 3},
                new object[]{-1, -2, -3},
            };

        [Theory]
        [MemberData(nameof(ValuesField))]
        public void MemberDataTestByField(int x, int y, int result)
        {
            Assert.Equal(result, Calculator.Add(x, y));
        }
    }

    public class FilesTests : IDisposable
    {
        private const string ExistFileName = "test.txt";
        private const string TextFileContent = "Hello, xUnit.net!";
        private const string NotExistFileName = "NotExistFile";

        // setup
        public FilesTests()
        {
            if (File.Exists(ExistFileName))
                File.Delete(ExistFileName);

            File.WriteAllText(ExistFileName, TextFileContent);
        }

        // teardown
        public void Dispose()
        {
            if (File.Exists(ExistFileName))
                File.Delete(ExistFileName);
        }

        [Fact]
        public void DeleteIfExistWhenExistFile()
        {
            Assert.True(Files.DeleteIfExist(ExistFileName));
            Assert.False(File.Exists(ExistFileName));
        }

        [Fact]
        public void DeleteIfExistWhenNotExistFile()
        {
            Assert.False(Files.DeleteIfExist("NotExistFile"));
        }
    }

    public class FilesTestsAsync
    {
        private const string ExistFileName = "test.txt";
        private const string TextFileContent = "Hello, xUnit.net!";
        private const string NotExistFileName = "NotExistFile";

        public FilesTestsAsync()
        {
            if (File.Exists(ExistFileName))
                File.Delete(ExistFileName);

            File.WriteAllText(ExistFileName, TextFileContent);
        }

        [Fact]
        public async Task ReadAllTextAsyncWhenExistFile()
        {
            Assert.Equal(TextFileContent, await Files.ReadAllTextAsync(ExistFileName));
        }

        [Fact]
        public async Task ReadAllTextAsyncWhenNotExistFile()
        {
            await Assert.ThrowsAsync<FileNotFoundException>(
                () => Files.ReadAllTextAsync(NotExistFileName));
        }
    }

    [Collection("Our Test Collection #1")]
    public class UnitTest1Collection
    {
        [Fact]
        public void Test1()
        {
            Thread.Sleep(3000);
        }
    }

    [Collection("Our Test Collection #1")]
    public class UnitTest2Collection
    {
        [Fact]
        public void Test2()
        {
            Thread.Sleep(5000);
        }
    }
}
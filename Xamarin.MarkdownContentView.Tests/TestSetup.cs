using NUnit.Framework;

namespace Xamarin.MarkdownContentView.Tests
{
    [SetUpFixture]
    public sealed class TestSetup
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            Forms.Mocks.MockForms.Init();
        }
    }
}
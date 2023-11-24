namespace Fonlow.Testing
{
	public static class TestConstants
	{
		public const string SyncMLData = "SyncMLData";
	}

	[CollectionDefinition(TestConstants.SyncMLData)]
	public class DeploymentItemCollection : ICollectionFixture<DeploymentItemFixture>
	{
		// This class has no code, and is never created. Its purpose is simply
		// to be the place to apply [CollectionDefinition] and all the
		// ICollectionFixture<> interfaces.
	}

}

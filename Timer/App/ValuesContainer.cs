namespace Timer.App
{
    public class ValuesContainer
    {
        public string SecretValue { get; }
        public string NotSecretValue { get; }

        public ValuesContainer(string secretValue, string notSecretValue)
        {
            SecretValue = secretValue;
            NotSecretValue = notSecretValue;
        }
    }
}
namespace MeetEric.Security
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

    public class IdentifierFactory : IIdentityFactory
    {
        public IdentifierFactory()
        {
            Hasher = MD5.Create();
        }

        private HashAlgorithm Hasher { get; }

        public IIdentifier Create()
        {
            return new CaseInsensitiveStringIdentity();
        }

        public IIdentifier Create<T>()
        {
            return new CaseInsensitiveStringIdentity();
        }

        public IIdentifier Parse(string id)
        {
            return new CaseInsensitiveStringIdentity(id);
        }

        public IIdentifier Generate(params string[] values)
        {
            return Generate((ICollection<string>)values);
        }

        public IIdentifier Generate(ICollection<string> values)
        {
            var line = string.Join("|", values.Where(x => !string.IsNullOrWhiteSpace(x)));
            var lowerCase = line.ToLowerInvariant();
            var bytes = Encoding.Default.GetBytes(lowerCase);
            bytes = Hasher.ComputeHash(bytes);
            return Parse(new Guid(bytes).ToString("D"));
        }
    }
}

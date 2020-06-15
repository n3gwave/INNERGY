namespace Innergy_app.Models.Entities
{
    using System;

    public class Product
    {
        public Product(string id, string name)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("invalid id", nameof(id));
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("invalid name", nameof(name));
            }

            this.Id = id;
            this.Name = name;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; }
    }
}
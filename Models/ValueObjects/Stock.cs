namespace Innergy_app.Models.ValueObjects
{
    using System;

    /// <summary>
    /// Stock value object.
    /// </summary>
    public class Stock
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Stock"/> class.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <exception cref="ArgumentException">Stock cannot be greater than 1000 - count</exception>
        public Stock(int count)
        {
            if (count > 1000)
            {
                throw new ArgumentException("Stock cannot be greater than 1000", nameof(count));
            }

            this.Count = count;
        }

        /// <summary>
        /// The count
        /// </summary>
        public int Count { get; }

        /// <summary>
        /// Equals the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        protected bool Equals(Stock other)
        {
            return this.Count == other.Count;
        }

        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        /// <see langword="true" /> if the specified object  is equal to the current object; otherwise, <see langword="false" />.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Stock) obj);
        }

        /// <summary>Serves as the default hash function.</summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return this.Count.GetHashCode();
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.Count.ToString();
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The LHS.</param>
        /// <param name="right">The RHS.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(Stock left, Stock right)
        {
            if (ReferenceEquals(left, null))
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (ReferenceEquals(right, null))
            {
                throw new ArgumentNullException(nameof(right));
            }

            return left.Count == right.Count;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(Stock left, Stock right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator <(Stock left, Stock right)
        {
            if (left == right)
            {
                return false;
            }

            return left.Count < right.Count;
        }

        /// <summary>
        /// Implements the operator &gt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator >(Stock left, Stock right)
        {
            if (left == right)
            {
                return false;
            }

            return left.Count > right.Count;
        }
    }
}

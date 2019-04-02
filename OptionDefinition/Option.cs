using System;
using System.Collections.Generic;

namespace Options
{
    /// <summary>
    /// Provides an Option type
    /// </summary>
    public abstract class Option : Option<object> { }

    /// <summary>
    /// Provides a generically typed Option
    /// </summary>
    public abstract class Option<T> : List<T>, IEquatable<T>
    {
        /// <summary>
        /// An Option factory that returns Some.
        /// </summary>
        /// <returns>A defined Option.</returns>
        /// <param name="value">The value to store in the option.</param>
        public static Option<T> Some(T value) => new SomeOption<T>(value);

        /// <summary>
        /// An Option factory that returns None.
        /// </summary>
        /// <value>An empty Option.</value>
        public static Option<T> None { get { return new NoneOption<T>(); } }

        /// <summary>
        /// Checks if an Option is empty.
        /// </summary>
        /// <value><c>true</c> if is empty; otherwise, <c>false</c>.</value>
        public bool IsEmpty { get { return base.Count == 0; } }

        /// <summary>
        /// Checks if an Option is defined.
        /// </summary>
        /// <value><c>true</c> if is defined; otherwise, <c>false</c>.</value>
        public bool IsDefined { get { return base.Count > 0; } }

        /// <summary>
        /// Gets the value of the Option.
        /// </summary>
        /// <value>The value of the Option.</value>
        public T Get { get { return IsDefined ? this[0] : throw new InvalidOperationException(); } }

        /// <summary>
        /// Gets the value of the Option or a default value.
        /// </summary>
        /// <returns>The value of the Option or a default value.</returns>
        /// <param name="value">The default value.</param>
        public T GetOrElse(T value) => IsDefined ? this[0] : value;

        public static Option<T> operator +(Option<T> o1, Option<T> o2) => o1.IsDefined ? o1 : o2;

        #region Equality
        public override bool Equals(object obj)
        {
            if ((this is NoneOption<T>) && (obj is NoneOption<T>)) return true;
            if ((this is SomeOption<T>) && (obj is SomeOption<T>)) return this[0].Equals((obj as SomeOption<T>)[0]);
            return false;
        }

        public bool Equals(T other) => Equals(other);

        public override int GetHashCode()
        {
            var valueHashCode = (IsDefined && this[0] != null) ? this[0].GetHashCode() : 0;
            return 48527 ^ (52813 * valueHashCode);
        }

        #endregion

        private class SomeOption<T1> : Option<T1>
        {
            public SomeOption(T1 value) { Add(value); }

            public new void Clear() => throw new InvalidOperationException();

            public override string ToString() => base[0] == null ? "Some(null)" : $"Some({base[0]})";
        }

        private class NoneOption<T1> : Option<T1>
        {
            public new void Add(T1 item) => throw new InvalidOperationException();

            public new void AddRange(IEnumerable<T1> collection) => throw new InvalidOperationException();

            public new int this[int key] { set => throw new InvalidOperationException(); }

            public override string ToString() => "None";
        }
    }

}
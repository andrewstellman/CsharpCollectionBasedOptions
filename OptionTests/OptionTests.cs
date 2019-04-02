using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OptionTests
{
    using Options;
    using System.Collections.Generic;
    using System.Linq;

    [TestClass]
    public class OptionTests
    {
        /// <summary>
        /// Tests the Option classes and extensions.
        /// </summary>
        [TestMethod]
        public void TestOptions()
        {
            Assert.AreEqual("Some(xyz)", Option.Some("xyz").ToString());
            Assert.AreEqual("None", Option.None.ToString());
            Assert.AreEqual("Some(null)", Option.Some(null).ToString());
            Assert.AreEqual(Option.Some(1), Option.Some(1) + Option.Some(2));
            Assert.AreEqual(Option.Some("xyz"), Option.None + Option.Some("xyz"));

            // contravariance causes Option<int> + Option<string> to call the + operator for Option<object>
            Assert.AreEqual(Option.Some(1), Option.Some(1) + Option.Some("a"));
            Assert.AreEqual(Option.Some("a"), Option.Some("a") + Option.Some(1));

            var l = new List<int>() { 1, 2, 3, 4, 5 };
            Assert.AreEqual(Option<int>.Some(1), l.FirstOption());
            Assert.AreEqual(Option<int>.Some(5), l.LastOption());
            Assert.AreEqual(Option<int>.Some(4), l.FindOption(e => e > 3));
            Assert.AreEqual(Option<int>.None, l.FindOption(e => e > 6));
            Assert.AreEqual(Option<int>.None, new List<int>().FirstOption());
            Assert.AreEqual(Option<int>.None, new List<int>().LastOption());

            var d = new Dictionary<int, string>() { { 1, "a" }, { 2, "b" }, { 3, "c" } };
            Assert.AreEqual(Option<string>.Some("b"), d.GetOrElse(2));
            Assert.AreEqual(Option<string>.None, d.GetOrElse(4));

            Assert.AreEqual("a", (d.GetOrElse(1) + d.GetOrElse(2)).GetOrElse("z"));
            Assert.AreEqual("c", (d.GetOrElse(3) + d.GetOrElse(4)).GetOrElse("z"));
            Assert.AreEqual("z", (d.GetOrElse(5) + d.GetOrElse(6)).GetOrElse("z"));
        }

        /// <summary>
        /// Tests the generically typed Option&lt;T&gt; and extensions.
        /// </summary>
        [TestMethod]
        public void TestGenericOptions()
        {
            Assert.AreEqual("Some(xyz)", Option<string>.Some("xyz").ToString());
            Assert.AreEqual("None", Option<string>.None.ToString());
            Assert.AreEqual("Some(null)", Option<string>.Some(null).ToString());
            Assert.AreEqual(Option<int>.Some(1), Option<int>.Some(1) + Option<int>.Some(2));
            Assert.AreEqual(Option<string>.Some("xyz"), Option<string>.None + Option<string>.Some("xyz"));

            var l = new List<int>() { 1, 2, 3, 4, 5 };
            Assert.AreEqual(Option<int>.Some(1), l.FirstOption());
            Assert.AreEqual(Option<int>.Some(5), l.LastOption());
            Assert.AreEqual(Option<int>.Some(4), l.FindOption<int>(e => e > 3));
            Assert.AreEqual(Option<int>.None, l.FindOption<int>(e => e > 6));
            Assert.AreEqual(Option<int>.None, new List<int>().FirstOption());
            Assert.AreEqual(Option<int>.None, new List<int>().LastOption());

            var d = new Dictionary<int, string>() { { 1, "a" }, { 2, "b" }, { 3, "c" } };
            Assert.AreEqual(Option<string>.Some("b"), d.GetOrElse(2));
            Assert.AreEqual(Option<string>.None, d.GetOrElse(4));
        }
    }
}

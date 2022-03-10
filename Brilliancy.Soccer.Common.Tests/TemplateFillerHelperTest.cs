using Brilliancy.Soccer.Common.Helpers;
using NUnit.Framework;

namespace Brilliancy.Soccer.Common.Tests
{
    public class TemplateFillerHelperTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TemplateFillerHelper_Simple()
        {
            var res = TemplateFillerHelper.FillTemplate("Hello @Model.Name!", new { Name = "Johny Kowalsky" });
            Assert.AreEqual("Hello Johny Kowalsky!", res);
        }

        [Test]
        public void TemplateFillerHelper_WrongProperty()
        {
            var res = TemplateFillerHelper.FillTemplate("Hello @Model.Name!", new { FullName = "Johny Kowalsky" });
            Assert.AreEqual("Hello -!", res);
        }

        [Test]
        public void TemplateFillerHelper_3Methods()
        {
            var res = TemplateFillerHelper.FillTemplate("Hello @Model.Name! Where is @Model.Friend? Is @Model.HeOrShe drinking again?",
                new { 
                    Name = "Johny Kowalsky",
                    Friend = "Martin",
                    HeOrShe = "he"
                });
            Assert.AreEqual("Hello Johny Kowalsky! Where is Martin? Is he drinking again?", res);
        }
    }
}
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XNASystem;

namespace XNASystemTest
{
    /// <summary>
    /// Summary description for DicTest
    /// </summary>
    [TestClass]
    public class DicTest
    {
        private Dictionary<String, String> _targetGenericDict;
        private SystemMain _target;

        public DicTest()
        {

            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void CreateDictionary()
        {
            _targetGenericDict = new Dictionary<String, String>();
            Assert.IsNotNull(_targetGenericDict);
        }

        [TestMethod]
        public void AddValuesToDictionary()
        {
            _targetGenericDict = new Dictionary<String, String>();
            _targetGenericDict.Add("CA", "California");
            _targetGenericDict.Add("FL", "Florida");
            Assert.AreEqual("California", _targetGenericDict["CA"]);
            Assert.AreEqual("Florida", _targetGenericDict["FL"]);
        }

        [TestMethod]
        public void CreateFontPackage()
        {
            _target = new SystemMain();
            Assert.IsNotNull(_target);
            Assert.IsNotNull(SystemMain.FontPackage);
        }

        [TestMethod]
        public void CreateTexturePackage()
        {
            _target = new SystemMain();
            Assert.IsNotNull(_target);
            Assert.IsNotNull(SystemMain.TexturePackage);
        }

        [TestMethod]
        public void AddValuesToDictionaryActualDict()
        {
            _target = new SystemMain();
            Assert.AreEqual(true, SystemMain.FontPackage.ContainsKey("Main"));
            Assert.AreEqual(true, SystemMain.TexturePackage.ContainsKey("Boss"));
        }

    }
}

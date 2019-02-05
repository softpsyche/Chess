﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.3.2.0
//      SpecFlow Generator Version:2.3.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Arcesoft.Chess.Tests
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.3.2.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute()]
    public partial class ArtificialIntelligenceFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private Microsoft.VisualStudio.TestTools.UnitTesting.TestContext _testContext;
        
#line 1 "ArtificialIntelligence.feature"
#line hidden
        
        public virtual Microsoft.VisualStudio.TestTools.UnitTesting.TestContext TestContext
        {
            get
            {
                return this._testContext;
            }
            set
            {
                this._testContext = value;
            }
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassInitializeAttribute()]
        public static void FeatureSetup(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContext)
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner(null, 0);
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "ArtificialIntelligence", "\tVerify Artificial intelligence ", ProgrammingLanguage.CSharp, new string[] {
                        "Unit"});
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassCleanupAttribute()]
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestInitializeAttribute()]
        public virtual void TestInitialize()
        {
            if (((testRunner.FeatureContext != null) 
                        && (testRunner.FeatureContext.FeatureInfo.Title != "ArtificialIntelligence")))
            {
                global::Arcesoft.Chess.Tests.ArtificialIntelligenceFeature.FeatureSetup(null);
            }
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCleanupAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Microsoft.VisualStudio.TestTools.UnitTesting.TestContext>(TestContext);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 16
#line 17
 testRunner.Given("I have a container", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 18
 testRunner.Given("I have a game factory", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 19
 testRunner.Given("I have an artificial intelligence", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Artificial intelligence should find mate in one for white")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "ArtificialIntelligence")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Unit")]
        public virtual void ArtificialIntelligenceShouldFindMateInOneForWhite()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Artificial intelligence should find mate in one for white", ((string[])(null)));
#line 22
this.ScenarioSetup(scenarioInfo);
#line 16
this.FeatureBackground();
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "A",
                        "B",
                        "C",
                        "D",
                        "E",
                        "F",
                        "G",
                        "H"});
            table1.AddRow(new string[] {
                        "BK",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        ""});
            table1.AddRow(new string[] {
                        "BP",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        ""});
            table1.AddRow(new string[] {
                        "WP",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        ""});
            table1.AddRow(new string[] {
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        ""});
            table1.AddRow(new string[] {
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        ""});
            table1.AddRow(new string[] {
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        ""});
            table1.AddRow(new string[] {
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        ""});
            table1.AddRow(new string[] {
                        "WK",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "WR"});
#line 23
 testRunner.Given("I start a new game in the following state", ((string)(null)), table1, "Given ");
#line 33
 testRunner.When("I have the AI calculate the best move to a depth of \'1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Source",
                        "Destination"});
            table2.AddRow(new string[] {
                        "H1",
                        "H8"});
#line 34
 testRunner.Then("I expect the best move found to be", ((string)(null)), table2, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Artificial intelligence should find mate in three for white (rook mate)")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "ArtificialIntelligence")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Unit")]
        public virtual void ArtificialIntelligenceShouldFindMateInThreeForWhiteRookMate()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Artificial intelligence should find mate in three for white (rook mate)", ((string[])(null)));
#line 38
this.ScenarioSetup(scenarioInfo);
#line 16
this.FeatureBackground();
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "A",
                        "B",
                        "C",
                        "D",
                        "E",
                        "F",
                        "G",
                        "H"});
            table3.AddRow(new string[] {
                        "BK",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        ""});
            table3.AddRow(new string[] {
                        "BP",
                        "",
                        "BP",
                        "",
                        "BP",
                        "",
                        "BP",
                        ""});
            table3.AddRow(new string[] {
                        "WP",
                        "",
                        "WP",
                        "",
                        "WP",
                        "",
                        "WP",
                        ""});
            table3.AddRow(new string[] {
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        ""});
            table3.AddRow(new string[] {
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        ""});
            table3.AddRow(new string[] {
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        ""});
            table3.AddRow(new string[] {
                        "",
                        "WP",
                        "",
                        "WP",
                        "",
                        "WP",
                        "",
                        ""});
            table3.AddRow(new string[] {
                        "WK",
                        "WR",
                        "",
                        "",
                        "",
                        "",
                        "",
                        ""});
#line 39
 testRunner.Given("I start a new game in the following state", ((string)(null)), table3, "Given ");
#line 49
 testRunner.When("I have the AI calculate the best move to a depth of \'3\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "Source",
                        "Destination"});
            table4.AddRow(new string[] {
                        "B1",
                        "H1"});
#line 50
 testRunner.Then("I expect the best move found to be", ((string)(null)), table4, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Artificial intelligence should find mate in three for white (knight mate)")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "ArtificialIntelligence")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Unit")]
        public virtual void ArtificialIntelligenceShouldFindMateInThreeForWhiteKnightMate()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Artificial intelligence should find mate in three for white (knight mate)", ((string[])(null)));
#line 54
this.ScenarioSetup(scenarioInfo);
#line 16
this.FeatureBackground();
#line hidden
            TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                        "A",
                        "B",
                        "C",
                        "D",
                        "E",
                        "F",
                        "G",
                        "H"});
            table5.AddRow(new string[] {
                        "",
                        "",
                        "",
                        "",
                        "WK",
                        "",
                        "",
                        "BK"});
            table5.AddRow(new string[] {
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "BP"});
            table5.AddRow(new string[] {
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "WP"});
            table5.AddRow(new string[] {
                        "",
                        "",
                        "",
                        "",
                        "WN",
                        "",
                        "",
                        ""});
            table5.AddRow(new string[] {
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "WN",
                        ""});
            table5.AddRow(new string[] {
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        ""});
            table5.AddRow(new string[] {
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        ""});
            table5.AddRow(new string[] {
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        ""});
#line 55
 testRunner.Given("I start a new game in the following state", ((string)(null)), table5, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                        "Source",
                        "Destination"});
            table6.AddRow(new string[] {
                        "E1",
                        "D1"});
            table6.AddRow(new string[] {
                        "E8",
                        "D8"});
#line 65
 testRunner.Given("I have the following move history", ((string)(null)), table6, "Given ");
#line 69
 testRunner.When("I have the AI calculate the best move to a depth of \'3\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                        "Source",
                        "Destination"});
            table7.AddRow(new string[] {
                        "E5",
                        "F7"});
#line 70
 testRunner.Then("I expect the best move found to be", ((string)(null)), table7, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Artificial intelligence should find mate in Five moves for black")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "ArtificialIntelligence")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Unit")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.IgnoreAttribute()]
        public virtual void ArtificialIntelligenceShouldFindMateInFiveMovesForBlack()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Artificial intelligence should find mate in Five moves for black", new string[] {
                        "ignore"});
#line 81
this.ScenarioSetup(scenarioInfo);
#line 16
this.FeatureBackground();
#line hidden
            TechTalk.SpecFlow.Table table8 = new TechTalk.SpecFlow.Table(new string[] {
                        "A",
                        "B",
                        "C",
                        "D",
                        "E",
                        "F",
                        "G",
                        "H"});
            table8.AddRow(new string[] {
                        "BR",
                        "",
                        "",
                        "WB",
                        "BK",
                        "",
                        "",
                        ""});
            table8.AddRow(new string[] {
                        "BP",
                        "BP",
                        "BP",
                        "",
                        "",
                        "BP",
                        "BP",
                        ""});
            table8.AddRow(new string[] {
                        "",
                        "",
                        "BN",
                        "BP",
                        "",
                        "",
                        "",
                        ""});
            table8.AddRow(new string[] {
                        "",
                        "",
                        "",
                        "",
                        "BP",
                        "",
                        "",
                        ""});
            table8.AddRow(new string[] {
                        "",
                        "",
                        "WB",
                        "",
                        "WP",
                        "",
                        "BN",
                        ""});
            table8.AddRow(new string[] {
                        "",
                        "",
                        "",
                        "WP",
                        "",
                        "",
                        "",
                        ""});
            table8.AddRow(new string[] {
                        "WP",
                        "WP",
                        "WP",
                        "WN",
                        "WK",
                        "BP",
                        "WP",
                        ""});
            table8.AddRow(new string[] {
                        "WR",
                        "WN",
                        "",
                        "BR",
                        "",
                        "",
                        "",
                        ""});
#line 82
 testRunner.Given("I start a new game in the following state", ((string)(null)), table8, "Given ");
#line 92
 testRunner.Given("Its blacks turn", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 93
 testRunner.When("I have the AI calculate the best move to a depth of \'4\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table9 = new TechTalk.SpecFlow.Table(new string[] {
                        "Source",
                        "Destination"});
            table9.AddRow(new string[] {
                        "",
                        ""});
#line 94
 testRunner.Then("I expect the best move found to be", ((string)(null)), table9, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion

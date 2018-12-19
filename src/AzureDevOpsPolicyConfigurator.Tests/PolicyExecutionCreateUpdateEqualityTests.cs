﻿using Xunit;

namespace AzureDevOpsPolicyConfigurator.Tests
{
    /// <summary>
    /// Policy execution equality tests.
    /// </summary>
    public class PolicyExecutionCreateUpdateEqualityTests
    {
        [Fact(DisplayName = "Check create", Skip = SkippingInformation.SkippingReason)]
        private void CheckCreate()
        {
            var result = new PolicyTester().RunTest(new TestData(@"
            {
              ""allowDeletion"":  false,
              ""ignoreTypes"": [],
              ""applyTo"": {
                ""projects"": [ ""##Project##"" ],
                ""repositories"": [""##Repository##""]
              },
              ""policies"": [
                {
                  ""type"": ""Status"",

                  ""SubTypePropertyName"": ""statusName"",
                  ""SubTypePropertyValue"": ""create-new"",

                  ""project"": """",
                  ""branch"": """",
                  ""repository"": """",

                  ""isBlocking"": true,

                  ""settings"": {
                    ""filenamePatterns"": []
                  }
                }
              ]
            }"));

            Assert.DoesNotContain(result[LogLevel.Info], x => x.Contains("update"));
            Assert.Contains(result[LogLevel.Info], x => x.Contains("create"));
            Assert.DoesNotContain(result[LogLevel.Info], x => x.StartsWith("Policy is up to date"));
        }

        [Fact(DisplayName = "Check update", Skip = SkippingInformation.SkippingReason)]
        private void CheckUpdate()
        {
            var result = new PolicyTester().RunTest(new TestData(@"
            {
              ""allowDeletion"":  false,
              ""ignoreTypes"": [],
              ""applyTo"": {
                ""projects"": [ ""##Project##"" ],
                ""repositories"": [""##Repository##""]
              },
              ""policies"": [
                {
                  ""type"": ""Status"",

                  ""SubTypePropertyName"": ""statusName"",
                  ""SubTypePropertyValue"": ""pr-title"",

                  ""project"": """",
                  ""branch"": """",
                  ""repository"": """",

                  ""isBlocking"": true,

                  ""settings"": {
                    ""filenamePatterns"": []
                  }
                }
              ]
            }"));

            Assert.DoesNotContain(result[LogLevel.Info], x => x.Contains("create"));
            Assert.Contains(result[LogLevel.Info], x => x.Contains("update"));
            Assert.DoesNotContain(result[LogLevel.Info], x => x.StartsWith("Policy is up to date"));
        }

        [Fact(DisplayName = "Check equals", Skip = SkippingInformation.SkippingReason)]
        private void CheckIsEqual()
        {
            var result = new PolicyTester().RunTest(new TestData(@"
            {
              ""allowDeletion"":  false,
              ""ignoreTypes"": [],
              ""applyTo"": {
                ""projects"": [ ""##Project##"" ],
                ""repositories"": [""##Repository##""]
              },
              ""policies"": [
                {
                  ""type"": ""Comment requirements"",

                  ""project"": """",
                  ""branch"": """",
                  ""repository"": """",

                  ""isBlocking"": true,

                  ""settings"": {
                    ""filenamePatterns"": []
                  }
                }
              ]
            }"));

            Assert.DoesNotContain(result[LogLevel.Info], x => x.Contains("create"));
            Assert.DoesNotContain(result[LogLevel.Info], x => x.Contains("update"));
            Assert.Contains(result[LogLevel.Info], x => x.StartsWith("Policy is up to date"));
        }

        [Fact(DisplayName = "Check is not equal with different IsBlocking", Skip = SkippingInformation.SkippingReason)]
        private void CheckIsNotEqualWithDifferentIsBlocking()
        {
            var result = new PolicyTester().RunTest(new TestData(@"
            {
              ""allowDeletion"":  false,
              ""ignoreTypes"": [],
              ""applyTo"": {
                ""projects"": [ ""##Project##"" ],
                ""repositories"": [""##Repository##""]
              },
              ""policies"": [
                {
                  ""type"": ""Comment requirements"",

                  ""project"": """",
                  ""branch"": """",
                  ""repository"": """",

                  ""isBlocking"": false,

                  ""settings"": {
                    ""filenamePatterns"": []
                  }
                }
              ]
            }"));

            Assert.DoesNotContain(result[LogLevel.Info], x => x.Contains("create"));
            Assert.Contains(result[LogLevel.Info], x => x.Contains("update"));
            Assert.DoesNotContain(result[LogLevel.Info], x => x.StartsWith("Policy is up to date"));
        }

        [Fact(DisplayName = "Check equals and ignore scope", Skip = SkippingInformation.SkippingReason)]
        private void CheckIsEqualAndIgnoreScope()
        {
            var result = new PolicyTester().RunTest(new TestData(@"
            {
              ""allowDeletion"":  false,
              ""ignoreTypes"": [],
              ""applyTo"": {
                ""projects"": [ ""##Project##"" ],
                ""repositories"": [""##Repository##""]
              },
              ""policies"": [
                {
                  ""type"": ""Comment requirements"",

                  ""project"": """",
                  ""branch"": """",
                  ""repository"": """",

                  ""isBlocking"": true,

                  ""settings"": {
                    ""filenamePatterns"": [],
                    ""scope"": [
                        {
                            ""refName"": ""wrong"",
                            ""matchKind"": ""NotExact"",
                            ""repositoryId"": ""notguid""
                        }
                    ]
                  }
                }
              ]
            }"));

            Assert.DoesNotContain(result[LogLevel.Info], x => x.Contains("create"));
            Assert.DoesNotContain(result[LogLevel.Info], x => x.Contains("update"));
            Assert.DoesNotContain(result[LogLevel.Info], x => x.Contains("wrong"));
            Assert.DoesNotContain(result[LogLevel.Info], x => x.Contains("NotExact"));
            Assert.DoesNotContain(result[LogLevel.Info], x => x.Contains("notguid"));
            Assert.Contains(result[LogLevel.Info], x => x.StartsWith("Policy is up to date"));
        }

        [Fact(DisplayName = "Check equals and ignore scope and sub type", Skip = SkippingInformation.SkippingReason)]
        private void CheckIsEqualAndIgnoreScopeAndSubType()
        {
            var result = new PolicyTester().RunTest(new TestData(@"
            {
              ""allowDeletion"":  false,
              ""ignoreTypes"": [],
              ""applyTo"": {
                ""projects"": [ ""##Project##"" ],
                ""repositories"": [""##Repository##""]
              },
              ""policies"": [
                {
                  ""type"": ""Status"",

                  ""SubTypePropertyName"": ""statusName"",
                  ""SubTypePropertyValue"": ""Framework_docframework_CI_RunBuildChain"",


                  ""project"": """",
                  ""branch"": """",
                  ""repository"": """",

                  ""isBlocking"": true,

                  ""settings"": {
                    ""filenamePatterns"": [],
                    ""statusGenre"": ""TeamCity"",
                    ""authorId"": ""ecba3d80-fec6-4826-9f11-f4dc6cbd4d17"",
                    ""invalidateOnSourceUpdate"": true,
                    ""statusName"": ""not-Framework_docframework_CI_RunBuildChain"",
                    ""scope"": [
                        {
                            ""refName"": ""wrong"",
                            ""matchKind"": ""NotExact"",
                            ""repositoryId"": ""notguid""
                        }
                    ]
                  }
                }
              ]
            }"));

            Assert.DoesNotContain(result[LogLevel.Info], x => x.Contains("create"));
            Assert.DoesNotContain(result[LogLevel.Info], x => x.Contains("update"));
            Assert.DoesNotContain(result[LogLevel.Info], x => x.Contains("wrong"));
            Assert.DoesNotContain(result[LogLevel.Info], x => x.Contains("NotExact"));
            Assert.DoesNotContain(result[LogLevel.Info], x => x.Contains("notguid"));
            Assert.DoesNotContain(result[LogLevel.Info], x => x.Contains("not-Framework_docframework_CI_RunBuildChain"));
            Assert.Contains(result[LogLevel.Info], x => x.StartsWith("Policy is up to date"));
        }
    }
}
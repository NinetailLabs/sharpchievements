﻿using Moq;
using NUnit.Framework;
using sebingel.sharpchievements;

namespace sharpchievements.UnitTest
{
    [TestFixture]
    public class AchievementManagerTests
    {
        [Test]
        public void RegisterAchievementCondition_GivenANewAchievementCondition_AddsTheAchievementCondition()
        {
            // Arrange
            AchievementManager am = AchievementManager.GetInstance();

            string key = "key";
            Mock<IAchievementCondition> iAchievmentConditionMock = new Mock<IAchievementCondition>();
            iAchievmentConditionMock.SetupGet(x => x.UniqueId).Returns("uniqueId");
            iAchievmentConditionMock.SetupGet(x => x.AchievementConditionKey).Returns(key);

            // Act
            am.RegisterAchievementCondition(iAchievmentConditionMock.Object);
            am.ReportProgress(key);

            // Assert
            iAchievmentConditionMock.Verify(x => x.MakeProgress(), Times.Once);
        }
    }
}
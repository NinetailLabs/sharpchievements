//Copyright 2015 Sebastian Bingel

//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at

//    http://www.apache.org/licenses/LICENSE-2.0

//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

namespace sebingel.sharpchievements
{
    /// <summary>
    ///     Condition that describes the requirements that must be met to unlock an achievement and can track the
    ///     iProgressCount.
    /// </summary>
    public interface IAchievementCondition
    {
        /// <summary>
        ///     Applicationwide Unique uniqueId of the AchievementCondition
        /// </summary>
        string UniqueId { get; set; }

        /// <summary>
        ///     Key of this AchivementCondition. Is used to identify one ore more AchievementConditions by the AchievementManager.
        /// </summary>
        string AchievementConditionKey { get; }

        /// <summary>
        ///     Gets the Unlocked status of an AchievementCondition.
        /// </summary>
        bool Unlocked { get; }

        /// <summary>
        ///     The current progress of the AchievementCondition in percent
        /// </summary>
        int Progress { get; }

        /// <summary>
        ///     The current absoute progress of the AchievementCondition
        /// </summary>
        int ProgressCount { get; }

        /// <summary>
        ///     Gets the number of progresses that needs to be made to complete this AchievementCondition
        /// </summary>
        int CountToUnlock { get; }

        /// <summary>
        ///     Event that fires when the iProgressCount of an AchievementCondition is changed.
        /// </summary>
        event AchievementConditionProgressChangedHandler ProgressChanged;

        /// <summary>
        ///     Event that fires when an AchievementCondition is completed.
        /// </summary>
        event AchievementConditionCompletedHandler ConditionCompleted;

        /// <summary>
        ///     Adds one iProgressCount step for this AchievementCondition
        /// </summary>
        void MakeProgress();
    }
}
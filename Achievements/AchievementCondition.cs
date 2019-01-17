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

using System;

namespace sebingel.sharpchievements
{
    /// <inheritdoc />
    [Serializable]
    public class AchievementCondition : IAchievementCondition
    {
        #region - Konstruktoren -

        /// <inheritdoc />
        public AchievementCondition(string uniqueId, string achievementConditionKey, int countToUnlock)
        {
            this.UniqueId = uniqueId;
            this.AchievementConditionKey = achievementConditionKey;
            this.CountToUnlock = countToUnlock;
            this.Unlocked = false;
        }

        #endregion

        #region - Methoden privat -

        private void InvokeProgressChanged(int iProgressCount)
        {
            this.ProgressChanged?.Invoke(this, new AchievementConditionProgressChangedArgs(iProgressCount));
        }

        private void InvokeConditionCompleted()
        {
            this.Unlocked = true;
            this.ConditionCompleted?.Invoke(this);
        }

        #endregion

        #region - Properties oeffentlich -

        /// <inheritdoc />
        public string UniqueId { get; set; }

        /// <inheritdoc />
        public string AchievementConditionKey { get; }

        /// <inheritdoc />
        public bool Unlocked { get; private set; }

        /// <inheritdoc />
        public int Progress => 100 / this.CountToUnlock * this.ProgressCount;

        /// <inheritdoc />
        public int ProgressCount { get; private set; }

        /// <inheritdoc />
        public int CountToUnlock { get; }

        #endregion

        #region AchievementCondition Members

        /// <inheritdoc />
        public event AchievementConditionProgressChangedHandler ProgressChanged;

        /// <inheritdoc />
        public event AchievementConditionCompletedHandler ConditionCompleted;

        /// <inheritdoc />
        public void MakeProgress()
        {
            this.ProgressCount++;
            this.InvokeProgressChanged(this.ProgressCount);

            if (this.ProgressCount >= this.CountToUnlock)
            {
                this.InvokeConditionCompleted();
            }
        }

        #endregion
    }
}
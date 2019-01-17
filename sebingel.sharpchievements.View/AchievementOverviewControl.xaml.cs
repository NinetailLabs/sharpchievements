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

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace sebingel.sharpchievements.View
{
    /// <summary>
    /// A basic overview of a set of Achievements
    /// </summary>
    public partial class AchievementOverviewControl : INotifyPropertyChanged
    {
        private AchievementManager _achievementList;
        private Visibility unlockedAchievementsVisibility;
        private Visibility separatorVisibility;
        private Visibility lockedAchievementsVisibility;
        private ObservableCollection<Achievement> achievementList;

        /// <summary>
        /// A list containing Achievements that are displayed
        /// </summary>
        public ObservableCollection<Achievement> AchievementList
        {
            get
            {
                return this.achievementList;
            }
        }

        /// <summary>
        /// State of the visibility of unlocked Achievements
        /// </summary>
        public Visibility UnlockedAchievementsVisibility
        {
            get
            {
                return this.unlockedAchievementsVisibility;
            }
            set
            {
                this.unlockedAchievementsVisibility = value;
                this.InvokePropertyChanged();
            }
        }

        /// <summary>
        /// State of the visibility of the Separator between locken and unlocked Achievements
        /// </summary>
        public Visibility SeparatorVisibility
        {
            get
            {
                return this.separatorVisibility;
            }
            set
            {
                this.separatorVisibility = value;
                this.InvokePropertyChanged();
            }
        }

        /// <summary>
        /// State of the visibility of locked Achievements
        /// </summary>
        public Visibility LockedAchievementsVisibility
        {
            get
            {
                return this.lockedAchievementsVisibility;
            }
            set
            {
                this.lockedAchievementsVisibility = value;
                this.InvokePropertyChanged();
            }
        }

        /// <summary>
        /// This construcor purely exists because a Usercontrol needs a parameterless constructor
        /// </summary>
        public AchievementOverviewControl()
        {
            this.InitializeComponent();
            this._achievementList = AchievementManager.GetInstance();

            // initialize the achievementList
            this.achievementList = new ObservableCollection<Achievement>();

            // If the Control is invoked without a list of Achivements to display it will register
            // to AchievementManager.Instance.AchievementsChanged and will load all registered Achievements
            _achievementList.AchievementRegistered += AchievementRegisteredHandler;

            // Get all already registered Achievements and add them to the list
            var achievements = _achievementList.RegisteredAchievements;


            // Add all Achievements to our List
            foreach (Achievement achievement in achievements)
                this.AchievementList.Add(achievement);
        }

        /// <summary>
        /// Call to initialize the view with an AchievementManager instance
        /// </summary>
        /// <param name="achievements">A set of Achievements to be desplyed</param>
        public void Init(AchievementManager achievementList)
        {
            
        }

        /// <summary>
        /// When the AchievementManager class fires the AchievementRegistered event the newly registered Achievement will be added to our AchievementList
        /// </summary>
        private void AchievementRegisteredHandler(Achievement a)
        {
            this.AchievementList.Add(a);
        }

        /// <summary>
        /// Happens when an Achievement is changed in the AchievementManager
        /// </summary>
        public void Refresh()
        {
            this.IcUnlocked.Items.Clear();
            foreach (Achievement achievement in this.AchievementList.Where(x => x.Unlocked))
                this.IcUnlocked.Items.Add(new AchievementControl(achievement) { Margin = new Thickness(5) });

            this.IcLocked.Items.Clear();
            foreach (Achievement achievement in this.AchievementList.Where(x => !x.Unlocked))
            {
                if (!achievement.Hidden)
                    this.IcLocked.Items.Add(new AchievementControl(achievement) { Margin = new Thickness(5) });
            }

            if (this.AchievementList.Any(x => x.Unlocked))
                this.UnlockedAchievementsVisibility = Visibility.Visible;
            else
                this.UnlockedAchievementsVisibility = Visibility.Collapsed;

            if (this.AchievementList.Any(x => !x.Unlocked && !x.Hidden))
                this.LockedAchievementsVisibility = Visibility.Visible;
            else
                this.LockedAchievementsVisibility = Visibility.Collapsed;

            if (this.AchievementList.Any(x => x.Unlocked) && this.AchievementList.Any(x => !x.Unlocked && !x.Hidden))
                this.SeparatorVisibility = Visibility.Visible;
            else
                this.SeparatorVisibility = Visibility.Collapsed;
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        
        private void InvokePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}

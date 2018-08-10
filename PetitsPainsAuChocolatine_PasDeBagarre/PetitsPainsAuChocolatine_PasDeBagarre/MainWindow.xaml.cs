using PetitsPainsAuChocolatine_PasDeBagarre.Entity;
using PetitsPainsAuChocolatine_PasDeBagarre.Resources;
using PetitsPainsAuChocolatine_PasDeBagarre.Seeder;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace PetitsPainsAuChocolatine_PasDeBagarre
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate Point GetPosition(IInputElement element);

        public UserCollection Users { get; set; }
        public UsersSeeder UsersSeeder { get; set; }
        public int RowIndex { get; set; } = -1;

        public MainWindow()
        {
            InitializeComponent();

            // Ensure the current culture passed into bindings
            // is the OS culture. By default, WPF uses en-US 
            // as the culture, regardless of the system settings.
            // Source : https://stackoverflow.com/questions/520115/stringformat-localization-issues-in-wpf/520334#520334
            FrameworkElement.LanguageProperty.OverrideMetadata(
              typeof(FrameworkElement),
              new FrameworkPropertyMetadata(
                  XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

            UsersSeeder = new UsersSeeder();

            Users = new UserCollection(UsersSeeder.GetUsers());

            DailyUpdateUsersDelivery();

            people.ItemsSource = Users.OrderBy(user => user.FirstName).ToList();

            InitEventHandlers();
        }

        private void InitEventHandlers()
        {
            CurrentPeopleList.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(CurrentPeopleList_PreviewMouseLeftButtonDown);
            CurrentPeopleList.Drop += new DragEventHandler(CurrentPeopleList_Drop);
        }

        #region Drag & Drop DataGrid
        private void CurrentPeopleList_Drop(object sender, DragEventArgs e)
        {
            if (RowIndex < 0)
                return;
            int index = this.GetCurrentRowIndex(e.GetPosition);
            if (index < 0)
                return;
            if (index == RowIndex)
                return;
            if (index == CurrentPeopleList.Items.Count - 1)
            {
                MessageBox.Show("This row-index cannot be drop");
                return;
            }
            UserCollection userCollection = Resources["UserList"] as UserCollection;
            User changedUser = userCollection[RowIndex];
            userCollection.RemoveAt(RowIndex);
            userCollection.Insert(index, changedUser);
        }

        private void CurrentPeopleList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RowIndex = GetCurrentRowIndex(e.GetPosition);

            if (RowIndex < 0)
            {
                return;
            }
            
            CurrentPeopleList.SelectedIndex = RowIndex;
            User selectedEmp = CurrentPeopleList.Items[RowIndex] as User;

            if (selectedEmp == null)
            {
                return;
            }

            DragDropEffects dragdropeffects = DragDropEffects.Move;
            if (DragDrop.DoDragDrop(CurrentPeopleList, selectedEmp, dragdropeffects)
                                != DragDropEffects.None)
            {
                CurrentPeopleList.SelectedItem = selectedEmp;
            }
        }

        private int GetCurrentRowIndex(GetPosition pos)
        {
            int curIndex = -1;
            for (int i = 0; i < CurrentPeopleList.Items.Count; i++)
            {
                DataGridRow itm = GetRowItem(i);

                if (GetMouseTargetRow(itm, pos))
                {
                    curIndex = i;
                    break;
                }
            }
            return curIndex;
        }

        private bool GetMouseTargetRow(Visual theTarget, GetPosition position)
        {
            Rect rect = VisualTreeHelper.GetDescendantBounds(theTarget);
            Point point = position((IInputElement)theTarget);

            return rect.Contains(point);
        }

        private DataGridRow GetRowItem(int index)
        {
            if (CurrentPeopleList.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
                return null;
            return CurrentPeopleList.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
        }
        #endregion

        private void BeginningDate_Loaded(object sender, RoutedEventArgs e)
        {
            BeginningDate.SelectedDate = Convert.ToDateTime(UsersSeeder.GetSelectedDate());
            UsersSeeder.RewriteSelectedDate(BeginningDate.SelectedDate);
        }

        private void BeginningDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(UsersSeeder != null)
            {
                UsersSeeder.RewriteSelectedDate(BeginningDate.SelectedDate);
                ShuffleDeliveryDate();
            }
        }

        private void NewUserButton_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(firstName.Text.Trim()) && !string.IsNullOrWhiteSpace(lastName.Text.Trim()))
            {
                User newUser = new User {
                    Id = GetLastUserId() + 1,
                    FirstName = firstName.Text.Trim(),
                    LastName = lastName.Text.Trim(),
                    LastDelivery = null,
                    Delivery = GetUserDeliveryDate(),
                };

                User formattedUser = UsersSeeder.FormatUser(newUser);
                UsersSeeder.WriteUser(formattedUser);
                Users.Add(formattedUser);

                RebindLists();
                ResetAddForm();

                GlobalMessageTextBlock.SuccessOperationMessage(GlobalMessage.AddUserOk);
            }
            else
            {
                GlobalMessageTextBlock.FailureOperationMessage(GlobalMessage.AddUserError);
            }
        }

        private async void UserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            GlobalMessageTextBlock.OperationMessage(GlobalMessage.SaveRunning);

            User editedUser = (User)(sender as TextBox).DataContext;

            ListBoxItem listBoxItem = (ListBoxItem)(people.ItemContainerGenerator.ContainerFromIndex(people.SelectedIndex));
            ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(listBoxItem);
            DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;

            TextBox userName = (TextBox)myDataTemplate.FindName("UserName", myContentPresenter);

            int startLength = userName.Text.Length;

            await Task.Delay(2000);

            if(startLength == userName.Text.Length)
            {
                bool isSaved = SaveNewName(editedUser, userName);

                if (isSaved)
                {
                    GlobalMessageTextBlock.SuccessOperationMessage(GlobalMessage.SaveOk);
                }
                else
                {
                    GlobalMessageTextBlock.FailureOperationMessage(GlobalMessage.SaveError);
                }
            }
        }

        private bool SaveNewName(User editedUser, TextBox userName)
        {
            if(userName.Text.Split(' ').Count() >= 2)
            {
                string firstName = userName.Text.Substring(0, userName.Text.IndexOf(' ')).FirstCharToUpper();
                string lastName = userName.Text.Substring(userName.Text.IndexOf(' '), (userName.Text.Length) - firstName.Length).ToUpper();

                editedUser.FirstName = firstName.Trim();
                editedUser.LastName = lastName.Trim();

                UsersSeeder.RewriteUsers(editedUser);

                Users.FirstOrDefault(user => user.Id == editedUser.Id).FirstName = editedUser.FirstName;
                Users.FirstOrDefault(user => user.Id == editedUser.Id).LastName = editedUser.LastName;

                RebindLists();

                return true;
            }

            return false;

        }

        private void RemoveUserButton_Click(object sender, RoutedEventArgs e)
        {
            User currentUser = (User)people.SelectedItem;
            UsersSeeder.EraseUser(currentUser.Id);
            Users.Remove(currentUser);
            ShuffleDeliveryDate();
            RebindLists();

            GlobalMessageTextBlock.SuccessOperationMessage(GlobalMessage.RemoveUserOk);
        }

        private void ShuffleDeliveryDate()
        {
            if (Users != null && Users.Any())
            {
                ResetUsersDates();

                Users.ToList().ForEach(user => user.Delivery = GetUserDeliveryDate());

                UsersSeeder.RewriteUsers(Users.ToList());
                RebindLists();
                DailyUpdateUsersDelivery();
            }
        }

        private void DailyUpdateUsersDelivery()
        {
            foreach (User user in Users.ToList())
            {
                if (user.Delivery < DateTime.Now)
                {
                    user.LastDelivery = user.Delivery;
                    user.IsDeliveryPast = true;
                }
            }
        }

        /// <summary>
        /// Get last user's id. Return the default beginning index (<see cref="Constants"/>) if there is no user.
        /// </summary>
        /// <returns>The last user's id as integer</returns>
        private int GetLastUserId()
        {
            DateTime? newUserDeliveryDate = GetUserDeliveryDate();
            User lastUser = Users.OrderBy(user => user.Id).LastOrDefault();

            return lastUser != null ? lastUser.Id : Constants.USER_BEGINNING_ID;
        }

        /// <summary>
        /// Generate a new delivery date for a user
        /// </summary>
        /// <param name="isNewPlanning">Define if the method is triggered manually (From changing date in the datepicker) or not (When adding a new user)</param>
        /// <returns>A new delivery date as DateTime?</returns>
        private DateTime? GetUserDeliveryDate()
        {
            DayOfWeek chosenDay = Constants.THE_DAY;

            User lastDeliverer = Users?.OrderBy(user => user.Delivery).LastOrDefault();
            DateTime? startDate = BeginningDate.SelectedDate;

            if (lastDeliverer != null && lastDeliverer.Delivery != null)
            {
                startDate = lastDeliverer.Delivery.Value.AddDays(1);
            }
            else if (DateTime.Now.DayOfWeek.Equals(chosenDay))
            {
                startDate = startDate.Value.AddDays(1);
            }

            return Helper.GetNextWeekday(startDate, chosenDay);
        }

        private void ResetUsersDates()
        {
            foreach (User user in Users)
            {
                user.LastDelivery = null;
                user.Delivery = null;
                user.IsDeliveryPast = false;
            }
        }

        private void ResetAddForm()
        {
            firstName.Text = string.Empty;
            lastName.Text = string.Empty;
            firstName.Focus();
        }

        private void RebindLists()
        {
            people.ItemsSource = null;
            people.ItemsSource = Users.OrderBy(user => user.FirstName);
            CurrentPeopleList.ItemsSource = null;
            CurrentPeopleList.ItemsSource = Users.OrderBy(user => user.Delivery);
        }

        /// <summary>
        /// Reset globalMessage color & visibility
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void GlobalMessageTextBlock_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox globalMessageTextBlock = (TextBox)sender;

            await Task.Delay(5000);
            globalMessageTextBlock.Foreground = Brushes.Black;
            globalMessageTextBlock.Text = string.Empty;
        }

        /// <summary>
        /// Source : https://stackoverflow.com/a/10891933/9868549
        /// </summary>
        /// <typeparam name="childItem"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        private childItem FindVisualChild<childItem>(DependencyObject obj)
            where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                    return (childItem)child;
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        
    }
}

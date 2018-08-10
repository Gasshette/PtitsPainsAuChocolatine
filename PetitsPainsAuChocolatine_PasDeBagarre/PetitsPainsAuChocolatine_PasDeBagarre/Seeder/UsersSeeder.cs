using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PetitsPainsAuChocolatine_PasDeBagarre.Entity;

namespace PetitsPainsAuChocolatine_PasDeBagarre.Seeder
{
    public class UsersSeeder : ConfigurationHandler
    {
       
        /// <summary>
        /// Get users from the usersFile. If there is no file, it is created, as well as its previous directories.
        /// </summary>
        /// <returns>The list of all users, or an empty List of <see cref="User">User</see></returns>
        public IEnumerable<User> GetUsers()
        {
            if (!File.Exists(UsersFilePath))
            {

                CreateUsersFilePath();
                return new List<User>();
            }
            else
            {
                IEnumerable<User> users = ReadCsvUsersFile();
                return users;
            }
        }

        /// <summary>
        /// Create every directories given in the UsersFilPath and the specified file
        /// </summary>
        private void CreateUsersFilePath()
        {
            string pathToCreate = UsersFilePath.Substring(0, UsersFilePath.LastIndexOf('/'));

            Directory.CreateDirectory(pathToCreate);
            File.Create(UsersFilePath).Close();
        }

        /// <summary>
        /// Read the .csv users file to create the list of user treated in the application
        /// </summary>
        /// <returns>enumerable object of <see cref="User">User</see></returns>
        private IEnumerable<User> ReadCsvUsersFile()
        {
            string[] lines = GetUserLines();
            List<User> users = new List<User>();

            foreach (string line in lines)
            {
                string[] splittedLine = line.Split(';');
                users.Add( new User
                    {
                        Id = Convert.ToInt32(splittedLine[0]),
                        FirstName = splittedLine[1],
                        LastName = splittedLine[2],
                        LastDelivery = !string.IsNullOrWhiteSpace(splittedLine[3]) ? splittedLine[3].ParseDate() : null,
                        Delivery = !string.IsNullOrWhiteSpace(splittedLine[4]) ? splittedLine[4].ParseDate() : null,
                    }
                );
            }
            return users;
            
        }

        /// <summary>
        /// Format an <see cref="User">User</see> object to make its properties values to look better
        /// </summary>
        /// <param name="user">The User object to format</param>
        /// <returns>The formatted User object</returns>
        public User FormatUser(User user)
        {
            return new User
            {
                Id = user.Id,
                FirstName = user.FirstName.FirstCharToUpper(),
                LastName = user.LastName.ToUpper(),
                LastDelivery = user.LastDelivery != null ? user.LastDelivery.FormatDate() : null,
                Delivery = user.Delivery != null ? user.Delivery.FormatDate() : null,
            };
        }

        /// <summary>
        /// Write a user at the end of the users file
        /// </summary>
        /// <param name="newUser">The user to add, typeof <see cref="User">User</see></param>
        public void WriteUser(User newUser)
        {
            List<string> lines = GetUserLines().ToList();
            lines.Add(GetLineTemplate(newUser));
            WriteFile(lines.ToArray());
        }

        /// <summary>
        /// Rewrite Users file with the given updated value
        /// </summary>
        /// <param name="userToUpdate">The user to update</param>
        public void RewriteUsers(User updatedUser)
        {
            string[] lines = GetUserLines();

            for (int i = 0; i < lines.Count(); i++)
            {
                string[] splittedLines = lines[i].Split(';');
                if (splittedLines[0].Equals(updatedUser.Id.ToString()))
                {
                    lines[i] = GetLineTemplate(updatedUser);
                    break;
                }
            }

            WriteFile(lines);
        }

        /// <summary>
        /// Rewrite Users file with the given updated value
        /// </summary>
        /// <param name="updatedUsers">The list of all updated users</param>
        public void RewriteUsers(List<User> updatedUsers)
        {
            string[] lines = GetUserLines();

            for (int i = 0; i < updatedUsers.Count(); i++)
            {
                if (!GetLineTemplate(updatedUsers[i]).Equals(lines[i]))
                {
                    string[] splittedLines = lines[i].Split(';');
                    User correspondingUser = updatedUsers.FirstOrDefault(user => user.Id == Convert.ToInt32(splittedLines[0]));
                    if (correspondingUser != null)
                    {
                        lines[i] = GetLineTemplate(correspondingUser);
                    }
                }
            }

            WriteFile(lines);
        }

        /// <summary>
        /// Erase a user'sline from users file
        /// </summary>
        /// <param name="id"></param>
        public void EraseUser(int id)
        {
            List<string> lines = GetUserLines().ToList();

            for (int i = 0; i < lines.Count(); i++)
            {
                string[] splittedLine = lines[i].Split(';');

                if (splittedLine[0].Equals(id.ToString()))
                {
                    lines.RemoveAt(i);
                }
            }

            WriteFile(lines.ToArray());
        }

        private string GetLineTemplate(User user)
        {
            return $"{user.Id};{user.FirstName};{user.LastName};{user.LastDelivery:dd MMMM yyyy};{user.Delivery:dd MMMM yyyy}";
        }

        /// <summary>
        /// Return users lines. Ignore last line of the file which ccorrespond to the selected date.
        /// </summary>
        /// <returns>A list of all user lines as string[]</returns>
        private string[] GetUserLines()
        {
            List<string> lines = File.ReadAllLines(UsersFilePath).ToList();
            lines.RemoveAt(lines.Count() - 1);

            return lines.ToArray();
        }

        /// <summary>
        /// Write the file with the given new users list
        /// </summary>
        /// <param name="users">The users list</param>
        private void WriteFile(string[] users)
        {
            string date = GetSelectedDate();
            List<string> lines = users.ToList();
            lines.Add(date);

            File.WriteAllLines(UsersFilePath, lines.ToArray());
        }

        /// <summary>
        /// return the recorded selected date (Last line of the file) as string. Test if the last line is convertible.
        /// </summary>
        /// <returns>The last line if convertible, formatted datetime.Now if not</returns>
        public string GetSelectedDate()
        {
            DateTime date;
            bool isConverted = false;
            string lastLine = File.ReadLines(UsersFilePath).LastOrDefault();

            isConverted = DateTime.TryParse(lastLine, out date);

            return isConverted ? lastLine : $"{DateTime.Now:dd MMMM yyyy}";
        }

        /// <summary>
        /// Rewrite the selected date. Erase the existing one (last line of the file)
        /// </summary>
        /// <param name="date">The given selected date</param>
        public void RewriteSelectedDate(DateTime? date)
        {
            string formattedDate = $"{date:dd MMMM yyyy}";
            List<string> lines = File.ReadAllLines(UsersFilePath).ToList();
            int count = lines.Count();

            if (lines.Count() > 0)
            {
                lines[lines.Count() - 1] = formattedDate;
            }
            else
            {
                lines.Add(formattedDate);
            }

            File.WriteAllLines(UsersFilePath, lines);
        }
    }
}

using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Windows;
using System.Windows.Controls;
using BootVerhuurWpf.Model;

namespace BootVerhuurWpf;

public class UserController : Database
{
    private static bool digits;
    private static bool special;

    private static bool IsEmailValid(string email)
    {
        var valid = true;

        try
        {
            var emailAddress = new MailAddress(email);
        }
        catch
        {
            valid = false;
        }

        return valid;
    }

    // Check to see if password contains a digit
    private static bool ContainsDigit(string password)
    {
        foreach (var l in password)
        {
            if (char.IsDigit(l)) digits = true;

            if (digits) break;
        }

        return digits;
    }

    // Check to see if a password contains a special character
    private static bool ContainsSpecial(string password)
    {
        var regexItem = @"\|!#$%&/+-()=?»«@£§€{}.-;'<>_,";

        foreach (var item in regexItem)
        {
            if (password.Contains(item)) special = true;
            if (special) break;
        }

        return special;
    }

    // Check to see if the fields or not empty
    public static bool EmptyFieldMessageAdmin(string username, string email)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email)) return true;
        return false;
    }

    // Check to see if any fields have been left empty
    public static bool EmptyFieldMessage(string firstname, string lastname, string username, string email,
        string phonenumber, string role, string boatingLevel)
    {
        if (string.IsNullOrWhiteSpace(firstname) || string.IsNullOrWhiteSpace(lastname) ||
            string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(phonenumber) || string.IsNullOrWhiteSpace(role) ||
            string.IsNullOrWhiteSpace(boatingLevel))
            return true;
        return false;
    }

    // Check to see if the email address is valid
    private static bool ValidEmailMessage(string email)
    {
        if (!IsEmailValid(email) || (!email.EndsWith(".nl") && !email.EndsWith(".com"))) return true;

        return false;
    }

    // Check to see if the password is valid
    private static bool ValidPasswordMessage(string password)
    {
        if (!digits || !special || password.Length <= 7) return true;
        return false;
    }

    private static void ValidCreationMessage(string firstname, string lastname, string username, string password,
        string email, string phonenumber, string role, string boatingLevel)
    {
        new Members(firstname, lastname, username, password, email, phonenumber, role, boatingLevel);
    }

    // Creates a new member in the 'member' table while being subjected to several validity checks.
    public static void CreateMember(string firstname, string lastname, string username, string password,
        string email, string phonenumber, string role, string boatingLevel)
    {
        ContainsDigit(password);
        ContainsSpecial(password);
        var emptyField = EmptyFieldMessage(firstname, lastname, username, email, phonenumber, role, boatingLevel);
        var invalidEmail = ValidEmailMessage(email);
        var inValidPassword = ValidPasswordMessage(password);
        ValidCreationMessage(firstname, lastname, username, password, email, phonenumber, role, boatingLevel);
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        if (emptyField)
        {
            MessageBox.Show("Alle velden moeten ingevuld zijn", "FAILED: Één of meerdere velden zijn leeg",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
        else if (invalidEmail)
        {
            MessageBox.Show("Email is ongeldig", "FAILED: Email Ongeldig", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        else if (inValidPassword)
        {
            MessageBox.Show("Wachtwoord moet 8 tekens lang zijn, een cijfer en een speciale teken bevatten",
                "FAILED: Wachtwoord voldoet niet aan de eisen", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        else
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var sql = "INSERT INTO member " +
                          "(first_name, last_name, phone_number, email, boating_level, role, username, password)" +
                          "VALUES ('" + firstname + "' , '" +
                          lastname + "', '" + phonenumber + "', '" + email + "', '" + boatingLevel + "', '" + role +
                          "', '" + username + "', '" + hashedPassword + "')";
                using (var command = new SqlCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8}", reader.GetInt32(0),
                                reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4),
                                reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8));
                    }

                    connection.Close();
                }
            }

            MessageBox.Show("User is aangemaakt", "SUCCES", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    // Create a new admin instance in the 'member' table
    public static void CreateAdmin(string username, string password, string email)
    {
        ContainsDigit(password);
        ContainsSpecial(password);
        var emptyFieldAdmin = EmptyFieldMessageAdmin(username, email);
        var invalidEmail = ValidEmailMessage(email);
        var inValidPassword = ValidPasswordMessage(password);
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        if (emptyFieldAdmin)
        {
            MessageBox.Show("Alle velden moeten ingevuld zijn", "FAILED: Één of meerdere velden zijn leeg",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
        else if (invalidEmail)
        {
            MessageBox.Show("Email is ongeldig", "FAILED: Email Ongeldig", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        else if (inValidPassword)
        {
            MessageBox.Show("Wachtwoord moet 8 tekens lang zijn, een cijfer en een speciale teken bevatten",
                "FAILED: Wachtwoord voldoet niet aan de eisen", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        else
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var sql = "INSERT INTO member " +
                          "(first_name, last_name, phone_number, email, boating_level, role, username, password)" +
                          "VALUES ('" + null + "' , '" +
                          null + "', '" + null + "', '" + email + "', '" + null + "', '" + "Admin" +
                          "', '" +
                          username + "', '" + hashedPassword + "')";

                using (var command = new SqlCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8}", reader.GetInt32(0),
                                reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4),
                                reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8));
                    }

                    connection.Close();
                }
            }

            MessageBox.Show("Admin is aangemaakt", "SUCCES", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    private string HashPassword(string newPassword)
    {
        var newSalt = BCrypt.Net.BCrypt.GenerateSalt(12);
        var newHashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword, newSalt);
        return newHashedPassword;
    }

    public void EditUser(string firstname, string lastname, string email, string phonenumber, string level,
        string username, string password, string id)
    {
        try
        {
            using (var connection = GetConnection())
            {
                var updateQuery = "UPDATE member SET first_name='" + firstname + "',last_name='" +
                                  lastname + "',email='" + email +
                                  "',phone_number='" + phonenumber + "',boating_level='" +
                                  level + "',username='" + username +
                                  "',password='" + HashPassword(password) + "' WHERE id = '" + id + "'";

                using (var command = new SqlCommand(updateQuery, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8}", reader.GetInt32(0),
                                reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4),
                                reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8));
                    }
                }
            }
        }
        catch
        {
        }

        MessageBox.Show("De gebruiker is aangepast!");
    }

    public void DeleteUser(string id)
    {
        try
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var query = "DELETE FROM member WHERE id = '" + id + "'";
                var sqlCmd = new SqlCommand(query, connection);
                sqlCmd.ExecuteNonQuery();

                connection.Close();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("howdydoody");
        }
    }

    public void UpdateTable(DataGrid datagrid1)
    {
        try
        {
            using (var connection = GetConnection())
            {
                var dt = new DataTable("member");
                connection.Open();
                var query =
                    "SELECT id,first_name,last_name,phone_number,email,boating_level,role,username,password FROM member";
                var sqlCmd = new SqlCommand(query, connection);

                sqlCmd.ExecuteNonQuery();

                var adapter = new SqlDataAdapter(sqlCmd);


                adapter.Fill(dt);

                datagrid1.ItemsSource = dt.DefaultView;

                adapter.Update(dt);

                connection.Close();
            }
        }
        catch (Exception ex)
        {
            // Expres leeg gelaten omdat hij anders een "fout"melding geeft als je de tabel opnieuwd laadt voor de eerste keer
            /*MessageBox.Show("ex.Message");*/
        }
    }
}
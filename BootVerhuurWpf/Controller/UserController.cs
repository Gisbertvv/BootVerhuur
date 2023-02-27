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
    private static bool ContainsDigit(string wachtwoord)
    {
        foreach (var l in wachtwoord)
        {
            if (char.IsDigit(l)) digits = true;

            if (digits) break;
        }

        return digits;
    }

    // Check to see if a password contains a special character
    private static bool ContainsSpecial(string wachtwoord)
    {
        var regexItem = @"\|!#$%&/+-()=?»«@£§€{}.-;'<>_,";

        foreach (var item in regexItem)
        {
            if (wachtwoord.Contains(item)) special = true;
            if (special) break;
        }

        return special;
    }

    // Check to see if the fields or not empty
    public static bool EmptyFieldMessageAdmin(string Gebruikersnaam, string Email)
    {
        if (string.IsNullOrWhiteSpace(Gebruikersnaam) || string.IsNullOrWhiteSpace(Email)) return true;
        return false;
    }

    // Check to see if any fields have been left empty
    public static bool EmptyFieldMessage(string Voornaam, string Achternaam, string Gebruikersnaam, string Email,
        string Telefoonnummer, string Rol, string Niveau)
    {
        if (string.IsNullOrWhiteSpace(Voornaam) || string.IsNullOrWhiteSpace(Achternaam) ||
            string.IsNullOrWhiteSpace(Gebruikersnaam) || string.IsNullOrWhiteSpace(Email) ||
            string.IsNullOrWhiteSpace(Telefoonnummer) || string.IsNullOrWhiteSpace(Rol) ||
            string.IsNullOrWhiteSpace(Niveau))
            return true;
        return false;
    }

    // Check to see if the email address is valid
    private static bool ValidEmailMessage(string Email)
    {
        if (!IsEmailValid(Email) || (!Email.EndsWith(".nl") && !Email.EndsWith(".com"))) return true;

        return false;
    }

    // Check to see if the password is valid
    private static bool ValidPasswordMessage(string Password)
    {
        if (!digits || !special || Password.Length <= 7) return true;
        return false;
    }

    private static void ValidCreationMessage(string Voornaam, string Achternaam, string Gebruikersnaam, string Password,
        string Email, string Telefoonnummer, string Rol, string Niveau)
    {
        new Members(Voornaam, Achternaam, Gebruikersnaam, Password, Email, Telefoonnummer, Rol, Niveau);
    }

    // Creates a new member in the 'member' table while being subjected to several validity checks.
    public static void CreateMember(string Voornaam, string Achternaam, string Gebruikersnaam, string Password,
        string Email, string Telefoonnummer, string Rol, string Niveau)
    {
        ContainsDigit(Password);
        ContainsSpecial(Password);
        var emptyField = EmptyFieldMessage(Voornaam, Achternaam, Gebruikersnaam, Email, Telefoonnummer, Rol, Niveau);
        var invalidEmail = ValidEmailMessage(Email);
        var inValidPassword = ValidPasswordMessage(Password);
        ValidCreationMessage(Voornaam, Achternaam, Gebruikersnaam, Password, Email, Telefoonnummer, Rol, Niveau);
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(Password);

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
                          "VALUES ('" + Voornaam + "' , '" +
                          Achternaam + "', '" + Telefoonnummer + "', '" + Email + "', '" + Niveau + "', '" + Rol +
                          "', '" + Gebruikersnaam + "', '" + hashedPassword + "')";
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
    public static void CreateAdmin(string Gebruikersnaam, string Password, string Email)
    {
        ContainsDigit(Password);
        ContainsSpecial(Password);
        var emptyFieldAdmin = EmptyFieldMessageAdmin(Gebruikersnaam, Email);
        var invalidEmail = ValidEmailMessage(Email);
        var inValidPassword = ValidPasswordMessage(Password);
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(Password);

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
                          null + "', '" + null + "', '" + Email + "', '" + null + "', '" + "Admin" +
                          "', '" +
                          Gebruikersnaam + "', '" + hashedPassword + "')";

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

    public void EditUser(string firstname, string lastname, string email, string phoneNumber, string level,
        string username, string password, string id)
    {
        try
        {
            using (var connection = GetConnection())
            {
                var updateQuery = "UPDATE member SET first_name='" + firstname + "',last_name='" +
                                  lastname + "',email='" + email +
                                  "',phone_number='" + phoneNumber + "',boating_level='" +
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
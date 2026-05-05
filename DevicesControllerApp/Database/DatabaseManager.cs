using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Npgsql;
using System.Windows.Forms;

namespace DevicesControllerApp.Database
{

    internal class DatabaseManager
    {
        static DatabaseManager instance = null;
        private static readonly object _lock = new object();

        internal static DatabaseManager Instance
        {
            get
            {
                if ((instance == null))
                {
                    instance = new DatabaseManager();
                }
                return instance;
            }
        }

        public bool OpenConnection()
        {
            try
            {
                if (conn == null)
                    conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=lokomat;User Id=postgres;Password=1234;");

                if (conn.State != ConnectionState.Open)
                    conn.Open();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veritabanı bağlantı hatası: " + ex.Message);
                return false;
            }
        }

        public void CloseConnection()
        {
            if (conn != null && conn.State == ConnectionState.Open)
                conn.Close();
        }

        public bool ValidateUserLogin(string kullaniciAdi, string sifre)
        {
            try
            {
                if (conn == null || conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                // SQL sorgusu: kullanici_adi eşleşmeli
                string query = "SELECT sifre_hash FROM kullanicihesaplari WHERE kullanici_adi=@kullanici_adi";

                using (var cmd = new Npgsql.NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@kullanici_adi", kullaniciAdi);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedHash = reader.GetString(0); // sifre_hash değerini al

                            // Şifreyi doğrula
                            return VerifyPassword(sifre, storedHash);
                        }
                        else
                        {
                            // Kullanıcı bulunamadı
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
                return false;
            }
        }



        private bool VerifyPassword(string sifre, string storedHash)
        {
            using (var sha = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(sifre);
                byte[] hashBytes = sha.ComputeHash(bytes);
                string hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

                return hash == storedHash.ToLower();
            }
        }



        private string connectionString;
        private DatabaseManager()
        {
            connectionString = "Server=localhost;Port=5432;Database=lokomat;User Id=postgres;Password=1234;";
        }

        Npgsql.NpgsqlConnection conn;
      

        public DataTable GetAllCitys()
        {
            if (conn.State == ConnectionState.Open)
            {
                string query = "SELECT * FROM sehirler";
                Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand(query, conn);
                Npgsql.NpgsqlDataAdapter da = new Npgsql.NpgsqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            return null;
        }

        public bool HastaSil(long tc)
        {
            string query = "DELETE FROM hastalar WHERE tc_kimlik_no=11122233344";
            Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand(query, conn);
            try
            {
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {

                return false;
            }

        }
      
        public bool TestConnection()
        {
            return false;
        }

        public bool AddPatient(string tcNo, string firstName, string lastName, DateTime birthDate,
            string gender, string address, string phone, string email, string diagnosis,
            int height, int weight, int shoeSize, int hipKneeDistance, int kneeHeelDistance)
        {
            return false;
        }

        public bool UpdatePatient(int patientId, string tcNo, string firstName, string lastName,
            DateTime birthDate, string gender, string address, string phone, string email,
            string diagnosis, int height, int weight, int shoeSize, int hipKneeDistance,
            int kneeHeelDistance)
        {
            return false;
        }

        public bool DeletePatient(int patientId)
        {
            return false;
        }

        public DataTable SearchPatient(string searchTerm)
        {
            return null;
        }

        public DataTable GetAllPatients()
        {
            return null;
        }

        public DataRow GetPatientDetails(int patientId)
        {
            return null;
        }

        public DataRow GetPatientByTcNo(string tcNo)
        {
            return null;
        }

  

        public DataRow GetUserByUsername(string username)
        {
            return null;
        }

        public bool AddUser(string username, string password, string tcNo, string firstName,
            string lastName, string role, string email, string phone)
        {
            return false;
        }

        public bool UpdateUser(int userId, string username, string tcNo, string firstName,
            string lastName, string role, string email, string phone)
        {
            return false;
        }

        public bool DeleteUser(int userId)
        {
            return false;
        }

        public bool ChangePassword(int userId, string oldPassword, string newPassword)
        {
            return false;
        }

        public string GeneratePasswordResetToken(string email)
        {
            return null;
        }

        public bool ResetPasswordWithToken(string token, string newPassword)
        {
            return false;
        }

        public DataTable GetAllUsers()
        {
            return null;
        }

        public bool UpdateLastLogin(int userId)
        {
            return false;
        }

        public int StartTherapy(int patientId, int operatorId, double speed, double weightReduction,
            double supportBarHeight, int shoeSize)
        {
            return 0;
        }

        public bool EndTherapy(int therapyId, string notes)
        {
            return false;
        }

        public bool UpdateTherapy(int therapyId, double speed, double weightReduction,
            double supportBarHeight)
        {
            return false;
        }

        public DataTable SearchTherapy(DateTime? startDate, DateTime? endDate, int? patientId,
            int? operatorId)
        {
            return null;
        }

        public DataRow GetTherapyDetails(int therapyId)
        {
            return null;
        }

        public DataTable GetPatientTherapyHistory(int patientId)
        {
            return null;
        }

        public bool UpdateTherapyStatus(int therapyId, string status)
        {
            return false;
        }

        public bool SaveLoadCellData(int therapyId, DateTime timestamp, double rightHeel,
            double leftHeel, double rightToe, double leftToe, double weightBalance, int index)
        {
            return false;
        }

        public bool SaveLoadCellDataBulk(int therapyId, List<LoadCellData> dataList)
        {
            return false;
        }

        public DataTable GetLoadCellData(int therapyId)
        {
            return null;
        }

        public DataTable GetLoadCellDataByTimeRange(int therapyId, DateTime startTime,
            DateTime endTime)
        {
            return null;
        }

        public bool AddSystemLog(int? userId, string operationType, string operationDetail,
            string ipAddress, string errorLevel)
        {
            return false;
        }

        public bool AddDeviceStatusLog(string servoMotorStatus, string stepMotorStatus,
            string limitSwitchStatus, string errorCodes)
        {
            return false;
        }

        public DataTable GetSystemLogs(DateTime? startDate, DateTime? endDate, int? userId,
            string errorLevel)
        {
            return null;
        }

        public DataTable GetDeviceStatusLogs(DateTime? startDate, DateTime? endDate)
        {
            return null;
        }

        public DataTable GetRecentErrors(int count)
        {
            return null;
        }

        public string GetSetting(string settingKey)
        {
            return null;
        }

        public bool SaveSetting(string settingKey, string settingValue, string description,
            int updatedByUserId)
        {
            return false;
        }

        public DataTable GetAllSettings()
        {
            return null;
        }

        public bool DeleteSetting(string settingKey)
        {
            return false;
        }

        public int GetTherapyCountByDateRange(DateTime startDate, DateTime endDate)
        {
            return 0;
        }

        public DataTable GetTherapyStatisticsByDateRange(DateTime startDate, DateTime endDate)
        {
            return null;
        }

        public DataTable GetPatientTherapyReport(int patientId)
        {
            return null;
        }

        public DataTable GetOperatorPerformanceReport(DateTime startDate, DateTime endDate)
        {
            return null;
        }

        public DataTable GetOperatorTherapyCount(int operatorId, DateTime startDate,
            DateTime endDate)
        {
            return null;
        }

        public DataTable GetAverageTherapyDuration(DateTime startDate, DateTime endDate)
        {
            return null;
        }

        public DataTable GetPatientProgressReport(int patientId)
        {
            return null;
        }

        public DataTable GetMostActivePatients(int topCount, DateTime startDate, DateTime endDate)
        {
            return null;
        }

        public DataTable GetDeviceUsageStatistics(DateTime startDate, DateTime endDate)
        {
            return null;
        }

        private string HashPassword(string password)
        {
            using (var sha = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha.ComputeHash(bytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        public bool ChangePasswordByUsername(string kullaniciAdi, string newPassword)
        {
            try
            {
                if (conn == null || conn.State != ConnectionState.Open)
                    OpenConnection();

                // 1️⃣ Eski sifre hash al
                string selectQuery = @"
            SELECT sifre_hash 
            FROM kullanicihesaplari 
            WHERE kullanici_adi = @kullanici_adi";

                using (var selectCmd = new NpgsqlCommand(selectQuery, conn))
                {
                    selectCmd.Parameters.AddWithValue("@kullanici_adi", kullaniciAdi);

                    object result = selectCmd.ExecuteScalar();
                    if (result == null)
                        return false;

                    string storedHash = result.ToString();

                    // 3️⃣ Yeni sifreyi guncelle
                    string updateQuery = @"
                UPDATE kullanicihesaplari 
                SET sifre_hash = @newHash 
                WHERE kullanici_adi = @kullanici_adi";

                    using (var updateCmd = new NpgsqlCommand(updateQuery, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@newHash", HashPassword(newPassword));
                        updateCmd.Parameters.AddWithValue("@kullanici_adi", kullaniciAdi);

                        return updateCmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sifre degistirme hatasi: " + ex.Message);
                return false;
            }
        }

        public string GetUserRole(string kullaniciAdi)
        {
            try
            {
                if (conn == null || conn.State != ConnectionState.Open)
                    OpenConnection();

                string query = @"
            SELECT r.rol_adi
            FROM kullanicihesaplari k
            JOIN roller r ON k.rol_id = r.rol_id
            WHERE k.kullanici_adi = @kullanici_adi
        ";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@kullanici_adi", kullaniciAdi);
                    object result = cmd.ExecuteScalar();
                    return result?.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Rol alma hatası: " + ex.Message);
                return null;
            }
        }



        private void LogError(Exception ex, string methodName)
        {
            // Hata loglama
        }

    }
    // LoadCell verisi için model class
    public class LoadCellData
    {
        public DateTime Timestamp { get; set; }
        public double RightHeel { get; set; }
        public double LeftHeel { get; set; }
        public double RightToe { get; set; }
        public double LeftToe { get; set; }
        public double WeightBalance { get; set; }
        public int Index { get; set; }
    }
}

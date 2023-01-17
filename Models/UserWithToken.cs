namespace S3_Api_indi.Models
{
    public class UserWithToken : User
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public UserWithToken(User user)
        {
            this.Id = user.Id;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Username = user.Username;
            this.Password = user.Password;
            this.Refreshtokens = user.Refreshtokens;
            this.Status = user.Status;

        }
    }
}

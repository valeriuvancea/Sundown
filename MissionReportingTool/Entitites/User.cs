using MissionReportingTool.Contracts;

namespace MissionReportingTool.Entities
{
    public class User: BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CodeName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }

        public override void Update<T>(T contract) 
        {
            base.Update(contract);
            var user = contract as UserContract;
            if (user != null)
            {
                FirstName = user.FirstName;
                LastName = user.LastName;
                CodeName = user.CodeName;
                Username = user.Username;
                Email = user.Email;
                Avatar = user.Avatar;
            }
        }
    }
}

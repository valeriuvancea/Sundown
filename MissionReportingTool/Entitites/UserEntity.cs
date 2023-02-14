using MissionReportingTool.Contracts;
using MissionReportingTool.Entitites;

namespace MissionReportingTool.Entities
{
    public class UserEntity: BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CodeName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public Role Role { get; set; }

        public override void Update(BaseContract contract) 
        {
            var user = contract as User;
            if (user != null)
            {
                FirstName = user.FirstName;
                LastName = user.LastName;
                CodeName = user.CodeName;
                Username = user.Username;
                Email = user.Email;
                Avatar = user.Avatar;
                Role = user.Role;
            }
        }
    }
}

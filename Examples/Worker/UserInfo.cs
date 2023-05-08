namespace Examples.Worker
{
    public class UserInfo
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public UserInfo(string name, string id)
        {
            Name = name;
            Id = id;
        }
    }
}
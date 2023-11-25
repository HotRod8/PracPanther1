namespace PP.API.Messengers
{
    //This is necessary to implement if I need multiple parameters at the same time
    //For example, implementing the project class.
    public class AddOrUpdateRequestClass
    {
        public int ClientID { get; set; }
        public int ProjectID { get; set; }
    }
}

namespace EncantoApadrinhamento.Core.Util
{
    public static class Util
    {
        public static string GenerateNickName(string name, string lastName)
        {
            string unickRandonNumber = new Random().Next(1000, 9999).ToString();
            string nickName = name + "_" + lastName + "@" + unickRandonNumber;

            return nickName;
        }


    }
}

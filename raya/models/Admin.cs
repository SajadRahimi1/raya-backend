public class Admin : BaseEntity
{
    public string? name { get; set; }
    public string? phoneNumber { get; set; }
    public string? username { get; set; }
    public string? password { get; set; }
    public Guid? token { get; set; }

    public string? smsCode { get; set; }
    public bool isEnable { get; set; }

}

/*
docker exec -it b1af875def10 /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "Sajadsajad1!"
*/
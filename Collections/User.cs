using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("name")]
    public string? Name { get; set; }
    
    [BsonElement("phoneNumber")]
    public string? PhoneNumber { get; set; }

    [BsonElement("fatherName")]
    public string? FatherName { get; set; }

    [BsonElement("birthday")]
    public string? birthday { get; set; }

    [BsonElement("bornCity")]
    public string? BornCity { get; set; }

    [BsonElement("nationalCode")]
    public string? NationalCode { get; set; }

    [BsonElement("nationalNumber")]
    public string? NationalNumber { get; set; }

    [BsonElement("education")]
    public string? Education { get; set; }

    [BsonElement("address")]
    public string? Address { get; set; }


    [BsonElement("emergancyNumber")]
    public string? EmergancyNumber { get; set; }


}
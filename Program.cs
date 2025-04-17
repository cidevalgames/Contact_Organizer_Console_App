using System.Text.Json;

class Contact
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Telephone { get; set; }
    public string Email { get; set; }

    public Contact(string firstName, string lastName, string telephone, string email)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Telephone = telephone;
        this.Email = email;
    }
}

class Program
{
    static List<Contact> contacts = new List<Contact>();

    static void Main()
    {
        LoadContacts();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("--- Bienvenue dans le gestionnaire de contacts. ---\n\n" +
            "1. Ajouter un contact \n" +
            "2. Voir mes contacts \n" +
            "3. Rechercher un contact \n" +
            "4. Supprimer un contact \n" +
            "5. Quitter \n");

            Console.Write("> Choix: ");
            int choice = 0;
            Int32.TryParse(Console.ReadLine(), out choice);

            switch (choice)
            {
                case 1:
                    AddContact();
                    break;
                case 2:
                    ListContacts();
                    break;
                case 3:
                    SearchContact();
                    break;
                case 4:
                    RemoveContact();
                    break;
                case 5:
                    SaveContacts();
                    return;
                default:
                    Console.WriteLine("Entrée incorrecte.");
                    break;
            }

            Console.WriteLine("\nAppuyez sur une touche pour continuer...");
            Console.ReadKey();
        }
    }

    static void AddContact()
    {
        Console.Write("\nPrénom: ");
        string contactFirstName = Console.ReadLine();
        Console.Write("Nom de famille: ");
        string contactLastName = Console.ReadLine();
        Console.Write("Numéro de téléphone: ");
        string contactTelephone = string.Format("{0:## ## ## ## ##}", Int32.Parse(Console.ReadLine()));
        Console.Write("Adresse mail: ");
        string contactEmail = Console.ReadLine();

        Contact newContact = new Contact(contactFirstName, contactLastName, contactTelephone, contactEmail);
        contacts.Add(newContact);

        Console.WriteLine("\nContact ajouté avec succès !");
    }

    static void ListContacts()
    {
        Console.WriteLine("\n--- Liste des contacts ---\n");
        foreach (Contact contact in contacts)
        {
            Console.WriteLine($"{contact.FirstName} {contact.LastName}");
        }
    }

    static void SearchContact()
    {
        Console.Write("\nNom du contact ou téléphone: ");
        string input = Console.ReadLine();

        foreach (Contact contact in contacts)
        {
            string name = $"{contact.FirstName} {contact.LastName}";

            if (name == input || contact.Telephone == input)
            {
                Console.WriteLine($"\nPrénom: {contact.FirstName}\n" +
                    $"Nom: {contact.LastName}\n" +
                    $"Téléphone: {contact.Telephone}\n" +
                    $"Adresse mail: {contact.Email}");
                return;
            }
        }
    }

    static void RemoveContact()
    {
        Console.Write("\nNom du contact ou téléphone: ");
        string input = Console.ReadLine();

        foreach (Contact contact in contacts)
        {
            string name = $"{contact.FirstName} {contact.LastName}";

            if (name == input || contact.Telephone == input)
            {
                contacts.Remove(contact);
                Console.WriteLine("Contact supprimé avec succès !");
                return;
            }
        }

        Console.WriteLine("Ce contact n'existe pas.");
    }

    static void LoadContacts()
    {
        string fileName = "Contacts.json";

        if (!File.Exists(fileName))
            File.WriteAllText(fileName, "");

        string jsonString = File.ReadAllText(fileName);

        if (jsonString == "")
            return;

        contacts = JsonSerializer.Deserialize<List<Contact>>(jsonString) ?? new List<Contact>();
    }

    static void SaveContacts()
    {
        string fileName = "Contacts.json";
        string jsonString = JsonSerializer.Serialize(contacts, new JsonSerializerOptions { WriteIndented = true });

        File.WriteAllText(fileName, jsonString);
    }
}

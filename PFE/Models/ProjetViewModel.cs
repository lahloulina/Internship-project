using PFE.Models;

public class ProjetViewModel
{
    public string ProjetName  { get; set; }
    public List<string> CollaboratorNames { get; set; }
    public List<string?> CollaboratorPrenom { get; internal set; }
}

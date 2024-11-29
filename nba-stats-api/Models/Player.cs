using System;
using System.Text.Json.Serialization;

public class Player
{
    public int PlayerId { get; set; }          
    public string? FirstName { get; set; }    
    public string? LastName { get; set; }      
    public string? DisplayName { get; set; }   
    public string? Position { get; set; }     
    public string? Height { get; set; }       
    public string? Weight { get; set; }        
    public string? BirthDate { get; set; }     
    public string? Country { get; set; }       
    public string? LastAffiliation { get; set; } 

    // Team Information
    public int? TeamId { get; set; }           
    public string? TeamName { get; set; }     
    public string? TeamCity { get; set; }     
    public string? TeamAbbreviation { get; set; } 

    // Career Details
    public string? DraftYear { get; set; }     
    public string? DraftRound { get; set; }   
    public string? DraftNumber { get; set; }  
    public int? SeasonExperience { get; set; } 

    // Stats for Current Season
    public double? PointsPerGame { get; set; } 
    public double? ReboundsPerGame { get; set; } 
    public double? AssistsPerGame { get; set; } 
    public double? PlayerImpactEstimate { get; set; } 

    // Additional Flags
    public string? JerseyNumber { get; set; } 
    public bool? IsActive { get; set; }        
    public bool? IsNBAPlayer { get; set; }     
    public bool? IsGreatest75 { get; set; }

    // Awards
    public int allDefensive {  get; set; }
    public int dpoy {  get; set; }
    public int allNBA {  get; set; }
    public int allStar {  get; set; }
    public int mvp {  get; set; }
    public int fmvp { get; set; }
    public int champion { get; set; }
    public int olympicBronze { get; set; }
    public int olympicSilver { get; set; }
    public int olympicGold { get; set; }


    public DateTime UpdatedAt { get; set; }
}

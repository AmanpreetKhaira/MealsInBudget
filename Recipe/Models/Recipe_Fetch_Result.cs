//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Recipe.Models
{
    using System;
    
    public partial class Recipe_Fetch_Result
    {
        public int RecipeID { get; set; }
        public string RecipeName { get; set; }
        public string Description { get; set; }
        public Nullable<int> PrepTime { get; set; }
        public Nullable<int> TotalTime { get; set; }
        public string Course { get; set; }
        public string Cuisine { get; set; }
        public Nullable<int> Servings { get; set; }
        public string Author { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public string ImagePath { get; set; }
        public Nullable<int> Views { get; set; }
        public string Instruction { get; set; }
        public Nullable<int> InstructionID { get; set; }
    }
}

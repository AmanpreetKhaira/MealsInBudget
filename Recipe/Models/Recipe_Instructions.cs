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
    using System.Collections.Generic;
    
    public partial class Recipe_Instructions
    {
        public int InstructionID { get; set; }
        public Nullable<int> RecipeID { get; set; }
        public string Instruction { get; set; }
    }
}

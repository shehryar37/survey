//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SurveyProject
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblCompWinner
    {
        public int CompWinID { get; set; }
        public int FirstWinner { get; set; }
        public Nullable<int> SecondWinner { get; set; }
        public Nullable<int> ThirdWinner { get; set; }
    
        public virtual tblCompParticipant tblCompParticipant { get; set; }
        public virtual tblCompParticipant tblCompParticipant1 { get; set; }
        public virtual tblCompParticipant tblCompParticipant2 { get; set; }
    }
}